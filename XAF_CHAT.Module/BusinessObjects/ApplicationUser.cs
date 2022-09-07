using System.ComponentModel;
using System.Text;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.CloneObject;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.PermissionPolicy;
using DevExpress.Xpo;

namespace XAF_CHAT.Module.BusinessObjects;

[MapInheritance(MapInheritanceType.ParentTable)]
[DefaultProperty(nameof(UserName))]
[ModelDefault(nameof(IModelClassCloneable.IsCloneable), "True")]
[ModelDefault(nameof(IModelClassShowAutoFilterRow.DefaultListViewShowAutoFilterRow), "True"),
        ModelDefault(nameof(IModelListViewShowFindPanel.ShowFindPanel), "True"),
        ModelDefault(nameof(IModelListView.IsGroupPanelVisible), "True")]
[VisibleInReports, ListViewAutoFilterRow(true), LookupEditorMode(LookupEditorMode.AllItems)]
[DefaultListViewOptions(MasterDetailMode.ListViewOnly, true, NewItemRowPosition.Top)]
public class ApplicationUser : PermissionPolicyUser, IObjectSpaceLink, ISecurityUserWithLoginInfo {
    public ApplicationUser(Session session) : base(session) { }


    private bool _IsOnline;
    [Browsable(false)]
    public bool IsOnline
    {
        get { return _IsOnline; }
        set { SetPropertyValue<bool>(nameof(IsOnline), ref _IsOnline, value); }
    }


    private DateTime _LastOnline;
    [Browsable(false)]
    public DateTime LastOnline
    {
        get { return _LastOnline; }
        set { SetPropertyValue<DateTime>(nameof(LastOnline), ref _LastOnline, value); }
    }




    [Browsable(false)]
    [Aggregated, Association("User-LoginInfo")]
    public XPCollection<ApplicationUserLoginInfo> LoginInfo {
        get { return GetCollection<ApplicationUserLoginInfo>(nameof(LoginInfo)); }
    }

    IEnumerable<ISecurityUserLoginInfo> IOAuthSecurityUser.UserLogins => LoginInfo.OfType<ISecurityUserLoginInfo>();

    IObjectSpace IObjectSpaceLink.ObjectSpace { get; set; }

    ISecurityUserLoginInfo ISecurityUserWithLoginInfo.CreateUserLoginInfo(string loginProviderName, string providerUserKey) {
        ApplicationUserLoginInfo result = ((IObjectSpaceLink)this).ObjectSpace.CreateObject<ApplicationUserLoginInfo>();
        result.LoginProviderName = loginProviderName;
        result.ProviderUserKey = providerUserKey;
        result.User = this;
        return result;
    }
}
