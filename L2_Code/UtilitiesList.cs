namespace L2_Code
{
    public sealed class UtilitiesList
    {
        private UtilitiesNode Start;
        private UtilitiesNode End;
        private UtilitiesNode ListInterface;

        public UtilitiesList()
        {
            Start = null;
            End = null;
            ListInterface = null;
        }

        public void AddData(Utilities newUtility)
        {
            var newNode = new UtilitiesNode(newUtility, null);
            if (Start != null)
            {
                End.NextObject = newNode;
                End = newNode;
            }
            else
            {
                Start = newNode;
                End = newNode;
            }
        }

        public void StartOfList()
        {
            ListInterface = Start;
        }

        public void NextListNode()
        {
            ListInterface = ListInterface.NextObject;
        }

        public bool Contains()
        {
            return ListInterface != null;
        }

        public Utilities GetData()
        {
            return ListInterface.UtilitiesData;
        }
    }
}
