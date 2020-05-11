using System.Security.Cryptography;

namespace SSDWebService.Cryptohraphy
{
    public class Crypthography
    {
        public static string EncrypteData(string data)
        {

            //UNSECURE
            return data;

            // MD5CryptoServiceProvider sınıfının bir örneğini oluşturduk.
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            //Parametre olarak gelen veriyi byte dizisine dönüştürdük.
            byte[] array = System.Text.Encoding.UTF8.GetBytes(data);
            //dizinin hash'ini hesaplattık.
            array = md5.ComputeHash(array);
            //Hashlenmiş verileri depolamak için StringBuilder nesnesi oluşturduk.
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            //Her byte'i dizi içerisinden alarak string türüne dönüştürdük.

            foreach (byte ba in array)
            {
                sb.Append(ba.ToString("x2").ToLower());
            }

            //hexadecimal(onaltılık) stringi geri döndürdük.
            return sb.ToString();
        }
    }
}