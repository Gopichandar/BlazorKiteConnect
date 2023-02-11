using BlazorKiteConnect.Shared.KiteModel;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace BlazorKiteConnect.Client.Pages
{
    public partial class FundsAndMargin
    {
        private FundsAndMarginResponse _marginResponse;
        private bool _isloading = false;

        [Inject]
        public HttpClient HttpClient { get; set; }
        protected override async Task OnInitializedAsync()
        {
            _isloading = true;
            var response = await HttpClient.GetAsync("api/margins");

            if (response.IsSuccessStatusCode)
            {
                _marginResponse = await response.Content.ReadFromJsonAsync<FundsAndMarginResponse>();
            }
            _isloading = false;
            await base.OnInitializedAsync();
        }
    }
}
