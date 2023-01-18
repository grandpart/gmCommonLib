using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using Microsoft.Win32;

namespace Zephry
{
    /// <summary>
    ///   FileUtils static class.
    /// </summary>
    /// <remarks>
    ///   namespace Zephry.
    /// </remarks>
    public static class FileUtils
    {
        #region Serialize

        /// <summary>
        ///   Serializes the specified file.
        /// </summary>
        /// <param name="aPath">A file path.</param>
        /// <param name="aObject">An object.</param>
        public static void Serialize(string aPath, object aObject)
        {
            if (aObject == null)
            {
                throw new ArgumentNullException("SerializeObject");
            }

            // Force create directory (ignores existing directory)
            if (!(string.IsNullOrWhiteSpace(Path.GetDirectoryName(aPath))))
            {
                System.IO.Directory.CreateDirectory(Path.GetDirectoryName(aPath));
            }

            Stream vStream = null;
            try
            {
                vStream = File.Open(aPath, FileMode.Create);
                var vFormatter = new BinaryFormatter();
                vFormatter.Serialize(vStream, aObject);
            }
            finally
            {
                if (vStream != null)
                    vStream.Close();
            }
        }

        #endregion

        #region Deserialize

        /// <summary>
        ///   Deserializes the specified file.
        /// </summary>
        /// <typeparam name="T">Generic Type.</typeparam>
        /// <param name="aPath">A file path.</param>
        /// <returns></returns>
        public static T Deserialize<T>(string aPath)
        {
            Stream vStream = null;
            {
                T vObject;
                try
                {
                    vStream = File.Open(aPath, FileMode.Open);
                    var vBinaryFormatter = new BinaryFormatter();
                    vObject = (T) vBinaryFormatter.Deserialize(vStream);
                }
                finally
                {
                    if (vStream != null)
                        vStream.Close();
                }
                return vObject;
            }
        }

        #endregion

        #region GetFileType

        /// <summary>
        ///   Gets the type of the file.
        /// </summary>
        /// <param name="aPath">A file path.</param>
        /// <returns>The type of the file as a <see cref="String"/>.</returns>
        public static string GetFileType(string aPath)
        {
            if (string.IsNullOrEmpty(Path.GetFileName(aPath)))
                return "Folder";

            string vExtension = Path.GetExtension(aPath);

            var vRegistryKey = Registry.ClassesRoot.OpenSubKey(vExtension);

            while (vRegistryKey != null)
            {
                string vName = (string)vRegistryKey.GetValue(null);

                if (vName == null)
                    return vExtension + " File";

                var vAssociatedKey = Registry.ClassesRoot.OpenSubKey(vName);

                if (vAssociatedKey == null)
                    return vName;

                vRegistryKey = vAssociatedKey;
            }

            return vExtension + " File";
        }

        #endregion

        #region GetFileSize

        /// <summary>
        ///   Gets the size of the file.
        /// </summary>
        /// <param name="aByteArray">A byte array.</param>
        /// <returns>The size of a file as a <see cref="String"/>.</returns>
        public static string GetFileSize(byte[] aByteArray)
        {
            int vByteCount = aByteArray.Length;
            string vSize = "0 Bytes";

            if (vByteCount >= 1073741824.0)
            {
                vSize = String.Format("{0:##.##} GB", vByteCount / 1073741824.0);
            }
            else if (vByteCount >= 1048576.0)
            {
                vSize = String.Format("{0:##.##} MB", vByteCount / 1048576.0);
            }
            else if (vByteCount >= 1024.0)
            {
                vSize = String.Format("{0:##.##} KB", vByteCount / 1024.0);
            }
            else if (vByteCount > 0 && vByteCount < 1024.0)
            {
                vSize = vByteCount + " Bytes";
            }

            return vSize;
        }

        #endregion
    }
}
