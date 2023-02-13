using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace WebApp
{
    public static class Helper
    {
        public static string Upload(IFormFile f)
        {
            if (f != null)
            {
                string root = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
                string ext = Path.GetExtension(f.FileName);
                string fileName = RandomString(32 - ext.Length) + ext;
                using (Stream stream = new FileStream(Path.Combine(root, fileName), FileMode.Create))
                {
                    f.CopyTo(stream);
                }
                return fileName;
            }
            return null;
        }
        public static string RandomString(int len)
        {
            string pattern = "qwertyuiopasdfghjklzxcvbnm1234567890";
            char[] arr = new char[len];
            Random rand = new Random();
            for (int i = 0; i < len; i++)
            {
                arr[i] = pattern[rand.Next(pattern.Length)];
            }
            return string.Join(string.Empty, arr);
        }
        public static string RemoveVietnameseTone(string text)
        {
            string[] arr =
            {
                         "à|á|ạ|ả|ã|â|ầ|ấ|ậ|ẩ|ẫ|ă|ằ|ắ|ặ|ẳ|ẵ|/g",
                         "è|é|ẹ|ẻ|ẽ|ê|ề|ế|ệ|ể|ễ|/g",
                         "ì|í|ị|ỉ|ĩ|/g",
                         "ò|ó|ọ|ỏ|õ|ô|ồ|ố|ộ|ổ|ỗ|ơ|ờ|ớ|ợ|ở|ỡ|/g",
                         "ù|ú|ụ|ủ|ũ|ư|ừ|ứ|ự|ử|ữ|/g",
                         "ỳ|ý|ỵ|ỷ|ỹ|/g",
                         "đ"
            };

            string[] brr =
            {
                "a","e","i","o","u","y","d"
            };

            string result = text.ToLower();
            for (int i = 0; i < arr.Length; i++)
            {
                result = Regex.Replace(result, arr[i], brr[i]);
            }

            return result;
        }
        public static string GenerateSlug(string text)
        {
            text = RemoveVietnameseTone(text);
            string str = RemoveAccent(text);
            // invalid chars           
            str = Regex.Replace(str, @"[^a-z0-9\s-]", "");
            // convert multiple spaces into one space   
            str = Regex.Replace(str, @"\s+", " ").Trim();
            // cut and trim 
            str = str.Substring(0, str.Length <= 60 ? str.Length : 60).Trim();
            str = Regex.Replace(str, @"\s", "-"); // hyphens   
            return str;
        }
        private static string RemoveAccent(string text)
        {
            byte[] bytes = System.Text.Encoding.GetEncoding("Cyrillic").GetBytes(text);
            return System.Text.Encoding.ASCII.GetString(bytes);
        }
        public static byte[] Hash(string plantext)
        {
            //HashAlgorithm algorithm = HashAlgorithm.Create("SHA-512");
            //return algorithm.ComputeHash(Encoding.ASCII.GetBytes(plantext));
            HashAlgorithm algorithm = HashAlgorithm.Create("SHA-512");
            return algorithm.ComputeHash(Encoding.ASCII.GetBytes(plantext));
        }
    }
}
