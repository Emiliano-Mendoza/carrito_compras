namespace WebApplicationMVC.Helpers
{
    public class imageHelper
    {

        public static string ConvertirBase64(string ruta,out bool conversion)
        {
            string textoBase64 = string.Empty;
            conversion = true;

            try
            {
                byte[] bytes = File.ReadAllBytes(ruta);
                textoBase64 = Convert.ToBase64String(bytes);
                
            }
            catch (Exception)
            {
                conversion = false;
            }

            return textoBase64;
        }

    }
}
