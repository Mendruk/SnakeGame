using System.Timers;
using Timer = System.Timers.Timer;

namespace Snake_Game
{
    public class Game
    {
        private Random rnd = new Random();
        private int _width, _height;
        private CellContent [,] _grid;
        private List<int[]> _snake;
        private SnakeDirection _sneakDirection;
        private Timer _timer;

        public enum CellContent { EMPTY, SNAKE, FOOD, BLOCK };
        public enum SnakeDirection { STOP, LEFT, RIGHT, UP, DOWN };

        public Game(int width,int height)
        {
            SetTimer();

            _width = width;
            _height = height;
            _grid=new CellContent [_width, _height];

            Start();
        }
        private void SnakeUpdate()
        {
            int sneakLastElementX = _snake[_snake.Count- 1 ][0];
            int sneakLastElementY = _snake[_snake.Count - 1][1];
            for (int i = _snake.Count - 1; i > 0; i--)
            {
                _snake[i][0] = _snake[i - 1][0];
                _snake[i][1] = _snake[i - 1][1];
            }
            switch (_sneakDirection)
            {
                case SnakeDirection.STOP:
                    SnakeStep(0, 0);
                    break;
                case SnakeDirection.LEFT:
                    SnakeStep(-1, 0);
                    break;
                case SnakeDirection.RIGHT:
                    SnakeStep(1, 0);
                    break;
                case SnakeDirection.UP:
                    SnakeStep(0, -1);
                    break;
                case SnakeDirection.DOWN:
                    SnakeStep(0, +1);
                    break;
                default:
                    break;
            }

            //
            if (_snake[0][0] > _width-1)
            {
                _snake[0][0] = 0;
            }
            else if (_snake[0][0] < 0)
            {
                _snake[0][0] = _width - 1;
            }

            if (_snake[0][1] > _height-1)
            {
                _snake[0][1] = 0;
            }
            else if (_snake[0][1] <0)
            {
                _snake[0][1] = _height - 1;
            }
            //

            switch (_grid[_snake[0][0], _snake[0][1]])
            {
                case CellContent.FOOD:
                    _snake.Add(new int[] { sneakLastElementX, sneakLastElementY });
                    AddItem(CellContent.FOOD);
                    AddItem(CellContent.BLOCK);
                    break;
                case CellContent.SNAKE:
                    if (_sneakDirection != SnakeDirection.STOP)
                        FailMessage();
                    break;
                case CellContent.BLOCK:
                    FailMessage();
                    break;
                default:
                    _grid[sneakLastElementX, sneakLastElementY] = CellContent.EMPTY;
                    break;
            }

            _grid[_snake[0][0], _snake[0][1]] = CellContent.SNAKE;
        }
        private void SnakeStep(int delataX,int delataY)
        {
            _snake[0][0]+=delataX;
            _snake[0][1] += delataY;
        }
        private void AddItem(CellContent content)
        {
            int randomX = rnd.Next(_height);
            int randomY = rnd.Next(_width);
            if(_grid[randomX, randomY] == CellContent.EMPTY)
            {
            _grid[randomX, randomY] = content;

            }
            else
            {
                AddItem(content);
            }
        }
        private void SetTimer()
        {
            _timer = new Timer(100);
            _timer.Elapsed += Update;
            _timer.AutoReset = true;
            _timer.Enabled = true;
        }
        private void Start()
        {
            for (int i = 0; i < _height; i++)
            {
                for (int j = 0; j < _width; j++)
                {
                    _grid[j, i] = CellContent.EMPTY;
                }
            }

            _snake = new List<int[]> { new int[] { rnd.Next(_width), rnd.Next(_height) } };
            _grid[_snake[0][0], _snake[0][1]] = CellContent.SNAKE;

            AddItem(CellContent.FOOD);
            _timer.Start();
            _sneakDirection = SnakeDirection.STOP;
        }
        private void FailMessage()
        {
            _timer.Stop();
            DialogResult result = MessageBox.Show("Вы проиграли. Начать с начала?", "Game Over", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                Start();
            }
            if (result == DialogResult.No)
            {
                FailMessage();
            }
        }
        private void Update(Object sourse, ElapsedEventArgs e)
        {
            SnakeUpdate();
        }
        private CellContent GetCellContent(int x,int y)
        {
            if (x > _width-1)
                x = 0;
            if (x < 0)
                x = _width-1;
            if (y > _height-1)
                y = 0;
            if (y < 0)
                y = _height-1;
            return _grid[x, y];
        }

        public CellContent[,] GetGrid()
        {
            return _grid;
        }
        public int GetSnakeCount()
        {
            return _snake.Count;
        }
        public void Input(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.D:
                    if (GetCellContent(_snake[0][0]+1,_snake[0][1])!=CellContent.SNAKE)
                    {
                        _sneakDirection = SnakeDirection.RIGHT;
                    }
                    break;
                case Keys.A:
                    if (GetCellContent(_snake[0][0] - 1, _snake[0][1]) != CellContent.SNAKE)
                    {
                        _sneakDirection = SnakeDirection.LEFT;
                    }
                    break;
                case Keys.S:
                    if (GetCellContent(_snake[0][0], _snake[0][1]+1) != CellContent.SNAKE)
                    {
                        _sneakDirection = SnakeDirection.DOWN;
                    }
                    break;
                case Keys.W:
                    if (GetCellContent(_snake[0][0], _snake[0][1]-1) != CellContent.SNAKE)
                    {
                        _sneakDirection = SnakeDirection.UP;
                    }
                    break;
                default:
                    break;
            }
        }

    }
}
