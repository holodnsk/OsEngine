using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OsEngine.Entity;
using OsEngine.Market;
using OsEngine.OsTrader.Panels.Tab;

namespace OsEngine.OsTrader.Panels
{
    class ThreeZoldatensRobot:BotPanel
    {
        // Вход в лонг:
        // Три подряд зеленые свичи и Close последней свечи выше 20 последних свечек
        // Выход:
        // Три подряд красные свечи

        private BotTabSimple _tab;
        public ThreeZoldatensRobot(string name, StartProgram startProgram) : base(name, startProgram)
        {
            TabCreate(BotTabType.Simple);
            _tab = TabsSimple[0];
            _tab.CandleFinishedEvent += Strateg_CandleFinishedEvent;
            _tab.PositionOpeningSuccesEvent += Strategy_PositionOpeningSuccesEvent;

        }

        private void Strategy_PositionOpeningSuccesEvent(Position position)
        {
            TabsSimple[0].CloseAtStop(position,_stopPrice,_stopPrice);
        }

        private void Strateg_CandleFinishedEvent(List<Candle> candles)
        {

            // если есть открытая позиция
            if (TabsSimple[0].PositionsOpenAll != null && TabsSimple[0].PositionsOpenAll.Count != 0)
            {
                // если три последние свечи были падающие
                for (int i = candles.Count - 1; i > candles.Count - 3; i--)
                {
                    if (candles[i].Open < candles[i].Close) //если расчетная свеча растущая
                        return;
                }
                TabsSimple[0].CloseAllAtMarket(); // 
            }
            
            // фильтр если свечей меньше 20
            if (candles.Count <20)
                return;
            
            // фильтр чтоб три последние свечи были растущие
            for (int i = candles.Count-1; i >candles.Count-3; i--)
            {
                if (candles[i].Open > candles[i].Close) //если расчетная свеча падающая
                    return;
            }

            // фильтр чтоб Close последней свечи выше 20 последних свечек
            Decimal lastClose = candles[candles.Count - 1].Close;
            for (int i = candles.Count-1; i > candles.Count-20; i--)
            {
                if (lastClose < candles[i].High)
                    return;
            }

            TabsSimple[0].BuyAtMarket(1);
            _stopPrice = candles[candles.Count - 1].Low - TabsSimple[0].Securiti.PriceStep;

        }

        public decimal _stopPrice;

        public override string GetNameStrategyType()
        {
            return "ThreeZoldatensRobot";
        }

        public override void ShowIndividualSettingsDialog()
        {
            
        }
    }
}
