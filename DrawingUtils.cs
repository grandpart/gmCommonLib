using System;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace Zephry
{
    /// <summary>
    ///   DrawingUtils static class.
    /// </summary>
    /// <remarks>
    ///   namespace Zephry.
    /// </remarks>
    public static class DrawingUtils
    {

        private const uint ShgfiIcon = 0x100;
        private const uint ShgfiLargeicon = 0x0;
        private const uint ShgfiSmallicon = 0x1;
        private const uint ShgfiUsefileattributes = 0x10;
        private const uint FileAttributeNormal = 0x80;

        [StructLayout(LayoutKind.Sequential)]
        public struct Shfileinfo
        {
            public IntPtr hIcon;
            public IntPtr iIcon;
            public uint dwAttributes;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string szDisplayName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
            public string szTypeName;
        };

        [DllImport("shell32.dll")]
        private static extern IntPtr SHGetFileInfo(string pszPath,
        uint dwFileAttributes,
        ref Shfileinfo psfi,
        uint cbSizeFileInfo,
        uint uFlags);

        #region Bitmap FilledBitmap
        /// <summary>
        /// Return a Bitmap of the specified width and height, with a border of the specified thickness and color,
        /// and filled with the specified color
        /// </summary>
        /// <param name="aWidth">The width of the bitmap</param>
        /// <param name="aHeight">The height of the bitmap</param>
        /// <param name="aBorderWidth">The thickness of the border</param>
        /// <param name="aBorderColor">The border color</param>
        /// <param name="aBackgroundColor">The fill color</param>
        /// <returns></returns>
        public static Bitmap FilledBitmap(int aWidth, int aHeight, int aBorderWidth, Color aBorderColor, Color aBackgroundColor)
        {
            var vBitmap = new Bitmap(aWidth, aHeight);
            for (int x = 0; x < vBitmap.Width; x++)
                for (int y = 0; y < vBitmap.Height; y++)
                    vBitmap.SetPixel(x, y, aBorderColor);
            for (int x = aBorderWidth; x < vBitmap.Width - aBorderWidth; x++)
                for (int y = aBorderWidth; y < vBitmap.Height - aBorderWidth; y++)
                    vBitmap.SetPixel(x, y, aBackgroundColor);
            return vBitmap;
        }
        #endregion

        #region Image LoadFromByteArray
        /// <summary>
        /// Return an Image from an array of bytes. If the array is empty, returns an alternate image.
        /// </summary>
        /// <param name="aByteArray">The byte array containing the serialized image</param>
        /// <param name="aAlternateImage">A alternate image that is returned if the Byte array is empty</param>
        /// <returns>
        /// The Image reference
        /// </returns>
        public static Image LoadFromByteArray(byte[] aByteArray, Image aAlternateImage)
        {
            if (aByteArray == null || aByteArray.Length < 1)
            {
                return aAlternateImage;
            }

            using (var vMemoryStream = new MemoryStream(aByteArray))
            {
                return (Image.FromStream(vMemoryStream));
            }
        }
        #endregion

        #region Image Resize
        /// <summary>
        /// Return a resized image with an argument size and aspect ratio
        /// </summary>
        /// <param name="aImageBytes"></param>
        /// <param name="aHeight"></param>
        /// <param name="aWidth"></param>
        /// <param name="aKeepAspectRatio"></param>
        /// <returns></returns>
        public static byte[] Resize(byte[] aImageBytes, int aHeight, int aWidth, bool aKeepAspectRatio = true)
        {
            using (var newImage = LoadFromByteArray(aImageBytes, null))
            {
                return (ImageToByteArray(Resize(newImage, aHeight, aWidth, aKeepAspectRatio)));
            }
        }
        #endregion

        #region Image Resize
        /// <summary>
        /// Return a resized image with an argument size and aspect ratio
        /// </summary>
        /// <param name="aImage"></param>
        /// <param name="aHeight"></param>
        /// <param name="aWidth"></param>
        /// <param name="aKeepAspectRatio"></param>
        /// <returns></returns>
        public static Image Resize(Image aImage, int aHeight, int aWidth, bool aKeepAspectRatio = true)
        {
            int vNewWidth;
            int vNewHeight;
            if (aKeepAspectRatio)
            {
                int vOrigWidth = aImage.Width;
                int vOrigHeight = aImage.Height;
                float vPercentWidth = (float)aWidth / (float)vOrigWidth;
                float vPercentHeight = (float)aHeight / (float)vOrigHeight;
                float percent = vPercentHeight < vPercentWidth ? vPercentHeight : vPercentWidth;
                vNewWidth = (int)(vOrigWidth * percent);
                vNewHeight = (int)(vOrigHeight * percent);
            }
            else
            {
                vNewWidth = aWidth;
                vNewHeight = aHeight;
            }
            Bitmap newImage = new Bitmap(vNewWidth, vNewHeight, aImage.PixelFormat); // this was Image, not bitmap

            newImage.SetResolution(aImage.HorizontalResolution, aImage.VerticalResolution);
            using (Graphics graphicsHandle = Graphics.FromImage(newImage))
            {
                //graphicsHandle.SmoothingMode = SmoothingMode.HighQuality;
                //graphicsHandle.PixelOffsetMode = PixelOffsetMode.HighQuality;
                graphicsHandle.Clear(Color.Transparent);
                graphicsHandle.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphicsHandle.DrawImage(aImage, new Rectangle(0, 0, vNewWidth, vNewHeight));
            }
            return newImage;
        }
        #endregion
        public static Bitmap RescaleImage(Image source, Size size)
        {
            // 1st bullet, pixel format
            var bmp = new Bitmap(size.Width, size.Height, source.PixelFormat);
            // 2nd bullet, resolution
            bmp.SetResolution(source.HorizontalResolution, source.VerticalResolution);
            using (var gr = Graphics.FromImage(bmp))
            {
                // 3rd bullet, background
                gr.Clear(Color.Transparent);
                // 4th bullet, interpolation
                gr.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                gr.DrawImage(source, new Rectangle(0, 0, size.Width, size.Height));
            }
            return bmp;
        }

        #region Icon GetExtensionIcon
        public static Icon GetExtensionIcon(string aFileExtension, bool largeIcon)
        {
            string vFileName = String.Format("*.{0}", aFileExtension);

            Shfileinfo shinfo = new Shfileinfo();
            IntPtr hImg;
            if (largeIcon)
            {
                hImg = SHGetFileInfo(vFileName, FileAttributeNormal, ref shinfo,
                (uint)Marshal.SizeOf(shinfo),
                ShgfiIcon |
                ShgfiLargeicon |
                ShgfiUsefileattributes);
            }
            else
            {
                hImg = SHGetFileInfo(vFileName, FileAttributeNormal, ref shinfo,
                (uint)Marshal.SizeOf(shinfo),
                ShgfiIcon |
                ShgfiSmallicon |
                ShgfiUsefileattributes);
            }
            try
            {
                return Icon.FromHandle(shinfo.hIcon);
            }
            catch
            {
                return null;
            }
        } 
        #endregion

        #region ByteToBase64String
        /// <summary>
        /// Returns the base64 encoded string representation of a byte array.
        /// </summary>
        /// <param name="aByteArray">A byte array.</param>
        /// <returns></returns>
        public static string ByteToBase64String(byte[] aByteArray)
        {
            if (aByteArray == null || aByteArray.Length == 0)
            {
                return String.Empty;
            }
            return Convert.ToBase64String(aByteArray, 0, aByteArray.Length);
        }
        #endregion

        #region Base64StringToByte
        /// <summary>
        /// Returns the byte array of a base64 encoded string.
        /// </summary>
        /// <param name="aBase64String">A base64 string.</param>
        /// <returns>A byte array</returns>
        public static byte[] Base64StringToByte(string aBase64String)
        {

            return aBase64String == null ? null: System.Convert.FromBase64String(aBase64String);
        }
        #endregion

        #region ImageToBase64String
        /// <summary>
        /// Returns the base64 encoded string representation of the given image.
        /// </summary>
        /// <param name="aImage">The image to encode.</param>
        /// <returns>A string</returns>
        public static string ImageToBase64String(Image aImage)
        {
            using (MemoryStream vMemoryStream = new MemoryStream())
            {
                aImage.Save(vMemoryStream, aImage.RawFormat);
                return Convert.ToBase64String(vMemoryStream.ToArray());
            }
        }
        #endregion

        #region Base64StringToImage
        /// <summary>
        /// Returns an image from a base64 encoded string.
        /// </summary>
        /// <param name="aBase64String">The base64 string to decode.</param>
        /// <returns>An Image</returns>
        public static Image Base64StringToImage(string aBase64String)
        {
            if (string.IsNullOrWhiteSpace(aBase64String))
            {
                return null;
            }
            using (var stream = new MemoryStream(Convert.FromBase64String(aBase64String)))
            using (var sourceImage = Image.FromStream(stream))
            {
                return new Bitmap(sourceImage);
            }
        }
        #endregion

        #region ImageToByteArray
        public static byte[] ImageToByteArray(Image aImage)
        {
            using (var vMemoryStream = new MemoryStream())
            {
                aImage.Save(vMemoryStream, System.Drawing.Imaging.ImageFormat.Bmp);
                return vMemoryStream.ToArray();
            }
        }
        #endregion
    }
}
