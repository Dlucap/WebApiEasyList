namespace EasyList.Business.Models
{
  public class Fornecedor : Entity
  {
    public string Nome { get; set; }
    public string NomeFantasia { get; set; }
    public string Cnpj { get; set; }
    public Endereco Endereco { get; set; }
       
    public bool Ativo { get; set; }    
   
  }
}
