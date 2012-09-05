
namespace VenderMachine.Models
{
    public class Coke : Abstract.Juice
    {

        public override void Setup()
        {
            Name = "Coke";
            Price = 120;
        }
    }
}