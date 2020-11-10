using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourseLibrary.API.Contexts;
using CourseLibrary.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace CourseLibrary.API.Services
{
    public class CourseRepository : ICourseRepository, IDisposable
    {
        private CoursesContext _contexts;

        public CourseRepository(CoursesContext contexts)
        {
            _contexts = contexts ?? throw new ArgumentNullException(nameof(contexts));
        }

        public async Task<Course> GetCourseAsync(Guid id)
        {
            return await _contexts.Courses.Include(c => c.Author).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Course>> GetCoursesAsync()
        {
            return await _contexts.Courses.Include(c => c.Author).ToListAsync();
        }

        public async Task<IEnumerable<Entities.Course>> GetCoursesAsync(IEnumerable<Guid> courseIds)
        {
            return await _contexts.Courses.Where(c => courseIds.Contains(c.Id)).Include(c => c.Author).ToListAsync();
        }

        public void AddCourse(Course course)
        {
            if (course == null)
            {
                throw new ArgumentNullException(nameof(course));
            }

            _contexts.Add(course);
        }

        public async Task<bool> SaveChangesAsync()
        {
            // return 1 if 1 or more entities were changed
            return (await _contexts.SaveChangesAsync() > 0);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_contexts != null)
                {
                    _contexts.Dispose();
                    _contexts = null;
                }
            }
        }
    }
}
