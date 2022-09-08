using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using FileSystemData.BusinessObjects;

namespace XAF_CHAT.Module.BusinessObjects {
    [DefaultClassOptions]
    [FileAttachment("File")]
    public class FileStore : BaseObject {
        public FileStore(Session session) : base(session) { }
        [Aggregated, ExpandObjectMembers(ExpandObjectMembers.Never), ImmediatePostData]
        public FileSystemStoreObject File {
            get { return GetPropertyValue<FileSystemStoreObject>("File"); }
            set { SetPropertyValue<FileSystemStoreObject>("File", value); }
        }


        //private string _Name;
        public string FilePath
        {
            get { return File != null ? File.FileName : null; }
            //set { SetPropertyValue<string>(nameof(Name), ref _Name, value); }
        }


    }
}
