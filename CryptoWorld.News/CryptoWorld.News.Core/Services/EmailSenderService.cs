﻿using CryptoWorld.Application.Server.Settings;
using CryptоWorld.News.Core.Interfaces;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Threading.Tasks;


namespace CryptоWorld.News.Core.Services
{
    public class EmailSenderService : IEmailSenderService
    {
        private readonly SendGridSettings sendGridSettings;

        public EmailSenderService(IOptions<SendGridSettings> _sendGridSettings)
        {
            sendGridSettings = _sendGridSettings.Value;
        }

        public Task SendEmailAsync(string reciever , string username)
        {
            var sender = "test.2010@abv.bg";

            var apiKey = sendGridSettings.ApiKey;
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(sender, "CryptoNews");
            var subject = "CryptoNews Registration";
            var to = new EmailAddress(reciever, username);
            var plainTextContent = "Your registration is confirmed!";
            var htmlContent = "Your registration is confirmed!";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

            return client.SendEmailAsync(msg);
        }
    }
}
