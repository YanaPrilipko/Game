
class Program
{
    public static void Main()
    {
        var tickRate = TimeSpan.FromMilliseconds(100);
        var snakeGame = new SnakeGame();
        var cts = new CancellationTokenSource();
        var keyPress = Console.ReadKey(intercept: true);

        do
        {
            if (keyPress.Key != 0)
            {
                snakeGame.OnKeyPress(keyPress.Key);
            }

            snakeGame.OnGameTick();
            snakeGame.Render();
            Thread.Sleep((int)tickRate.TotalMilliseconds);

            if (Console.KeyAvailable)
            {
                keyPress = Console.ReadKey(intercept: true);
            }
            else
            {
                keyPress = new ConsoleKeyInfo();
            }
        } while (!snakeGame.GameOver);

        for (var i = 0; i < 3; i++)
        {
            Console.Clear();
            Thread.Sleep(300);
            snakeGame.Render();
            Thread.Sleep(300);
        }

        cts.Cancel();
    }


}

