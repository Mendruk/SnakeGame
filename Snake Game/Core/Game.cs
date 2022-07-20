namespace Snake_Game;

public class Game
{
    public enum Content
    {
        Empty,
        Snake,
        Food,
        Block
    }

    public enum SnakeDirection
    {
        Stop,
        Left,
        Right,
        Up,
        Down
    }

    private readonly int height;
    private readonly Random rnd = new();
    private readonly int width;
    private List<Point> snake;
    private SnakeDirection snakeDirection;

    public Game(int width, int height)
    {
        this.width = width;
        this.height = height;
        Grid = new Content[this.width, this.height];
        Start();
    }

    private Point snakeHead => snake[snake.Count - 1];
    private Point snakeTail => snake[0];

    public Content[,] Grid { get; }
    public int snakeLength => snake.Count;

    private Content CellContent(int x, int y)
    {
        x = (x + width) % width;
        y = (y + height) % height;
        return Grid[x, y];
    }

    private void Start()
    {
        Array.Clear(Grid, 0, Grid.Length);

        snake = new List<Point> { new(rnd.Next(width), rnd.Next(height)) };
        Grid[snakeHead.X, snakeHead.Y] = Content.Snake;
        AddItem(Content.Food);
        snakeDirection = SnakeDirection.Stop;
    }

    private Point SnakeNextPoint(SnakeDirection direction)
    {
        var nextPoint = new Point(snakeHead.X, snakeHead.Y);

        switch (direction)
        {
            case SnakeDirection.Left:
                nextPoint.X -= 1;
                break;
            case SnakeDirection.Right:
                nextPoint.X += 1;
                break;
            case SnakeDirection.Up:
                nextPoint.Y -= 1;
                break;
            case SnakeDirection.Down:
                nextPoint.Y += 1;
                break;
        }

        nextPoint.X = (nextPoint.X + width) % width;
        nextPoint.Y = (nextPoint.Y + height) % height;
        return nextPoint;
    }

    private void AddItem(Content content)
    {
        var freeCells = new List<Point>();
        for (var i = 0; i < width; i++)
        for (var j = 0; j < height; j++)
            if (Grid[i, j] == Content.Empty)
                freeCells.Add(new Point(i, j));

        if (freeCells.Count > 0)
        {
            var randomIndex = rnd.Next(freeCells.Count - 1);
            Grid[freeCells[randomIndex].X, freeCells[randomIndex].Y] = content;
        }
        else
        {
            Start();
        }
    }

    private void ShowFailMessage()
    {
        var result = MessageBox.Show("Game Over! Restart?", "Game Over", MessageBoxButtons.YesNo);
        if (result == DialogResult.Yes) Start();
        if (result == DialogResult.No) ShowFailMessage();
    }

    public void Update()
    {
        var snakeNextPoint = SnakeNextPoint(snakeDirection);

        switch (CellContent(snakeNextPoint.X, snakeNextPoint.Y))
        {
            case Content.Food:
                snake.Add(snakeNextPoint);
                AddItem(Content.Food);
                AddItem(Content.Block);
                break;
            case Content.Snake:
                if (snakeDirection != SnakeDirection.Stop)
                {
                    snakeDirection = SnakeDirection.Stop;
                    ShowFailMessage();
                }

                break;
            case Content.Block:
                if (snakeDirection != SnakeDirection.Stop)
                {
                    snakeDirection = SnakeDirection.Stop;
                    ShowFailMessage();
                }

                break;
            default:
                snake.Add(snakeNextPoint);
                Grid[snakeTail.X, snakeTail.Y] = Content.Empty;
                snake.RemoveAt(0);
                break;
        }

        Grid[snakeHead.X, snakeHead.Y] = Content.Snake;
    }

    public void Input(KeyEventArgs e)
    {
        switch (e.KeyCode)
        {
            case Keys.D:
                if (CellContent(snakeHead.X + 1, snakeHead.Y) != Content.Snake)
                    snakeDirection = SnakeDirection.Right;
                break;
            case Keys.A:
                if (CellContent(snakeHead.X - 1, snakeHead.Y) != Content.Snake)
                    snakeDirection = SnakeDirection.Left;
                break;
            case Keys.S:
                if (CellContent(snakeHead.X, snakeHead.Y + 1) != Content.Snake)
                    snakeDirection = SnakeDirection.Down;
                break;
            case Keys.W:
                if (CellContent(snakeHead.X, snakeHead.Y - 1) != Content.Snake)
                    snakeDirection = SnakeDirection.Up;
                break;
        }
    }
}