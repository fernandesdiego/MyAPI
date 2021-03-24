using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MyAPI.Api.ViewModels
{
    public class ProductViewModel
    {
        [Key]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "The field {0} is required")]
        public Guid SupplierId { get; set; }
        [Required(ErrorMessage = "The field {0} is required")]
        [StringLength(200, ErrorMessage = "The field must be between {2} and {1} characters", MinimumLength = 2)]
        public string Name { get; set; }
        [Required(ErrorMessage = "The field {0} is required")]
        [StringLength(1000, ErrorMessage = "The field must be between {2} and {1} characters", MinimumLength = 2)]
        public string Description { get; set; }
        public string ImageUpload { get; set; }
        public string Image { get; set; }
        [Required(ErrorMessage = "The field {0} is required")]
        public decimal Price { get; set; }
        [ScaffoldColumn(false)]
        public DateTime RegisterDate { get; set; }
        public bool Active { get; set; }
        [ScaffoldColumn(false)]
        public string SupplierName { get; set; }

    }
}
