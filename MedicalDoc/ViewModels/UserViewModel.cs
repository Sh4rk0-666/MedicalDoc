using MedicalDoc.Helpers;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace MedicalDoc.ViewModels
{
    public class UserViewModel : AppResult
    {
        [Required]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

    }
}
