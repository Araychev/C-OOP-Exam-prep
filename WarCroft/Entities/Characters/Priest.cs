using WarCroft.Entities.Characters.Contracts;
using WarCroft.Entities.Inventory;
using WarCroft.Entities.Items;

namespace WarCroft.Entities.Characters
{
   public class Priest : Character, IHealer
    {
        public Priest(string name) : base(name, 50, 25, 40, new Backpack())
        {
        }

        public void Heal(Character character)
        {
            EnsureAlive();

            character.UseItem(new HealthPotion());
            character.UseItem(new HealthPotion());
        }
    }
}
