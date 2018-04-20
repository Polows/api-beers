using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using beersapi.Config;
using beersapi.Data;
using beersapi.Filters;
using beersapi.Models;
using beers_api.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace beersapi.Controllers
{
	[Route("[controller]")]
	public class BeersController : Controller
	{
		private readonly IBeersRepository _beersRepository;
		private readonly BeersSettings _settings;

		public BeersController(IBeersRepository repository, IOptions<BeersSettings> settings)
		{
			_beersRepository = repository;
			_settings = settings.Value;
		}

		[HttpGet]
		//[OnlyJson]
		public IActionResult GetAll()
		{
			/*
			var beers = ((BeersRepository)_beersRepository).BeerAsDbSet;
			return Ok(beers.First().Brewery.Name);
			*/
			//return Ok(_beersRepository.Beers);
			
			return Ok(_beersRepository.Beers.Select(e => new Beer()
			{
				Name = e.Name,
				Abv = e.Abv,
				Id = e.Id,
				BreweryName = e.Brewery?.Name
			}));
		}

		[HttpGet("{id}")]
		public IActionResult FilterBeersByName(string name)
		{
			var beers = _beersRepository.GetByName(name).Select(
				e => new Beer()
				{
					Name = e.Name,
					Abv = e.Abv,
					Id = e.Id,
					BreweryName = e.Brewery?.Name
				});
			return Ok(beers);
		}

		[HttpGet("{id}")]
		public IActionResult GetBeer(int id)
		{
			var beer = _beersRepository.GetById(id);
			//var beer = _db.Beers.SingleOrDefault(b => b.Id == id);
			if (beer == null)
			{
				return NotFound($"Beer whith id {id} not found");
			}
			return Ok(beer);
		}

		[HttpPost]
		public async Task<IActionResult> AddBeer([FromBody] UpdateBeerRequest beer)
		{
			if (ModelState.IsValid)
			{
				var entity = new BeerEntity()
				{
					Abv = beer.Abv,
					Name = beer.Name,
					BreweryId = beer.BreweryId
				};
				await _beersRepository.Add(entity);
					/*
				await _beersRepository.Add(new BeerEntity()
				{
					Abv = beer.Abv,
					Name = beer.Name,
					BreweryId =  beer.BreweryId
				});
				*/
					//return StatusCode((int)System.Net.HttpStatusCode.Created);
				return new StatusCodeResult((int) System.Net.HttpStatusCode.Created);
			}
			else
			{
				return BadRequest();
			}
		}

		[HttpDelete]
		public IActionResult DeleteBeer(int id)
		{
			if (_beersRepository.ContainsBeer(id))
			{
				_beersRepository.DeleteBeer(id);
				return Ok($"Beer with id {id} deleted");
			}
			return NotFound($"Beer whith id {id} not found");
		}

		//[HttpPost("test")]
		//public IActionResult Test([FromBody] Guid guid)
		//{
		//	return Ok("Guid :" + guid.ToString());
		//}
	}
}
