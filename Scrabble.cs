using System;
using System.IO;
using System.Security.Cryptography;

namespace Zephry
{
    /// <summary>
    ///   Scrabble class.
    /// </summary>
    /// <remarks>
    ///   namespace Zephry.
    /// </remarks>
    public static class Scrabble
    {
        #region Mixit
        /// <summary>
        ///   Scrabbles the specified a string.
        /// </summary>
        /// <param name="aString">A string.</param>
        /// <param name="aKey">A key.</param>
        /// <param name="aIv">A IV.</param>
        /// <returns></returns>
        public static byte[] Mixit(string aString, byte[] aKey, byte[] aIv)
        {
            if (aString == null || aString.Length <= 0)
                throw new ArgumentNullException(nameof(aString));
            if (aKey == null || aKey.Length <= 0)
                throw new ArgumentNullException(nameof(aKey));
            if (aIv == null || aIv.Length <= 0)
                throw new ArgumentNullException(nameof(aIv));

            byte[] mixed;

            using (var vRijndael = Rijndael.Create())
            {
                vRijndael.Key = aKey;
                vRijndael.IV = aIv;

                using (var vICryptoTransform = vRijndael.CreateEncryptor(vRijndael.Key, vRijndael.IV))
                {
                    using (var vMemoryStream = new MemoryStream())
                    {
                        using (var vCryptoStream = new CryptoStream(vMemoryStream, vICryptoTransform, CryptoStreamMode.Write))
                        {
                            using (var vStreamWriter = new StreamWriter(vCryptoStream))
                            {
                                vStreamWriter.Write(aString);
                            }
                            mixed = vMemoryStream.ToArray();
                        }
                    }
                }
            }
            return mixed;
        }
        #endregion

        #region Fixit
        /// <summary>
        ///   Unscrabbles the specified a byte array.
        /// </summary>
        /// <param name="aByteArray">A byte array.</param>
        /// <param name="aKey">A key.</param>
        /// <param name="aIv">A IV.</param>
        /// <returns></returns>
        public static string Fixit(byte[] aByteArray, byte[] aKey, byte[] aIv)
        {
            if (aByteArray == null || aByteArray.Length <= 0)
                throw new ArgumentNullException(nameof(aByteArray));
            if (aKey == null || aKey.Length <= 0)
                throw new ArgumentNullException(nameof(aKey));
            if (aIv == null || aIv.Length <= 0)
                throw new ArgumentNullException(nameof(aIv));

            string _fixed = null;

            using (var vRijndael = Rijndael.Create())
            {
                vRijndael.Key = aKey;
                vRijndael.IV = aIv;

                using (var vICryptoTransform = vRijndael.CreateDecryptor(vRijndael.Key, vRijndael.IV))
                {
                    using (var vMemoryStream = new MemoryStream(aByteArray))
                    {
                        using (var vCryptoStream = new CryptoStream(vMemoryStream, vICryptoTransform, CryptoStreamMode.Read))
                        {
                            using (var vStreamReader = new StreamReader(vCryptoStream))
                            {
                                _fixed = vStreamReader.ReadToEnd();
                            }
                        }
                    }
                }
            }
            return _fixed;
        }
        #endregion
    }
}
