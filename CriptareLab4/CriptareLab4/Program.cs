using System;

namespace CriptareLab4
{
    class Program
    {
        static void Main(string[] args)
        {
            string text = "Hide the gold in the tree stump!";
            Console.WriteLine("Starting text:        " + text);
            Console.WriteLine();

            Console.WriteLine("Vingenere encryption: " + VingenereEncryption.I.Encrypt(text));
            Console.WriteLine("Vingenere decryption: " + VingenereEncryption.I.Decrypt(VingenereEncryption.I.Encrypt(text)));
            Console.WriteLine();

            Console.WriteLine("Playfair encryption:  " + PlayfairEncryption.I.Encrypt(text));
            Console.WriteLine("Playfair decryption:  " + PlayfairEncryption.I.Decrypt(PlayfairEncryption.I.Encrypt(text)));
            Console.WriteLine();

            Console.WriteLine("Jefferson encryption: " + JeffersonEncryption.I.Encrypt(text));
            Console.WriteLine("Jefferson decryption: " + JeffersonEncryption.I.Decrypt(JeffersonEncryption.I.Encrypt(text)));
            JeffersonEncryption.I.Break(JeffersonEncryption.I.Encrypt(text));
        }
    }
}
