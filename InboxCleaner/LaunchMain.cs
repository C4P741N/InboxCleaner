using MailKit.Net.Imap;
using MailKit.Search;
using MailKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InboxCleaner
{
    class LaunchMain
    {
        const string imapServer = "imap.gmail.com";
        const int imapPort = 993;
        const bool useSsl = true;
        const string username = "";
        const string password = "";
        public void Begin()
        {
            using (var client = new ImapClient())
            {
                try
                {
                    Login(client);

                    client.Inbox.Open(FolderAccess.ReadWrite);

                    Body(client);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
                finally
                {
                    client.Disconnect(true);
                }
            }
        }

        private void Body(ImapClient client)
        {
            bool repeatLoop = true;

            while (repeatLoop)
            {
                Console.WriteLine("Paste the sender title");
                var subject = Console.ReadLine();

                var query = SearchQuery.FromContains(subject);
                var uids = client.Inbox.Search(query);

                if (uids.Count == 0)
                {
                    Console.WriteLine("Nothing by that subject found");

                    repeatLoop = CheckIf("retry");
                }
                else
                {
                    Console.WriteLine($"{uids.Count} number of mails found");

                    bool proceed = CheckIf("proceed deleting");

                    if (proceed)
                    {
                        client.Inbox.AddFlags(uids, MessageFlags.Deleted, true);

                        client.Inbox.Expunge();

                        Console.WriteLine("Deleted successfully");
                    }

                    repeatLoop = CheckIf("add another");
                }
            }
        }

        private void Login(ImapClient client)
        {
            Console.WriteLine("Logging in...");

            client.Connect(imapServer, imapPort, useSsl);

            client.Authenticate(username, password);

            Console.WriteLine("Logged In!");
        }

        private bool CheckIf(string test)
        {
            bool repeat = false;

            Console.WriteLine($"Input 'y' to {test} 'n' to stop task");
            var value = Console.ReadLine();

            if (string.IsNullOrEmpty(value))
            {
                Console.WriteLine("Please input a value");

                CheckIf("retry");
            }

            if (value?.ToLower() != "y" && value?.ToLower() != "n")
            {
                Console.WriteLine("Invalid value");

                CheckIf("retry");
            }

            if (value == "y")
                return repeat = true;

            return repeat;
        }
    }
}
