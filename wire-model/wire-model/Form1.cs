using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace wire-model
{
    public partial class Form1 : Form
    {
        private class Point3D
        {
            public double X { get; set; }

            public double Y { get; set; }

            public double Z { get; set; }
        }

        public Form1()
        {
            InitializeComponent();
            transMatrix = GetTarnsMatrix();
            RecalculatingValues();
        }
        private int L;
        private int Ro { get; set; }
        private int D { get; set; }
        private int Xlen { get; set; }
        private int Ylen { get; set; }
        private int Zlen { get; set; }
        private int CurrentRo { get; set; }
        private double currentPhi { get; set; }
        private double CurrentTheta { get; set; }
        private double[,] StartPrism { get; set; }
        private double[,] transMatrix;

        private const int X0 = -100;
        private const int Y0 = 50;
        private const int Z0 = 100;
        private readonly int[,] prismEnds =
        {

            { 0, 1 },{ 1, 2 },{ 2, 3 },{3,4},{4,5},{5,1},{5,2},{4,0},{3,0}
            //{ 0, 1 }, { 0, 3 },
            //{ 0, 4 }, { 1, 2 }, 
            //{ 1, 5 }, { 2, 3 },
            //{ 2, 5 }, { 3, 4 },
            //{ 4, 5 }
        };


        private Color boxBackColor;
        private Pen penPrism;
        private Bitmap image;
        private Graphics graphics;
        private int displacementX = -30;
        private int displacementY = -30;


        private void Form1_Load(object sender, EventArgs e)
        {
            this.boxBackColor = Color.White;
            this.penPrism = new Pen(Color.Black, 3);
        }

        private void BoxColor_Click(object sender, EventArgs e)
        {
            if (ColorBox.ShowDialog() == DialogResult.OK)
            {
                this.BoxColor.BackColor = ColorBox.Color;
                this.boxBackColor = ColorBox.Color;
            }
        }

        private void PrismColor_Click(object sender, EventArgs e)
        {
            if (ColorPrism.ShowDialog() == DialogResult.OK)
            {
                this.PrismColor.BackColor = ColorPrism.Color;
                this.penPrism.Color = ColorPrism.Color;
            }
        }

        private void goButton_Click(object sender, EventArgs e)
        {
            this.currentPhi = Convert.ToInt32(this.PhiTxt.Text) * Math.PI / 180;
            this.CurrentTheta = Convert.ToInt32(this.ThetaTxt.Text) * Math.PI / 180;

            image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            graphics = Graphics.FromImage(image);
            graphics.ScaleTransform(1, -1);
            graphics.TranslateTransform((float)(pictureBox1.Width / 2.0), (float)(-pictureBox1.Height / 2.0));
            graphics.Clear(boxBackColor);
            CurrentRo = Ro;
            transMatrix = GetTarnsMatrix();
            DoAnimation(graphics);
            pictureBox1.Image = image;
        }

        private void ExitBtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            currentPhi += 0.1;
            if (CurrentRo < D)
                timer1.Enabled = false;
            else
            {
                WriteNewFigure();
            }
                
        }

        private void WriteNewFigure()
        {
            graphics.Clear(boxBackColor);
            transMatrix = GetTarnsMatrix();
            DrawPrism(graphics);
            pictureBox1.Image = image;
        }

        private double[,] GetTarnsMatrix()
        {
            var a = Math.Cos(currentPhi);
            var b = Math.Sin(currentPhi);
            var c = Math.Cos(CurrentTheta);
            var d = Math.Sin(CurrentTheta);
            return new[,]
            {
                { -d, -a * c, -b * d, 0 },
                { c, -a * d, -b * d, 0 },
                { 0, b, -a, 0 },
                { 0, 0, CurrentRo, 1 }
            };
        }

        private void DrawPrism(Graphics graphics1)
        {
            var viewCoordinats = Multiplication(StartPrism, transMatrix);
            var listCoordinate = Get2DCoordinate(viewCoordinats);
            //var hidenPoint = CalculateHiddenPoints(viewCoordinats);

            var hidenPoint = GetHidenPoint(viewCoordinats);

            // отрисовка фигуры
            WriteFigure(listCoordinate, graphics1, penPrism, hidenPoint);
        }

        //перемножение матриц
        private static double[,] Multiplication(double[,] a, double[,] b)
        {
            var numArray = a.GetLength(1) == b.GetLength(0)
                ? new double[a.GetLength(0), b.GetLength(1)]
                : throw new Exception("Матрицы нельзя перемножить");
            for (var index1 = 0; index1 < a.GetLength(0); ++index1)
            for (var index2 = 0; index2 < b.GetLength(1); ++index2)
            for (var index3 = 0; index3 < b.GetLength(0); ++index3)
                numArray[index1, index2] += a[index1, index3] * b[index3, index2];
            return numArray;
        }

        private PointF Get2DCoordinate(double x, double y, double z) => new PointF((float)(D * x / z), (float)(D * y / z));

        private List<PointF> Get2DCoordinate(double[,] viewCoordinats)
        {
            if (viewCoordinats.GetLength(1) < 3)
                throw new ArgumentException("Invalid view coordinates");
            var pointFList = new List<PointF>();
            for (var index = 0; index < viewCoordinats.GetLength(0); ++index)
                pointFList.Add(Get2DCoordinate(viewCoordinats[index, 0], viewCoordinats[index, 1],
                    viewCoordinats[index, 2]));
            return pointFList;
        }

        private int GetHidenPoint(double[,] viewCoordinats)
        {
            var point3DList = CoordinateList(viewCoordinats);
            var range1 = point3DList.GetRange(0, 6);
            range1.Sort((x, y) => -(int)(x.Y - y.Y));
            var range2 = range1.GetRange(0, 4);
            range2.Sort((x, y) => -(int)(x.X * x.X + x.Z * x.Z - (y.X * y.X + y.Z * y.Z)));
            var point = range2[0];
            //return point3DList.FindIndex(x => x.Equals(point));
            var hidenPoint = point3DList.FindIndex(x => Math.Abs(x.X - point.X) < 0.1 && Math.Abs(x.Y - point.Y) < 0.1 && Math.Abs(x.Z - point.Z) < 0.1);
            int[] hiddenFace = null;
            //for (int i = 0; i < prismEnds.GetLength(0); i++)
            //{
            //    bool isHiddenFace = true;

            //    for (int j = 0; j < prismEnds.GetLength(1); j++)
            //    {
                   
            //    }

            //    if (isHiddenFace)
            //    {
            //        hiddenFace = prismEnds[i,0].;
            //        break;
            //    }
            //}

            return point3DList.FindIndex(x => Math.Abs(x.X - point.X) < 0.1 && Math.Abs(x.Y - point.Y) < 0.1 && Math.Abs(x.Z - point.Z) < 0.1);
        }

        private List<Point3D> CoordinateList(double[,] viewCoordinats)
        {
            // получаем координаты точек
            var point3DList = new List<Point3D>();
            for (var index = 0; index < viewCoordinats.GetLength(0); ++index)
                point3DList.Add(new Point3D
                {
                    X = viewCoordinats[index, 0],
                    Y = viewCoordinats[index, 1],
                    Z = viewCoordinats[index, 2]
                });
            return point3DList;
        }

        private void DoAnimation(Graphics graphics1)
        {
            //начинаем анимацию и отрисовываем первичное положение
            DrawPrism(graphics1);
            timer1.Enabled = true;
        }

        private void WriteFigure(IReadOnlyList<PointF> points, Graphics gr, Pen pen, int hidenPoint)
        {
            //рисуем сами оси если поставлена галка
            if (checkBox1.Checked)
            {
                gr.DrawLine(pen, displacementX - 50, displacementY, displacementX - 50, pictureBox1.Size.Height / 2 - 20); //ось Y
                gr.DrawString("Y", this.Font, Brushes.Purple, new PointF(displacementX - 50, pictureBox1.Size.Height / 2 - 20));
                gr.DrawLine(pen, displacementX - 50, displacementY, pictureBox1.Size.Width / 2 - 150, displacementY); //ось Х
                gr.DrawString("X", this.Font, Brushes.Purple, new PointF(pictureBox1.Size.Width / 2 - 150, displacementY - 20));
                gr.DrawLine(pen, displacementX - 250, displacementY - 100, displacementX + 150, displacementY + 100); //Z
                gr.DrawString("Z",this.Font, Brushes.Purple,new PointF(displacementX - 250, displacementY - 120));
            }

            for (var index = 0; index < prismEnds.GetLength(0); ++index)
            {

                if (prismEnds[index, 0] != hidenPoint  && prismEnds[index, 1] != hidenPoint)
                {
                    //рисусем единую линию
                    pen.DashStyle = DashStyle.Solid;
                    gr.DrawLine(pen, points[prismEnds[index, 0]], points[prismEnds[index, 1]]);
                    gr.DrawString(prismEnds[index, 0].ToString(), this.Font, Brushes.Black, points[prismEnds[index, 0]]);
                    gr.DrawString(prismEnds[index, 1].ToString(), this.Font, Brushes.Black, points[prismEnds[index, 1]]);
                }
                else
                {
                    // рисуем линию сторон которые не видны
                    pen.DashStyle = DashStyle.Dash;
                    gr.DrawLine(pen, points[prismEnds[index, 0]], points[prismEnds[index, 1]]);

                }
            }


            pen.DashStyle = DashStyle.Solid;
        }

        //пересчитываем значения при изменении масштаба и при первой инициализации 
        private void RecalculatingValues()
        {
            this.L = lLengthTrackBar.Value;
            this.Xlen = this.L;
            this.Ylen = L * 3 / 4;
            this.Zlen = this.L;
            this.Ro = 5 * L;
            this.D = 2 * L;
            CurrentRo = Ro;
            this.StartPrism = new double[,]
            {
                { X0, Y0, Z0, 1 },
                { X0, Y0, Z0 + Zlen, 1 },
                { X0 + Xlen, Y0, Z0 + Zlen, 1 },
                { X0 + Xlen, Y0, Z0, 1 },
                { X0, Y0 + Ylen, Z0, 1 },
                { X0, Y0 + Ylen, Z0 + Zlen, 1 },
                //{ X0 + Xlen, Y0 + Ylen, Z0 + Zlen, 1 },
               // { X0 + Xlen, Y0 + Ylen, Z0, 1 }
            };
        }

        private void lLengthTrackBar_Scroll(object sender, EventArgs e)
        { 
            // пересчитываем значения
            RecalculatingValues();
        }
    }
}