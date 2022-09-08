using DevExpress.Persistent.BaseImpl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XAF_CHAT.Module
{
    public static class FileStreamHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="savePath"></param>
        public static void SaveAsFile(this byte[] stream, string savePath)
        {
            File.WriteAllBytes(savePath, stream);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileData"></param>
        /// <param name="savePath"></param>
        public static void SaveToPath(this FileData fileData, string savePath)
        {
            if (fileData != null)
            {
                using (Stream stream = new MemoryStream())
                {
                    fileData.SaveToStream(stream);

                    using (FileStream fileStream = File.Create(savePath, (int)stream.Length))
                    {
                        fileStream.Write(fileData.Content, 0, fileData.Content.Length);
                        fileStream.Close();
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileData"></param>
        /// <param name="savePath"></param>
        public static void SaveToPath(this CustomFileData fileData, string savePath)
        {
            if (fileData != null)
            {
                using (Stream stream = new MemoryStream())
                {
                    fileData.SaveToStream(stream);

                    using (FileStream fileStream = File.Create(savePath, (int)stream.Length))
                    {
                        fileStream.Write(fileData.Content, 0, fileData.Content.Length);
                        fileStream.Close();
                    }
                }
            }
        }
    }

}
