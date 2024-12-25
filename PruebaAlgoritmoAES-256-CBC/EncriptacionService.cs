using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Paddings;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaAlgoritmoAES_256_CBC
{
    public class EncriptacionService
    {
        string secretKey = "my-secret-key-long-1234567890111";
        public EncriptacionService()
        {
            
        }

        public string EncryptText(string plainText)
        {
            byte[] encryptData = EncryptAES256(secretKey, plainText);
            
            string encryptDataBase64 = Convert.ToBase64String(encryptData);


            return encryptDataBase64;
        }

        private static byte[] EncryptAES256(string secretKey, string plainText)
        {
            byte[] textBytes = Encoding.UTF8.GetBytes(plainText);
            byte[] keyBytes = Encoding.UTF8.GetBytes(secretKey);

            byte[] iv = new byte[16];

            KeyParameter keyParameter  = new KeyParameter(keyBytes);
            ParametersWithIV keyWithIv = new ParametersWithIV(keyParameter, iv);
            IBlockCipher aesEngine = new AesEngine();
            IBlockCipherMode cipherMode = new CbcBlockCipher(aesEngine);
            PaddedBufferedBlockCipher cipher = new PaddedBufferedBlockCipher(cipherMode);

            cipher.Init(true, keyWithIv);
            byte[] outputSize = new byte[cipher.GetOutputSize(textBytes.Length)];
            int lenght = cipher.ProcessBytes(textBytes, 0, textBytes.Length, outputSize, 0);
            lenght += cipher.DoFinal(outputSize, lenght);

            return outputSize.Take(lenght).ToArray();
        }
        
        public string DesEncryptText(string cipherText)
        {
            byte[] encryptData = Convert.FromBase64String(cipherText);
            string aux = DesDecryptTextAES256(secretKey, encryptData);
            return aux;
        }

        private static string DesDecryptTextAES256(string secretKey, byte[] encryptData)
        {
            byte[] keyBytes = Encoding.UTF8.GetBytes(secretKey);
            byte[] iv = new byte[16];

            KeyParameter keyParameter = new KeyParameter(keyBytes);
            ParametersWithIV keyParameterWithIV = new ParametersWithIV(keyParameter, iv);

            IBlockCipher aesEngine = new AesEngine();
            IBlockCipherMode blockCipherMode = new CbcBlockCipher(aesEngine);
            PaddedBufferedBlockCipher cipher = new PaddedBufferedBlockCipher(blockCipherMode);

            cipher.Init(false, keyParameterWithIV);
            byte[] outputBytes = new byte[cipher.GetOutputSize(encryptData.Length)];

            int lenght = cipher.ProcessBytes(encryptData, 0, encryptData.Length, outputBytes, 0);
            lenght += cipher.DoFinal(outputBytes, lenght);

            return Encoding.UTF8.GetString(outputBytes,0, lenght);
        }

    }
}
