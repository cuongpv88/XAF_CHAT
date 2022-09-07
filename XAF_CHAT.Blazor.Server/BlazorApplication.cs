using DevExpress.ExpressApp;
using DevExpress.ExpressApp.ApplicationBuilder;
using DevExpress.ExpressApp.Blazor;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.Security.ClientServer;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Xpo;
using XAF_CHAT.Blazor.Server.Services;
using XAF_CHAT.Blazor.Server.Templates;

namespace XAF_CHAT.Blazor.Server;

public class XAF_CHATBlazorApplication : BlazorApplication {
    public XAF_CHATBlazorApplication() {
        ApplicationName = "XAF_CHAT";
        CheckCompatibilityType = DevExpress.ExpressApp.CheckCompatibilityType.DatabaseSchema;
        DatabaseVersionMismatch += XAF_CHATBlazorApplication_DatabaseVersionMismatch;
    }
    protected override void OnSetupStarted() {
        base.OnSetupStarted();
#if DEBUG
        if(System.Diagnostics.Debugger.IsAttached && CheckCompatibilityType == CheckCompatibilityType.DatabaseSchema) {
            DatabaseUpdateMode = DatabaseUpdateMode.UpdateDatabaseAlways;
        }
#endif
    }
    private void XAF_CHATBlazorApplication_DatabaseVersionMismatch(object sender, DatabaseVersionMismatchEventArgs e) {
#if EASYTEST
        e.Updater.Update();
        e.Handled = true;
#else
        if(System.Diagnostics.Debugger.IsAttached) {
            e.Updater.Update();
            e.Handled = true;
        }
        else {
            string message = "The application cannot connect to the specified database, " +
                "because the database doesn't exist, its version is older " +
                "than that of the application or its schema does not match " +
                "the ORM data model structure. To avoid this error, use one " +
                "of the solutions from the https://www.devexpress.com/kb=T367835 KB Article.";

            if(e.CompatibilityError != null && e.CompatibilityError.Exception != null) {
                message += "\r\n\r\nInner exception: " + e.CompatibilityError.Exception.Message;
            }
            throw new InvalidOperationException(message);
        }
#endif
    }

    protected override IFrameTemplate CreateDefaultTemplate(TemplateContext context)
    {
        if (context == TemplateContext.ApplicationWindow)
        {
            return new MainApplicationWindowTemplate();
        }
        //if (context == TemplateContext.LogonWindow)
        //{
        //    return new ERPLogonWindowTemplate();
        //}
        //if (context == TemplateContext.NestedFrame)
        //{
        //    return new ERPNestedFrameTemplate();
        //}
        //if (context == TemplateContext.PopupWindow)
        //{
        //    return new ERPPopupWindowTemplate();
        //}

        return base.CreateDefaultTemplate(context);
    }
}
