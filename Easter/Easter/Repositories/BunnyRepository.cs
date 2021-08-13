using System.Collections.Generic;
using System.Linq;
using Easter.Models.Bunnies.Contracts;
using Easter.Repositories.Contracts;

namespace Easter.Repositories
{
    public class BunnyRepository : IRepository<IBunny>
    {
        private readonly List<IBunny> _bunnies;
        public BunnyRepository()
        {
            _bunnies = new List<IBunny>();
        }

        public IReadOnlyCollection<IBunny> Models { get; private set; } =  new List<IBunny>();
        public void Add(IBunny model)
        {
            _bunnies.Add(model);
            Models = _bunnies;
        }

        public bool Remove(IBunny model)
        {
            if (_bunnies.Contains(model))
            {
                _bunnies.Remove(model);
                Models = _bunnies;
                return true;
            }

            return false;
        }

        public IBunny FindByName(string name)
        {
            return _bunnies.FirstOrDefault(x => x.Name == name);
        }
    }
}
