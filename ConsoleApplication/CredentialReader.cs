using System;
using System.IO;
using Newtonsoft.Json;
namespace ConsoleApplication
{
    public class CredentialReader
    {
        public static string MyDocuments { get; } = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        private static string CredentialsFileName = "telegram_app_credentials.json";
        private static string AppConfigPath { get; } = Path.Combine(MyDocuments, CredentialsFileName);

        public static AppCredentials Load()
        {
           var text =  File.ReadAllText(AppConfigPath);
            return JsonConvert.DeserializeObject<AppCredentials>(text);
        }

        public static void SaveToDisc(AppCredentials credentials)
        {

          var text =  JsonConvert.SerializeObject(credentials);
            Console.WriteLine($"save to {AppConfigPath} content = {text}");
            File.WriteAllText(AppConfigPath, text );
        }
    }
}