
namespace VenderMachine.Models
{
    public class CokeFactory : Abstract.JuiceFactory
    {
        protected override Abstract.Juice CreateJuice()
        {
            var juice = new Coke();
            return juice;
        }
    }
}