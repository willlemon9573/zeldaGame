namespace SprintZero1.XMLParsers.XMLEntityBuilder.EventParser
{
    internal class EventInfo
    {
        private int x;
        private int y;
        private string doorDirection;
        public int TriggerX { get { return x; } set { x = value; } }
        public int TriggerY { get { return y; } set { y = value; } }
        public string DoorDirectionToOpen { get { return doorDirection; } set { doorDirection = value; } }

    }
}
