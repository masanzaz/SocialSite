using Social.Application.Interfaces;
using Social.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Twilio;
using System.Threading.Tasks;
using Twilio.Clients;
using Twilio.Rest.Api.V2010.Account;

namespace Social.Infrastructure.Services
{
    public class AccountService : IAccountService
    {
        public async Task<Response<bool>> ConfirmPhoneNumber(string phoneNumber, string otp)
        {
            //string accountSid = Environment.GetEnvironmentVariable("TWILIO_ACCOUNT_SID");
           // string authToken = Environment.GetEnvironmentVariable("TWILIO_AUTH_TOKEN");

            string accountSid = "AC3905ac52ccab537530efcd8c8a8e6241";
            string authToken = "d156638373d0f3db6d345a57c46da69b";
            string fromPhoneNumer = "d156638373d0f3db6d345a57c46da69b";
            TwilioClient.Init(accountSid, authToken);
            try
            {
                var message = MessageResource.Create(
                    body: "Your verification code is: " + otp,
                    from: new Twilio.Types.PhoneNumber(fromPhoneNumer),
                    to: new Twilio.Types.PhoneNumber(phoneNumber)
                );

            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
                return new Response<bool>(false);
            }
            return new Response<bool>(true);
        }

        public int? GetAge(DateTime dateOfBirth)
        {
            if (dateOfBirth == DateTime.MinValue)
                return null;
            var today = DateTime.Today;
            var a = (today.Year * 100 + today.Month) * 100 + today.Day;
            var b = (dateOfBirth.Year * 100 + dateOfBirth.Month) * 100 + dateOfBirth.Day;
            return (a - b) / 10000;
        }

        public string GetLocalPath(string file, string name)
        {
            try
            {
                byte[] bytes = Convert.FromBase64String(file);
                string fileName = "C:/inetpub/wwwroot/images/image_" + name + ".jpeg";
                Image image;
                using (MemoryStream ms = new MemoryStream(bytes))
                {
                    image = Image.FromStream(ms);
                    image.Save(fileName, ImageFormat.Jpeg);
                }
                return "images/image_" + name + ".jpeg";
            }
            catch (Exception ex)
            {
                return "images/man_no_image.png";
            }
        }

        public string GetTime(DateTime start)
        {
            DateTime end = DateTime.Now;
            TimeSpan diff = end - start;
            if (diff.Days > 0)
                return String.Format("{0} days", diff.Days);
            if (diff.Hours > 0)
                return String.Format("{0} hours", diff.Hours);
            if (diff.Minutes > 0)
                return String.Format("{0} minues", diff.Minutes);
            if (diff.Seconds > 0)
                return String.Format("{0} seconds", diff.Seconds);
            return "now";
        }


    }
}
