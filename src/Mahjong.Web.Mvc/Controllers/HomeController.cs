using Microsoft.AspNetCore.Mvc;
using Abp.AspNetCore.Mvc.Authorization;
using Mahjong.Controllers;

namespace Mahjong.Web.Controllers
{
    [AbpMvcAuthorize]
    public class HomeController : MahjongControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
