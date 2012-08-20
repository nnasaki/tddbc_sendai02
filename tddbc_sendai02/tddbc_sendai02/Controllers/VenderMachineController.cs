using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VenderMachine.Controllers
{
    /// <summary>
    /// 自動販売機クラス
    /// </summary>
    public class VenderMachineController
    {
        /// <summary>
        /// 投入額の総計。直接代入は出来ない。
        /// int 型の初期値は 0 であることが保障されている。
        /// </summary>
        public int AmountOfMoney { get; private set; }

        /// <summary>
        /// お金を投入する
        /// </summary>
        /// <param name="money"></param>
        public void Insert(int money)
        {
           AmountOfMoney += money;
        }

        /// <summary>
        /// お金を払い戻す。払い戻し後は総計をクリアする。
        /// </summary>
        /// <returns>払い戻し金額</returns>
        public int Refund()
        {
            int change = AmountOfMoney;
            AmountOfMoney = 0;
            return change;
        }
    }
}
