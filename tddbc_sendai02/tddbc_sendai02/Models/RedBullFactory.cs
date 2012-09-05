
namespace VenderMachine.Models
{
    public class RedBullFactory : Abstract.JuiceFactory
    {
        protected override Abstract.Juice CreateJuice()
        {
            var juice = new RedBull();
            return juice;
        }
    }
}