using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Text;
using System.Xml.Serialization;
using System.IO;

namespace Zephry
{
    /// <summary>
    ///   XmlUtils class.
    /// </summary>
    /// <remarks>
    ///   namespace Zephry.
    /// </remarks>
    public class XmlUtils
    {
        #region Serialize
        /// <summary>
        ///   Serializes the specified a object.
        /// </summary>
        /// <remarks>
        ///   Serialize an object to string
        /// </remarks>
        /// <typeparam name="T"></typeparam>
        /// <param name="aObject">A object.</param>
        /// <param name="aOmitXmlDeclaration">if set to <c>true</c> [a omit XML declaration].</param>
        /// <returns></returns>
        public static string Serialize<T>(object aObject, bool aOmitXmlDeclaration)
        {
            XmlWriterSettings vXmlWriterSettings = null;
            XmlSerializerNamespaces vXmlSerializerNamespaces = null;
            MemoryStream vMemoryStream = null;
            XmlWriter vXmlWriter = null;
            XmlSerializer vXmlSerializer = null;
            try
            {
                // Initialize XML writer settings
                vXmlWriterSettings = new XmlWriterSettings { Indent = false, OmitXmlDeclaration = aOmitXmlDeclaration, Encoding = Encoding.UTF8, NewLineChars = Environment.NewLine };
                // Force empty namespace
                vXmlSerializerNamespaces = new XmlSerializerNamespaces();
                vXmlSerializerNamespaces.Add("", "");
                // Serialize with a writer
                vMemoryStream = new MemoryStream();
                vXmlWriter = XmlWriter.Create(vMemoryStream, vXmlWriterSettings);
                vXmlSerializer = new XmlSerializer(typeof(T));
                vXmlSerializer.Serialize(vXmlWriter, aObject, vXmlSerializerNamespaces);
                vXmlWriter.Flush();
                // Return XML with settings as string
                return vXmlWriterSettings.Encoding.GetString(vMemoryStream.ToArray());
            }
            finally
            {
                vMemoryStream.Close();
                vMemoryStream.Dispose();
            }
        }
        #endregion

        #region Deserialize
        /// <summary>
        ///   Deserializes the specified XML string.
        /// </summary>
        /// <remarks>
        ///   Deserialize a string of XML and return the object.
        /// </remarks>
        /// <typeparam name="T"></typeparam>
        /// <param name="aXml">A XML.</param>
        /// <returns></returns>
        public static T Deserialize<T>(string aXml)
        {
            var vXmlSerializer = new XmlSerializer(typeof(T));
            using (var vMemoryStream = new MemoryStream(Encoding.UTF8.GetBytes(aXml)))
            {
                return (T)vXmlSerializer.Deserialize(vMemoryStream);
            }
        }
        #endregion

        #region Concatenate
        /// <summary>
        ///   Concatenates the specified status document with a specified object document.
        /// </summary>
        /// <remarks>
        ///   Slap two documents together - really, really ugly.
        /// </remarks>
        /// <param name="aStatusDoc">A status doc.</param>
        /// <param name="aObjectDoc">A object doc.</param>
        /// <param name="aRootAttribute">A root attribute.</param>
        /// <returns></returns>
        public static string Concatenate(string aStatusDoc, string aObjectDoc, string aRootAttribute)
        {
            var vStringBuilder = new StringBuilder();
            // include the XML declaration
            vStringBuilder.AppendLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            vStringBuilder.AppendLine(String.Format("<{0}>", aRootAttribute));
            if (!(String.IsNullOrWhiteSpace(aStatusDoc)))
            {
                vStringBuilder.AppendLine(aStatusDoc);
            }
            if (!(String.IsNullOrWhiteSpace(aObjectDoc)))
            {
                vStringBuilder.AppendLine(aObjectDoc);
            }
            vStringBuilder.AppendLine(String.Format("</{0}>", aRootAttribute));
            return vStringBuilder.ToString();
        }
        #endregion
    }
}
