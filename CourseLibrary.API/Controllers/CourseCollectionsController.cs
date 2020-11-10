using AutoMapper;
using CourseLibrary.API.Filters;
using CourseLibrary.API.ModelBinders;
using CourseLibrary.API.Models;
using CourseLibrary.API.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseLibrary.API.Controllers
{
    [ApiController]
    [Route("api/courseColletions")]
    [CoursesResultFilter]
    public class CourseCollectionsController : ControllerBase
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IMapper _mapper;

        public CourseCollectionsController(ICourseRepository courseRepository, IMapper mapper)
        {
            _courseRepository = courseRepository ?? throw new ArgumentNullException(nameof(courseRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet("({courseIds})", Name ="GetCourseCollection")]
        public async Task<IActionResult> GetCourseCollention(
            [ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> courseIds)
        {
            var courseEntities = await _courseRepository.GetCoursesAsync(courseIds);

            if(courseIds.Count() != courseEntities.Count())
            {
                return NotFound();
            }

            return Ok(courseEntities);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCourseCollection(IEnumerable<CourseForCreation> courseCollection)
        {
            var courseEntities = _mapper.Map<IEnumerable<Entities.Course>>(courseCollection);

            foreach (var course in courseEntities)
            {
                _courseRepository.AddCourse(course);
            }

            await _courseRepository.SaveChangesAsync();

            var courseListId = courseEntities.Select(c => c.Id);
            var courseToReturn = await _courseRepository.GetCoursesAsync(courseListId.ToList());
            var courseIds = string.Join(",", courseListId);

            return CreatedAtRoute("GetCourseCollection", new { courseIds }, courseToReturn);
        }
    }
}
