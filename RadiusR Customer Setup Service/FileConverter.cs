using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace RadiusR_Customer_Setup_Service
{
    public static class FileConverter
    {
        public const long FileSizeLimit = 10485760;
        private static string[] acceptableTypes = new[]
        {
            "pdf",
            "jpg",
            "png",
            "jpeg"
        };

        public static string GetFileCode(Stream stream)
        {
            if (stream.Length > FileSizeLimit)
            {
                throw new Exception("File size is bigger than limit.");
            }
            BinaryReader reader = new BinaryReader(stream);
            var bytes = reader.ReadBytes((int)stream.Length);
            return Convert.ToBase64String(bytes);
        }

        public static void WriteToStream(Stream stream, string fileCode)
        {
            if (fileCode.LongCount() > FileSizeLimit)
            {
                throw new Exception("File size is bigger than limit.");
            }
            var writer = new BinaryWriter(stream);
            var bytes = Convert.FromBase64String(fileCode);
            writer.Write(bytes, 0, bytes.Length);

        }

        public static bool IsFileTypeAcceptable(string fileType)
        {
            return acceptableTypes.Contains(fileType.ToLower());
        }

        public static bool IsFileSizeAcceptable(string fileCode)
        {
            return fileCode.LongCount() <= FileSizeLimit;
        }
    }
}