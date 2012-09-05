
namespace VenderMachine.Models
{
    public class Water : Abstract.Juice
    {

        public override void Setup()
        {
            Name = "Water";
            Price = 100;
        }
    }
}