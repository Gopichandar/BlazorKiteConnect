using Microsoft.AspNetCore.Components;

namespace BlazorKiteConnect.Client.Pages
{
    public partial class InstrumentsRefresh
    {

        [Inject]
        public HttpClient HttpClient { get; set; }

        private bool _processing = false;

        private string _text = "";

        async Task ProcessSomething()
        {
            _processing = true;
            var response = await HttpClient.GetAsync("api/instruments/refresh");
            if (response.IsSuccessStatusCode)
            {
                _text = "Refresh successful!";
            }
            else
            {
                _text = "RRefresh failed!";
            }
            _processing = false;
        }
    }
}
