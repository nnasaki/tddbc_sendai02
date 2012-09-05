
namespace VenderMachine.Models
{
    public class WaterFactory : Abstract.JuiceFactory
    {
        protected override Abstract.Juice CreateJuice()
        {
            var juice = new Water();
            return juice;
        }
    }
}