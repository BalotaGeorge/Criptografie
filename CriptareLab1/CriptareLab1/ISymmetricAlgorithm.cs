using System;
using System.Collections.Generic;
using System.Text;

namespace CriptareLab1
{
    public interface ISymmetricAlgorithm
    {
        string Encrypt(string toEncryptCode);
        string Decrypt(string toDecryptCode);
        void Break(string toBreakCode);
    }
}
