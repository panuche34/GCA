using MimeKit;
using System.Net.Mail;


namespace Util.Extensions
{
   
    public class Email
    {
        

        private readonly EmailSenderVM sender;
        
        public Email()
        {
            sender = new EmailSenderVM
            {
                User = "notificacoes@teclaconsultoria.com.br",
                Password = "Rp340451*",
                Smtp = "smtp.titan.email",
                Porta = 465,
                IsSsl = true,
            };
        }


        public async Task SendMail(string titulo, string body, string emailTo1, string emailTo2, string attach)
        {
            try
            {
                // Criar a mensagem do e-mail
                MimeMessage mail = new MimeMessage();
                mail.From.Add(new MailboxAddress("Notificações Tecla Consultoria", "notificacoes@teclaconsultoria.com.br"));

                // Dividir os e-mails pelo ponto e vírgula e remover espaços extras
                var emails = emailTo1?.Split(';').Select(e => e.Trim()).Where(e => !string.IsNullOrEmpty(e)).ToList() ?? new List<string>();

                if (emails.Count > 0)
                {
                    // O primeiro e-mail vai para TO
                    mail.To.Add(new MailboxAddress(emails[0], emails[0]));

                    // Se houver mais e-mails, eles vão para CC
                    for (int i = 1; i < emails.Count; i++)
                    {
                        mail.Cc.Add(new MailboxAddress(emails[i], emails[i]));
                    }
                }

                // Adicionar um destinatário Cc adicional, se fornecido
                if (!string.IsNullOrEmpty(emailTo2))
                {
                    mail.Cc.Add(new MailboxAddress(emailTo2, emailTo2));
                }

                mail.Subject = titulo;

                // Criar o BodyBuilder para o corpo do e-mail
                BodyBuilder bodyBuilder = new BodyBuilder
                {
                    HtmlBody = body
                };

                // Adicionar o anexo, se houver
                if (!string.IsNullOrEmpty(attach) && File.Exists(attach))
                {
                    bodyBuilder.Attachments.Add(attach);
                }

                // Configurar o corpo da mensagem
                mail.Body = bodyBuilder.ToMessageBody();

                // Enviar o e-mail
                using (var client = new MailKit.Net.Smtp.SmtpClient())
                {
                    client.Connect(sender.Smtp, sender.Porta, sender.IsSsl);
                    client.Authenticate(sender.User, sender.Password);
                    client.Send(mail);
                    client.Disconnect(true);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Erro ao enviar e-mail: {e.Message}");
                throw;
            }
        }

    }
}
