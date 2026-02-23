namespace Core.Entity
{
    public class Log : BaseEntity
    {
        public DateTime CreatedOnUtc { get; set; }
        public string Status { get; set; } = string.Empty; // Sucesso ou Falha
        public string? DetailsFailed { get; set; }
        
    }
}
