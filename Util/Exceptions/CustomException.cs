namespace Util.Exceptions
{
    public class CustomException : Exception
    {
        public CustomException() { }
        public CustomException(string message) : base(message) { }
        public CustomException(string message, Exception inner) : base(message, inner) { }
        public CustomException(int errorCode, string message)
            : base($"{errorCode}: {message}") { }
        public CustomException(int errorCode, string message, Exception inner)
            : base($"{errorCode}: {message}", inner) { }

        public static CustomException CreateFromException(Exception ex)
        {
            if ((ex.InnerException != null) && (ex.InnerException.Message.Contains("Failed to connect to")))
                return new CustomException(GetInnerException(ex, "Unable to access the database or Offline"));

            return new CustomException(GetInnerException(ex, string.Empty));
        }

        public static string GetInnerException(Exception ex, string text)
        {
            if (text == null)
                text += string.Empty;

            if (ex == null)
                return text;

            return GetInnerException(ex.InnerException, text +
                $"{(string.IsNullOrWhiteSpace(text) ? string.Empty : "<br /><br />")}" + ex.Message);
        }
    }
}
