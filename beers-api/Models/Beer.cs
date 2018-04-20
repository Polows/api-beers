using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace beersapi.Models
{
    public class Beer : IValidatableObject	
    {
		public int Id { get; set; }

		[Required]
	    public string Name { get; set; }

		[Range(0.0, 99.0)]
	    public double Abv { get; set; }

	    public string BreweryName { get; set; }

	    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
	    {
			var list = new List<ValidationResult>();

		    if (Name.Contains("Free") && Abv > 0.0)
		    {
			    var result = new ValidationResult("Abv must be 0.0", new []{ "Abv"});
				list.Add(result);
		    }
		    return list;
	    }
    }
}
