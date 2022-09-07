using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace XAF_CHAT.Module.BusinessObjects
{
    [DefaultClassOptions]
    //[ImageName("BO_Contact")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class SubMessage : BaseObject
    { 
        public SubMessage(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();


            if (Session.IsNewObject(this))
            {
                this.CreatedDate = DateTime.Now;
                
            }
        }


        private ChatMessage _Chat;
        [Persistent("ChatID")]
        [Association("Chat-SubMessages")]
        public ChatMessage Chat
        {
            get { return _Chat; }
            set { SetPropertyValue<ChatMessage>(nameof(Chat), ref _Chat, value); }
        }


        private string _Message;
        /// <summary>
        /// Nội dung tin nhắn
        /// </summary>
        public string Message
        {
            get { return _Message; }
            set { SetPropertyValue<string>(nameof(Message), ref _Message, value); }
        }


        private bool _IsRead;
        public bool IsRead
        {
            get { return _IsRead; }
            set { SetPropertyValue<bool>(nameof(IsRead), ref _IsRead, value); }
        }



        private DateTime _CreatedDate;
        /// <summary>
        /// Ngày gửi
        /// </summary>
        public DateTime CreatedDate
        {
            get { return _CreatedDate; }
            set { SetPropertyValue<DateTime>(nameof(CreatedDate), ref _CreatedDate, value); }
        }


        private DateTime? _ReadTime;
        public DateTime? ReadTime
        {
            get { return _ReadTime; }
            set { SetPropertyValue<DateTime?>(nameof(ReadTime), ref _ReadTime, value); }
        }



        /// <summary>
        /// Người viết Nội dung tin nhắn
        /// </summary>

        private ApplicationUser _Owner;
        [Persistent("OwnerID")]
        public ApplicationUser Owner
        {
            get { return _Owner; }
            set { SetPropertyValue<ApplicationUser>(nameof(Owner), ref _Owner, value); }
        }

    }
}