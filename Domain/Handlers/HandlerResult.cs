using Domain.Handlers.Contracts;

namespace Domain.Handlers;
public class HandlerResult : IHandlerResult
{
    public HandlerResult(bool isSuccess, dynamic data)
    {
        IsSuccess = isSuccess;
        Data = data;
    }

    public HandlerResult()
    {

    }

    public bool IsSuccess { get; set ; }
    public dynamic Data { get ; set ; }
}