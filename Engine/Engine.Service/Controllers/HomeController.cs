using Microsoft.AspNetCore.Mvc;

namespace Engine.Service
{
  public class HomeController : Controller
  {
    public IActionResult Index()
    {
      return new RedirectResult("~/sawgger");
    }
  }

}