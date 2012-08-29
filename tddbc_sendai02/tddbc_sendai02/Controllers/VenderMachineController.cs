﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VenderMachine.Models;
using VenderMachine.Models.Abstract;

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
        public IList<IJuice> StockOfJuice { get; set; }

        /// <summary>
        /// 売り上げ金額
        /// </summary>
        public int SaleAmount { get; private set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public VenderMachineController()
        {
            var cokeFactory = new CokeFactory();
            var redBullFactory = new RedBullFactory();
            var waterFactory = new WaterFactory();
            StockOfJuice = new List<IJuice>()
            {
                cokeFactory.Create(),
                cokeFactory.Create(),
                cokeFactory.Create(),
                cokeFactory.Create(),
                cokeFactory.Create(),
                redBullFactory.Create(),
                redBullFactory.Create(),
                redBullFactory.Create(),
                redBullFactory.Create(),
                redBullFactory.Create(),
                waterFactory.Create(),
                waterFactory.Create(),
                waterFactory.Create(),
                waterFactory.Create(),
                waterFactory.Create()
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

        /// <summary>
        /// ジュースの在庫情報を返す
        /// </summary>
        public IList<StockOfJuiceInfo> GetStockOfJuiceInfo()
        {
            // Linq でジュースの在庫コレクションを名前と値段でグルーピングする。
            // SQL の GROUP BY と同じ。
            var statistics = StockOfJuice.GroupBy(x => new { x.Name, x.Price });
            var stockList = new List<StockOfJuiceInfo>();
            foreach (var item in statistics)
            {
                var stockOfJuiceInfo = new StockOfJuiceInfo()
                {
                    Name = item.Key.Name,
                    Price = item.Key.Price,
                    CanOfJuice = item.Count()
                };
                stockList.Add(stockOfJuiceInfo);
            }
            
            return stockList;

        }

        /// <summary>
        /// 在庫と投入金額から商品が買えるか判定する
        /// </summary>
        /// <param name="juice"></param>
        /// <returns>true:商品が買える false:商品が買えない</returns>
        public bool IsPurchase(IJuice juice)
        {
            var stock = GetStockOfJuiceInfo().SingleOrDefault(x=>x.Name == juice.Name);
            if (IsSufficientStock(juice, stock))
            {
                if (IsSufficientMoney(juice))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// ジュースを購入する
        /// </summary>
        /// <param name="coke"></param>
        public void Purchase(IJuice coke)
        {
            if (IsPurchase(coke))
            {
                AmountOfMoney -= coke.Price;
                SaleAmount += coke.Price;
                StockOfJuice.Remove(coke);
            }

        }

        /// <summary>
        /// 購入可能なジュースのリストを取得する
        /// </summary>
        /// <returns></returns>
        public IList<IJuice> GetAvailableForPurchaseList()
        {
            return StockOfJuice.Where(x => x.Price <= AmountOfMoney).ToList();
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

        /// <summary>
        /// 在庫が十分かどうか確認する
        /// </summary>
        /// <param name="juice"></param>
        /// <param name="stock"></param>
        /// <returns>true:十分 false:不十分</returns>
        private bool IsSufficientStock(IJuice juice, StockOfJuiceInfo stock)
        {
            if (stock == null)
            {
                return false;
            }

            if (stock.Name == juice.Name)
            {
                if (stock.CanOfJuice > 0)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// お金が十分かどうか確認する
        /// </summary>
        /// <param name="juice"></param>
        /// <returns>true:十分 false:不十分</returns>
        private bool IsSufficientMoney(IJuice juice)
        {
            if (AmountOfMoney >= juice.Price)
            {
                return true;
            }

            return false;
        }
        #endregion
    }
}
