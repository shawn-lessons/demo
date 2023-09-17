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
            Courses = courses,
            Current = new Course()
        };
        return View(model);
    }

    public async Task<IActionResult> Save(Course course)
    {
        if (!ModelState.IsValid)
        {
            return RedirectToAction("Index");
        }
        if (course.Id == Guid.Empty)
        {
            course.Id = Guid.NewGuid();
            _context.Courses.Add(course);

        }
        else
        {
            _context.Courses.Update(course);
        }
        var successful = await _context.SaveChangesAsync();
        if (successful != 1)
        {
            return BadRequest("Could not add item.");
        }
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Delete(Guid id)
    {
        var course = _context.Courses.Find(id);
        if (course == null)
        {
            return NotFound(); // Optionally handle the case where the item is not found
        }
        _context.Courses.Remove(course);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Edit(Guid id)
    {
        var course = _context.Courses.Find(id);
        if (course == null)
        {
            return NotFound(); // Optionally handle the case where the item is not found
        }

        var courses = await _context.Courses
            .ToArrayAsync();

        var model = new CourseViewModel()
        {
            Courses = courses,
            Current = course
        };
        return View("Index", model);


    }
}
