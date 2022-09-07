using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Blazor.Components.Models;
using DevExpress.ExpressApp.Blazor.Templates;
using DevExpress.ExpressApp.Blazor.Templates.Navigation.ActionControls;
using DevExpress.ExpressApp.Blazor.Templates.Security.ActionControls;
using DevExpress.ExpressApp.Blazor.Templates.Toolbar.ActionControls;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Templates.ActionControls;
using DevExpress.Persistent.Base;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace XAF_CHAT.Blazor.Server.Templates
{
    public class MainApplicationWindowTemplate : WindowTemplateBase, ISupportActionsToolbarVisibility, ISelectionDependencyToolbar
    {
        public MainApplicationWindowTemplate()
        {
            NavigateBackActionControl = new NavigateBackActionControl();
            AddActionControl(NavigateBackActionControl);
            AccountComponent = new AccountComponentAdapter();
            AddActionControls(AccountComponent.ActionControls);
            ShowNavigationItemActionControl = new ShowNavigationItemActionControl();
            AddActionControl(ShowNavigationItemActionControl);

            IsActionsToolbarVisible = true;
            Toolbar = new DxToolbarAdapter(new DxToolbarModel());
            Toolbar.AddActionContainer(nameof(PredefinedCategory.ObjectsCreation));
            Toolbar.AddActionContainer(nameof(PredefinedCategory.Save));
            Toolbar.AddActionContainer("Close");
            Toolbar.AddActionContainer(nameof(PredefinedCategory.Export));
            Toolbar.AddActionContainer(nameof(PredefinedCategory.UndoRedo));
            Toolbar.AddActionContainer(nameof(PredefinedCategory.Edit));
            Toolbar.AddActionContainer(nameof(PredefinedCategory.RecordEdit));
            Toolbar.AddActionContainer(nameof(PredefinedCategory.RecordsNavigation));
            Toolbar.AddActionContainer(nameof(PredefinedCategory.View));
            Toolbar.AddActionContainer(nameof(PredefinedCategory.Reports), DevExpress.Blazor.ToolbarItemAlignment.Right);
            Toolbar.AddActionContainer(nameof(PredefinedCategory.Search), DevExpress.Blazor.ToolbarItemAlignment.Right);
            Toolbar.AddActionContainer(nameof(PredefinedCategory.Filters), DevExpress.Blazor.ToolbarItemAlignment.Right);
            Toolbar.AddActionContainer(nameof(PredefinedCategory.FullTextSearch), DevExpress.Blazor.ToolbarItemAlignment.Right);
            Toolbar.AddActionContainer(nameof(PredefinedCategory.Tools), DevExpress.Blazor.ToolbarItemAlignment.Right);
            Toolbar.AddActionContainer("Diagnostic");
            Toolbar.AddActionContainer(nameof(PredefinedCategory.Unspecified));
        }
        protected override IEnumerable<IActionControlContainer> GetActionControlContainers() => Toolbar.ActionContainers;
        protected override RenderFragment CreateComponent() => MainApplicationWindowTemplateComponent.Create(this);
        protected override void BeginUpdate()
        {
            base.BeginUpdate();
            ((ISupportUpdate)Toolbar).BeginUpdate();
        }
        protected override void EndUpdate()
        {
            ((ISupportUpdate)Toolbar).EndUpdate();
            base.EndUpdate();
        }
        public bool IsActionsToolbarVisible { get; private set; }
        public NavigateBackActionControl NavigateBackActionControl { get; }
        public AccountComponentAdapter AccountComponent { get; }
        public ShowNavigationItemActionControl ShowNavigationItemActionControl { get; }
        public DxToolbarAdapter Toolbar { get; }
        public string AboutInfoString { get; set; }
        void ISupportActionsToolbarVisibility.SetVisible(bool isVisible) => IsActionsToolbarVisible = isVisible;
    }
}
