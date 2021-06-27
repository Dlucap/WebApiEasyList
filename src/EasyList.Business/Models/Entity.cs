using System.ComponentModel.DataAnnotations;

namespace EasyList.Business.Models
{
  public abstract class Entity 
  {
    [Key]
    public int Id { get; set; }
  }
}
