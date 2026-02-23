using System.Text.Json.Serialization;

namespace Core.ViewModel
{
    public class LogVM
    {
        public string Date { get; set; }
        public string hour { get; set; }
        public string Status { get; set; } = string.Empty; // Sucesso ou Falha
        public string? DetailsFailed { get; set; }
    }
}
