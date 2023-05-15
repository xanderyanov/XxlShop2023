using System.ComponentModel.DataAnnotations;

namespace XxlStore.Models
{
    public class Message
    {
        [Required(ErrorMessage = "Please enter the email address")]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "Please enter the subject")]
        public string Subject { get; set; }
        
        [Required(ErrorMessage = "Please enter the text")]
        public string Text { get; set; }
    }
}
