using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogCore.Controllers;

[ApiController]
[Authorize]
public class BaseAuthorizedController : ControllerBase { }
