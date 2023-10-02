using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InboxCleaner
{
    class Validators
    {
        public bool CheckUserInput(string val)
        {
            bool repeat = false;

            Console.WriteLine($"Input 'y' to {val} 'n' to stop task");
            var value = Console.ReadLine();

            if (string.IsNullOrEmpty(value))
            {
                Console.WriteLine("Please input a value");

                CheckUserInput("retry");
            }

            if (value?.ToLower() != "y" && value?.ToLower() != "n")
            {
                Console.WriteLine("Invalid value");

                CheckUserInput("retry");
            }

            if (value == "y")
                return repeat = true;

            return repeat;
        }

        public bool CheckLoginCredentials(string value)
        {
            bool IsValid = true;

            if (string.IsNullOrEmpty(value))
                return IsValid = false;

            return IsValid;
        }
    }
}
