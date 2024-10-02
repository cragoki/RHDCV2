using System.ComponentModel.DataAnnotations;

namespace DAL.Enums
{
    public enum CourseType
    {
        [Display(Name = "Generic")]
        Generic = 0,
        [Display(Name ="All Weather")]
        AllWeather = 1
    }
}
