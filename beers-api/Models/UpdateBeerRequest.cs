using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace beersapi.Models
{
    public class UpdateBeerRequest
    {
		[Required]
	    public string Name { get; set; }

		[Range(0.0, 99.0)]
	    public double Abv { get; set; }

		[Required]
	    public int BreweryId { get; set; }
    }
}
