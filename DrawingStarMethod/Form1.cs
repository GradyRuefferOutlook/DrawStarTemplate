using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DrawingStarMethod
{
    public partial class Form1 : Form
    {
        PointF[] pentArrayPoints = new PointF[5];
        List<PointF> pentDraw = new List<PointF>();
        List<PointF> circList = new List<PointF>();
        Int32 circDrawTime = 0;
        Int32 circDrawSpeed = 60;
        Int32 pentDrawTime = 0;
        Int32 pentDrawSpeed = 100;
        int radius = 50;
        PointF[] pointFs;
        PointF[] pentFs;
        int alpha = 0;
        int alphaAdd = 5;
        bool alphaInc = true;
        public Form1()
        {
            InitializeComponent();
            xInput.Text = circDrawSpeed.ToString();
            yInput.Text = pentDrawSpeed.ToString();
            sizeInput.Text = radius.ToString();
            DrawEr.Start();
        }

        private void drawButton_Click(object sender, EventArgs e)
        {
            ///The following line of code demonstrates how to get input
            ///and convert it to a float value. Use this to help you in 
            ///Parts 2 - 4 where you need to get inputs from the 
            ///user and store them in float variables. This code is 
            ///not needed for Part 1.

            ///float size = Convert.ToSingle(sizeInput.Text); 

            Graphics g = this.CreateGraphics(); //for part 4 this gets moved to the custom methods.
            Pen blackPen = new Pen(Color.Black);

            circDrawTime = 0;
            pentDrawTime = 0;

            try
            {
                pentDrawSpeed = Convert.ToInt16(yInput.Text);
            }
            catch
            {
                yInput.Text = "1";
                pentDrawSpeed = Convert.ToInt16(yInput.Text);
            }

            try
            {
                circDrawSpeed = Convert.ToInt16(xInput.Text);
            }
            catch
            {
                xInput.Text = "1";
                circDrawSpeed = Convert.ToInt16(xInput.Text);
            }

            try
            {
                radius = Convert.ToInt16(sizeInput.Text);
            }
            catch
            {
                sizeInput.Text = "1";
                circDrawSpeed = Convert.ToInt16(sizeInput.Text);
            }

            DrawCircle(radius);
            DrawPentPoint(radius);
            Refresh();
        }

        void DrawPentPoint(int radius)
        {
            pentArrayPoints = new PointF[5];
            pentDraw.Clear();
            pentArrayPoints[0] = new PointF(0, -radius);
            pentArrayPoints[1] = new PointF(-1 * Convert.ToSingle(Math.Sqrt(Math.Pow(radius, 2) - Math.Pow((radius / 3.5), 2))), -1 * Convert.ToSingle(radius / 3.5));
            pentArrayPoints[2] = new PointF(-1 * Convert.ToSingle(Math.Sqrt(Math.Pow(radius, 2) - Math.Pow(((radius / 2.5) * 2), 2))), Convert.ToSingle((radius / 2.5) * 2));
            pentArrayPoints[3] = new PointF(Convert.ToSingle(Math.Sqrt(Math.Pow(radius, 2) - Math.Pow(((radius / 2.5) * 2), 2))), Convert.ToSingle((radius / 2.5) * 2));
            pentArrayPoints[4] = new PointF(Convert.ToSingle(Math.Sqrt(Math.Pow(radius, 2) - Math.Pow((radius / 3.5), 2))), -1 * Convert.ToSingle(radius / 3.5));
            for (int i = 0; i < pentArrayPoints.Length; i++)
            {
                pentArrayPoints[i].X += radius + 5;
                pentArrayPoints[i].Y += radius + 5;
            }
            float slope = ((pentArrayPoints[2].Y - pentArrayPoints[0].Y) / (pentArrayPoints[2].X - pentArrayPoints[0].X));
            for (double i = 0; i <= Math.Abs(pentArrayPoints[2].X - pentArrayPoints[0].X); i += 0.1)
            {
                pentDraw.Add(new PointF(Convert.ToSingle(pentArrayPoints[0].X + i), Convert.ToSingle(pentArrayPoints[0].Y - (slope * i))));
            }
            slope = ((pentArrayPoints[4].Y - pentArrayPoints[2].Y) / (pentArrayPoints[4].X - pentArrayPoints[2].X));
            for (double i = 0; i <= Math.Abs(pentArrayPoints[4].X - pentArrayPoints[2].X); i += 0.1)
            {
                pentDraw.Add(new PointF(Convert.ToSingle(pentArrayPoints[3].X - i), Convert.ToSingle(pentArrayPoints[3].Y - (slope * -i))));
            }
            slope = ((pentArrayPoints[1].Y - pentArrayPoints[4].Y) / (pentArrayPoints[1].X - pentArrayPoints[4].X));
            for (double i = 0; i <= Math.Abs(pentArrayPoints[1].X - pentArrayPoints[4].X); i += 0.1)
            {
                pentDraw.Add(new PointF(Convert.ToSingle(pentArrayPoints[1].X + i), Convert.ToSingle(pentArrayPoints[1].Y - (slope * i))));
            }
            slope = ((pentArrayPoints[3].Y - pentArrayPoints[1].Y) / (pentArrayPoints[3].X - pentArrayPoints[1].X));
            for (double i = 0; i <= Math.Abs(pentArrayPoints[3].X - pentArrayPoints[1].X); i += 0.1)
            {
                pentDraw.Add(new PointF(Convert.ToSingle(pentArrayPoints[4].X - i), Convert.ToSingle(pentArrayPoints[4].Y - (slope * -i))));
            }
            slope = ((pentArrayPoints[0].Y - pentArrayPoints[3].Y) / (pentArrayPoints[0].X - pentArrayPoints[3].X));
            for (double i = 0; i <= Math.Abs(pentArrayPoints[0].X - pentArrayPoints[3].X); i += 0.1)
            {
                pentDraw.Add(new PointF(Convert.ToSingle(pentArrayPoints[2].X + i), Convert.ToSingle(pentArrayPoints[2].Y - (slope * i))));
            }
            pentFs = new PointF[pentDraw.Count];
            for (int i = 0; i < pentDraw.Count; i++)
            {
                pentFs[i] = new PointF(pentDraw[i].X, pentDraw[i].Y);
            }
        }

        void DrawCircle(int radius)
        {
            circList.Clear();
            for (double x = -radius; x <= radius; x += 0.1)
            {
                if (x == radius)
                {
                    circList.Add(new PointF(Convert.ToSingle(x), 0));
                }
                else
                {
                    circList.Add(new PointF(Convert.ToSingle(x), Convert.ToSingle(Math.Sqrt(Math.Pow(radius, 2) - Math.Pow(x, 2)))));
                }
            }
            for (double x = radius; x >= -radius; x -= 0.1)
            {
                if (x == -radius)
                {
                    circList.Add(new PointF(Convert.ToSingle(x), 0));
                }
                else
                {
                    circList.Add(new PointF(Convert.ToSingle(x), -1 * Convert.ToSingle(Math.Sqrt(Math.Pow(radius, 2) - Math.Pow(x, 2)))));
                }
            }
            circList.Add(circList[0]);
            for (int i = 0; i < circList.Count; i++)
            {
                circList[i] = new PointF(circList[i].X + radius + 5, circList[i].Y + radius + 5);
            }
            pointFs = new PointF[circList.Count];
            for (int i = 0; i < circList.Count; i++)
            {
                pointFs[i] = new PointF(circList[i].X, circList[i].Y);
            }
        }

        private void fillButton_Click(object sender, EventArgs e)
        {
        }

        public void DrawStar(Pen starPen, float x, float y, float pixels)
        {

        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            if (circDrawTime + 1 >= circList.Count)
            {
                circDrawTime = circList.Count - 2;
                if (circList.Count > 0)
                {
                    e.Graphics.FillPolygon(new SolidBrush(Color.FromArgb(255 - alpha, 125, 125, 125)), pointFs);
                }
            }
            for (int i = 0; i <= circDrawTime; i++)
            {
                e.Graphics.DrawLine(new Pen(Color.Blue), pointFs[i], pointFs[i + 1]);
            }

            if (pentDrawTime + 1 >= pentDraw.Count)
            {
                pentDrawTime = pentDraw.Count - 2;
                if (pentDraw.Count > 0)
                {
                    e.Graphics.FillPolygon(new SolidBrush(Color.FromArgb(alpha,255,0,0)), pentFs);
                }
            }
            for (int i = 0; i < pentDrawTime; i++)
            {
                e.Graphics.DrawLine(new Pen(Color.FromArgb(alpha, 0, 0, 0)), pentDraw[i], pentDraw[i + 1]);
            }
        }

        private void DrawEr_Tick(object sender, EventArgs e)
        {
            circDrawTime += circDrawSpeed;
            pentDrawTime += pentDrawSpeed;
            if (alphaInc)
            {
                alpha += alphaAdd;
                if (alpha > 255)
                {
                    alphaInc = !alphaInc;
                    alpha = 254;
                }
            }
            else
            {
                alpha -= alphaAdd;
                if (alpha < 0)
                {
                    alphaInc = !alphaInc;
                    alpha = 1;
                }
            }
            Refresh();
        }
    }
}
