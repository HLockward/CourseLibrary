﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using CourseLibrary.API.Entities;

namespace CourseLibrary.API.Services
{
    public interface ICourseRepository
    {
        Task<IEnumerable<Course>> GetCoursesAsync();

        Task<Course> GetCourseAsync(Guid id);

        Task<IEnumerable<Entities.Course>> GetCoursesAsync(IEnumerable<Guid> courseIds);

        void AddCourse(Course course);

        Task<bool> SaveChangesAsync();
    }
}
