
using Microsoft.AspNetCore.Mvc;
using StudentManagementSystem.Data;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.Controllers
{
    public class StudentsController : Controller
    {
        private readonly StudentStore _store;

        public StudentsController(StudentStore store)
        {
            _store = store;
        }

        public IActionResult Index()
        {
            var students = _store.GetAll();
            return View(students);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Student student)
        {
            if (!ModelState.IsValid)
                return View(student);

            var students = _store.GetAll();

            if (students.Any(s =>
                s.RollNumber.Equals(
                    student.RollNumber,
                    StringComparison.OrdinalIgnoreCase)))
            {
                ModelState.AddModelError(
                    nameof(student.RollNumber),
                    "This roll number already exists.");

                return View(student);
            }

            student.Id = students.Count == 0
                ? 1
                : students.Max(s => s.Id) + 1;

            students.Add(student);
            _store.SaveAll(students);

            TempData["Message"] = "Student added successfully!";
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            var student = _store.GetAll()
                .FirstOrDefault(s => s.Id == id);

            if (student == null)
                return NotFound();

            return View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Student student)
        {
            if (id != student.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return View(student);

            var students = _store.GetAll();

            if (students.Any(s =>
                s.Id != student.Id &&
                s.RollNumber.Equals(
                    student.RollNumber,
                    StringComparison.OrdinalIgnoreCase)))
            {
                ModelState.AddModelError(
                    nameof(student.RollNumber),
                    "This roll number already exists.");

                return View(student);
            }

            var existing = students.FirstOrDefault(s => s.Id == id);

            if (existing == null)
                return NotFound();

            existing.Name = student.Name;
            existing.RollNumber = student.RollNumber;
            existing.Course = student.Course;
            existing.Email = student.Email;

            _store.SaveAll(students);

            TempData["Message"] = "Student updated successfully!";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var students = _store.GetAll();
            var student = students.FirstOrDefault(s => s.Id == id);

            if (student != null)
            {
                students.Remove(student);
                _store.SaveAll(students);
                TempData["Message"] = "Student deleted successfully!";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
