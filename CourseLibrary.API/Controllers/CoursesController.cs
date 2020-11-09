using System;
using System.Threading.Tasks;
using AutoMapper;
using CourseLibrary.API.Filters;
using CourseLibrary.API.Models;
using CourseLibrary.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace CourseLibrary.API.Controllers
{
    [ApiController]
    [Route("api/courses")]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IMapper _mapper;

        public CoursesController(ICourseRepository courseRepository, IMapper mapper)
        {
            _courseRepository = courseRepository ??
                throw new ArgumentNullException(nameof(courseRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        [CoursesResultFilter]
        public async Task<IActionResult> GetCourses()
        {
            var coursesEntities = await _courseRepository.GetCoursesAsync();
            return Ok(coursesEntities);
        }

        [HttpGet]
        [Route("{id}", Name ="GetCourse")]
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

        [HttpPost]
        [CourseResultFilter]
        public async Task<IActionResult> CreateCourse(CourseForCreation courseForCreation)
        {
            var courseEntity = _mapper.Map<Entities.Course>(courseForCreation);
            _courseRepository.AddCourse(courseEntity);

            await _courseRepository.SaveChangesAsync();
            await _courseRepository.GetCourseAsync(courseEntity.Id);

            return CreatedAtRoute("GetCourse", new { id = courseEntity.Id }, courseEntity);
        }
    }
}
