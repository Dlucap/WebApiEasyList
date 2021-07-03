using System;
using System.ComponentModel.DataAnnotations;

namespace EasyList.Business.Models
{
  public abstract class Entity 
  {
    protected Entity()
    {
      Id = Guid.NewGuid();
    }

    public Guid Id { get; set; }
  }
}
