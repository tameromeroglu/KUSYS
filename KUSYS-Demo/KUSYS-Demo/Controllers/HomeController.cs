using KUSYS_Demo.Business;
using KUSYS_Demo.Business.DTOs;
using KUSYS_Demo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace KUSYS_Demo.Controllers
{
   
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly StudentService _studentService;

        public HomeController(ILogger<HomeController> logger, StudentService studentService)
        {
            _logger = logger;
            _studentService = studentService;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {

            var students = await _studentService.GetAllStudents();


            return View(students.Data);
        }

        [Authorize]
        public IActionResult NewStudent()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> NewStudent(NewStudentDto newStudent)
        {

            var result = await _studentService.NewStudent(newStudent);

            if (result.ResultStatus == Business.Enums.ResultStatusEnum.Error)
            {
                return RedirectToAction("Error");
            }


            return Redirect("Index");
        }



        [Authorize]
        [HttpGet]
        public async Task<IActionResult> UpdateStudent(int id)
        {
            var student = await _studentService.GetStudentDetailById(id);

            return View(student.Data);

        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> UpdateStudent(UpdateStudentDto updateStudent)
        {

            var result = await _studentService.UpdateStudent(updateStudent);

            if (result.ResultStatus == Business.Enums.ResultStatusEnum.Error)
            {
                return RedirectToAction("Error");
            }

            return RedirectToAction("Index");
        }


        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var result = await _studentService.DeleteStudent(id);

            if (result.ResultStatus == Business.Enums.ResultStatusEnum.Error)
            {
                return Json(new { success = false, message = result.Data });
            }

            return Json(new { success = true, message = result.Data });
        }



        [Authorize]
        [HttpGet("studentdetail")]
        public async Task<IActionResult> StudentDetail(int id)
        {

            var students = await _studentService.GetStudentDetailById(id);

            return View(students.Data);
        }



        [Authorize]
        public async Task<IActionResult> StudentsCourses()
        {
            if (HttpContext.User.IsInRole("Admin"))
            {
                var students = await _studentService.GetAllStudentsCourses();

                return View(students.Data);
            }
            else
            {
                var students = await _studentService.GetStudentCourseByUsername(HttpContext.User.Identity.Name);

                return View(students.Data);
            }

        }


        [Authorize]
        [HttpGet]
        public async Task<IActionResult> UpdateStudentCourse(int id)
        {
            var student = await _studentService.GetStudentCourseById(id);

            var courses = await _studentService.GetAllCourses();

            ViewBag.list = courses.Data;

            return View(student.Data);

        }


        [Authorize]
        [HttpPost]
        public async Task<IActionResult> UpdateStudentCourse(StudentsCoursesDto studentsCoursesDto)
        {

            var result = await _studentService.UpdateStudentCourse(studentsCoursesDto);

            if (result.ResultStatus == Business.Enums.ResultStatusEnum.Error)
            {
                return RedirectToAction("Error");
            }

            return RedirectToAction("StudentsCourses");
        }



        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}