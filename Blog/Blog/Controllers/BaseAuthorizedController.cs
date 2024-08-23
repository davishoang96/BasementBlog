using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers;

[ApiController]
[Authorize]
public class BaseAuthorizedController : ControllerBase { }
