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
        /// <returns>お釣りを表す。お釣りがない場合は0を返す</returns>
        public int Insert(int money)
        {
            if (IsExpectedMoney(money))
            {
                AmountOfMoney += money;
                return 0;
            }
            else
            {
                return money;
            }
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

        /// <summary>
        /// 対象外のお金かどうか判定する
        /// </summary>
        /// <param name="money">お金</param>
        /// <returns>true:期待通り、false:対象外のお金</returns>
        public bool IsExpectedMoney(int money)
        {
            List<int> expectedMoney = new List<int>{ 10, 50, 100, 500, 1000 };
            return expectedMoney.Contains(money);
        }
    }
}
