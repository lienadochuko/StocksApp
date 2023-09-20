namespace StocksApp.Models
{
    public class Stock
    {
        public string StockSymbol { get; set; }
        public double? CurrentPrice { get; set; }
        public double? LowestPrice { get; set; }
        public double? HighestPrice { get; set; }
        public double? OpenPrice { get; set; }

        //public int? c { get; set; }
        //public int? d { get; set; }
        //public int? dp { get; set; }
        //public int? h { get; set; }
        //public int? l { get; set; }
        //public int? o { get; set; }
        //public int? pc { get; set; }
        //public int? t { get; set; }
    }
}
