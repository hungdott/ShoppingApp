using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingApp.Model.Models
{
  [Table("Feedbacks")]
  public class Feedback
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ID { set; get; }
    [MaxLength(250, ErrorMessage = "không nhập quá 250 ký tự")]
    [Required(ErrorMessage ="mời nhập trường này")]
    public string Name{ set; get; }
    [MaxLength(250, ErrorMessage = "không nhập quá 250 ký tự")]
    [Required(ErrorMessage = "mời nhập trường này")]
    public string Email { set; get; }
    [MaxLength(500,ErrorMessage ="không nhập quá 500 ký tự")]
    [Required(ErrorMessage = "mời nhập trường này")]
    public string Message { set; get; }
    public DateTime CreatedDate{ set; get; }
    [Required]
    public bool Status{ set; get; }
  }
}
