class Snake
{
    private List<Position> _body;

    public Snake(Position spawnLocation, int initialSize = 1)
    {
        _body = new List<Position> { spawnLocation };
        for (int i = 1; i < initialSize; i++)
        {
            _body.Add(new Position(spawnLocation.Left - i, spawnLocation.Top)); 
        }
        Dead = false;
    }

    public bool Dead { get; private set; }
    public Position Head => _body.First();
    private IEnumerable<Position> Body => _body.Skip(1);

    public void Move(Direction direction)
    {
        if (Dead) throw new InvalidOperationException();

        Position newHead = direction switch
        {
            Direction.Up => Head.DownBy(-1),
            Direction.Left => Head.RightBy(-1),
            Direction.Down => Head.DownBy(1),
            Direction.Right => Head.RightBy(1),
            _ => throw new ArgumentOutOfRangeException()
        };

        if (_body.Contains(newHead) || !PositionIsValid(newHead))
        {
            Dead = true;
            return;
        }

        _body.Insert(0, newHead);
        _body.RemoveAt(_body.Count - 1); 
    }

    public void Grow()
    {
        if (Dead) throw new InvalidOperationException();
        _body.Add(_body.Last()); 
    }

    public void Render()
    {
        Console.SetCursorPosition(Math.Max(0, Head.Left), Math.Max(0, Head.Top));

        foreach (var position in Body)
        {
            Console.SetCursorPosition(Math.Max(0, position.Left), Math.Max(0, position.Top));
            Console.Write("■");
        }
    }

    private static bool PositionIsValid(Position position) =>
        position.Top >= 0 && position.Left >= 0;
}
