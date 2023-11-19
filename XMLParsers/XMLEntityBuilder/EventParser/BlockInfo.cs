namespace SprintZero1.XMLParsers.XMLEntityBuilder.EventParser
{
    internal class BlockInfo
    {
        string _blockName;
        int _startingX;
        int _startingY;
        int _endingX;
        int _endingY;
        string movableDirection;
        public string Name { get { return _blockName; } set { _blockName = value; } }
        public int StartX { get { return _startingX; } set { _startingX = value; } }
        public int StartY { get { return _startingY; } set { _startingY = value; } }
        public int EndX { get { return _endingX; } set { _endingX = value; } }
        public int EndY { get { return _endingY; } set { _endingY = value; } }
        public string MovableDirection { get { return movableDirection; } set { movableDirection = value; } }
    }
}
