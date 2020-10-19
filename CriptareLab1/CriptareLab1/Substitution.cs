using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CriptareLab1
{
    public class Substitution : ISymmetricAlgorithm
    {
        public static Substitution Instance = new Substitution();
        private static List<char> plainText;
        private static List<char> ciphertext;

        private void CreateCipherText(string keyword)
        {
            plainText = new List<char>("abcdefghijklmnopqrstuvwxyz");
            ciphertext = new List<char>(keyword);
            plainText.RemoveAll(c => keyword.Contains(c));
            ciphertext.AddRange(plainText);
            plainText = new List<char>("abcdefghijklmnopqrstuvwxyz");
        }

        public string Encrypt(string toEncryptCode, string keyword)
        {
            CreateCipherText(keyword);
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

        public string Encrypt(string toEncryptCode)
        {
            return Encrypt(toEncryptCode, "default");
        }

        public string Decrypt(string toDecryptCode)
        {
            string decryptedCode = "";
            foreach (char letter in toDecryptCode)
            {
                if (letter >= 'a' && letter <= 'z')
                    decryptedCode += plainText[ciphertext.IndexOf(letter)];
                else
                {
                    if (letter >= 'A' && letter <= 'Z')
                        decryptedCode += (char)(plainText[ciphertext.IndexOf((char)(letter + 32))] - 32);
                    else
                        decryptedCode += letter;
                }
            }
            return decryptedCode;
        }

        public void Break(string toBreakCode)
        {
            List<char> letterFrequencyTexts = new List<char>("etaoinshrdlcumwfgypbvkxjqz");
            List<(char letter, int frequency)> frequencyTable = new List<(char letter, int frequency)>();
            foreach (char letter in toBreakCode)
            {
                char letterToCheck = (letter >= 'A' && letter <= 'Z') ? (char)(letter + 32) : letter;

                if (letterToCheck >= 'a' && letterToCheck <= 'z')
                {
                    if (frequencyTable.Exists(c => c.letter == letterToCheck))
                    {
                        (char letter, int frequency) currentValue = frequencyTable.Find(c => c.letter == letterToCheck);
                        (char letter, int frequency) updatedValue = (letterToCheck, currentValue.frequency + 1);
                        frequencyTable.Remove(currentValue);
                        frequencyTable.Add(updatedValue);
                    }
                    else
                        frequencyTable.Add((letterToCheck, 1));
                }
            }

            frequencyTable = frequencyTable.OrderByDescending(c => c.frequency).ToList();

            string brokeCode = "";
            foreach (char letter in toBreakCode)
            {
                if (letter >= 'a' && letter <= 'z')
                    brokeCode += letterFrequencyTexts[frequencyTable.FindIndex(c => c.letter == letter)];
                else
                {
                    if (letter >= 'A' && letter <= 'Z')
                        brokeCode += (char)(letterFrequencyTexts[frequencyTable.FindIndex(c => c.letter == (char)(letter + 32))] - 32);
                    else
                        brokeCode += letter;
                }
            }

            Console.WriteLine(brokeCode);
            Console.WriteLine("\nLetter frequency:");
            frequencyTable.ForEach(c => Console.WriteLine(c.frequency + " " + c.letter + " -> " + letterFrequencyTexts[frequencyTable.FindIndex(l => l.letter == c.letter)]));
        }
    }
}
