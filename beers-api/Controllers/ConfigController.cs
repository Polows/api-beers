using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace beersapi.Controllers
{
	[Route("[controller]")]
    public class ConfigController: Controller
    {
	    private readonly  IConfiguration _configuration;

	    public ConfigController(IConfiguration configuration)
	    {
		    _configuration = configuration;
	    }

	    [HttpGet]
	    public IActionResult GetConfig()
	    {
			/*
		    var items = new List<string>();
		    foreach (var item in _configuration.AsEnumerable())
		    {
			    items.Add($"[{item.Key}]=[${item.Value}]");
		    }
			*/
		    var items = _configuration.AsEnumerable()
			    .Select(kvp => $"[{kvp.Key}]=[{kvp.Value}]")
			    .ToList();

		    return Ok(items);
	    }
    }
}
