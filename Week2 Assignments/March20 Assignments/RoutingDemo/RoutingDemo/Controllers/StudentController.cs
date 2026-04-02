using Microsoft.AspNetCore.Mvc;
using RoutingDemo.Models;
namespace RoutingDemo.Controllers
{
    public class StudentController : Controller
    {
        List<Student> studlist = new List<Student>()
            {
              new Student {Id=101,Name="kiran",Class="class4"},

              new Student {Id=102, Name="Mohan", Class="class7"},
                 new Student {Id=103,Name="Suhana",Class="class8"},

            };
        [Route("studs")]
        public IActionResult GetAllStudent()
        {
            return View(studlist);
        }
        [Route("studs/{id}")]

        public IActionResult GetStudent(int id)
        {
            var student = studlist.FirstOrDefault(x=>x.Id==id);
            return View();
        }
        [Route("studsfew")]

        public IActionResult fewcolumns()
        {
            var fewcolumns = studlist.Select(x => new 
            {
                Class = x.Class,
                Name = x.Name
            });
            return View(fewcolumns);
        }



    }
}
