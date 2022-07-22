
namespace Snake_Game
{
    public partial class MainForm : Form
    {
        private Game game;

        public MainForm()
        {
            InitializeComponent();
            SetStyle(ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.UserPaint, true);
            UpdateStyles();
            game = new Game(picGameField.Height, picGameField.Width);
        }
        private void picGameField_Paint(object sender, PaintEventArgs e)
        {
            game.Draw(e.Graphics);
        }
        private void mainTimer_Tick(object sender, EventArgs e)
        {
            game.Update();
            labelSnakeCount.Text = game.snakeLength.ToString();
            Refresh();

        }
        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.D:
                    game.Input(SnakeDirection.Right);
                    break;
                case Keys.A:
                    game.Input(SnakeDirection.Left);
                    break;
                case Keys.S:
                    game.Input(SnakeDirection.Down);
                    break;
                case Keys.W:
                    game.Input(SnakeDirection.Up);
                    break;
            }
        }
        private void picGameField_SizeChanged(object sender, EventArgs e)
        {
            game.Change—ellSize(picGameField.Height, picGameField.Width);
            Refresh();
        }
    }
}