using System;
using System.Collections.Generic;
using System.Linq;
using WarCroft.Constants;
using WarCroft.Entities.Items;

namespace WarCroft.Entities.Inventory
{
    public abstract class Bag : IBag
    {
        private readonly List<Item> _items;

        protected Bag(int capacity)
        {
            Capacity = capacity;
            _items = new List<Item>();
        }

        public int Capacity { get; set; } = 100;

        public int Load => _items.Sum(x => x.Weight);

        public IReadOnlyCollection<Item> Items => _items;

        public void AddItem(Item item)
        {
            if ((item.Weight + Load) > Capacity)
            {
                throw new InvalidOperationException(ExceptionMessages.ExceedMaximumBagCapacity);
            }

            _items.Add(item);
        }

        public Item GetItem(string name)
        {
            if (_items.Count == 0)
            {
                throw new InvalidOperationException(ExceptionMessages.EmptyBag);
            }

            if (_items.Any(x=>x.GetType().Name == name) == false)
            {
                throw new ArgumentException(string.Format(ExceptionMessages.ItemNotFoundInBag,name));
            }

            Item item = _items.FirstOrDefault(x => x.GetType().Name == name);

            _items.Remove(item);

            return item;

        }
    }
}
