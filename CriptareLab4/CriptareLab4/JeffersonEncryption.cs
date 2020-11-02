using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace CriptareLab4
{
    public class JeffersonEncryption : EncryptionBase
    {
        private readonly Random rnd = new Random();
        public static JeffersonEncryption I = new JeffersonEncryption();
        public char[,] encryptionMatrix;
        private bool decrypt = false;

        private JeffersonEncryption()
        {
            CreateEncryptionMatrix();
        }

        public void CreateEncryptionMatrix(params int[] key)
        {
            if (key.Length == 0)
                key = Enumerable.Range(1, 36).OrderBy(c => rnd.Next()).ToArray();
            else
            {
                List<int> validKeyValues = Enumerable.Range(1, 36).Where(nr => key.FirstOrDefault(nrk => nrk == nr) != 0).ToList();
                validKeyValues.AddRange(Enumerable.Range(1, 36).Where(nr => validKeyValues.FirstOrDefault(nrk => nrk == nr) == 0));
                key = validKeyValues.ToArray();
            }

            char[] ShuffledAlphabet() => "abcdefghijklmnopqrstuvwxyz".OrderBy(c => rnd.Next()).ToArray();
            encryptionMatrix = new char[36, 26];
            for (int i = 0; i < 36; i++)
            {
                char[] alphabet = ShuffledAlphabet();
                for (int j = 0; j < 26; j++)
                    encryptionMatrix[key[i] - 1, j] = alphabet[j];
            }


        }

        public override string Encrypt(string toEncryptText)
        {
            string encryptedText = "";

            for (int i = 0; i < toEncryptText.Length; i++)
            {
                if (char.IsLetter(toEncryptText[i]))
                {
                    for (int j = 0; j < 26; j++)
                    {
                        if (encryptionMatrix[i % 36, j] == char.ToLower(toEncryptText[i]))
                        {
                            char encryptedLetter = encryptionMatrix[i % 36, (j + (decrypt ? 20 : 6)) % 26];
                            encryptedText += char.IsLower(toEncryptText[i]) ? encryptedLetter : char.ToUpper(encryptedLetter);
                            break;
                        }
                    }
                }
                else
                    encryptedText += toEncryptText[i];
            }

            return encryptedText;
        }

        public override string Decrypt(string toDecryptText)
        {
            decrypt = true;
            string decrptedText = Encrypt(toDecryptText);
            decrypt = false;
            return decrptedText;
        }

        public void Break(string toBreakText)
        {
            string mockText = "defaulttextfortestingthathasthirtysi";
            string encryptedMockText = Encrypt(mockText);
            int[] frequency = new int[mockText.Length];
            int[,] table = new int[36, mockText.Length];
            for (int i = 0; i < 36; i++)
            {
                for (int j = 0; j < mockText.Length; j++)
                {
                    int letter1Pos = 0;
                    int letter2Pos = 0;
                    for (int c = 0; c < 26; c++)
                    {
                        if (mockText[j] == encryptionMatrix[i, c])
                            letter1Pos = c;

                        if (encryptedMockText[j] == encryptionMatrix[i, c])
                            letter2Pos = c;

                        if (letter1Pos != 0 && letter2Pos != 0)
                            break;
                    }
                    int gap = letter1Pos < letter2Pos ? letter2Pos - letter1Pos : 26 - letter1Pos + letter2Pos;
                    table[i, j] = gap;
                    frequency[gap]++;
                }
            }

            int mostFrequent = 0;
            for (int i = 0; i < frequency.Length; i++)
                if (frequency[i] == frequency.Max())
                {
                    mostFrequent = i;
                    break;
                }

            int[] rearangedTable = new int[36];
            for (int i = 0; i < 36; i++)
            {
                for (int j = 0; j < mockText.Length; j++)
                {
                    if (table[i, j] != mostFrequent)
                        table[i, j] = 0;

                    if (table[i, j] != 0)
                        rearangedTable[j] = i;
                }
            }

            string brokeText = "";
            for (int i = 0; i < toBreakText.Length; i++)
            {
                if (char.IsLetter(toBreakText[i]))
                {
                    for (int j = 0; j < 26; j++)
                    {
                        if (encryptionMatrix[i % 36, j] == char.ToLower(toBreakText[i]))
                        {
                            char encryptedLetter = encryptionMatrix[rearangedTable[i] % 36, (j + (decrypt ? 20 : 6)) % 26];
                            brokeText += char.IsLower(toBreakText[i]) ? encryptedLetter : char.ToUpper(encryptedLetter);
                            break;
                        }
                    }
                }
                else
                    brokeText += toBreakText[i];
            }
            Console.WriteLine("Jefferson break attempt:\nFrom -> " + toBreakText + "\nTo -> " + brokeText);
        }
    }
}
