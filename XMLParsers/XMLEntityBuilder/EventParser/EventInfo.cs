using System;
using System.Collections.Generic;

namespace SprintZero1.XMLParsers.XMLEntityBuilder.EventParser
{
    internal class EventInfo
    {
        private int x;
        private int y;
        private List<Tuple<int, int>> triggerLocations = new List<Tuple<int, int>>();
        private string doorDirection;
        private List<string> doorDirections = new List<string>();
        public int TriggerX { get { return x; } set { x = value; } }
        public int TriggerY { get { return y; } set { y = value; } }
        public string DoorDirectionToOpen { get { return doorDirection; } set { doorDirection = value; } }

        public void AddToTriggerList(int x, int y)
        {
            triggerLocations.Add(new Tuple<int, int>(x, y));
        }

        public List<Tuple<int, int>> EventTriggers { get { return triggerLocations; } }

        public void AddDirection(string direction)
        {
            doorDirections.Add(direction);
        }
    }
}
