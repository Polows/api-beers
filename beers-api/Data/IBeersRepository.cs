using System.Collections.Generic;
using System.Threading.Tasks;
using beersapi.Models;
using beers_api.Data;

namespace beersapi.Data
{
	public interface IBeersRepository
	{
		IEnumerable<BeerEntity> Beers { get; }

		Task<int> Add(BeerEntity beer);

		BeerEntity GetById(int id);

		bool ContainsBeer(int id);

		void DeleteBeer(int id);
		IEnumerable<BeerEntity> GetByName(string name);
	}
}