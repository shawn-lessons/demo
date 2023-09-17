using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using sp.Models;

namespace sp.Controllers;

public class CourseController : Controller
{

    public CourseController()
    {
    }

    public IActionResult Index(int? id)
    {
        return View();
    }

    public IActionResult Test(int? id)
    {
        return Ok(id);
    }
}
