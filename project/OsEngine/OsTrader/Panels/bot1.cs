using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using OsEngine.Entity;
using OsEngine.Market;
using OsEngine.OsTrader.Panels.Tab;
using ru.micexrts.cgate.message;

namespace OsEngine.OsTrader.Panels
{
    class bot1 : BotPanel
    {
        private BotTabSimple _tab;
        public bot1(string name, StartProgram startProgram) : base(name, startProgram)
        {
            /* BotTabSimple  - является базовой реализацией вкладки или другими словами стандартом для написания множества базовых стратегий, чаще всего это и нужно.
             У BotTabSimple может быть всего один источник (инструмент), над которым мы можем производить торговые операции.
             Также существуют другие реализации такие 
             BotTabIndex – это еще один вид реализации вкладки. Эта реализация позволяет подгружать множественные источники (инструменты) и строить из них индекс (синтетику) 
             У одной панели (бота) могут быть несколько вкладок (табов). Например: 3 BotTabSimple и 1 BotTabIndex  */
            TabCreate(BotTabType.Simple);
            _tab = TabsSimple[0];
            _tab.CandleFinishedEvent += TradeLogic;
        }

        private void TradeLogic(List<Candle> candles)
        {
            if (candles.Count < 5)
                return;

            // если уже есть открытые позиции - закрываем и выходим
            if (_tab.PositionsOpenAll != null && _tab.PositionsOpenAll.Count != 0)
            {
                _tab.CloseAllAtMarket();
                return;
            }

            // если закрытие последней свечи выше закрытия предыд - покупаем
            if (candles[candles.Count - 1].Close > candles[candles.Count - 2].Close)
                _tab.BuyAtMarket(1);

            // если закрытие последней свечи ниже закрытия предыд - продаем
            if (candles[candles.Count - 1].Close < candles[candles.Count - 2].Close)
                _tab.SellAtMarket(1);
        }

        

        public override string GetNameStrategyType()
        {
            return "bot1";
        }

        public override void ShowIndividualSettingsDialog()
        {
            MessageBox.Show("e cnhfntubb bot1 пока нет настроек");
        }
    }
}
