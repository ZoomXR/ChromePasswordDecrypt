using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace BrowserPass
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Google Chrome Password Decrypt - by ZoomX";
            string title = @"
 /$$$$$$$   /$$$$$$                      /$$$$$$$$                                   /$$   /$$
| $$__  $$ /$$__  $$                    |_____ $$                                   | $$  / $$
| $$  \ $$| $$  \__/                         /$$/   /$$$$$$   /$$$$$$  /$$$$$$/$$$$ |  $$/ $$/
| $$$$$$$/| $$             /$$$$$$          /$$/   /$$__  $$ /$$__  $$| $$_  $$_  $$ \  $$$$/ 
| $$__  $$| $$            |______/         /$$/   | $$  \ $$| $$  \ $$| $$ \ $$ \ $$  >$$  $$ 
| $$  \ $$| $$    $$                      /$$/    | $$  | $$| $$  | $$| $$ | $$ | $$ /$$/\  $$
| $$  | $$|  $$$$$$/                     /$$$$$$$$|  $$$$$$/|  $$$$$$/| $$ | $$ | $$| $$  \ $$
|__/  |__/ \______/                     |________/ \______/  \______/ |__/ |__/ |__/|__/  |__/
                                                                                              ";

            Console.WriteLine(title + "\n");
            List<IPassReader> readers = new List<IPassReader>();
            readers.Add(new ChromePassReader());

            foreach (var reader in readers)
            {
                foreach (Process proc in Process.GetProcessesByName("chrome"))
                {
                    proc.Kill();
                }
                Console.WriteLine($"=================================== GOOGLE CHROME =================================== \n");
                try
                {
                    PrintCredentials(reader.ReadPasswords());
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Şifre çözme işlemi başarısız {reader.BrowserName} şifreler: " + ex.Message);
                }
            }

#if DEBUG
            Console.ReadLine();
#endif

        }

        static void PrintCredentials(IEnumerable<CredentialModel> data)
        {
            foreach (var d in data)
                Console.WriteLine($"{d.Url}\r\n\tKullanıcı: {d.Username}\r\n\tŞifre: {d.Password}\r\n");
        }
    }
}
