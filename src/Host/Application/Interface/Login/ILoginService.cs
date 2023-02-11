using BlazorKiteConnect.Server.KiteModel;

namespace BlazorKiteConnect.Server.Application.Interface.Login;

public interface ILoginService : ICompiledKiteApiService<string, GetTokenReponse>
{
    
}


