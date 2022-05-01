using System.Security.Cryptography;
using System.Text;

using System.Net;
using System.IO;
using System.Net.Mail;

namespace WebApplicationMVC.Helpers
{
    public class claveHandler
    {
        public static string generarClave()
        {
            string clave = Guid.NewGuid().ToString("N").Substring(0,6);
            return clave;
        }

        //Encriptacion de clave
        public static string convertirSha256(string texto)
        {
            StringBuilder sb = new StringBuilder();
            using ( SHA256 hash = SHA256Managed.Create())
            {
                Encoding enc = Encoding.UTF8;
                byte[] result = hash.ComputeHash(enc.GetBytes(texto));

                foreach (byte b in result)
                    sb.Append(b.ToString("x2"));
            }

            return sb.ToString();
        }

        public static bool EnviarCorreo(string correo, string asunto, string mensaje)
        {
            bool resultado = false;

            try
            {
                MailMessage mail = new MailMessage();
                mail.To.Add(correo);
                mail.From = new MailAddress("emilianolol2021@gmail.com");
                mail.Subject = asunto;
                mail.Body = mensaje;
                mail.IsBodyHtml = true;

                var smtp = new SmtpClient()
                {
                    Credentials = new NetworkCredential("emilianolol2021@gmail.com", "uunlvbrqvbuflzbc"),
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true
                };

                smtp.Send(mail);
                resultado = true;

            }catch (Exception)
            {

            }

            return resultado;
        }
    }
}
