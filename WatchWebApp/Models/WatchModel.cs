using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using static WatchWebApp.Models.WatchModel;

namespace WatchWebApp.Models
{
    public class ImageModel:MainModel
    {
        [Required(ErrorMessage ="Please select image")]
        [Display(Name = "Image")]
        public IFormFile? File { get; set; }
    }
    public class WatchModel : MainModel
    {
        [Display(Name = "Image")]
        public string? Image { get; set; }
    }
    public class ImageModelV2:MainModel
    {
        [Display(Name = "Image")]
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

        [Required]
        [StringLength(100, MinimumLength = 10)]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Short Description")]
        public string? ShortDescription { get; set; }

        [StringLength(500, MinimumLength = 20)]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Full Description")]
        public string? FullDescription { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:C0}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Currency)]
        [Range(1,float.MaxValue, ErrorMessage ="Please enter a value greater than 0")]
        [Display(Name = "Price($)")]
        public decimal Price { get; set; }

        [Required]
        [Display(Name = "Caliber")]
        public string? Caliber { get; set; }

        [Required]
        [Display(Name = "Movement")]
        public string? Movement { get; set; }

        [Required]
        [Display(Name = "Chronograph")]
        public string? Chronograph { get; set; }

        [Required]
        [Range(1, float.MaxValue, ErrorMessage = "Please enter a value greater than 0")]
        [Display(Name = "Weight(kg)")]
        public decimal Weight { get; set; }

        [Required]
        [Range(1, float.MaxValue, ErrorMessage = "Please enter a value greater than 0")]
        [Display(Name = "Height(cm)")]
        public decimal Height { get; set; }

        [Required]
        [Display(Name = "Diameter")]
        [DataType(DataType.Text)]
        public string? Diameter { get; set; }

        [Required]
        [Display(Name = "Thickness(mm)")]
        public decimal Thickness { get; set; }

        [Required]
        [Range(1, float.MaxValue, ErrorMessage = "Please enter a value greater than 0")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Please enter valid Number")]
        [Display(Name = "Jewel")]
        public int Jewel { get; set; }

        [Required]
        [Display(Name = "Case Material")]
        [DataType(DataType.Text)]
        public string? CaseMaterial { get; set; }

        [Required]
        [Display(Name = "Strap Material")]
        [DataType(DataType.Text)]
        public string? StrapMaterial { get; set; }
    }
    public class WatchModel2 : WatchModel
    {
        [Required]
        [Display(Name = "Item Code")]
        public int ItemNo { get; set; }

        [Display(Name = "Image")]
        public IFormFile? File { get; set; }
    }
    public class ResData
    {
        public WatchModel2[] data { get; set; }
    }
    public class BlobUpload
    {
        public IFormFile? file { get; set;}
    }
    public class WatchImage
    {
        public string? Image { get; set;}
        [Required]
        public IFormFile File { get; set;}
        public int ItemNo { get; set; }
    }
}
    
