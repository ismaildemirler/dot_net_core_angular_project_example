using DershaneBul.Entities.Abstract;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DershaneBul.Entities.Concrete
{
    [Table("CourseAndCourseType")]
    public class CourseAndCourseType : IBaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public Guid CourseAndCourseTypeId { get; set; }        
        [Required]
        public Guid CourseTypeId { get; set; }
        [Required]
        public Guid CourseId { get; set; }
    }
}
