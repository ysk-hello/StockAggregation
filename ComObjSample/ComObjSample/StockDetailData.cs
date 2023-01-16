using System;

namespace ComObjSample
{
    public class StockDetailData
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public string StockType { get; set; }

        public double Per { get; set; }

        public double Pbr { get; set; }

        public double DividendYield { get; set; }

        public double CreditRatio { get; set; }

        public DateTime Date { get; set; }

        public DateTime OpenDateTime { get; set; }

        public double OpenPrice { get; set; }

        public DateTime HighDateTime { get; set; }

        public double HighPrice { get; set; }

        public DateTime LowDateTime { get; set; }

        public double LowPrice { get; set; }

        public DateTime CloseDateTime { get; set; }

        public double ClosePrice { get; set; }

        public double Volume { get; set; }

        public double NumberOfContracts { get; set; }
    }
}
