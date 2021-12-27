using DershaneBul.Entities.Abstract;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DershaneBul.Entities.Concrete
{
    [Table("CourseType")]
    public class CourseType : IBaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int CourseTypeId { get; set; }        
        [Required]
        public string CourseTypeDescription { get; set; }
    }
}
