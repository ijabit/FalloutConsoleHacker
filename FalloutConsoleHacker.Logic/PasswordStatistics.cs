using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FalloutConsoleHacker.Logic
{
    public class PasswordStatistics
    {
        public PasswordInfo Password { get; set; }
        public int NumberOfCharactersInOtherPasswords { get; set; }

        public int NumberOfOtherPasswordsWithSameCharacters { get; set; }

        public double CharacterMatchesPerMatchingPassword
        {
            get
            {
                return NumberOfOtherPasswordsWithSameCharacters < 1
                    ? 0
                    : (double)NumberOfCharactersInOtherPasswords / (double) NumberOfOtherPasswordsWithSameCharacters;
            }
        }
    }
}
