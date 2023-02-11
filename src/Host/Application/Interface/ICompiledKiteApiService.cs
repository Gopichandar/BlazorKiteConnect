using BlazorKiteConnect.Server.KiteModel;

namespace BlazorKiteConnect.Server.Application.Interface;

public interface ICompiledKiteApiService<TRequest, TResponse> : IKiteApiService<TRequest, TResponse>, IPrepareRequestBody<TResponse>, IAddRequiredHeader<TResponse>, ISendRequest<TResponse>, IGetResponse<TResponse>
{
    
}

public interface IKiteApiService<TRequest, TResponse>
{
    IPrepareRequestBody<TResponse> Create(TRequest addtionalParams);
}


public interface IPrepareRequestBody<TResponse>
{
    IAddRequiredHeader<TResponse> PrepareRequestBody();
}

public interface IAddRequiredHeader<TResponse>
{
    ISendRequest<TResponse> AddRequiredHeader();
}

public interface ISendRequest<TResponse>
{
    Task<IGetResponse<TResponse>> SendRequestAsync();
}

public interface IGetResponse<TResponse>
{
    TResponse GetResponse();
}
