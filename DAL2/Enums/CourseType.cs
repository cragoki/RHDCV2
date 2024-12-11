using System.ComponentModel.DataAnnotations;

namespace DAL.Enums
{
    public enum CourseType
    {
        [Display(Name = "Hurdle")]
        Hurdle = 0,
        [Display(Name = "Flat")]
        Flat = 1,
        [Display(Name = "Dual Purpose")]
        Both = 1
    }
}
