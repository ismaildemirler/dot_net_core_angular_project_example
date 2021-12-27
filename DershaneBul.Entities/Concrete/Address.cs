using DershaneBul.Entities.Abstract;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DershaneBul.Entities.Concrete
{
    [Table("Address")]
    public class Address: IBaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public Guid AddressId { get; set; }
        [Required] 
        public int TownId { get; set; }
        [Required]
        public int CityId { get; set; }
        [MaxLength(250)]
        [StringLength(250)]
        [Required]
        public string AddressDescription { get; set; }
        [MaxLength(50)]
        [StringLength(50)]
        [Required]
        public string AddressName { get; set; }
        [MaxLength(50)]
        [StringLength(50)]
        [Required]
        public string Street { get; set; }
        [MaxLength(50)]
        [StringLength(50)]
        [Required]
        public string DoorNumber { get; set; }
        [MaxLength(50)]
        [StringLength(50)]
        [Required]
        public string Longtitude { get; set; }
        [MaxLength(50)]
        [StringLength(50)]
        [Required]
        public string Latitude { get; set; }
        [Required]
        public int StateId { get; set; }
        [Required] 
        public DateTime CreationDate { get; set; }
        public DateTime? UpdateDate { get; set; }  

    }
}
