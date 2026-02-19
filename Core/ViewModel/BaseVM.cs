using System.Text;

namespace Core.ViewModel
{
    public class BaseVM
    {
        public const string DATE_FORMATING = "dd/MM/yyy";
        public const string TIME_FORMATING = "hh:mm:ss";
        public const string DATE_AND_TIME_FORMATING = $"{DATE_FORMATING} {TIME_FORMATING}";

        public static string GetTypePatrimony(int type)
        {
            switch (type)
            {
                case 1: return "Próprio";
                case 2: return "Alugado";
                default:
                    return string.Empty;
            }
        }

        //public static string SetButtonActive(string customButton, bool isActive, long Id)
        //{
        //    if (!string.IsNullOrEmpty(customButton))
        //        return customButton;
        //    else if (!isActive)
        //        return ButtonsVM.GetButtonsBy(Id, "reactive");
        //    else 
        //        return ButtonsVM.GetButtonsBy(Id, "edit", "delete");
          
        //}

        public static string GetDecimal(decimal value)
        {
            return value.ToString("N2");
        }

        public static string GetPathProductImg(string basePath, long productId)
        {
            var path = Path.Combine(basePath, "attachments", "product", productId.ToString());
            if (Directory.Exists(path))
            {
                var files = Directory.GetFiles(path);
                if (files.Length > 0)
                {
                    string firstFilePath = files[0];
                    var fileName = Path.GetFileName(firstFilePath);
                    return $"/attachments/product/{productId.ToString()}/{fileName}";
                }
            }
            return $"/img/semproduto.png";
        }

        public static string GetPathUserImg(string basePath, long userId)
        {
            var path = Path.Combine(basePath, "attachments", "user", userId.ToString());
            if (Directory.Exists(path))
            {
                var files = Directory.GetFiles(path);
                if (files.Length > 0)
                {
                    string firstFilePath = files[0];
                    var fileName = Path.GetFileName(firstFilePath);
                    return $"/attachments/user/{userId.ToString()}/{fileName}";
                }
            }
            return $"/img/withoutsign.png";
        }
    }
}
