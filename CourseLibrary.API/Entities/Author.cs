using System;
using System.ComponentModel.DataAnnotations;

namespace CourseLibrary.API.Entities
{
    public class Author
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [MaxLength(150)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(150)]
        public string LastName { get; set; }

    }
}
