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
    public partial class SudokuGrid : UserControl
    {
        /// <summary>
        /// Gets the inputs (indexed from top-left to bottom-right)
        /// </summary>
        public InputControl[] Inputs { get; private set; }

        /// <summary>
        /// Gets or sets the event that occurs when the grid
        /// is changed.
        /// </summary>
        public event Action OnGridChanged;
        public SudokuGrid()
        {
            InitializeComponent();
            Inputs = new InputControl[] {
                t00, t10, t20, t30, t40, t50, t60, t70, t80,
                t01, t11, t21, t31, t41, t51, t61, t71, t81,
                t02, t12, t22, t32, t42, t52, t62, t72, t82,
                t03, t13, t23, t33, t43, t53, t63, t73, t83,
                t04, t14, t24, t34, t44, t54, t64, t74, t84,
                t05, t15, t25, t35, t45, t55, t65, t75, t85,
                t06, t16, t26, t36, t46, t56, t66, t76, t86,
                t07, t17, t27, t37, t47, t57, t67, t77, t87,
                t08, t18, t28, t38, t48, t58, t68, t78, t88,
            };

            foreach(InputControl control in Inputs)
            {
                control.OnValueChanged += Control_OnValueChanged;
            }
        }

        public Font InputFont
        {
            get
            {
                return t00.Font;
            }
            set
            {
                foreach (InputControl ctrl in Inputs)
                {
                    ctrl.Font = value;
                }
            }
        }

        private void Control_OnValueChanged()
        {
            OnGridChanged?.Invoke();
        }

        /// <summary>
        /// Gets or sets whether this grid is readonly.
        /// </summary>
        public bool ReadOnly { 
            get
            {
                return t00.ReadOnly;
            }
            set
            {
                foreach(InputControl ctrl in Inputs)
                {
                    ctrl.ReadOnly = value;
                }
            }
        }


        /// <summary>
        /// Gets or sets the grid.
        /// </summary>
        public string Value
        {
            get
            {
                StringBuilder grid = new StringBuilder();
                for(int i = 0; i < Inputs.Length; i++) { 
                    if(i%9 == 0 && i != 0)
                    {
                        grid.Append("\n");
                    }
                    grid.Append(Inputs[i].Value);
                }
                return grid.ToString();
            }
            set
            {
                string[] lines = value.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
                int i = 0;

                if(lines.Length != 9)
                {
                    throw new Exception("Invalid format");
                }

                bool rdonly = ReadOnly;
                // Update inputs with corresponding value
                foreach(string rline in lines)
                {
                    string line = rline.Replace("\r", "");
                    if (line.Length != 9)
                    {
                        throw new Exception("Invalid format");
                    }
                    foreach(char c in line)
                    {
                        InputControl ctrl = Inputs[i];
                        ctrl.ReadOnly = true;
                        ctrl.Value = c;
                        ctrl.ReadOnly = rdonly;
                        i++;
                    }
                }
            }
        }
    }
}
