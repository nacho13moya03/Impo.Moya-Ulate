using System.Collections.Specialized;
using System.Configuration;
using System.Net.Mail;

namespace APIProyectoSC_601.Entities
{
    public class Utilitarios
    {
        public string CuentaCorreo { get; } = ((NameValueCollection)ConfigurationManager.GetSection("secureAppSettings"))["cuentaCorreo"];
        public string ClaveCorreo { get; } = ((NameValueCollection)ConfigurationManager.GetSection("secureAppSettings"))["claveCorreo"];

        public void EnviarCorreo(string destino, string asunto, string contenido)
        {
            MailMessage message = new MailMessage();
            message.From = new MailAddress(CuentaCorreo);
            message.To.Add(new MailAddress(destino));
            message.Subject = asunto;
            message.Body = contenido;
            message.Priority = MailPriority.Normal;
            message.IsBodyHtml = true;

            SmtpClient client = new SmtpClient("smtp.office365.com", 587);
            client.Credentials = new System.Net.NetworkCredential(CuentaCorreo, ClaveCorreo);
            client.EnableSsl = true;
            client.Send(message);
        }
    }
}