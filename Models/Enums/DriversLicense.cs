using System.ComponentModel.DataAnnotations;

namespace ResumeStripper.Models.Enums
{
    public enum DriversLicense
    {
        None,
        A,
        A1,
        A2,
        AM,
        B,
        BE,

        [Display(Name = "B+")]
        Bplus,

        C,
        CE,
        C1,
        C1E,
        D,
        DE,
        D1,
        D1E,
        T
    }
}