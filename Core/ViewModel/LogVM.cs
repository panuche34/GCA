using System.Text.Json.Serialization;

namespace Core.ViewModel
{
    public class LogVM
    {
        public string PathFileOrig { get; set; } = string.Empty; //Aqui poser um json com os caminhos das planilhas.
        public string PathFileDest { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty; // Sucesso ou Falha
        public string? DetailsFailed { get; set; }
    }
}
