using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Table_Calculator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void TablePick_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateText();
        }

        private void EquationTable_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateText();
        }

        void UpdateText()
        {
            TableBox.Clear();

            string tableIndex = (TablePick.SelectedIndex + 1).ToString();

            float answer = 0;
            string equation = "   ";

            for (float i = 1; i <= TablePick.Items.Count; i++)
            {
                switch (EquationTable.SelectedIndex)
                {
                    case (0):
                        answer = (TablePick.SelectedIndex + 1f) * i;
                        equation = " * ";
                        break;
                    case (1):
                        answer = (TablePick.SelectedIndex + 1f) / i;
                        equation = " / ";
                        break;
                    case (2):
                        answer = (TablePick.SelectedIndex + 1f) + i;
                        equation = " + ";
                        break;
                    case (3):
                        answer = (TablePick.SelectedIndex + 1f) - i;
                        equation = " - ";
                        break;
                    default:
                        break;
                }

                string code = ((answer % 1) > 0) ? "F1" : null;

                TableBox.AppendText(tableIndex + equation + (i) + " = " + answer.ToString(code));
                TableBox.AppendText(Environment.NewLine);
            }
        }
    }
}
