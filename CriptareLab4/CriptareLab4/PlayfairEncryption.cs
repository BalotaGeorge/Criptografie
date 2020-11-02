using System;
using System.Collections.Generic;
using System.Text;

namespace CriptareLab4
{
    class PlayfairEncryption : EncryptionBase
    {
        public static PlayfairEncryption I = new PlayfairEncryption();
        public char[,] encryptionMatrix;
        private bool decrypt = false;

        private PlayfairEncryption()
        {
            CreateEncryptionMatrix();
        }

        public void CreateEncryptionMatrix(string key = "playfair example")
        {
            encryptionMatrix = new char[5, 5];
            string normalizedKey = "";
            foreach (char letter in key)
                if (char.IsLetter(letter) && !normalizedKey.Contains(char.ToLower(letter)))
                    normalizedKey += char.ToLower(letter);

            foreach (char letter in "abcdefghijklmnopqrstuvwxyz")
                if (!normalizedKey.Contains(letter) && !((letter == 'i' || letter == 'j') && (normalizedKey.Contains('i') || normalizedKey.Contains('j'))))
                    normalizedKey += letter;

            for (int i = 0; i < normalizedKey.Length; i++)
                encryptionMatrix[i % 5, i / 5] = normalizedKey[i];
        }
        public override string Encrypt(string toEncryptText)
        {
            string encryptedText = "";
            char? letter1 = null;
            char? letter2 = null;
            foreach (char letter in toEncryptText)
            {
                if (!char.IsLetter(letter))
                {
                    encryptedText += letter;
                    continue;
                }

                if (letter1 == null)
                    letter1 = letter;
                else
                {
                    if (letter2 == null)
                    {
                        letter2 = letter == letter1 ? 'x' : letter;
                        (int row, int col) letter1Pos = default;
                        (int row, int col) letter2Pos = default;

                        for (int j = 0; j < 5; j++)
                            for (int i = 0; i < 5; i++)
                            {
                                if (encryptionMatrix[i, j] == char.ToLower((char)letter1))
                                    letter1Pos = (i, j);
                                if (encryptionMatrix[i, j] == char.ToLower((char)letter2))
                                    letter2Pos = (i, j);
                            }

                        char encryptedLetter1 = default;
                        char encryptedLetter2 = default;

                        if (letter1Pos.row != letter2Pos.row && letter1Pos.col != letter2Pos.col)
                        {
                            encryptedLetter1 = encryptionMatrix[letter2Pos.row, letter1Pos.col];
                            encryptedLetter2 = encryptionMatrix[letter1Pos.row, letter2Pos.col];
                        }
                        if (letter1Pos.col == letter2Pos.col)
                        {
                            encryptedLetter1 = encryptionMatrix[(letter1Pos.row + (decrypt ? 4 : 1)) % 5, letter1Pos.col];
                            encryptedLetter2 = encryptionMatrix[(letter2Pos.row + (decrypt ? 4 : 1)) % 5, letter2Pos.col];
                        }
                        if (letter1Pos.row == letter2Pos.row)
                        {
                            encryptedLetter1 = encryptionMatrix[letter1Pos.row, (letter1Pos.col + (decrypt ? 4 : 1)) % 5];
                            encryptedLetter2 = encryptionMatrix[letter2Pos.row, (letter2Pos.col + (decrypt ? 4 : 1)) % 5];
                        }

                        encryptedText += char.IsLower((char)letter1) ? encryptedLetter1 : char.ToUpper(encryptedLetter1);
                        encryptedText += char.IsLower((char)letter2) ? encryptedLetter2 : char.ToUpper(encryptedLetter2);

                        if (letter != letter1)
                            letter1 = null;
                        letter2 = null;
                    }
                }
            }

            return encryptedText;
        }

        public override string Decrypt(string toDecryptText)
        {
            decrypt = true;
            string decryptedCode = Encrypt(toDecryptText);
            decrypt = false;
            return decryptedCode;
        }
    }
}
