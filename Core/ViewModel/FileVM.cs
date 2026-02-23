using System.Text.Json.Serialization;

namespace Core.ViewModel
{
    public class FileVM
    {
        public string TipoNF { get; set; }
        public string NroNF { get; set; }
        public string Data { get; set; }
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public string Municipio { get; set; }
        public string CEP { get; set; }
        public string UF { get; set; }
        public string CNPJ_CPF { get; set; }
        public string InscricaoEstadual { get; set; }
        public string CodigoProd { get; set; }
        public string NomeProd { get; set; }
        public decimal Quantidade { get; set; }
        public decimal ValorItem { get; set; }
        public decimal ValorTotal { get; set; }
        public decimal ValorICMS { get; set; }
        public decimal VlrPISItem { get; set; }
        public decimal VlrCOFINSItem { get; set; }
        public decimal ValorDifal { get; set; }
        public decimal ValorIPI { get; set; }
        public decimal ValorFCP { get; set; }
        public decimal NET { get; set; }
        public decimal AliqICMS { get; set; }
        public decimal AliquotaIPI { get; set; }
        public string ClassFiscalProduto { get; set; }
        public string CFOPItem { get; set; }
        public string NomeVendedor { get; set; }
        public string DescricaoCFOPItem { get; set; }
        public string Tipo { get; set; }
        public string Marca { get; set; }
        public string NomeClassProd { get; set; }
        public string InfExtra2 { get; set; }
        public string ClientePorCanal { get; set; }
        public string NumPedido { get; set; }
        public decimal VlrIIItem { get; set; }
        public decimal BCIIItem { get; set; }
        public decimal QuantidadeKit { get; set; }
    }
}