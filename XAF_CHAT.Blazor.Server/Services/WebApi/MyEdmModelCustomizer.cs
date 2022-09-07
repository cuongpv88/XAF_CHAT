using DevExpress.ExpressApp.WebApi.Services;
using Microsoft.OData.ModelBuilder;
using XAF_CHAT.Module.BusinessObjects;

namespace ERP.WebApi.Services.WebApi
{
    public class MyEdmModelCustomizer : IEdmModelCustomizer
    {
        public void CustomizeEdmModel(ODataModelBuilder builder)
        {
            builder.EntityType<ChatMessage>()
                .Expand(SelectExpandType.Automatic,
                nameof(ChatMessage.ToUser),
                nameof(ChatMessage.FromUser));


            //builder.EntityType<UserLogin>()
            //    .Expand(SelectExpandType.Automatic, nameof(UserLogin.FactoryID));


            //builder.EntityType<Mold>()
            //    .Expand(SelectExpandType.Automatic, nameof(Mold.Factory));



            //builder.EntityType<Ring_Inspection>()
            //    .Expand(SelectExpandType.Automatic, nameof(Ring_Inspection.OrdersReceive), nameof(Ring_Inspection.Auditor));

            //builder.EntityType<Ring_ExternalInspection>()
            //    .Expand(SelectExpandType.Automatic, nameof(Ring_ExternalInspection.Factory))
            //    .Expand(SelectExpandType.Automatic, nameof(Ring_ExternalInspection.ProducingProcesses));


            //var function = builder.EntityType<Ring_ExternalInspection>()
            //    .Collection
            //    .Function("PutExternalInspection");

            //function.Parameter<Guid>("inspectionID");
            //function.CollectionParameter<Guid>("processIds");

            //var func = builder.EntityType<Ring_ExternalInspection>()
            //    .Collection
            //    .Function("GetExternalInspection");

            //func.Parameter<Guid>("inspectionID");
            //func.Parameter<string>("processIds");
            //func.Returns<string>();


            //var action = builder.EntityType<Ring_ExternalInspection>()
            //    .Collection
            //    .Action("PutExternalInspection");

            //action.Parameter<Guid>("inspectionID");
            //action.Parameter<string>("processIds");


            // Thêm cho ProducingProcess
            //var funcpro = builder.EntityType<ProducingProcess>()
            //    .Collection
            //    .Function("GetProducingProcess");
            //func.Parameter<Guid>("processID");
            //func.Parameter<string>("inspectionIDs");
            //func.Returns<string>();


            //var actionpro = builder.EntityType<ProducingProcess>()
            //    .Collection
            //    .Action("PutProducingProcess");

            //action.Parameter<Guid>("processID");
            //action.Parameter<string>("inspectionIDs");


            //builder.EntityType<Ring_ExternalInspectionHistory>()
            //    .Expand(SelectExpandType.Automatic,
            //    nameof(Ring_ExternalInspectionHistory.OrdersReceive),
            //    nameof(Ring_ExternalInspectionHistory.ProducingProcess),
            //    nameof(Ring_ExternalInspectionHistory.ExternalInspection),
            //    nameof(Ring_ExternalInspectionHistory.Auditor));

            //builder.EntityType<Ring_Plating>()
            //    .Expand(SelectExpandType.Automatic, nameof(Ring_Plating.OrdersReceive), nameof(Ring_Plating.ProducingProcess));

            //builder.EntityType<ProducingProcess>()
            //    .Expand(SelectExpandType.Automatic, nameof(ProducingProcess.Factory))
            //    .Select(SelectExpandType.Automatic, nameof(ProducingProcess.ExternalInspections));

            //.Expand(SelectExpandType.Automatic, nameof(ProducingProcess.ExternalInspections));





            //builder.EntityType<UPK_Inspection>()
            //    .Expand(SelectExpandType.Automatic, nameof(UPK_Inspection.OrdersReceive), nameof(UPK_Inspection.Auditor));


            //var function = builder.EntityType<zz_juchuu_main_vietnam>()
            //    .Collection
            //    .Function("GetOrderByDate");

            //function.Parameter<DateTime>("dateFrom");
            //function.Parameter<DateTime>("dateTo");
            //function.ReturnsCollection<IEnumerable<object>>();

            //builder.EntityType<zz_juchuu_main_vietnam>()
            //    .Collection
            //    .Function("GetOrderBySlipNos")
            //    .ReturnsCollection<IEnumerable<object>>()
            //    .CollectionParameter<IList<string>>("slipNos");

            //Collection.Ac
            //.Action("CustomGet").ReturnsFromEntitySet<ClientSetting>("TestWebApi");
        }
    }
}
