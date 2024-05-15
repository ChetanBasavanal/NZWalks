using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllStudentNames()
        {
            string[] StudentNames = new string[] { "Chetan", "Sachin", "Akshay", "Anand" };
            return Ok(StudentNames);
        }
    }
}
