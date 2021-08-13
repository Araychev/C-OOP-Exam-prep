using System.Collections.Generic;
using System.Linq;
using Easter.Models.Eggs.Contracts;
using Easter.Repositories.Contracts;

namespace Easter.Repositories
{
   public class EggRepository : IRepository<IEgg>
   {
       private readonly List<IEgg> _eggs;
        public EggRepository()
        {
            _eggs = new List<IEgg>();
        }

        public IReadOnlyCollection<IEgg> Models { get; private set; } = new List<IEgg>();
        public void Add(IEgg model)
        {
            _eggs.Add(model);
            Models = _eggs;

        }

        public bool Remove(IEgg model)
        {
            if (_eggs.Contains(model))
            {
                _eggs.Remove(model);
                Models = _eggs;
                return true;
            }

            return false;
        }

        public IEgg FindByName(string name)
        {
                return _eggs.FirstOrDefault(b => b.Name == name);
        }
        
    }
}
