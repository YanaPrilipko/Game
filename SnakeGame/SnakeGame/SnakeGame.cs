
class SnakeGame
{

    private static readonly Position Origin = new Position(0, 0);

    private Direction _currentDirection;
    private Direction _nextDirection;
    private Snake _snake;
    private Mouse _mouse;

    public SnakeGame()
    {
        _snake = new Snake(Origin, initialSize: 5);
        _mouse = CreateMouse();
        _currentDirection = Direction.Right;
        _nextDirection = Direction.Right;
    }

    public bool GameOver => _snake.Dead;

    public void OnKeyPress(ConsoleKey key)
    {
        Direction newDirection;

        switch (key)
        {
            case ConsoleKey.UpArrow:
                newDirection = Direction.Up;
                break;

            case ConsoleKey.LeftArrow:
                newDirection = Direction.Left;
                break;

            case ConsoleKey.DownArrow:
                newDirection = Direction.Down;
                break;

            case ConsoleKey.RightArrow:
                newDirection = Direction.Right;
                break;

            default:
                return;
        }

        if (newDirection == OppositeDirectionTo(_currentDirection))
        {
            return;
        }
        _nextDirection = newDirection;
    }

    public void OnGameTick()
    {
        if (GameOver) throw new InvalidOperationException();

        _currentDirection = _nextDirection;
        _snake.Move(_currentDirection);

        if (_snake.Head.Equals(_mouse.Position))
        {
            _snake.Grow();
            _mouse = CreateMouse();
        }
    }

    public void Render()
    {
        Console.Clear();
        _snake.Render();
        _mouse.Render();
        Console.SetCursorPosition(0, 0);
    }

    private static Direction OppositeDirectionTo(Direction direction)
    {
        switch (direction)
        {
            case Direction.Up: return Direction.Down;
            case Direction.Left: return Direction.Right;
            case Direction.Right: return Direction.Left;
            case Direction.Down: return Direction.Up;
            default: throw new ArgumentOutOfRangeException();
        }
    }

    private static Mouse CreateMouse()
    {
        const int numberOfRows = 20;
        const int numberOfColumns = 20;

        var random = new Random();
        var top = random.Next(0, numberOfRows + 1);
        var left = random.Next(0, numberOfColumns + 1);
        var position = new Position(top, left);
        var mouse = new Mouse(position);

        return mouse;
    }
}
