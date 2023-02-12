using BlazorKiteConnect.Shared.ViewModel;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace BlazorKiteConnect.Client.Pages
{
    public partial class InstrumentsRefresh
    {

        protected override async Task OnInitializedAsync()
        {
            var lutResponse = await HttpClient.GetFromJsonAsync<InstrumentsLutResponse>("api/instruments/lut");
            _lut = lutResponse.Data.Lut;            
            await base.OnInitializedAsync();
        }

        [Inject]
        public HttpClient HttpClient { get; set; }

        private bool _processing = false;

        private string _text = "";

        private DateTime _lut;

        async Task Process()
        {
            _processing = true;
            var response = await HttpClient.GetAsync("api/instruments/refresh");
            if (response.IsSuccessStatusCode)
            {
                _text = "Refresh successful!";
            }
            else
            {
                _text = "Refresh failed!";
            }
            _processing = false;
        }
    }
}
