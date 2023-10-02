using MailKit.Net.Imap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InboxCleaner
{
    class LogIn
    {
        const string imapServer = "imap.gmail.com";
        const int imapPort = 993;
        const bool useSsl = true;
        private string username = "";
        private string password = "";
        private Validators validators = new Validators();

        private string GetValidInput(string prompt)
        {
            while (true)
            {
                Console.WriteLine(prompt);
                var input = Console.ReadLine();

                if (validators.CheckLoginCredentials(input))
                    return input;

                Console.WriteLine("Invalid input. Please try again.");
            }
        }

        public void Login(ImapClient client)
        {
            username = GetValidInput("Please input your username");
            password = GetValidInput("Please input your password");

            Console.WriteLine("Logging in...");

            client.Connect(imapServer, imapPort, useSsl);
            client.Authenticate(username, password);

            Console.WriteLine("Logged In!");
        }
    }
}
