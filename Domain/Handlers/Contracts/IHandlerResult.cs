namespace Domain.Handlers.Contracts;
public interface IHandlerResult
{
    bool IsSuccess { get; set; }
    object Data { get; set; }
}
