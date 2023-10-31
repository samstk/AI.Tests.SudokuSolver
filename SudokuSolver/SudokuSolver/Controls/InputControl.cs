using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SudokuSolver.Controls
{
    public partial class InputControl : UserControl
    {
        public InputControl()
        {
            InitializeComponent();
            this.TabStop = true;
        }

        private void InputControl_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Gets or sets events that occurs when the value is changed.
        /// </summary>
        public event Action OnValueChanged;

        private char _Value = ' ';
        /// <summary>
        /// Gets or sets the value of the control.
        /// </summary>
        public char Value {
            get { return _Value; }
            set
            {
                bool changed = _Value != value;
                _Value = value;
                if (changed)
                {
                    if (!ReadOnly)
                        OnValueChanged?.Invoke();
                    Refresh();
                }
            }
        }

        /// <summary>
        /// If true, then the value cannot be changed
        /// </summary>
        public bool ReadOnly { get; set; } = false;
        private void InputControl_KeyDown(object sender, KeyEventArgs e)
        {
            Value = e.KeyCode switch
            {
                Keys.Delete or Keys.Space => ' ',
                Keys.D0 or Keys.NumPad0 => '0',
                Keys.D1 or Keys.NumPad1 => '1',
                Keys.D2 or Keys.NumPad2 => '2',
                Keys.D3 or Keys.NumPad3 => '3',
                Keys.D4 or Keys.NumPad4 => '4',
                Keys.D5 or Keys.NumPad5 => '5',
                Keys.D6 or Keys.NumPad6 => '6',
                Keys.D7 or Keys.NumPad7 => '7',
                Keys.D8 or Keys.NumPad8 => '8',
                Keys.D9 or Keys.NumPad9 => '9',
                _ => Value
            };
        }

        protected override void OnGotFocus(EventArgs e)
        {
            Refresh();
            base.OnGotFocus(e);
        }
        protected override void OnLostFocus(EventArgs e)
        {
            Refresh();
            base.OnLostFocus(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (Focused)
                e.Graphics.FillRectangle(Brushes.WhiteSmoke, 0, 0, Width, Height);

            string text = $"{Value}";
            SizeF textSize = e.Graphics.MeasureString(text, Font);

            e.Graphics.DrawString(text, Font, Brushes.RoyalBlue,
                Width / 2 - textSize.Width / 2, 
                Height / 2 - textSize.Height / 2);
        }
    }
}
