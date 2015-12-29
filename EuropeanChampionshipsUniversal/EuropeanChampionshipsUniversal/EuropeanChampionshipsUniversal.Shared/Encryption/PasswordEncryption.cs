using System;
using System.Text;
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.Core;
using Windows.Storage.Streams;

namespace EuropeanChampionshipsUniversal.Encryption
{
    public class PasswordEncryption
    {
        public static string cryptPwd(string pwd)
        {
            try
            {
                CryptographicKey key = GenerateKey();
                IBuffer buffer = CryptographicBuffer.CreateFromByteArray(Encoding.UTF8.GetBytes(pwd));
                return CryptographicBuffer.EncodeToBase64String(CryptographicEngine.Encrypt(key, buffer, null));
            }
            catch
            {

                return null;
            }
        }

        private static CryptographicKey GenerateKey()
        {
            HashAlgorithmProvider algorithm = HashAlgorithmProvider.OpenAlgorithm(HashAlgorithmNames.Sha512);
            CryptographicHash cryptographicHash = algorithm.CreateHash();
            SymmetricKeyAlgorithmProvider provider = SymmetricKeyAlgorithmProvider.OpenAlgorithm(SymmetricAlgorithmNames.AesEcbPkcs7);

            byte[] hash = new byte[32];
            cryptographicHash.Append(CryptographicBuffer.CreateFromByteArray(Encoding.UTF8.GetBytes("Pa$$w0rd")));

            byte[] temp;
            CryptographicBuffer.CopyToByteArray(cryptographicHash.GetValueAndReset(), out temp);

            Array.Copy(temp, 0, hash, 0, 16);
            Array.Copy(temp, 0, hash, 15, 16);

            CryptographicKey key = provider.CreateSymmetricKey(CryptographicBuffer.CreateFromByteArray(hash));

            return key;
        }
    }
}
