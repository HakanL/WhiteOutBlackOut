using System;
using System.Drawing;
using System.Windows.Forms;

namespace WhiteOutBlackOut
{
    public partial class Form1 : Form
    {
        private Color inputColor;
        private Color outputColor;
        private Random rnd;

        public Form1()
        {
            InitializeComponent();
            rnd = new Random();
        }

        public static System.Drawing.Color AdjustColor(System.Drawing.Color input, float blackOut, float whiteOut)
        {
            var hsv = new HSV(input);
            hsv.Saturation = hsv.Saturation + (HSV.White.Saturation - hsv.Saturation) * whiteOut;
            hsv.Value = Math.Min(1.0, hsv.Value + whiteOut) * (1 - blackOut);
            return hsv.Color;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            inputColor = Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));
            outputColor = AdjustColor(inputColor, trackBarBlackOut.Value / 100.0f, trackBarWhiteOut.Value / 100.0f);

            tableLayoutPanel1.Invalidate();
            tableLayoutPanel2.Invalidate();
        }

        private void tableLayoutPanel1_CellPaint(object sender, TableLayoutCellPaintEventArgs e)
        {
            if (e.Row == 1)
                e.Graphics.FillRectangle(new SolidBrush(inputColor), e.ClipRectangle);
        }

        private void tableLayoutPanel2_CellPaint(object sender, TableLayoutCellPaintEventArgs e)
        {
            if (e.Row == 1)
                e.Graphics.FillRectangle(new SolidBrush(outputColor), e.ClipRectangle);

        }
    }
}
