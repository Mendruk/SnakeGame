namespace Snake_Game;

public class Game
{
    private readonly Dictionary<Content, Brush> contentBrushes = new()
    {
        { Content.Block, Brushes.Black },
        { Content.Snake, Brushes.Green },
        { Content.Food, Brushes.Orange }
    };

    private readonly int fieldSize = 15;
    private readonly int height;
    private readonly int width;
    private readonly Random random = new();
    private int cellSizeHeight;
    private int cellSizeWidth;
    private Queue<Point> snake;
    private Point snakeHead;
    private SnakeDirection snakeDirection;

    public Game(int gameFieldHeight, int gameFieldWidth)
    {
        width = fieldSize;
        height = fieldSize;
        cellSizeHeight = gameFieldHeight / fieldSize;
        cellSizeWidth = gameFieldWidth / fieldSize;
        Grid = new Content[width, height];
        Start();
    }

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
        snake = new Queue<Point>();
        snakeHead = new Point(random.Next(width), random.Next(height));
        snake.Enqueue(snakeHead);
        Grid[snakeHead.X, snakeHead.Y] = Content.Snake;
        AddContentToRandomPosition(Content.Food);
        snakeDirection = SnakeDirection.Stop;
    }

    public void Input(SnakeDirection direction)
    {
        switch (direction)
        {
            case SnakeDirection.Right:
                if (CellContent(snakeHead.X + 1, snakeHead.Y) != Content.Snake)
                    snakeDirection = direction;
                break;
            case SnakeDirection.Left:
                if (CellContent(snakeHead.X - 1, snakeHead.Y) != Content.Snake)
                    snakeDirection = direction;
                break;
            case SnakeDirection.Down:
                if (CellContent(snakeHead.X, snakeHead.Y + 1) != Content.Snake)
                    snakeDirection = direction;
                break;
            case SnakeDirection.Up:
                if (CellContent(snakeHead.X, snakeHead.Y - 1) != Content.Snake)
                    snakeDirection = direction;
                break;
        }
    }

    public void Update()
    {
        var snakeNextPoint = GetSnakeNextPoint(snakeDirection);

        switch (CellContent(snakeNextPoint.X, snakeNextPoint.Y))
        {
            case Content.Food:
                snakeHead = snakeNextPoint;
                snake.Enqueue(snakeHead);
                AddContentToRandomPosition(Content.Food);
                AddContentToRandomPosition(Content.Block);
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
            case Content.Empty:
                snakeHead = snakeNextPoint;
                snake.Enqueue(snakeHead);
                Grid[snake.Peek().X, snake.Peek().Y] = Content.Empty;
                snake.Dequeue();
                break;
        }

        Grid[snakeHead.X, snakeHead.Y] = Content.Snake;
    }

    public void Draw(Graphics graphics)
    {
        for (var x = 0; x < fieldSize; x++)
            for (var y = 0; y < fieldSize; y++)
                if (contentBrushes.TryGetValue(Grid[x, y], out var brush))
                    graphics.FillRectangle(brush, x * cellSizeWidth, y * cellSizeHeight, cellSizeWidth, cellSizeHeight);
    }

    private Point GetSnakeNextPoint(SnakeDirection direction)
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

    private void AddContentToRandomPosition(Content content)
    {
        var offsetX = random.Next(width);
        var offsetY = random.Next(height);

        for (var x = 0; x < width; x++)
            for (var y = 0; y < height; y++)
                if (Grid[(x + offsetX) % width, (y + offsetY) % height] == Content.Empty)
                {
                    Grid[(x + offsetX) % width, (y + offsetY) % height] = content;
                    return;
                }
    }

    private void ShowFailMessage()
    {
        var result = MessageBox.Show("Game Over! Restart?", "Game Over", MessageBoxButtons.YesNo);
        if (result == DialogResult.Yes) Start();
        if (result == DialogResult.No) ShowFailMessage();
    }

    public void ChangeСellSize(int gameFieldHeight, int gameFieldWidth)
    {
        cellSizeHeight = gameFieldHeight / fieldSize;
        cellSizeWidth = gameFieldWidth / fieldSize;
    }
}

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