using System;
using System.IO;

namespace CriptareLab1
{
    class Program
    {
        static void Main(string[] args)
        {
            string code = "Hello World!";
            Console.WriteLine("Starting code: " + code);
            Console.WriteLine();

            Console.WriteLine("Caesar encryption: " + Caesar.Instance.Encrypt(code));
            Console.WriteLine("Caesar decryption: " + Caesar.Instance.Decrypt(Caesar.Instance.Encrypt(code)));
            Console.WriteLine();

            Console.WriteLine("Rot13 encryption: " + Rot13.Instance.Encrypt(code));
            Console.WriteLine("Rot13 decryption: " + Rot13.Instance.Decrypt(Rot13.Instance.Encrypt(code)));
            Console.WriteLine();

            Console.WriteLine("Substitution encryption: " + Substitution.Instance.Encrypt(code));
            Console.WriteLine("Substitution decryption: " + Substitution.Instance.Decrypt(Substitution.Instance.Encrypt(code)));
            Console.WriteLine();

            Console.WriteLine("Caesar break:");
            Caesar.Instance.Break(Caesar.Instance.Encrypt(code));
            Console.WriteLine();

            Console.WriteLine("Rot13 break:");
            Rot13.Instance.Break(Rot13.Instance.Encrypt(code));
            Console.WriteLine();

            Console.WriteLine("Substitution break:");
            string longText = File.ReadAllText("../../../LongText.txt");
            Substitution.Instance.Break(Substitution.Instance.Encrypt(longText));

            Console.ReadLine();
        }
    }
}
