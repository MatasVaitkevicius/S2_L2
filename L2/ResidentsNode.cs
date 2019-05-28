namespace L2
{
    public sealed class ResidentsNode
    {
        public Residents ResidentsData { get; set; }
        public ResidentsNode NextObject { get; set; }

        public ResidentsNode(Residents residentsValue, ResidentsNode nextObjectAddress)
        {
            ResidentsData = residentsValue;
            NextObject = nextObjectAddress;
        }
    }
}
