namespace Snake_Game
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.picGameField = new System.Windows.Forms.PictureBox();
            this.mainTimer = new System.Windows.Forms.Timer(this.components);
            this.lableSneak = new System.Windows.Forms.Label();
            this.labelSnakeCount = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picGameField)).BeginInit();
            this.SuspendLayout();
            // 
            // picGameField
            // 
            this.picGameField.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.picGameField.BackColor = System.Drawing.Color.SkyBlue;
            this.picGameField.Location = new System.Drawing.Point(32, 32);
            this.picGameField.Name = "picGameField";
            this.picGameField.Size = new System.Drawing.Size(513, 513);
            this.picGameField.TabIndex = 0;
            this.picGameField.TabStop = false;
            this.picGameField.SizeChanged += new System.EventHandler(this.picGameField_SizeChanged);
            this.picGameField.Paint += new System.Windows.Forms.PaintEventHandler(this.picGameField_Paint);
            // 
            // mainTimer
            // 
            this.mainTimer.Enabled = true;
            this.mainTimer.Interval = 10;
            this.mainTimer.Tick += new System.EventHandler(this.mainTimer_Tick);
            // 
            // lableSneak
            // 
            this.lableSneak.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lableSneak.AutoSize = true;
            this.lableSneak.Location = new System.Drawing.Point(569, 32);
            this.lableSneak.Name = "lableSneak";
            this.lableSneak.Size = new System.Drawing.Size(111, 20);
            this.lableSneak.TabIndex = 1;
            this.lableSneak.Text = "Длина змейки:";
            // 
            // labelSnakeCount
            // 
            this.labelSnakeCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelSnakeCount.AutoSize = true;
            this.labelSnakeCount.Location = new System.Drawing.Point(686, 32);
            this.labelSnakeCount.Name = "labelSnakeCount";
            this.labelSnakeCount.Size = new System.Drawing.Size(17, 20);
            this.labelSnakeCount.TabIndex = 2;
            this.labelSnakeCount.Text = "1";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(748, 646);
            this.Controls.Add(this.labelSnakeCount);
            this.Controls.Add(this.lableSneak);
            this.Controls.Add(this.picGameField);
            this.Name = "MainForm";
            this.Text = "Snake Game";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.picGameField)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private PictureBox picGameField;
        private System.Windows.Forms.Timer mainTimer;
        private Label lableSneak;
        private Label labelSnakeCount;

    }
}