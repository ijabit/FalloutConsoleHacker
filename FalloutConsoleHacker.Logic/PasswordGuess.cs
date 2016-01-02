using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FalloutConsoleHacker.Logic
{
    public class PasswordGuess
    {
        public PasswordGuess(PasswordInfo password, int numberCharactersCorrect)
        {
            this.Password = password;
            this.NumberOfCharactersCorrect = numberCharactersCorrect;
        }

        public PasswordInfo Password { get; set; }

        public int NumberOfCharactersCorrect { get; set; }
    }
}
