using System.ComponentModel.DataAnnotations;

namespace DAL.Enums
{
    public enum AlertType
    {
        [Display(Name = "New Course")]
        NewCourse = 0,
        [Display(Name ="Failed Scrape")]
        FailedScrape = 1
    }
}
