using EasyList.Api.Data;
using Microsoft.EntityFrameworkCore;
using System;

namespace EasyList.Data
{
  public class EstoqueCompraMapping : IEntityTypeConfiguration<Estoque>
  {
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Estoque> builder)
    {
      throw new NotImplementedException();
    }
  }
}
