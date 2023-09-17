using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;

using sp.Data;
using sp.Models;

namespace sp.Controllers;

public class CourseController : Controller
{
    private readonly ApplicationDbContext _context;


    public CourseController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var courses = await _context.Courses
            .ToArrayAsync();

        var model = new CourseViewModel()
        {
            Courses = courses
        };
        return View(model);
    }

    public async Task<IActionResult> AddTestCourse(int? id)
    {
        Course course = new Course {Title = "Programming"};
        course.Id = Guid.NewGuid();
        course.CourseId = "D101";
        _context.Courses.Add(course);
        var saveResult = await _context.SaveChangesAsync();
        return Ok(saveResult);
    }
}
