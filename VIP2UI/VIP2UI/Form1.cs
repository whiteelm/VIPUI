using System;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace VIP
{
    public partial class Form1 : Form
    {

        [DllImport("VIP2.dll", CharSet = CharSet.Ansi, EntryPoint = "_vip2@76",
            CallingConvention = CallingConvention.StdCall)]
        public static extern void VIP2(double aw, double h1, double h2, double t, 
            double e1, double e2, double e3, ref double c, ref double l,
            ref double um, ref double em, ref double z0);

        public Form1()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, System.EventArgs e)
        {
            Calculate();
            try
            {
                
            }
            catch
            {
                MessageBox.Show("Проверьте входные данные", 
                    "Ошибка", MessageBoxButtons.OK);
            }
        }

        private void Calculate()
        {
            CLabel.Text = null;
            LLabel.Text = null;
            UmLabel.Text = null;
            Z0Label.Text = null;
            EmLabel.Text = null;
            var aw = Convert.ToDouble(AwTextBox.Text);
            var h1 = Convert.ToDouble(H1TextBox.Text);
            var h2 = Convert.ToDouble(H2TextBox.Text);
            var t = Convert.ToDouble(TTextBox.Text);
            var e1 = Convert.ToDouble(E1TextBox.Text);
            var e2 = Convert.ToDouble(E2TextBox.Text);
            var e3 = Convert.ToDouble(E3TextBox.Text);
            var l = new double[2, 2];
            var c = new double[2, 2];
            var um = new double[2, 2];
            var z0 = new double[2, 2];
            var em = new double[2];
            VIP2(aw, h1, h2, t, e1, e2, e3, ref l[0, 0], ref c[0, 0],
                ref um[0, 0], ref em[0], ref z0[0, 0]);

            for (var i = 0; i < 2; i++)
            {
                for (var j = 0; j < 2; j++)
                {
                    LLabel.Text += l[i, j].ToString("0.0000") + @"  ";
                    CLabel.Text += c[i, j].ToString("0.0000") + @"  ";
                    UmLabel.Text += um[i, j].ToString("0.0000") + @"  ";
                    Z0Label.Text += z0[i, j].ToString("0.0000") + @"  ";
                }
                LLabel.Text += Environment.NewLine;
                CLabel.Text += Environment.NewLine;
                UmLabel.Text += Environment.NewLine;
                Z0Label.Text += Environment.NewLine;
            }

            EmLabel.Text += em[0].ToString("0.0000") + @"  ";
            EmLabel.Text += em[1].ToString("0.0000");
        }

        /// <summary>
        /// Событие ввода с клавиатуры в текстбокс.
        /// </summary>
        private void ValidateDoubleTextBoxes_KeyPress(object sender,
            KeyPressEventArgs e)
        {
            e.Handled = !Regex.IsMatch(e.KeyChar.ToString(),
                @"[\d\b,]");
        }
    }
}
