using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VenderMachine.Models;

namespace VenderMachine.Controllers
{
    /// <summary>
    /// 自動販売機クラス
    /// </summary>
    public class VenderMachineController
    {
        /// <summary>
        /// 投入可能なお金の定義
        /// </summary>
        private static readonly List<int> expectedMoney = new List<int> { 10, 50, 100, 500, 1000 };

        /// <summary>
        /// 投入額の総計。直接代入は出来ない。
        /// int 型の初期値は 0 であることが保障されている。
        /// </summary>
        public int AmountOfMoney { get; private set; }

        /// <summary>
        /// ジュースの在庫情報を格納する
        /// </summary>
        public IList<Juice> StockOfJuice { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public VenderMachineController()
        {
            StockOfJuice = new List<Juice>()
            {
                new Juice { Name="Coke", Price=120},
                new Juice { Name="Coke", Price=120},
                new Juice { Name="Coke", Price=120},
                new Juice { Name="Coke", Price=120},
                new Juice { Name="Coke", Price=120}
            };
        }

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

        #region "private メソッド"
        /// <summary>
        /// 対象外のお金かどうか判定する
        /// </summary>
        /// <param name="money">お金</param>
        /// <returns>true:期待通り、false:対象外のお金</returns>
        private bool IsExpectedMoney(int money)
        {
            return expectedMoney.Contains(money);
        }
        #endregion

    }
}
