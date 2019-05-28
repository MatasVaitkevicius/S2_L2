namespace L2
{
    public sealed class UtilitiesNode
    {
        public Utilities UtilitiesData { get; set; }
        public UtilitiesNode NextObject { get; set; }
        
        public UtilitiesNode(Utilities utilitiesValue, UtilitiesNode nextObjectAddress)
        {
            UtilitiesData = utilitiesValue;
            NextObject = nextObjectAddress;
        }
    }
}
