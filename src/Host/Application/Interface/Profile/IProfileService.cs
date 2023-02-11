using BlazorKiteConnect.Shared.KiteModel;

namespace BlazorKiteConnect.Server.Application.Interface.Profile;

public interface IProfileService : ICompiledKiteApiService<string , GetProfileResponse>
{

}
