using API.Controllers.Contracts;

namespace API.Controllers;
public class ControllerResult : IControllerResult
{
    public ControllerResult(bool isSuccess, object data)
    {
        IsSuccess = isSuccess;
        Data = data;
    }

    public bool IsSuccess { get; set; }
    public dynamic Data { get; set; }
}