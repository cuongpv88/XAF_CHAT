using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using FileSystemData.BusinessObjects;

namespace XAF_CHAT.Module.BusinessObjects {
    [DefaultClassOptions]
    [FileAttachment("File")]
    public class FileLink : BaseObject {
        public FileLink(Session session) : base(session) { }
        [Aggregated, ExpandObjectMembers(ExpandObjectMembers.Never), ImmediatePostData]
        public FileSystemLinkObject File {
            get { return GetPropertyValue<FileSystemLinkObject>("File"); }
            set { SetPropertyValue<FileSystemLinkObject>("File", value); }
        }

        //private string _Name;
        public string FilePath
        {
            get { return File != null ? File.FileName : null; }
            //set { SetPropertyValue<string>(nameof(Name), ref _Name, value); }
        }
    }
}
