namespace SudokuSolver
{
    partial class InputForm
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
            this.sudokuGrid1 = new SudokuSolver.Controls.SudokuGrid();
            this.sudokuGrid2 = new SudokuSolver.Controls.SudokuGrid();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.outputUpdateTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // sudokuGrid1
            // 
            this.sudokuGrid1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.sudokuGrid1.InputFont = new System.Drawing.Font("Consolas", 28.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.sudokuGrid1.Location = new System.Drawing.Point(24, 17);
            this.sudokuGrid1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.sudokuGrid1.MaximumSize = new System.Drawing.Size(482, 482);
            this.sudokuGrid1.MinimumSize = new System.Drawing.Size(482, 482);
            this.sudokuGrid1.Name = "sudokuGrid1";
            this.sudokuGrid1.ReadOnly = false;
            this.sudokuGrid1.Size = new System.Drawing.Size(482, 482);
            this.sudokuGrid1.TabIndex = 0;
            this.sudokuGrid1.Value = "         \n         \n         \n         \n         \n         \n         \n         \n " +
    "        ";
            this.sudokuGrid1.OnGridChanged += new System.Action(this.sudokuGrid1_OnGridChanged);
            // 
            // sudokuGrid2
            // 
            this.sudokuGrid2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.sudokuGrid2.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.sudokuGrid2.InputFont = new System.Drawing.Font("Consolas", 28.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.sudokuGrid2.Location = new System.Drawing.Point(527, 17);
            this.sudokuGrid2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.sudokuGrid2.MaximumSize = new System.Drawing.Size(482, 482);
            this.sudokuGrid2.MinimumSize = new System.Drawing.Size(482, 482);
            this.sudokuGrid2.Name = "sudokuGrid2";
            this.sudokuGrid2.ReadOnly = true;
            this.sudokuGrid2.Size = new System.Drawing.Size(482, 482);
            this.sudokuGrid2.TabIndex = 1;
            this.sudokuGrid2.Value = "         \n         \n         \n         \n         \n         \n         \n         \n " +
    "        ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 512);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "Input";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.Location = new System.Drawing.Point(828, 512);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(181, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "Result";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // outputUpdateTimer
            // 
            this.outputUpdateTimer.Enabled = true;
            this.outputUpdateTimer.Tick += new System.EventHandler(this.outputUpdateTimer_Tick);
            // 
            // InputForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1039, 548);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.sudokuGrid2);
            this.Controls.Add(this.sudokuGrid1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "InputForm";
            this.Text = "Sudoku Solver";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.SudokuGrid sudokuGrid1;
        private Controls.SudokuGrid sudokuGrid2;
        private Label label1;
        private Label label2;
        private System.Windows.Forms.Timer outputUpdateTimer;
    }
}