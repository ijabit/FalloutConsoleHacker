using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FalloutConsoleHacker.Logic
{
    public static class Chooser
    {
        public static List<PasswordStatistics> GetPasswordStatistics(List<PasswordInfo> allPasswords, List<PasswordGuess> passwordGuesses)
        {
            var passwordStatistics = new List<PasswordStatistics>();

            var possiblePasswords =
                allPasswords.Except(passwordGuesses.Select(g => g.Password)) // Eliminate already guessed passwords
                    .Where(
                        password =>
                            // Eliminate passwords  which don't have the same amount of matching characters as those guessed correct.
                            passwordGuesses.All(g => g.Password.GetMatchingCharacters(password) == g.NumberOfCharactersCorrect)
                            // Eliminate passwords that have characters in common with guessed passwords that don't have any correct characters.
                            & !passwordGuesses.Any(
                                g =>
                                    g.NumberOfCharactersCorrect < 1 &&
                                    password.Characters.Any(pc => g.Password.Characters.Contains(pc)))
                    ).ToList();
            if (possiblePasswords.Count < 1)
                return passwordStatistics; // throw new Exception("All passwords have already been guessed!");

            foreach (var password in possiblePasswords)
            {
                // Calculate how many other passwords this password has characters in common with.
                var otherPasses = possiblePasswords.Where(p => !p.Equals(password)).ToList();
                int passwordsWithCharactersMatching =
                    otherPasses.Count(
                        otherPass =>
                            otherPass.GetMatchingCharacters(password) > 0
                        );
                int characterMatchesInOtherPasswords =
                    otherPasses.Sum(
                        otherPass =>
                            otherPass.GetMatchingCharacters(password)
                        );

                passwordStatistics.Add(new PasswordStatistics()
                {
                    Password = password,
                    NumberOfCharactersInOtherPasswords = characterMatchesInOtherPasswords,
                    NumberOfOtherPasswordsWithSameCharacters = passwordsWithCharactersMatching
                });
            }

            return passwordStatistics;
        }
    }
}
