using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VenderMachine.Controllers
{
    public class VenderMachineController
    {
        public int AmountOfMoney { get; set; }

        public void Insert(int p)
        {
           AmountOfMoney += p;
        }

    }
}
