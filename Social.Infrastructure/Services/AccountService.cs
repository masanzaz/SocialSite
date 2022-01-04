using Social.Application.Interfaces;
using Social.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social.Infrastructure.Services
{
    public class AccountService : IAccountService
    {
        public async Task<Response<bool>> ConfirmPhoneNumber(string phoneNumber, string code)
        {
            //validate code
            return new Response<bool>(true);

            //return new Response<string>(phoneNumber, message: $"Phone Number {phoneNumber} Confirmed ");
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
                return "http://40.113.3.39/images/image_" + name + ".jpeg";
            }
            catch (Exception ex)
            {
                return "http://40.113.3.39/images/man_no_image.png";
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
