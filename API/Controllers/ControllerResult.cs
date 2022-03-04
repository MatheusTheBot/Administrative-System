using API.Controllers.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
public class ControllerResult<T> where T : ControllerBase
{
    public ControllerResult(bool isSuccess, object data)
    {
        IsSuccess = isSuccess;
        Data = data;
    }

    public bool IsSuccess { get; set; }
    public dynamic Data { get; set; }
}