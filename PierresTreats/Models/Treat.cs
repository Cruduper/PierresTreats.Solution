using System;
using System.Collections.Generic;

namespace PierresTreats.Models
{
  public class Treat
  {

    public Treat()
    {
      this.JoinEntities = new HashSet<FlavorTreat>();
    }
    public int TreatId { get; set; }
    public string Name { get; set; }
    public int Price { get; set; }
    public string Description { get; set; }

    public virtual ICollection<FlavorTreat> JoinEntities { get; set;}
  }

}