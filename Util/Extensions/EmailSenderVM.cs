namespace Util.Extensions
{
    public class EmailSenderVM
    {
        public string User { get; set; }
        public string Password { get; set; }
        public string Smtp { get; set; }
        public int Porta { get; set; }
        public bool IsSsl { get; set; }
    }
}
