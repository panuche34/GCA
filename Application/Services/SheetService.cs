using System.Globalization;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using Application.Interface;
using ClosedXML.Excel;
using Core.Entity;
using Core.ViewModel;
using Core.VO;
using DocumentFormat.OpenXml.Spreadsheet;
using Infrastructure.Contexties;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Util.Exceptions;
using Util.Extensions;


namespace Application.Services
{
    internal class SheetService(ILogger<SheetService> logger, AppDbContext ctx,
        IConfiguration configuration) : ISheetService
    {
        private readonly ILogger<SheetService> _logger = logger;
        private readonly AppDbContext _ctx = ctx;

        private readonly string _pathBaseOrig = configuration["PathBaseOrig"] ?? string.Empty;
        private readonly string _pathBaseResult = configuration["PathBaseResult"] ?? string.Empty;
        private readonly string _pathBaseLog = configuration["PathBaseLog"] ?? string.Empty;
        private static HashSet<string> _arquivosFaltandoAnterior = new();
        private static bool _emailJaEnviado = false;
        public async Task GetSheetAsync()
        {
            if (string.IsNullOrWhiteSpace(_pathBaseResult))
                throw new CustomException("path to result not config in appsettings.json");
            if (string.IsNullOrWhiteSpace(_pathBaseOrig))
                throw new CustomException("path to source not config in appsettings.json");
            if (string.IsNullOrWhiteSpace(_pathBaseLog))
                throw new CustomException("path to source not config in appsettings.json");

            //aqui a magia acontece
            try
            {
                bool files = false;

                while (files == false)
                {
                    files = await Files_Verification(_pathBaseOrig);
                    if (files) {

                        break;

                    }

                    Thread.Sleep(300000);
                    
                }

                // processamento de dados
                if (files)
                {
                    var excelfile = new List<FileVM>();

                    excelfile.AddRange(await ReadFile(_pathBaseOrig, "*BON*.txt"));
                    excelfile.AddRange(await ReadFile(_pathBaseOrig, "*FAT*.txt"));
                    excelfile.AddRange(await ReadFile(_pathBaseOrig, "*DEV*.txt"));

                    Console.WriteLine(excelfile);

                    //gerar resultado
                    if (await Final_file(_pathBaseResult, excelfile))
                    {
                        DailyLog("succeed","-");
                        return;
                    }
                    else
                    {
                        DailyLog("failed", "Arquivo excel não gerado");
                        return;
                    }
                }

            }
            catch (Exception ex)
            {
                SendSupMail($"Erro na verificação: {ex.Message}", "Falha");
                throw new CustomException("Arquivos não encontrados.");
            }

        }

        public Task<bool> Files_Verification(string path)
        {
            try
            {
                var arquivosEsperados = new[] { "BON", "FAT", "DEV" };
                var arquivosFaltandoAtual = new HashSet<string>();

                foreach (var tipo in arquivosEsperados)
                {
                    var arquivo = Directory.GetFiles(path, $"*{tipo}*.txt")
                                           .FirstOrDefault();

                    if (arquivo == null)
                    {
                        arquivosFaltandoAtual.Add(tipo);
                        DailyLog("failed", $"Arquivo {tipo} Faltante");
                    }
                }

                // ✅ Se agora está tudo OK
                if (arquivosFaltandoAtual.Count == 0)
                {
                    if (_arquivosFaltandoAnterior.Count > 0)
                    {
                        var encontrados = _arquivosFaltandoAnterior;

                        var corpoMail = string.Join("<br>",
                            encontrados.Select(a => $"-- Arquivo {a}.txt foi encontrado"));

                        var mensagemFinal =
                            $"Arquivos anteriormente faltantes foram encontrados:<br><br>" +
                            corpoMail +
                            "<br><br>Continuando os processos normalmente.";

                        SendMail(mensagemFinal, "Arquivos encontrados");
                    }

                    _arquivosFaltandoAnterior.Clear();
                    _emailJaEnviado = false;

                    return Task.FromResult(true);
                }

                // ❌ Existem arquivos faltando
                if (!_emailJaEnviado)
                {
                    var agora = DateTime.Now;
                    var proximaExecucao = agora.AddMinutes(5);

                    var corpoMail = string.Join("<br>",
                        arquivosFaltandoAtual.Select(a => $"-- Arquivo {a}.txt não encontrado"));

                    var mensagemFinal =
                        $"Robô procurou os arquivos às {agora:dd/MM/yyyy HH:mm}<br><br>" +
                        corpoMail +
                        $"<br><br>Irá rodar novamente às {proximaExecucao:dd/MM/yyyy HH:mm}";

                    SendMail(mensagemFinal, "Falta de arquivo(s)");

                    _emailJaEnviado = true;
                }

                // 🔁 Atualiza controle
                _arquivosFaltandoAnterior = arquivosFaltandoAtual;

                return Task.FromResult(false);
            }
            catch (Exception ex)
            {
                DailyLog("failed", $"Erro na verificação: {ex.Message}");
                SendSupMail($"Erro na verificação: {ex.Message}", "Falha");
                return Task.FromResult(false);
            }
        }

        public async Task<List<FileVM>> ReadFile(string path, string filtro)
        {
            var arquivo = Directory.GetFiles(path, filtro).FirstOrDefault();

            if (arquivo == null)
                throw new CustomException($"Arquivo {filtro} não encontrado.");

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            var encoding = Encoding.GetEncoding(1252);
            var linhas = await File.ReadAllLinesAsync(arquivo, encoding);

            if (linhas.Length <= 1)
                throw new Exception("Arquivo vazio.");

            var lista = new List<FileVM>();
            var culture = new CultureInfo("pt-BR");

            for (int i = 0; i < linhas.Length; i++)
            {

                var valores = linhas[i]
                    .Replace("\"", "")
                    .Split('|');

                if (valores[0] == "Tipo NF")
                    continue;

                if (valores.Length < 36)
                    continue; // ignora linha inválida

                lista.Add(MapearFileVM(valores, culture));
            }

            return lista;
        }

        private FileVM MapearFileVM(string[] valores, CultureInfo culture)
        {            

            return new FileVM
            {
                TipoNF = valores[0],
                NroNF = valores[1],
                Data = valores[2],
                Codigo = valores[3],
                Nome = valores[4],
                Municipio = valores[5],
                CEP = valores[6],
                UF = valores[7],
                CNPJ_CPF = valores[8],
                InscricaoEstadual = valores[9],
                CodigoProd = valores[10],
                NomeProd = valores[11],

                Quantidade = valores[10] == "01KR"
                    ? ParseDecimal(valores[12], culture) * 5
                    : valores[10] == "01BR"
                        ? ParseDecimal(valores[12], culture) / 5
                        : ParseDecimal(valores[12], culture),

                ValorItem = ParseDecimal(valores[13], culture),
                ValorTotal = ParseDecimal(valores[14], culture),
                ValorICMS = ParseDecimal(valores[15], culture),
                VlrPISItem = ParseDecimal(valores[16], culture),
                VlrCOFINSItem = ParseDecimal(valores[17], culture),
                ValorDifal = ParseDecimal(valores[18], culture),
                ValorIPI = ParseDecimal(valores[19], culture),
                ValorFCP = ParseDecimal(valores[20], culture),
                NET = ParseDecimal(valores[21], culture),
                AliqICMS = ParseDecimal(valores[22], culture),
                AliquotaIPI = ParseDecimal(valores[23], culture),
                ClassFiscalProduto = valores[24],
                CFOPItem = valores[25],
                NomeVendedor = valores[26],
                DescricaoCFOPItem = valores[27],
                Tipo = valores[28],
                Marca = valores[29],
                NomeClassProd = valores[30],
                InfExtra2 = valores[31],
                ClientePorCanal = valores[32],
                NumPedido = valores[33],
                VlrIIItem = ParseDecimal(valores[34], culture),
                BCIIItem = ParseDecimal(valores[35], culture),
                QuantidadeKit = valores.Length > 36
                    ? ParseDecimal(valores[36], culture)
                    : 0
            };
        }

        public async Task<bool> Final_file(string path, List<FileVM> excelfile)
        {
            try
            {
                if (excelfile == null || !excelfile.Any())
                    return false;

                // 🔹 Criar workbook (arquivo Excel)
                using var workbook = new XLWorkbook();

                // 🔹 Criar planilha
                var worksheet = workbook.Worksheets.Add("Export");

                // 🔹 Inserir dados como Tabela (já cria cabeçalhos automaticamente)
                var table = worksheet.Cell(1, 1).InsertTable(excelfile);

                // 🔹 Deixar cabeçalho em negrito
                table.Theme = XLTableTheme.TableStyleMedium2;

                // 🔹 Ajustar largura automática das colunas
                worksheet.Columns().AdjustToContents();

                // 🔹 Congelar primeira linha (opcional, mas profissional)
                worksheet.SheetView.FreezeRows(1);

                // 🔹 Criar nome do arquivo com data/hora

                var folderName = $"{DateTime.Now:yyyyMMdd_HHmm}";
                var folderPath = Path.Combine(path, folderName);
                Directory.CreateDirectory(folderPath);
                var origemFolderPath = Path.Combine(folderPath, "origem");
                Directory.CreateDirectory(origemFolderPath);

                var fileName = $"Arquivo_Final_{folderName}.xlsx";
                var filePath =Path.Combine(folderPath,fileName);
                // 🔹 Salvar arquivo
                workbook.SaveAs(filePath);


                ClearFolder(_pathBaseOrig, origemFolderPath);

                return true;
            }
            catch (Exception ex)
            {
                DailyLog("failed", "Erro ao criar arquivo Excel");
                SendSupMail($"Erro na verificação: {ex.Message}", "Falha");
                return false;
            }
        }

        public void SendMail(string mensagemFinal, string sub)
        {
            var vo = new EmailVO()
            {
                To = "lucasrigou@uol.com.br",
                Subject = $"Robô CGA - {sub}",
                BodyMessage = mensagemFinal

            };
            try
            {
                var email = new Email();

                 email.SendMail(vo.Subject, vo.BodyMessage, vo.To, "" );


            }
            catch
            {
                DailyLog("failed", "E-mail não enviado");
                
            }
        
        }

        public void SendSupMail(string mensagem, string sub)
        {
            var vo = new EmailVO()
            {
                To = "panuche@gmail.com",
                Subject = $"Robô CGA - {sub}",
                BodyMessage = mensagem

            };
                var email = new Email();


                email.SendMail(vo.Subject, vo.BodyMessage, vo.To, "lucasrigou@uol.com.br");

        }

        public void ClearFolder(string origem,string destino)
        {
            var arquivos = Directory.GetFiles(origem);
            try
            {
                foreach (var arquivo in arquivos)
                {
                    var nomeArquivo = Path.GetFileName(arquivo);
                    var destinoCompleto = Path.Combine(destino, nomeArquivo);
                    File.Move(arquivo, destinoCompleto);
                }
            }
            catch (Exception ex) {
                DailyLog("failed", "Erro ao mover arquivos");
                SendSupMail($"Erro na verificação: {ex.Message}", "Falha");
            }
            
        }

        public void DailyLog(string status, string motivo)
        {
            try
            {
                Directory.CreateDirectory(_pathBaseLog);

                var logPath = Path.Combine(_pathBaseLog, $"{DateTime.Now:yyyyMMdd}.json");

                var log = new LogVM
                {
                    Date = DateTime.Now.ToString("yyyy/MM/dd"),
                    Hour = DateTime.Now.ToString("HH:mm"),
                    Status = status,
                    DetailsFailed = motivo,
                };

                List<LogVM> list;

                if (File.Exists(logPath))
                {
                    var jsonExistente = File.ReadAllText(logPath);
                    list = JsonSerializer.Deserialize<List<LogVM>>(jsonExistente)
                           ?? new List<LogVM>();
                }
                else
                {
                    list = new List<LogVM>();
                }

                list.Add(log);

                var jsonAtualizado = JsonSerializer.Serialize(list, new JsonSerializerOptions
                {
                    WriteIndented = true
                });

                File.WriteAllText(logPath, jsonAtualizado);
            }
            catch (Exception ex)
            {

                SendSupMail($"Erro na verificação: {ex.Message}", "Falha");
            }
        }

        #region
        private decimal ParseDecimal(string valor, CultureInfo culture)
        {
            if (string.IsNullOrWhiteSpace(valor))
                return 0;

            return decimal.TryParse(valor, NumberStyles.Any, culture, out var result)
                ? result
                : 0;
        }
        #endregion
    }
}