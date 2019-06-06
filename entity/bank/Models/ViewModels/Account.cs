using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace bank.Models
{
  public class Account
  {
    public string Username {get;set;}

    public string Balance {get;set;}
    public List<History> History {get;set;}
    public ChangeForm changeForm {get;set;}
  }
}