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
    class MainController
    {
        public void Begin()
        {
            LogIn logIn = new LogIn();
            BusinessLogic businessLogic = new BusinessLogic();

            using (var client = new ImapClient())
            {
                try
                {
                    logIn.Login(client);

                    client.Inbox.Open(FolderAccess.ReadWrite);

                    businessLogic.Body(client);
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
    }
}
