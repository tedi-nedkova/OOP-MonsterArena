using MonsterArena.Inventory.SpecialItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterArena.Inventory
{
    public class Bag<T> where T : Item
    {
        private List<T> items;

        public List<T> Items
        {
            get { return items; }
            private set { items = value; }
        }

        private List<T> defeatedMonsters;

        public List<T> DefeatedMonsters
        {
            get { return defeatedMonsters; }
            set { defeatedMonsters = value; }
        }

        public Bag()
        {
            Items = new List<T>();
            DefeatedMonsters = new List<T>();
        }

        public void Add(T item)
        {
            Items.Add(item);
        }

        public void Remove(T item)
        {
            Items.Remove(item);
        }
    }
}
