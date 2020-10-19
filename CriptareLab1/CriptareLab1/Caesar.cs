using System;
using System.Collections.Generic;
using System.Text;

namespace CriptareLab1
{
    public class Caesar : ISymmetricAlgorithm
    {
        public static Caesar Instance = new Caesar();
        private static List<char> plainText;
        private static List<char> ciphertext;
        private int shiftValue = 3;

        private void CreateCipherText()
        {
            plainText = new List<char>("abcdefghijklmnopqrstuvwxyz");
            ciphertext = new List<char>();
            for (int i = 0; i < 26; i++)
                ciphertext.Add(plainText[(i + shiftValue + 26) % 26]);
        }

        public string Encrypt(string toEncryptCode, int shiftValue)
        {
            this.shiftValue = shiftValue;
            return Encrypt(toEncryptCode);
        }

        public string Encrypt(string toEncryptCode)
        {
            CreateCipherText();
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
            shiftValue *= -1;
            string decryptedCode = Encrypt(toDecryptCode);
            shiftValue *= -1;
            return decryptedCode;
        }

        public void Break(string toBreakCode)
        {
            for (int i = 1; i <= 25; i++)
            {
                shiftValue = i * -1;
                string value = Decrypt(toBreakCode);
                Console.WriteLine(value);
            }
        }
    }
}
