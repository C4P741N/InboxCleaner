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
     class BusinessLogic
    {
        public void Body(ImapClient client)
        {
            bool repeatLoop = true;
            Validators validators = new Validators();

            while (repeatLoop)
            {
                Console.WriteLine("Paste the sender title");
                var subject = Console.ReadLine();

                var query = SearchQuery.FromContains(subject);
                var uids = client.Inbox.Search(query);

                if (uids.Count == 0)
                {
                    Console.WriteLine("Nothing by that subject found");

                    repeatLoop = validators.CheckUserInput("retry");
                }
                else
                {
                    Console.WriteLine($"{uids.Count} number of mails found");

                    bool proceed = validators.CheckUserInput("proceed deleting");

                    if (proceed)
                    {
                        client.Inbox.AddFlags(uids, MessageFlags.Deleted, true);

                        client.Inbox.Expunge();

                        Console.WriteLine("Deleted successfully");
                    }

                    repeatLoop = validators.CheckUserInput("add another");
                }
            }
        }
    }
}
