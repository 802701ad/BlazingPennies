using BlazingPennies.Shared.BlazorApp.Services;
using BlazingPennies.Shared.Models;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.ComponentModel.DataAnnotations;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Web;
using static System.Net.WebRequestMethods;

namespace BlazingPennies.Shared
{
    public class RegisterRequest
    {
        public string user_id { get; set; }
        [Required]
        public string FIRST_NAME { get; set; }
        [Required]
        public string LAST_NAME { get; set; }
        [Required]
        public string email { get; set; }
        [Required]
        private string _userName;
        public string UserName
        {
            get => _userName;
            set => _userName = value.ToLower();
        }
        [Required]
        public string Password { get; set; }
        [Required]
        [Compare(nameof(Password), ErrorMessage = "Passwords do not match!")]
        public string PasswordConfirm { get; set; }
    }

    public class LoginRequest
    {
        [Required]
        public string email { get; set; }
        [Required]
        public string pass { get; set; }
        public bool RememberMe { get; set; }
    }

    public class CurrentUser
    {
        public bool IsAuthenticated { get; set; }
        public string UserName { get; set; }
        public string FIRST_NAME { get; set; }
        public string LAST_NAME { get; set; }
        public string Id { get; set; }
        public Dictionary<string, string> Claims { get; set; }
    }

    public interface IAuthService
    {
        Task Login(LoginRequest loginRequest);
        Task Register(RegisterRequest registerRequest);
        Task Logout();
        Task<string> CurrentUserId();
        Task<CurrentUser> CurrentUserInfo();
    }

    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        private ILocalStorageService _localStorageService;
        private IConfiguration _cfg;
        public AuthService(HttpClient httpClient, ILocalStorageService localStorageService, IConfiguration cfg)
        {
            _localStorageService = localStorageService;
            _httpClient = httpClient;
            _cfg = cfg;
        }

        public async Task<string> CurrentUserId()
        {
            return await _localStorageService.GetItem<string>("user_id") ?? "";
        }

        public async Task<CurrentUser> CurrentUserInfo()
        {

            var user_id = await _localStorageService.GetItem<string>("user_id")??"";
            var c= new CurrentUser
            {
                IsAuthenticated = user_id != "",
                Claims = new Dictionary<string, string>(),
            };
            if (user_id != "")
            {
                var userinfo = await _httpClient.GetFromJsonAsync<User>(Utility.BackendUrl(_cfg, "user/get.php", new { user_id = user_id });
                c.UserName = userinfo.EMAIL;
                c.FIRST_NAME= userinfo.FIRST_NAME;
                c.LAST_NAME= userinfo.LAST_NAME;
                c.Id= user_id;
            }
            return c;

        }
        public async Task Login(LoginRequest loginRequest)
        {
            var formContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("email", loginRequest.email),
                new KeyValuePair<string, string>("pass", loginRequest.pass),
            });

            string url=Utility.BackendUrl(_cfg, "user/verify.php");
            var result = await _httpClient.PostAsync(url, formContent);
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var user=JsonSerializer.Deserialize<User>(await result.Content.ReadAsStringAsync());
                await _localStorageService.SetItem("user_id", user.USER_ID);
            }
            result.EnsureSuccessStatusCode();
        }
        public async Task Logout()
        {
            await _localStorageService.SetItem("user_id","");
            
        }
        public async Task Register(RegisterRequest registerRequest)
        {
            var values = new Dictionary<string, string>
            {
                { "FIRST_NAME", registerRequest.FIRST_NAME },
                { "LAST_NAME", registerRequest.LAST_NAME },
                { "email", registerRequest.email },
                { "UserName", registerRequest.UserName },
                { "Password", registerRequest.Password },
                { "user_id", Guid.NewGuid().ToString() } //pass the user_id as a new guid
            };

            string url=Utility.BackendUrl(_cfg, "user/register.php");
            var result = await _httpClient.PostAsync(url, new FormUrlEncodedContent(values));
            if (result.StatusCode == System.Net.HttpStatusCode.BadRequest) throw new Exception(await result.Content.ReadAsStringAsync());
            result.EnsureSuccessStatusCode();
        }
    }



    public class CustomStateProvider : AuthenticationStateProvider
    {
        private readonly IAuthService api;
        private CurrentUser _currentUser;
        public CustomStateProvider(IAuthService api)
        {
            this.api = api;
        }
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var identity = new ClaimsIdentity();
            try
            {
                var userInfo = await GetCurrentUser();
                if (userInfo.IsAuthenticated)
                {
                    var claims = new[] { new Claim(ClaimTypes.Name, _currentUser.UserName) }.Concat(_currentUser.Claims.Select(c => new Claim(c.Key, c.Value)));
                    identity = new ClaimsIdentity(claims, "Server authentication");
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine("Request failed:" + ex.ToString());
            }
            return new AuthenticationState(new ClaimsPrincipal(identity));
        }
        private async Task<CurrentUser> GetCurrentUser()
        {
            if (_currentUser != null && _currentUser.IsAuthenticated) return _currentUser;
            _currentUser = await api.CurrentUserInfo();
            return _currentUser;
        }
        public async Task Logout()
        {
            await api.Logout();
            _currentUser = null;
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
        public async Task Login(LoginRequest loginParameters)
        {
            await api.Login(loginParameters);
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
        public async Task Register(RegisterRequest registerParameters)
        {
            await api.Register(registerParameters);
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
    }
}
