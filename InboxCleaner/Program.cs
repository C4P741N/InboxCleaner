﻿using System;
using InboxCleaner;
using MailKit;
using MailKit.Net.Imap;
using MailKit.Search;
using MailKit.Security;

class Program
{
    static void Main(string[] args)
    {
        LaunchMain program = new LaunchMain();
        program.Begin();
    }    
}
