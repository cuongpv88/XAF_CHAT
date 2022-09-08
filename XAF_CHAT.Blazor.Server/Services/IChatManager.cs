using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Blazor.Services;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Xpo;
using RestSharp;
using RestSharp.Serializers.NewtonsoftJson;
using System.ServiceModel.Channels;
using System.Threading;
using XAF_CHAT.Module.BusinessObjects;

namespace XAF_CHAT.Blazor.Server.Services
{
    /// <summary>
    /// 
    /// </summary>
    public interface IChatManager
    {
        Task<IList<ApplicationUser>> GetUsersAsync(Guid currentUserId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="currentUserID"></param>
        /// <param name="fromUserID"></param>
        /// <returns></returns>
        Task<IList<ChatMessage>> GetChatsAsync(Guid currentUserID, Guid fromUserID);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="currentUserID"></param>
        /// <param name="fromUserID"></param>
        /// <returns></returns>
        Task<ChatMessage> GetChatAsync(Guid currentUserID, Guid fromUserID);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="currentUserId"></param>
        /// <param name="fromUserId"></param>
        /// <returns></returns>
        Task SaveMessageAsync(string message, Guid currentUserId, Guid fromUserId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="currentUserId"></param>
        /// <param name="fromUserId"></param>
        /// <param name="chat"></param>
        /// <returns></returns>
        Task SaveMessageAsync(string message, Guid currentUserId, Guid fromUserId, ChatMessage chat);

        Task<List<ChatMessage>> GetConversationAsync(string contactId);
        Task<ApplicationUser> GetUserDetailsAsync(string userId);
    }
    /// <summary>
    /// 
    /// </summary>
    public class ChatManager : IChatManager
    {
        private readonly IServiceProvider _serviceProvider;
        //private HttpClient _httpClient;
        public ChatManager(
            IServiceProvider serviceProvider
            /*HttpClient httpClient*/)
        {
            _serviceProvider = serviceProvider;
            //_httpClient = httpClient;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="contactId"></param>
        /// <returns></returns>
        public async Task<IList<ChatMessage>> GetChatsAsync(Guid currentUserID, Guid fromUserID)
        {
            XafApplication application = _serviceProvider.GetService<IXafApplicationProvider>().GetApplication();
            IObjectSpace obs = application.CreateObjectSpace(typeof(ChatMessage));

            return obs.GetObjects<ChatMessage>(CriteriaOperator.FromLambda<ChatMessage>(c => c.CreatedDate.Date == DateTime.Now.Date))
                .OrderBy(c => c.CreatedDate).ToList();
            //HttpContext context = _serviceProvider.GetService<IHttpContextAccessor>().HttpContext;
            //Uri uri = new Uri($"{context.Request.Scheme}://{context.Request.Host}{context.Request.PathBase}");
            //var client = new RestClient(uri + "/api/odata").UseNewtonsoftJson();
            //var request = new RestRequest("ChatMessage");
            //var response = await client.GetAsync<ODataResponse<ChatMessage>>(request);
            ////response.Value.Any()

            //        return response.Value;

            //return await HttpClient.GetFromJsonAsync<List<ChatMessage>>($"api/chat/{contactId}");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="contactId"></param>
        /// <returns></returns>
        public async Task<ChatMessage> GetChatAsync(Guid currentUserID, Guid fromUserID)
        {
            XafApplication application = _serviceProvider.GetService<IXafApplicationProvider>().GetApplication();
            IObjectSpace obs = application.CreateObjectSpace(typeof(ChatMessage));

            return obs.FirstOrDefault<ChatMessage>(c => c.CreatedDate.Date == DateTime.Now.Date);
            //return await HttpClient.GetFromJsonAsync<List<ChatMessage>>($"api/chat/{contactId}");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="contactId"></param>
        /// <returns></returns>
        public async Task<List<ChatMessage>> GetConversationAsync(string contactId)
        {
            //return await HttpClient.GetFromJsonAsync<List<ChatMessage>>($"api/chat/{contactId}");
            throw new NotImplementedException();
        }
        public async Task<ApplicationUser> GetUserDetailsAsync(string userId)
        {
            //return await HttpClient.GetFromJsonAsync<ApplicationUser>($"api/chat/users/{userId}");
            throw new NotImplementedException();
        }
        /// <summary>
        /// Danh sách người dùng
        /// </summary>
        /// <returns></returns>
        public async Task<IList<ApplicationUser>> GetUsersAsync(Guid currentUserId)
        {
            //HttpContext context = _serviceProvider.GetService<IHttpContextAccessor>().HttpContext;
            //var data = await HttpClient.GetFromJsonAsync<List<ApplicationUser>>("/api/odata/ApplicationUser");

            XafApplication application = _serviceProvider.GetService<IXafApplicationProvider>().GetApplication();
            IObjectSpace obs = application.CreateObjectSpace(typeof(ApplicationUser));
            return obs.GetObjects<ApplicationUser>(CriteriaOperator.FromLambda<ApplicationUser>(c => c.Oid != currentUserId));
            //HttpContext context = _serviceProvider.GetService<IHttpContextAccessor>().HttpContext;
            //Uri uri = new Uri($"{context.Request.Scheme}://{context.Request.Host}{context.Request.PathBase}");
            //var client = new RestClient(uri + "/api/odata").UseNewtonsoftJson();
            //var request = new RestRequest("ApplicationUser");
            //var response = await client.GetAsync<ODataResponse<ApplicationUser>>(request);
            //return response.Value;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task SaveMessageAsync(string message, Guid currentUserId, Guid fromUserId)
        {
            //await HttpClient.PostAsJsonAsync("api/chat", message);
            XafApplication application = _serviceProvider.GetService<IXafApplicationProvider>().GetApplication();
            IObjectSpace obs = application.CreateObjectSpace(typeof(ChatMessage));

            var ToUser = obs.GetObjectByKey<ApplicationUser>(currentUserId);
            var FromUser = obs.GetObjectByKey<ApplicationUser>(fromUserId);

            Session session = ((XPObjectSpace)obs).Session;
            SubMessage sub = new SubMessage(session);
            sub.Message = message;
            sub.CreatedDate = DateTime.Now;
            sub.Owner = ToUser;
            sub.Save();
            obs.CommitChanges();

            ChatMessage chat = GetChatAsync(currentUserId, fromUserId).GetAwaiter().GetResult();
            if (chat == null)
            {
                chat = obs.CreateObject<ChatMessage>();
                chat.ToUser = ToUser;
                chat.FromUser = FromUser;
                chat.CreatedDate = DateTime.Now;
                chat.Save();
                obs.CommitChanges();
            }

            sub.Chat = obs.GetObjectByKey<ChatMessage>(chat.Oid);
            obs.CommitChanges();
            await Task.CompletedTask;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task SaveMessageAsync(string message, Guid currentUserId, Guid fromUserId, ChatMessage chat)
        {
            //await HttpClient.PostAsJsonAsync("api/chat", message);
            XafApplication application = _serviceProvider.GetService<IXafApplicationProvider>().GetApplication();
            IObjectSpace obs = application.CreateObjectSpace(typeof(ChatMessage));
            var ToUser = obs.GetObjectByKey<ApplicationUser>(currentUserId);
            var FromUser = obs.GetObjectByKey<ApplicationUser>(fromUserId);

            if (chat == null)
            {
                chat = obs.CreateObject<ChatMessage>();
                chat.ToUser = ToUser;
                chat.FromUser = FromUser;
                chat.CreatedDate = DateTime.Now;
                chat.Save();
            }
            Session session = ((XPObjectSpace)obs).Session;

            SubMessage sub = new SubMessage(session);
            sub.Chat = chat;
            sub.Message = message;
            sub.CreatedDate = DateTime.Now;
            sub.Owner = ToUser;
            sub.Save();

            obs.CommitChanges();

            await Task.CompletedTask;
        }
    }
}
