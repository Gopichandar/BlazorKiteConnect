using BlazorKiteConnect.Shared.KiteModel;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http;
using System.Net.Http.Json;

namespace BlazorKiteConnect.Client.Pages
{
    public partial class Index
    {
        private GetProfileResponse _user;
        private bool _isloading = false;

        [Inject]
        public HttpClient HttpClient { get; set; }
        protected override async Task OnInitializedAsync()
        {
            _isloading = true;
            var response = await HttpClient.GetAsync("api/profile");

            if (response.IsSuccessStatusCode)
            {
                _user = await response.Content.ReadFromJsonAsync<GetProfileResponse>();
            }
            _isloading = false;
            await base.OnInitializedAsync();
        }
    }
}
