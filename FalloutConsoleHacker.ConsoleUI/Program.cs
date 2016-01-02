using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FalloutConsoleHacker.Logic;

namespace FalloutConsoleHacker.ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = String.Empty;
            do
            {
                Console.Clear();
                HackPasswords();
                Console.WriteLine("Hit [Enter] to close or type 'reset'.");
                Console.ReadLine();
            } while (input != "reset");
            
        }

        public static void HackPasswords()
        {
            var passwordGuesses = new List<PasswordGuess>();
            var passwordStatistics = new List<PasswordStatistics>();

            string input = string.Empty;
            while (string.IsNullOrEmpty(input) || input.Trim() == string.Empty)
            {
                Console.WriteLine("Enter comma separated or space separated list of passwords:");
                input = Console.ReadLine() ?? string.Empty;
            }

            var passwords =
                input.Trim()
                    .Split(", ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
                    .Select(p => p.Trim())
                    .Select(p => new PasswordInfo(p))
                    .ToList();

            do
            {
                passwordStatistics = Chooser.GetPasswordStatistics(passwords, passwordGuesses);
                int totalPasswords = passwordStatistics.Count;

                PasswordInfo passwordToTry = null;
                if (totalPasswords == 1)
                {
                    Console.WriteLine("There can be only one!");
                    passwordToTry = passwordStatistics.First().Password;
                }
                else
                {
                    passwordToTry = passwordStatistics.Where(
                    ps =>
                        ps.CharacterMatchesPerMatchingPassword == passwordStatistics.Max(s => s.CharacterMatchesPerMatchingPassword)
                    ).Select(p => p.Password).FirstOrDefault();

                    Console.Write(
                    string.Join(Environment.NewLine,
                        passwordStatistics.OrderByDescending(s => s.CharacterMatchesPerMatchingPassword).Select(
                            ps =>
                                string.Format("'{0}' has an average of {1:N} characters in each other matched password.",
                                    ps.Password.OriginalPassword, ps.CharacterMatchesPerMatchingPassword))) + Environment.NewLine);
                    Console.WriteLine(Environment.NewLine);
                }


                if (passwordToTry == null)
                {
                    Console.WriteLine("Something went wrong. Please check your input and try again.");
                    break;
                }

                Console.WriteLine("Try this password: {0}", passwordToTry);
                int numCharsCorrect = GetPositiveNumber("How many characters were correct? (Type '100' if it worked)");
                if (numCharsCorrect >= passwords.Max(p => p.Characters.Length))
                {
                    Console.WriteLine("Congrats!");
                    break;
                }

                passwordGuesses.Add(new PasswordGuess(passwordToTry, numCharsCorrect));
            } while (true);
        }

        private static int GetPositiveNumber(string prompt)
        {
            int number = -1;
            string input = string.Empty;
            while (number < 0)
            {
                Console.WriteLine(prompt);
                input = Console.ReadLine();
                if (int.TryParse(input, out number) == false)
                    number = -1;
            }

            return number;
        }
    }
}
