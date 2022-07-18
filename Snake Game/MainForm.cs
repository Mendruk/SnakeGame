namespace Snake_Game
{
    public partial class MainForm : Form
    {
        private Game _game;
        private int _cellSizeH ;
        private int _cellSizeW;
        private Brush _sneikBrush=new SolidBrush(Color.Green);
        private Brush _foodBrush = new SolidBrush(Color.Orange);
        private Brush _blockBrush = new SolidBrush(Color.Black);
        private int _size = 15;
        
        public MainForm()
        {
            InitializeComponent();
            // Убирает мерцание
            SetStyle(ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.UserPaint, true);
            UpdateStyles();
            _cellSizeH = picGameField.Height / _size;
            _cellSizeW = picGameField.Width / _size;
            _game = new Game(15, 15);

        }
        private void picGameField_Paint(object sender, PaintEventArgs e)
        {
            for (int i = 0; i < _size; i++)
            {
                for(int j = 0; j < _size; j++)
                {
                    if (_game.GetGrid()[i,j] == Game.CellContent.SNAKE)
                        e.Graphics.FillRectangle(_sneikBrush, i * _cellSizeW, j * _cellSizeH, _cellSizeW, _cellSizeH);

                    if (_game.GetGrid()[i,j] == Game.CellContent.FOOD)
                        e.Graphics.FillEllipse(_foodBrush, i * _cellSizeW, j * _cellSizeH, _cellSizeW, _cellSizeH);

                    if (_game.GetGrid()[i, j] == Game.CellContent.BLOCK)
                        e.Graphics.FillRectangle(_blockBrush, i * _cellSizeW, j * _cellSizeH, _cellSizeW, _cellSizeH);
                }
            }
        }
        private void mainTimer_Tick(object sender, EventArgs e)
        {
            labelSnakeCount.Text = _game.GetSnakeCount().ToString();
            Refresh();// Перерисовка форм
        }
        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            _game.Input(e);
        }
        private void picGameField_SizeChanged(object sender, EventArgs e)
        {
            _cellSizeH = picGameField.Height / _size;
            _cellSizeW = picGameField.Width / _size;
            Refresh();// Перерисовка форм
        }

    }
}