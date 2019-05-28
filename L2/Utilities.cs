namespace L2
{
    public class Utilities
    {
        public string ServiceCode { get; set; }
        public string ServiceName { get; set; }
        public double ServiceUnitPrice { get; set; }

        public Utilities (string serviceCode, string serviceName, double serviceUnitPrice)
        {
            ServiceCode = serviceCode;
            ServiceName = serviceName;
            ServiceUnitPrice = serviceUnitPrice;
        }

        public string UtilitiesPrintToTable()
        {
            return $"| {ServiceCode, 15} | {ServiceName, -25} | {ServiceUnitPrice, 30} |";
        }
    }
}
