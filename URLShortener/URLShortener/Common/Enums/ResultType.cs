using System.ComponentModel.DataAnnotations;

namespace URLShortener.Common
{
    public enum ResultType
    {
        [Display(Name = "Success")]
        Success = 1,

        [Display(Name = "Error")]
        Error = 2,

        [Display(Name = "Warning")]
        Warning = 3
    }
}
