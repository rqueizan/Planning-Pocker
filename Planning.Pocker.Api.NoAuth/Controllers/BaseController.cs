using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Planning.Pocker.Api.NoAuth.Controllers
{
    public class BaseController : Controller
    {
        public IMediator Mediator { get; set; }
    }
}
