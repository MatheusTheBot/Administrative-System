using Domain.Handlers.Contracts;

namespace Domain.Handlers;
public class HandlerResult : IHandlerResult
{
    public HandlerResult(bool isSuccess, object data)
    {
        IsSuccess = isSuccess;
        Data = data;
    }

    public bool IsSuccess { get; set ; }
    public object Data { get ; set ; }
}