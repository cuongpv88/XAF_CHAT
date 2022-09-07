using XAF_CHAT.Module.BusinessObjects;

namespace XAF_CHAT.Blazor.Server.Services
{
    /// <summary>
    /// 
    /// </summary>
    public interface IChatManager
    {
        Task<List<ApplicationUser>> GetUsersAsync();
        Task SaveMessageAsync(ChatMessage message);
        Task<List<ChatMessage>> GetConversationAsync(string contactId);
        Task<ApplicationUser> GetUserDetailsAsync(string userId);
    }
    /// <summary>
    /// 
    /// </summary>
    public class ChatManager : IChatManager
    {
        private readonly IServiceProvider _serviceProvider;
        private HttpClient _httpClient;
        public ChatManager(
            IServiceProvider serviceProvider,
            HttpClient httpClient)
        {
            _serviceProvider = serviceProvider;
            _httpClient = httpClient;
        }

        /// <summary>
        /// 
        /// </summary>
        public HttpClient HttpClient 
        { 
            get
            {
                HttpContext context = _serviceProvider.GetRequiredService<IHttpContextAccessor>().HttpContext;
                _httpClient.BaseAddress = new Uri($"{context.Request.Scheme}://{context.Request.Host}{context.Request.PathBase}");

                return _httpClient;
            } 
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="contactId"></param>
        /// <returns></returns>
        public async Task<List<ChatMessage>> GetConversationAsync(string contactId)
        {
            return await HttpClient.GetFromJsonAsync<List<ChatMessage>>($"api/chat/{contactId}");
        }
        public async Task<ApplicationUser> GetUserDetailsAsync(string userId)
        {
            return await HttpClient.GetFromJsonAsync<ApplicationUser>($"api/chat/users/{userId}");
        }
        /// <summary>
        /// Danh sách người dùng
        /// </summary>
        /// <returns></returns>
        public async Task<List<ApplicationUser>> GetUsersAsync()
        {
            //HttpContext context = _serviceProvider.GetRequiredService<IHttpContextAccessor>().HttpContext;
            var data = await HttpClient.GetFromJsonAsync<List<ApplicationUser>>("/api/odata/ApplicationUser");
            return data;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task SaveMessageAsync(ChatMessage message)
        {
            await HttpClient.PostAsJsonAsync("api/chat", message);
        }
    }
}
