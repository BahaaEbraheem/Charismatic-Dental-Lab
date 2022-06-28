using System.ComponentModel.DataAnnotations;

namespace Charismatic.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}