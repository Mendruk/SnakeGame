
namespace Snake_Game
{
    public partial class MainForm : Form
    {
        private Game game;
        private int cellSizeH;
        private int cellSizeW;
        private int fieldSize = 15;

        public MainForm()
        {
            InitializeComponent();
            SetStyle(ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.UserPaint, true);
            UpdateStyles();
            cellSizeH = picGameField.Height / fieldSize;
            cellSizeW = picGameField.Width / fieldSize;
            game = new Game(fieldSize, fieldSize);
        }
        private void picGameField_Paint(object sender, PaintEventArgs e)
        {
            for (int i = 0; i < fieldSize; i++)
            {
                for (int j = 0; j < fieldSize; j++)
                {
                    if (game.Grid[i, j] == Game.Content.Snake)
                        e.Graphics.FillRectangle(Brushes.DarkGreen, i * cellSizeW, j * cellSizeH, cellSizeW, cellSizeH);
                    else if (game.Grid[i, j] == Game.Content.Food)
                        e.Graphics.FillRectangle(Brushes.DarkOrange, i * cellSizeW, j * cellSizeH, cellSizeW, cellSizeH);
                    else if (game.Grid[i, j] == Game.Content.Block)
                        e.Graphics.FillRectangle(Brushes.Black, i * cellSizeW, j * cellSizeH, cellSizeW, cellSizeH);
                }
            }
        }
        private void mainTimer_Tick(object sender, EventArgs e)
        {
            game.Update();
            labelSnakeCount.Text = game.snakeLength.ToString();
            Refresh();
            
        }
        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            game.Input(e);
        }
        private void picGameField_SizeChanged(object sender, EventArgs e)
        {
            cellSizeH = picGameField.Height / fieldSize;
            cellSizeW = picGameField.Width / fieldSize;
            Refresh();
        }
    }
}