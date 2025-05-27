using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;
using MailKit.Security;
using MimeKit.Utils;
namespace LogicaAplicacion.ServicioCorreo
{
    public class ServicioCorreo
    {
        public async Task EnviarCodigoPorCorreo(string emailDestino, string codigo)
        {
            var message = new MimeMessage();

            // Nombre y correo del remitente
            message.From.Add(new MailboxAddress("Dental Latina", "DentalLatina@sotosantiago.xyz"));
            message.To.Add(MailboxAddress.Parse(emailDestino));
            message.Subject = "Tu código de verificación - Dental Latina";

            var bodyBuilder = new BodyBuilder
            {
                HtmlBody = $@"
    <div style='font-family:Segoe UI, Roboto, sans-serif; background-color:#f9f9f9; padding:30px; border-radius:10px; max-width:600px; margin:auto; box-shadow:0 2px 8px rgba(0,0,0,0.1);'>
        <h2 style='color:#3F51B5; text-align:center;'>¡Hola!</h2>
        <p style='font-size:16px; color:#333; text-align:center;'>Tu código de verificación es:</p>
        <h1 style='color:#00BCD4; text-align:center; font-size:42px; letter-spacing:4px;'>{codigo}</h1>
        <p style='font-size:15px; color:#555; text-align:center;'>Si no solicitaste este código, podés ignorar este mensaje.</p>
        <hr style='margin:30px 0; border:none; border-top:1px solid #e0e0e0;'/>
        <p style='font-size:12px; color:#999; text-align:center;'>Dental Latina • <a href='https://sotosantiago.xyz' style='color:#9C27B0; text-decoration:none;'>sotosantiago.xyz</a></p>
    </div>",

                TextBody = $"Tu código de verificación es: {codigo}. Si no solicitaste este código, podés ignorar este mensaje. Dental Latina - sotosantiago.xyz"
            };
            message.Body = bodyBuilder.ToMessageBody();

            using var client = new MailKit.Net.Smtp.SmtpClient();
            client.ServerCertificateValidationCallback = (s, c, h, e) => true; // ignorar errores SSL (solo pruebas)
            try
            {
                await client.ConnectAsync("mail.sotosantiago.xyz", 587, SecureSocketOptions.StartTls);
                await client.AuthenticateAsync("DentalLatina@sotosantiago.xyz", "4&L0en31k");
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
            catch (Exception ex)
            {
                // Log o lanzar la excepción si querés manejarlo fuera
                Console.WriteLine($"Error al enviar correo: {ex.Message}");
            }
        }
        public async Task EnviarCorreoMasivo(
    List<string> destinatarios,
    string asunto,
    string mensajeHtml,
    string? rutaAdjunto = null)
        {
            var message = new MimeMessage();

            // Configurar remitente
            message.From.Add(new MailboxAddress("Dental Latina", "DentalLatina@sotosantiago.xyz"));

            // Configurar destinatarios (puede ser una lista)
            foreach (var email in destinatarios)
            {
                message.To.Add(MailboxAddress.Parse(email));
            }

            message.Subject = asunto;

            // Pie de página profesional
            string pieDePagina = @"
        <div style='margin-top: 30px; padding-top: 20px; border-top: 1px solid #e0e0e0; font-family: Arial, sans-serif; color: #555555; font-size: 12px;'>
            <table width='100%'>
                <tr>
                    <td>
                        <p style='margin: 5px 0;'><strong>Dental Latina</strong></p>
                        <p style='margin: 5px 0;'>Email: info@dentallatina.com.uy</p>
                        <p style='margin: 5px 0;'>Dirección: Mercedes 1541</p>
                    </td>
                    <td style='text-align: right;'>
                        <img src='wwwroot/img/logopng.png' alt='Logo Dental Latina' style='max-height: 80px;' />
                    </td>
                </tr>
            </table>
            <p style='margin-top: 15px; font-size: 10px; color: #999999;'>
                Este mensaje es confidencial y está dirigido únicamente al destinatario. 
                Si ha recibido este mensaje por error, por favor notifíquelo al remitente 
                y elimínelo de su sistema.
            </p>
        </div>";

            // Combinar mensaje con pie de página
            string mensajeCompleto = mensajeHtml + pieDePagina;

            var bodyBuilder = new BodyBuilder
            {
                HtmlBody = mensajeCompleto,
                TextBody = ConvertirHtmlATexto(mensajeCompleto) // Función auxiliar para versión texto
            };

            // Adjuntar archivo si existe
            if (!string.IsNullOrEmpty(rutaAdjunto) && File.Exists(rutaAdjunto))
            {
                bodyBuilder.Attachments.Add(rutaAdjunto);
            }


            message.Body = bodyBuilder.ToMessageBody();

            using var client = new MailKit.Net.Smtp.SmtpClient();
            client.ServerCertificateValidationCallback = (s, c, h, e) => true;

            try
            {
                await client.ConnectAsync("mail.sotosantiago.xyz", 587, SecureSocketOptions.StartTls);
                await client.AuthenticateAsync("DentalLatina@sotosantiago.xyz", "4&L0en31k");
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al enviar correo: {ex.Message}");
                throw;
            }
        }
        // Método auxiliar para convertir HTML a texto plano
        private string ConvertirHtmlATexto(string html)
        {
            try
            {
                var doc = new HtmlAgilityPack.HtmlDocument();
                doc.LoadHtml(html);
                return doc.DocumentNode.InnerText;
            }
            catch
            {
                return "Contenido del correo electrónico (HTML no convertible)";
            }
        }
        public string GenerarCodigo()
        {
            return new Random().Next(100000, 999999).ToString();
        }
    }
}
