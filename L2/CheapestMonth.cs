namespace L2
{
    class CheapestMonth
    {
        public string Month { get; set; }
        public string UtilityCode { get; set; }
        public double Price { get; set; }

        public CheapestMonth(string month, string utilityCode, double price)
        {
            Month = month;
            UtilityCode = utilityCode;
            Price = price;
        }
    }
}
