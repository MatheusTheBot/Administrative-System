namespace API.Controllers.Contracts;
public interface IControllerResult
{
    bool IsSuccess { get; set; }
    object Data { get; set; }
}
