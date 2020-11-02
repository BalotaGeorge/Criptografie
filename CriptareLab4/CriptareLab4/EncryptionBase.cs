using System;
using System.Collections.Generic;
using System.Text;

namespace CriptareLab4
{
    public abstract class EncryptionBase
    {
        public abstract string Encrypt(string toEncryptText);

        public abstract string Decrypt(string toDecryptText);
    }
}
