class Mouse
{
    public Mouse(Position position)
    {
        Position = position;
    }

    public Position Position { get; }

    public void Render()
    {
        Console.SetCursorPosition(Position.Left, Position.Top);
        Console.ForegroundColor = ConsoleColor.DarkRed;
        Console.Write("■");
        Console.ResetColor();
    }
}
