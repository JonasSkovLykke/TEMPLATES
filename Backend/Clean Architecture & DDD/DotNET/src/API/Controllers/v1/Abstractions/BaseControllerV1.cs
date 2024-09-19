using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

/// <summary>
/// A base controller class for version 1 of the API controllers. Provides common functionality and utilities.
/// </summary>
[ApiVersion(1)]
[Route("v{v:apiVersion}/[controller]")]
public class BaseControllerV1 : BaseController
{    
}
