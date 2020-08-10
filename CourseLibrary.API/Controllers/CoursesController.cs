using System;
using System.Threading.Tasks;
using CourseLibrary.API.Filters;
using CourseLibrary.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace CourseLibrary.API.Controllers
{
    [ApiController]
    [Route("api/courses")]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseRepository _courseRepository;

        public CoursesController(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository ??
                throw new ArgumentNullException(nameof(courseRepository));
        }

        [HttpGet]
        [CoursesResultFilter]
        public async Task<IActionResult> GetCourses()
        {
            var coursesEntities = await _courseRepository.GetCoursesAsync();
            return Ok(coursesEntities);
        }

        [HttpGet]
        [Route("{id}")]
        [CourseResultFilter]
        public async Task<IActionResult> GetCourseById(Guid id)
        {
            var courseEntity = await _courseRepository.GetCourseAsync(id);
            if(courseEntity == null)
            {
                return NotFound();
            }

            return Ok(courseEntity);
        }
    }
}
