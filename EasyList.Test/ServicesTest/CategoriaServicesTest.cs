using EasyList.Business.Interfaces.IRepository;
using EasyList.Business.Interfaces.IServices;
using EasyList.Business.Services;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace EasyList.Test
{

  public class CategoriaServicesTest
  {
    private readonly ICategoriaService _categoriaSevices;

    public CategoriaServicesTest(ICategoriaService categoriaSevices)
    {
      _categoriaSevices = categoriaSevices;
    }

    [Fact]
    public void Categoria_VerificarSeCategoriaExiste()
    {
      
      //Arrange
      Mock<ICategoriaService> categoriaMock = new Mock<ICategoriaService>();
      categoriaMock.Setup(cat => cat.CategoriaExists(It.IsAny<Guid>())).Returns(Task.FromResult(true));

      //Act
      var resultado =  _categoriaSevices.CategoriaExists(Guid.NewGuid());
      categoriaMock.
      //Assert
      categoriaMock.Verify(s => s.CategoriaExists(Guid.NewGuid()), Times.Once);
      Assert.True(true);

    }
  }
}
