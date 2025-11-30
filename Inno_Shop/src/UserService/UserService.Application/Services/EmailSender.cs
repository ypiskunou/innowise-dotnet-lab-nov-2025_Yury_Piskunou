using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using UserService.Application.Contracts;

namespace UserService.Application.Services;

public class EmailSender : IEmailSender
{
    private readonly IConfiguration _configuration;

    public EmailSender(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task SendEmailAsync(string email, string subject, string message)
    {
        var emailMessage = new MimeMessage();

        var from = _configuration["EmailSettings:From"];
        emailMessage.From.Add(new MailboxAddress("InnoShop", from));
        emailMessage.To.Add(new MailboxAddress("", email));
        emailMessage.Subject = subject;

        var bodyBuilder = new BodyBuilder { HtmlBody = message };
        emailMessage.Body = bodyBuilder.ToMessageBody();

        using var client = new SmtpClient();
        var server = _configuration["EmailSettings:SmtpServer"];
        var port = int.Parse(_configuration["EmailSettings:Port"] ?? "1025");
            
        var useSsl = bool.Parse(_configuration["EmailSettings:UseSsl"] ?? "false");
        await client.ConnectAsync(server, port, useSsl);
            
        var password = _configuration["EmailSettings:Password"];
        if (!string.IsNullOrEmpty(password))
        {
            await client.AuthenticateAsync(_configuration["EmailSettings:UserName"], password);
        }

        await client.SendAsync(emailMessage);
        await client.DisconnectAsync(true);
    }
}