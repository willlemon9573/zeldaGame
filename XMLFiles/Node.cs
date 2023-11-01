namespace SprintZero1.XMLFiles
{
    internal class Node
    {
        public int X { get; set; }
        public int Y { get; set; }
        public string Name { get; set; }

        public Node(int x, int y, string name)
        {
            X = x;
            Y = y;
            Name = name;
        }

    }
}
