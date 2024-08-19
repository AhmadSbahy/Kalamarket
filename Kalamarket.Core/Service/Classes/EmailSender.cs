using System.Net.Mail;
using Kalamarket.Core.Service.Interface;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;
using MimeKit;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace Kalamarket.Core.Service.Classes
{
	public class EmailSender : IEmailSender
	{
		private readonly IConfiguration _configuration;

		public EmailSender(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		//public async Task<bool> SendEmail(string email, string subject, string body, bool isHtml = true)
		//{
		//	TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();

		//	SmtpClient client = new SmtpClient("smtp-relay.brevo.com", 587)
		//	{
		//		Credentials = new NetworkCredential(
		//			_configuration.GetSection("EmailSettings:UserName").Value,
		//			_configuration.GetSection("EmailSettings:Password").Value
		//		),
		//		EnableSsl = true
		//	};

		//	MailAddress from = new MailAddress(
		//		_configuration.GetSection("EmailSettings:FromEmail").Value,
		//		_configuration.GetSection("EmailSettings:DisplayName").Value,
		//		Encoding.UTF8
		//	);

		//	MailAddress to = new MailAddress(email);

		//	MailMessage message = new MailMessage(from, to)
		//	{
		//		Body = body,
		//		BodyEncoding = Encoding.UTF8,
		//		Subject = subject,
		//		SubjectEncoding = Encoding.UTF8,
		//		IsBodyHtml = isHtml
		//	};

		//	client.SendCompleted += (s, e) =>
		//	{
		//		string token = (string)e.UserState;

		//		if (e.Cancelled)
		//		{
		//			Console.WriteLine("[{0}] Send canceled.", token);
		//			tcs.TrySetResult(false);
		//		}
		//		if (e.Error != null)
		//		{
		//			Console.WriteLine("[{0}] {1}", token, e.Error.ToString());
		//			tcs.TrySetResult(false);
		//		}
		//		else
		//		{
		//			Console.WriteLine("Message sent.");
		//			tcs.TrySetResult(true);
		//		}
		//		message.Dispose();
		//	};

		//	string userState = "test message1";
		//	client.SendAsync(message, userState);

		//	return await tcs.Task;
		//}
		public async Task<bool> SendEmail(string email, string subject, string body, bool isHtml = true)
		{
			MimeMessage message = new MimeMessage();
			message.From.Add(new MailboxAddress(_configuration.GetSection("EmailSettings:DisplayName").Value, _configuration.GetSection("EmailSettings:FromEmail").Value));
			message.To.Add(new MailboxAddress(email, email));
			message.Subject = subject;
			var builder = new BodyBuilder();

			builder.HtmlBody = body;
			message.Body = builder.ToMessageBody();
			SmtpClient client = new SmtpClient();
			client.Connect("akalamarket.ir", 465, true);
			client.Authenticate(_configuration.GetSection("EmailSettings:UserName").Value, _configuration.GetSection("EmailSettings:Password").Value);
			client.Send(message);
			client.Disconnect(true);
			return true;
		}
	}
}
