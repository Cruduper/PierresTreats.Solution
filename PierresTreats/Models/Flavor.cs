using System;
using System.Collections.Generic;

namespace PierresTreats.Models
{
  public class Flavor
  {

    public Flavor()
    {
      this.JoinEntities = new HashSet<RecipeIngredient>();
    }
    public int FlavorId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    public virtual ICollection<RecipeIngredient> JoinEntities { get; set;}
  }

}