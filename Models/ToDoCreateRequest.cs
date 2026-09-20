using System.ComponentModel.DataAnnotations;

namespace TodoApi.Models
{
    public class ToDoCreateRequest
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Title is required to create a To Do Item.")]
        [StringLength(200, MinimumLength = 1, ErrorMessage = "Title must be between 1 and 100 characters.")]
        public required string Title { get; set; } = string.Empty;

        [Required(AllowEmptyStrings = false, ErrorMessage = "Description is required to create a To Do Item.")]
        [StringLength(200, MinimumLength = 1, ErrorMessage = "Description must be between 1 and 200 characters.")]
        public required string Description { get; set; } = string.Empty;     
        
    }
}
