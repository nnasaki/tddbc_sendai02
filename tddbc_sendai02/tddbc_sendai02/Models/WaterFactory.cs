
namespace VenderMachine.Models
{
    public class WaterFactory : Abstract.IJuiceFactory
    {
        protected override Abstract.IJuice CreateJuice()
        {
            var juice = new Water();
            return juice;
        }
    }
}