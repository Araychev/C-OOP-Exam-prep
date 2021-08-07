namespace AquaShop.Models.Fish
{
   public class FreshwaterFish : Fish
   {
       private const int FreshWaterInitialSize = 3;

       public FreshwaterFish(string name, string species, decimal price) 
           : base(name, species, price)
       {
           this.Size = FreshWaterInitialSize;
       }

        public override void Eat()
        {
            Size += 3;
        }
    }
}
