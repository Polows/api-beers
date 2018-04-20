using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using beersapi.Models;
using beers_api.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace beersapi.Data
{
	public class BeersRepository : IBeersRepository
	{
		/*
		 Refactory
		private static BeersRepository _instance;

		public static BeersRepository Instance => _instance;

		static BeersRepository()
		{
			_instance = new BeersRepository();
		}
		*/

		private readonly BeersContext _db;
		//private List<Beer> _beers;

		public BeersRepository(BeersContext db) => _db = db;
		/*
		{
			_beers = new List<Beer>();
			_beers.AddRange(new[]
				{
					new Beer
					{
						Id= 1, Name = "Voll Damm", Abv = 7.4
					},
					new Beer
					{
						Id = 2, Name = "Cruzcampo", Abv = 4.5
					}
				}
			);
		}
		*/

		//public IEnumerable<Beer> Beers => _beers;
		//public Beer GetById(int id) => _beers.SingleOrDefault(b => b.Id == id);

		//public IEnumerable<BeerEntity> Beers => _db.Beers.AsEnumerable();
		public IEnumerable<BeerEntity> Beers => _db.Beers.Include(x => x.Brewery).AsEnumerable();

		public IEnumerable<BeerEntity> GetByName(string name)
		{
			return _db.Beers.Where(x => x.Name.ToLowerInvariant().StartsWith(name.ToLowerInvariant())).AsEnumerable();
		}

		public DbSet<BeerEntity> BeerAsDbSet => _db.Beers;

		public BeerEntity GetById(int id) => _db.Beers.SingleOrDefault(b => b.Id == id);

		public async Task<int> Add(BeerEntity beer)
		{
			_db.Beers.Add(beer);
			await _db.SaveChangesAsync();
			return beer.Id;
		}

		public bool ContainsBeer(int id)
		{
			return _db.Beers.Any(b => b.Id == id);
		}

		public void DeleteBeer(int id)
		{
			var beer = _db.Beers.Single(b => b.Id == id);
			_db.Remove(beer);
			_db.SaveChanges();
		}

	}

}
