using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Xml.Linq;
using static WatchWebApp.Models.WatchModel;

namespace WatchWebApp.Models
{
    public class ImageModel:MainModel
    {
        [Display(Name = "Upload image")]
        public IFormFile? File { get; set; }
    }
    public class WatchModel : MainModel
    {
        [Display(Name = "Upload image")]
        public string? Image { get; set; }
    }
    public class ImageModelV2:MainModel
    {
        [Display(Name = "Upload image")]
        public IFormFile? File { get; set; }

        [Required]
        [Display(Name = "Item Code")]
        [Range(0, Int32.MaxValue, ErrorMessage = "Please input valid integer")]
        public int ItemNo { get; set; }

        [Display(Name = "Upload image")]
        public string? Image { get; set; }
    }
    public class MainModel
    {
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Item Name")]
        public string? ItemName { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Short Description")]
        [Required]
        public string? ShortDescription { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Full Description")]
        public string? FullDescription { get; set; }

        [Range(0, float.MaxValue, ErrorMessage = "Please valid input number")]
        [Display(Name = "Price")]
        [Required]
        public decimal Price { get; set; }

        [Display(Name = "Caliber")]
        [DataType(DataType.Text)]
        [Required]
        public string? Caliber { get; set; }

        [Display(Name = "Movement")]
        [DataType(DataType.Text)]
        [Required]
        public string? Movement { get; set; }

        [Display(Name = "Chronograph")]
        [DataType(DataType.Text)]
        [Required]
        public string? Chronograph { get; set; }

        [Display(Name = "Weight")]
        [DataType(DataType.Text)]
        [Required]
        public string? Weight { get; set; }

        [Display(Name = "Height")]
        [DataType(DataType.Text)]
        [Required]
        public string? Height { get; set; }

        [Display(Name = "Diameter")]
        [DataType(DataType.Text)]
        [Required]
        public string? Diameter { get; set; }

        [Display(Name = "Thickness")]
        [DataType(DataType.Text)]
        [Required]
        public string? Thickness { get; set; }

        [Display(Name = "Jewel")]
        [Range(0, Int32.MaxValue, ErrorMessage = "Please input valid integer")]
        [Required]
        public int Jewel { get; set; }

        [Display(Name = "Case Material")]
        [DataType(DataType.Text)]
        [Required]
        public string? CaseMaterial { get; set; }

        [Display(Name = "Strap Material")]
        [DataType(DataType.Text)]
        public string? StrapMaterial { get; set; }
    }
    public class WatchModel2 : WatchModel
    {
        [Required]
        [Display(Name = "Item Code")]
        [Range(0, Int32.MaxValue, ErrorMessage = "Please input valid integer")]
        public int ItemNo { get; set; }
    }
    public class ResData
    {
        public WatchModel2[] data { get; set; }
    }
}
    
