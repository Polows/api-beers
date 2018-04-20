using System;
using System.Collections.Generic;
using System.Text;

namespace beers_api.Data
{
    public class BreweryEntity
    {
		public int Id { get; set; }

		public  string Name { get; set; }

		public IEnumerable<BeerEntity> Beers { get; set; }
    }
}
