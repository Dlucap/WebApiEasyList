using EasyList.Business.Interfaces.IRepository;
using EasyList.Business.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EasyList.Test.RepositoryTest
{
  public class CategoriaRepositoryTest
  {
    [Fact]
    public void Categoria_ObterPorIf()
    {
      IList<Categoria> listCategoria = new List<Categoria>() {
          new Categoria()
          {
            Id = Guid.NewGuid(),
            NomeCategoria ="Limpeza",
            Ativo = true
          },
      };

      //Arrange
      Mock<ICategoriaRepository> categoriaMock = new Mock<ICategoriaRepository>();
      categoriaMock.Setup(cat => cat.ObterPorId(It.IsAny<Guid>())).Returns(listCategoria);

      //Act
      var resultado = _categoriaSevices.CategoriaExists(Guid.NewGuid());
      categoriaMock.
      //Assert
      categoriaMock.Verify(s => s.CategoriaExists(Guid.NewGuid()), Times.Once);
      Assert.True(true);
    }
  }
}
