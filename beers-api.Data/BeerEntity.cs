using System;

namespace beers_api.Data
{
    public class BeerEntity
    {
	    public int Id { get; set; }

	    public string Name { get; set; }

	    public double Abv { get; set; }

		public int BreweryId { get; set; }

	    public BreweryEntity Brewery { get; set; }
    }
}
