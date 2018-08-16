using EmailWrapper;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace BandDrop.Utils
{
    public static class APIUtility
    {
        private static string pusherKey = "de9851a0744ee8227c97";
        private static string pusherSecretKey = "7a898025cb24b73f148e";
        private static string pusherCluster = "us2";
        private static string pusherAppId = "576132";
        private static string mailgunAPIKey = "b8bc7a931edd4cfe2062de5a9ae8f2f1-6b60e603-0a8be4e3";
        public static string PusherKey
        {
            get
            {
                return pusherKey;
            }
        }
        public static string PusherSecretKey
        {
            get
            {
                return pusherSecretKey;
            }
        }
        public static string PusherCluster
        {
            get
            {
                return pusherCluster;
            }
        }
        public static string PusherAppId
        {
            get
            {
                return pusherAppId;
            }
        }
        public static string MailgunAPIKey
        {
            get
            {
                return mailgunAPIKey;
            }
        }
        public static IRestResponse SendSimpleMessage(string to, string subject, string body)
        {
            RestClient client = new RestClient();
            client.BaseUrl = new Uri("https://api.mailgun.net/v3");
            client.Authenticator =
                new HttpBasicAuthenticator("api",
                                            MailgunAPIKey);
            RestRequest request = new RestRequest();
            request.AddParameter("domain", "sandbox38e5a6fcb19b49f4956bb7740f9eeedb.mailgun.org", ParameterType.UrlSegment);
            request.Resource = "{domain}/messages";
            request.AddParameter("from", "postmaster@sandbox38e5a6fcb19b49f4956bb7740f9eeedb.mailgun.org");
            request.AddParameter("to", to);
            request.AddParameter("subject",subject);
            request.AddParameter("text",body);
            request.Method = Method.POST;
            return client.Execute(request);
        }
        
        public static void SendEmailWrapper(string to, string subject, string body)
        {
            /*EmailClient em = new EmailClient(MailgunAPIKey, "sandbox1ab1a435ae524b5585b5cff24f259125.mailgun.org");  */              // Mailgun
            EmailClient em = new EmailClient("smtp.mailgun.org", 587, "postmaster@sandbox1ab1a435ae524b5585b5cff24f259125.mailgun.org", "dodo11", false);   // SMTP

            Email email = new Email();
            email.IsHtml = false;
            email.FromAddress = "postmaster@sandbox1ab1a435ae524b5585b5cff24f259125.mailgun.org";
            email.ReplyAddress = "postmaster@sandbox1ab1a435ae524b5585b5cff24f259125.mailgun.org";
            email.ToAddress = to;
            email.Subject = subject;
            email.Body = body;
            em.Send(email);
        }
    }
}