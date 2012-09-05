
using System.Collections.Generic;

namespace VenderMachine.Models.Abstract
{
    public abstract class JuiceFactory
    {
        public Juice Create()
        {
            var juice = CreateJuice();
            juice.Setup();
            return juice;
        }

        public IList<Juice> Create(int canOfJuice)
        {
            var juiceList = new List<Juice>();

            for (var i = 0; i < canOfJuice; i++)
            {
                var juice = CreateJuice();
                juice.Setup();
                juiceList.Add(juice);
            }

            return juiceList;
        }

        abstract protected Juice CreateJuice();
    }
}