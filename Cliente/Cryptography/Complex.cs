using System.Security.Cryptography;

namespace Cliente.Cryptography
{
    public class Complex
    {
        public byte[] Key =
        {
            0x00, 0x11, 0x2b, 0x21,
            0x12, 0x32, 0xf7, 0x1d,
            0x20, 0x51, 0x12, 0xf1,
            0x1a, 0x32, 0xaf, 0xdd,
            0x12, 0x3b, 0x4f, 0x13,
            0x13, 0x72, 0xf1, 0x1d
        };

        public void Encrypt(ref byte[] inputArray)
        {
            var tripleDes = new TripleDESCryptoServiceProvider
            {
                Key = Key,
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };
            ICryptoTransform cTransform = tripleDes.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
            tripleDes.Clear();
            inputArray = resultArray;
        }

        public void Decrypt(ref byte[] inputArray)
        {
            var tripleDes = new TripleDESCryptoServiceProvider
            {
                Key = Key,
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };
            ICryptoTransform cTransform = tripleDes.CreateDecryptor();
            inputArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
            tripleDes.Clear();
        }
    }
}