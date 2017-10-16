using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Equinox.Application.ViewModels
{
    public class ProductViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "The Name is Required")]
        [MinLength(2)]
        [MaxLength(100)]
        [DisplayName("Name")]
        public string Name { get; set; }


        [Required(ErrorMessage = "The Code is Required")]
        [MinLength(2)]
        [MaxLength(100)]
        [DisplayName("Code")]
        public string Code { get; set; }

        [Required(ErrorMessage = "The Category is Required")]
        [MinLength(2)]
        [MaxLength(100)]
        [DisplayName("Category")]
        public string Category { get; set; }


    }
}
