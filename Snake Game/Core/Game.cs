namespace Snake_Game;

public class Game
{
    private readonly Dictionary<Content, Brush> contentBrushes = new()
    {
        { Content.Block, Brushes.Black },
        { Content.Snake, Brushes.Green },
        { Content.Food, Brushes.Orange }
    };

    private readonly int heightInCells;
    private readonly int widthInCells;
    private readonly Random random = new();
    private int cellSizeHeight;
    private int cellSizeWidth;
    private Queue<Point> snake;
    private Point snakeHead;
    private SnakeDirection snakeDirection;
    private Content[,] grid;

    public Game()
    {
        widthInCells = 15;
        heightInCells = 15;
        grid = new Content[widthInCells, heightInCells];
        Start();
    }

    public int SnakeLength => snake.Count;

    private Content GetCellContent(int x, int y)
    {
        x = (x + widthInCells) % widthInCells;
        y = (y + heightInCells) % heightInCells;
        return grid[x, y];
    }

    private void Start()
    {
        Array.Clear(grid, 0, grid.Length);
        snake = new Queue<Point>();
        snakeHead = new Point(random.Next(widthInCells), random.Next(heightInCells));
        snake.Enqueue(snakeHead);
        grid[snakeHead.X, snakeHead.Y] = Content.Snake;
        AddContentToRandomPosition(Content.Food);
        snakeDirection = SnakeDirection.Stop;
    }

    public void Input(SnakeDirection direction)
    {
        switch (direction)
        {
            case SnakeDirection.Right:
                if (snakeDirection != SnakeDirection.Left || snake.Count == 1)
                    snakeDirection = direction;
                break;
            case SnakeDirection.Left:
                if (snakeDirection != SnakeDirection.Right || snake.Count == 1)
                    snakeDirection = direction;
                break;
            case SnakeDirection.Down:
                if (snakeDirection != SnakeDirection.Up || snake.Count == 1)
                    snakeDirection = direction;
                break;
            case SnakeDirection.Up:
                if (snakeDirection != SnakeDirection.Down || snake.Count == 1)
                    snakeDirection = direction;
                break;
        }
    }

    public void Update()
    {
        Point snakeNextPoint = GetSnakeNextPoint(snakeDirection);

        if (snakeDirection == SnakeDirection.Stop)
            return;

        switch (GetCellContent(snakeNextPoint.X, snakeNextPoint.Y))
        {
            case Content.Empty:
                snakeHead = snakeNextPoint;
                snake.Enqueue(snakeHead);
                Point desappearedTail = snake.Dequeue();
                grid[desappearedTail.X, desappearedTail.Y] = Content.Empty;
                break;
            case Content.Food:
                snakeHead = snakeNextPoint;
                snake.Enqueue(snakeHead);
                AddContentToRandomPosition(Content.Food);
                AddContentToRandomPosition(Content.Block);
                break;
            case Content.Snake:
            case Content.Block:
                if (snakeDirection != SnakeDirection.Stop)
                {
                    snakeDirection = SnakeDirection.Stop;
                    ShowFailMessage();
                }
                break;
        }

        grid[snakeHead.X, snakeHead.Y] = Content.Snake;
    }

    public void Draw(Graphics graphics)
    {
        for (int x = 0; x < widthInCells; x++)
            for (int y = 0; y < heightInCells; y++)
                if (contentBrushes.TryGetValue(grid[x, y], out var brush))
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

        nextPoint.X = (nextPoint.X + widthInCells) % widthInCells;
        nextPoint.Y = (nextPoint.Y + heightInCells) % heightInCells;
        return nextPoint;
    }

    private void AddContentToRandomPosition(Content content)
    {
        int offsetX = random.Next(widthInCells);
        int offsetY = random.Next(heightInCells);

        for (int x = 0; x < widthInCells; x++)
            for (int y = 0; y < heightInCells; y++)
                if (grid[(x + offsetX) % widthInCells, (y + offsetY) % heightInCells] == Content.Empty)
                {
                    grid[(x + offsetX) % widthInCells, (y + offsetY) % heightInCells] = content;
                }
    }

    private void ShowFailMessage()
    {
        DialogResult result = MessageBox.Show("Game Over! Restart?", "Game Over", MessageBoxButtons.YesNo);
        if (result == DialogResult.Yes) Start();
        if (result == DialogResult.No) ShowFailMessage();
    }

    public void ChangeCellSize(int gameFieldHeight, int gameFieldWidth)
    {
        cellSizeHeight = gameFieldHeight / heightInCells;
        cellSizeWidth = gameFieldWidth / widthInCells;
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