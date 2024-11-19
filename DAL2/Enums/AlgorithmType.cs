using System.ComponentModel.DataAnnotations;

namespace DAL.Enums
{
    public enum AlgorithmType
    {
        [Display(Name = "Alphabetical")]
        Alphabetical = 0,
        [Display(Name ="Bentners Model")]
        BentnersModel = 1
    }
}
