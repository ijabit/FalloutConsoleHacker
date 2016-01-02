using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FalloutConsoleHacker.Logic
{
    public class PasswordInfo
    {
        public string OriginalPassword { get; }
        public IndexedPasswordChar[] Characters { get; set; }
        public PasswordInfo(string password)
        {
            this.OriginalPassword = password;
            this.Characters = new IndexedPasswordChar[password.Length];

            for (int i = 0; i < password.Length; i++)
            {
                Characters[i] = new IndexedPasswordChar(password[i], i);
            }
        }

        public int GetMatchingCharacters(PasswordInfo password)
        {
            return this.Characters.Count(c => password.Characters.Contains(c));
        }

        public override string ToString()
        {
            return OriginalPassword;
        }

        public override bool Equals(object obj)
        {
            if (obj is PasswordInfo)
            {
                return this.OriginalPassword.Equals(((PasswordInfo)obj).OriginalPassword);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return OriginalPassword.GetHashCode();
        }

        public class IndexedPasswordChar
        {
            public int Index { get; }
            public char Character { get; }

            public IndexedPasswordChar(char character, int index)
            {
                this.Character = character;
                this.Index = index;
            }

            public override string ToString()
            {
                return string.Format("{0} at index {1}", Character, Index);
            }

            public override int GetHashCode()
            {
                return this.Character.GetHashCode();
            }

            public override bool Equals(object obj)
            {
                if (!(obj is IndexedPasswordChar))
                    return false;

                IndexedPasswordChar otherObj = (IndexedPasswordChar)obj;
                if (otherObj.Index == this.Index && otherObj.Character == this.Character)
                    return true;
                else
                    return false;
            }
        }
    }
}
