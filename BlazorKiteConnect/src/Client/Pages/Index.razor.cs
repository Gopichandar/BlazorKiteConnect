using Microsoft.AspNetCore.Components;

namespace BlazorKiteConnect.Client.Pages
{
    public partial class Index
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        private void Login()
        {
            NavigationManager.NavigateTo("/api/login/authorize", true);
        }
    }
}
