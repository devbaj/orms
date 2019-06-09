using System.ComponentModel.DataAnnotations;

namespace weddingplanner.Models
{
  public class Login
  {
    public string Email {get;set;}

    [DataType(DataType.Password)]
    public string Password {get;set;}
  }
}