﻿using Microsoft.AspNetCore.Mvc;

namespace CoreProjectLib.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public abstract class BaseController : ControllerBase
    {
    }
}
