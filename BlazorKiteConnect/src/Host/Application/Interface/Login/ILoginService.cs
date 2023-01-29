using BlazorKiteConnect.Server.KiteModel;

namespace BlazorKiteConnect.Server.Application.Interface.Login
{
    public interface ICompiledLoginService : ILoginService, IPrepareChecksum, IPrepareBodyParameters, IAddRequiredHeader, ISendRequest, IGetResponse
    {
        
    }

    public interface ILoginService 
    {
        IPrepareChecksum Create(string requestToken);
    }

    public interface IPrepareChecksum
    {
        IPrepareBodyParameters PrepareChecksum();
    }

    public interface IPrepareBodyParameters
    {
        IAddRequiredHeader PrepareBodyParameters();
    }

    public interface IAddRequiredHeader
    {
        ISendRequest AddRequiredHeader();
    }

    public interface ISendRequest
    {
        Task<IGetResponse> SendRequestAsync();
    }

    public interface IGetResponse
    {
        GetTokenReponse GetResponse();
    }
}
