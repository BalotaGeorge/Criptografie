using System;
using System.Collections.Generic;
using System.Text;

namespace CriptareLab1
{
    public class Rot13 : ISymmetricAlgorithm
    {
        public static Rot13 Instance = new Rot13();
        private static List<char> plainText = new List<char>("abcdefghijklmnopqrstuvwxyz");
        private static List<char> ciphertext = new List<char>("nopqrstuvwxyzabcdefghijklm");

        public string Encrypt(string toEncryptCode)
        {
            string encryptedCode = "";
            foreach (char letter in toEncryptCode)
            {
                if (letter >= 'a' && letter <= 'z')
                    encryptedCode += ciphertext[plainText.IndexOf(letter)];
                else
                {
                    if (letter >= 'A' && letter <= 'Z')
                        encryptedCode += (char)(ciphertext[plainText.IndexOf((char)(letter + 32))] - 32);
                    else
                        encryptedCode += letter;
                }
            }
            return encryptedCode;
        }

        public string Decrypt(string toDecryptCode)
        {
            return Encrypt(toDecryptCode);
        }

        public void Break(string toBreakCode)
        {
            Console.WriteLine(Decrypt(toBreakCode) + " <-");
        }
    }
}
