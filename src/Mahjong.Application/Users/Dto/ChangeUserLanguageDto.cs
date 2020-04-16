using System.ComponentModel.DataAnnotations;

namespace Mahjong.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}