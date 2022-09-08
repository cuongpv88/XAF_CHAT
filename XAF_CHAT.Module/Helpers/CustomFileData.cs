using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XAF_CHAT.Module
{
    [DomainComponent]
    public class CustomFileData : IFileData
    {
        /// <summary>
        /// 
        /// </summary>
        public CustomFileData()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        string fileName;
        public string FileName
        {
            get { return fileName; }
            set { fileName = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int Size
        {
            get { return Content == null ? 0 : Content.Length; }
        }
        /// <summary>
        /// 
        /// </summary>
        [Browsable(false)]
        public byte[] Content { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public void Clear()
        {
            this.Content = null;
            this.FileName = string.Empty;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="stream"></param>
        public void LoadFromStream(string fileName, Stream stream)
        {
            Guard.ArgumentNotNull(stream, "stream");
            Guard.ArgumentNotNullOrEmpty(fileName, "fileName");
            this.FileName = fileName;
            byte[] array = new byte[stream.Length];
            stream.Read(array, 0, array.Length);
            this.Content = array;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stream"></param>
        /// <exception cref="InvalidOperationException"></exception>
        public void SaveToStream(Stream stream)
        {
            if (string.IsNullOrEmpty(this.FileName))
            {
                throw new InvalidOperationException();
            }
            stream.Write(this.Content, 0, this.Size);
            stream.Flush();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.FileName;
        }
    }
}
