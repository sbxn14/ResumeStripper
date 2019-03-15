using System.ComponentModel.DataAnnotations;

namespace ResumeStripper.Models.Enums
{
    public enum LanguageLevel
    {
        Basic,
        Good,

        [Display(Name = "Very Good")]
        VeryGood,

        Native
    }
}