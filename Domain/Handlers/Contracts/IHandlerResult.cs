namespace Domain.Handlers.Contracts;
public interface IHandlerResult
{
    bool IsSuccess { get; set; }
    dynamic Data { get; set; }
}
