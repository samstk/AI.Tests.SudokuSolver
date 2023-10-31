namespace SudokuSolver.Controls
{
    partial class InputControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // InputControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Margin = new System.Windows.Forms.Padding(7, 5, 7, 5);
            this.MaximumSize = new System.Drawing.Size(64, 64);
            this.MinimumSize = new System.Drawing.Size(64, 64);
            this.Name = "InputControl";
            this.Size = new System.Drawing.Size(64, 64);
            this.Load += new System.EventHandler(this.InputControl_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.InputControl_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
