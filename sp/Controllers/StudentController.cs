using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;

using sp.Data;
using sp.Models;

namespace sp.Controllers;

public class StudentController : Controller
{
    private readonly ApplicationDbContext _context;


    public StudentController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var students = await _context.Students
            .ToArrayAsync();

        var model = new StudentViewModel()
        {
            Students = students,
            Current = new Student()
        };
        return View(model);
    }

    public async Task<IActionResult> Save(Student student)
    {
        if (!ModelState.IsValid)
        {
            return RedirectToAction("Index");
        }
        if (student.Id == Guid.Empty)
        {
            student.Id = Guid.NewGuid();
            _context.Students.Add(student);

        }
        else
        {
            _context.Students.Update(student);
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
        var student = _context.Students.Find(id);
        if (student == null)
        {
            return NotFound(); // Optionally handle the case where the item is not found
        }
        _context.Students.Remove(student);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Edit(Guid id)
    {
        var student = _context.Students.Find(id);
        if (student == null)
        {
            return NotFound(); // Optionally handle the case where the item is not found
        }

        var students = await _context.Students
            .ToArrayAsync();

        var model = new StudentViewModel()
        {
            Students = students,
            Current = student
        };
        return View("Index", model);


    }

    public IActionResult CheckUser()
    {
        var userEmail = User.Identity.Name;
        return Ok(userEmail);
    }
}
