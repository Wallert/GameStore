using GameStore.Core.Validation;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameStore.Core.Entities
{
    public class Products
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int CategoryId {  get; set; }
        [ForeignKey(nameof(CategoryId))]
        public Categories? Categories { get; set; }
        public string Image { get; set; } = "noimage.png";

        [NotMapped]
        [FileExtension]
        public IFormFile? ImageUpload { get; set; }
    }
}
