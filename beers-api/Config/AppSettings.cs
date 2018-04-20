using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace beersapi.Config
{
    public class AppSettings
    {
	    public string Name { get; set; }

	    public string Server { get; set; }

	    public AddressSettings Address { get; set; }
    }
}
