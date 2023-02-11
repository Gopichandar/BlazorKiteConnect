using BlazorKiteConnect.Shared.KiteModel;

namespace BlazorKiteConnect.Server.Application.Interface.Login
{
    public interface ILogoutService : ICompiledKiteApiService<string, LogoutResponse>
    {
    }
}
