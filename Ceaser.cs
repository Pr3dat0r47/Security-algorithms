using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public class Ceaser : ICryptographicTechnique<string, int>
    {
        public string Encrypt(string plainText, int key)
        {
            string ciphertext = "";

            for (int i = 0; i < plainText.Length; i++)
            {
                if (char.IsLower(plainText[i]))
                {
                    ciphertext += (char)(((int)plainText[i] + key - 97) % 26 + 97);
                }
                else
                {
                    ciphertext += (char)(((int)plainText[i] + key - 65) % 26 + 65);
                }
            }
            return ciphertext;
        }

        public string Decrypt(string cipherText, int key)
        {
            string plaintext = Encrypt(cipherText, 26 - key);
            return plaintext;
        }

        public int LetterToNum(char letter)
        {
            string letters = "abcdefghijklmnopqrstuvwxyz";
            for (int i = 0; i < 26; i++)
            {
                if (letter == letters[i])
                {
                    return i;
                }
            }

            return -1;
        }
        public int Analyse(string plainText, string cipherText)
        {

            int Plainkey = LetterToNum(plainText[0]);
            int cipherkey = LetterToNum(char.ToLower(cipherText[0]));
            if (plainText.Length != cipherText.Length)
            {
                return -1;
            }
            if (cipherkey - Plainkey < 0)
            {
                return (cipherkey - Plainkey) + 26;
            }
            else
            {
                return (cipherkey - Plainkey);
            }
        }
    }
}
