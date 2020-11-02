using System;
using System.Collections.Generic;
using System.Text;

namespace CriptareLab4
{
    public class VingenereEncryption : EncryptionBase
    {
        public static VingenereEncryption I = new VingenereEncryption();
        public char[,] encryptionMatrix;

        private VingenereEncryption() { }

        private void CreateEncryptionMatrix()
        {
            encryptionMatrix = new char[26, 26];
            for (int j = 0; j < 26; j++)
                for (int i = 0; i < 26; i++)
                    encryptionMatrix[i, j] = (char)((i + j) % 26 + 97);
        }

        public override string Encrypt(string toEncryptText)
        {
            CreateEncryptionMatrix();
            string key = "lemonade";
            string encryptedCode = "";
            for (int i = 0; i < toEncryptText.Length; i++)
            {
                if (char.IsLetter(toEncryptText[i]))
                {
                    if (char.IsLower(toEncryptText[i]))
                        encryptedCode += encryptionMatrix[toEncryptText[i] - 97, key[i % key.Length] - 97];
                    else
                        encryptedCode += char.ToUpper(encryptionMatrix[toEncryptText[i] - 65, key[i % key.Length] - 97]);
                }
                else
                    encryptedCode += toEncryptText[i];
            }
            return encryptedCode;
        }

        public override string Decrypt(string toDecryptText)
        {
            CreateEncryptionMatrix();
            string key = "lemonade";
            string decryptedCode = "";
            for (int i = 0; i < toDecryptText.Length; i++)
            {
                if (char.IsLetter(toDecryptText[i]))
                {
                    if (char.IsLower(toDecryptText[i]))
                    {
                        for (int l = 0; l < 26; l++)
                            if (encryptionMatrix[l, key[i % key.Length] - 97] == toDecryptText[i])
                            {
                                decryptedCode += (char)(l + 97);
                                break;
                            }
                    }
                    else
                    {
                        for (int l = 0; l < 26; l++)
                            if (encryptionMatrix[l, key[i % key.Length] - 97] == char.ToLower(toDecryptText[i]))
                            {
                                decryptedCode += (char)(l + 65);
                                break;
                            }
                    }
                }
                else
                    decryptedCode += toDecryptText[i];
            }
            return decryptedCode;
        }
    }
}
