namespace API.Controllers.Contracts;
public interface IControllerResult<ControllerBase>
{
    bool IsSuccess { get; set; }
    object Data { get; set; }
}
