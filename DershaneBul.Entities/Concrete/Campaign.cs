using DershaneBul.Entities.Abstract;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DershaneBul.Entities.Concrete
{
    [Table("Campaign")]
    public class Campaign:IBaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public Guid CampaignId { get; set; }
        [MaxLength(100)]
        [StringLength(100)]
        [Required]
        public string CampaignDescription { get; set; } 
    }
}
