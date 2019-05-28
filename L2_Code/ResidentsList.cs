namespace L2_Code
{
    public sealed class ResidentsList
    {
        private ResidentsNode Start;
        private ResidentsNode End;
        private ResidentsNode ListInterface;

        public ResidentsList()
        {
            Start = null;
            End = null;
            ListInterface = null;
        }

        public void AddData(Residents newResident)
        {
            var newNode = new ResidentsNode(newResident, null);

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

        public void RemoveResident(ResidentsNode residentNode)
        {
            if (Start == residentNode)
            {
                Start = Start.NextObject;
                residentNode.NextObject = null;
            }
            else if (End == residentNode)
            {
                var elementBefore = ElementBefore(residentNode);

                if (elementBefore != null)
                {
                    elementBefore.NextObject = null;
                    End = elementBefore;
                    residentNode.NextObject = null;
                }
            }
            else
            {
                var elementBefore = ElementBefore(residentNode);
                elementBefore.NextObject = residentNode.NextObject;
                residentNode.NextObject = null;
            }
        }

        public ResidentsNode GetNodeLocation(Residents resident)
        {
            for (ResidentsNode node = Start; node != null; node = node.NextObject)
            {
                if (node.ResidentsData == resident)
                {
                    return node;
                }
            }

            return null;
        }

        public bool IsLastNode(Residents resident)
        {
            for (ResidentsNode node = Start; node != null; node = node.NextObject)
            {
                if (End.ResidentsData == resident)
                {
                    return false;
                }
            }

            return true;
        }

        public ResidentsNode ElementBefore(ResidentsNode node)
        {
            ResidentsNode nodeBefore;

            for (nodeBefore = Start; nodeBefore.NextObject != node; nodeBefore = nodeBefore.NextObject)
            {
            }

            return nodeBefore;
        }

        public void StartOfList()
        {
            ListInterface = Start;
        }

        public ResidentsNode ReturnStart()
        {
            return Start;
        }

        public void NextListNode()
        {
            ListInterface = ListInterface.NextObject;
        }

        public void NextList()
        {
            if (ListInterface != null)
            {
                ListInterface = ListInterface.NextObject;
            }
            else if(End != null)
            {
                ListInterface == 
            }
        }

        public bool Contains()
        {
            return ListInterface != null;
        }

        public bool ContainsEverywhere()
        {
            return ListInterface != null || Start != null;
        }

        public Residents GetData()
        {
            return ListInterface.ResidentsData;
        }

        public void CalculateMoneySpent(Utilities utilities)
        {
            for (var node = Start; node != null; node = node.NextObject)
            {
                if (node.ResidentsData.UtilityCode == utilities.ServiceCode)
                {
                    node.ResidentsData.MoneySpent = node.ResidentsData.ServiceCount * utilities.ServiceUnitPrice;
                }
            }
        }

        public void Sort()
        {
            for (var firstNode = Start; firstNode != null; firstNode = firstNode.NextObject)
            {
                var min = firstNode;

                for (var secondNode = Start; secondNode != null; secondNode = secondNode.NextObject)
                {
                    if (secondNode.ResidentsData > min.ResidentsData)
                    {
                        min = secondNode;
                    }

                    var resident = firstNode.ResidentsData;
                    firstNode.ResidentsData = min.ResidentsData;
                    min.ResidentsData = resident;
                }
            }
        }
    }
}
