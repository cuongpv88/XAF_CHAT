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
    public class ChatMessage : BaseObject
    { 
        public ChatMessage(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //SecuritySystem.CurrentUser
        }


        private DateTime _CreatedDate;
        /// <summary>
        /// Ngày gửi
        /// </summary>
        [Indexed]
        public DateTime CreatedDate
        {
            get { return _CreatedDate; }
            set { SetPropertyValue<DateTime>(nameof(CreatedDate), ref _CreatedDate, value); }
        }

        /// <summary>
        /// Người gửi
        /// </summary>
        private ApplicationUser _ToUser;
        [Persistent("ToUserID")]
        [Indexed]
        public ApplicationUser ToUser
        {
            get { return _ToUser; }
            set { SetPropertyValue<ApplicationUser>(nameof(ToUser), ref _ToUser, value); }
        }



        /// <summary>
        /// Gửi đến người dùng
        /// </summary>
        private ApplicationUser _FromUser;
        [Persistent("FromUserID")]
        [Indexed]
        public ApplicationUser FromUser
        {
            get { return _FromUser; }
            set { SetPropertyValue<ApplicationUser>(nameof(FromUser), ref _FromUser, value); }
        }


        [Association("Chat-SubMessages")]
        public XPCollection<SubMessage> SubMessages
        {
            get { return GetCollection<SubMessage>(nameof(SubMessages)); }
        }


    }
}