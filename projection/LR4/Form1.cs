using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace projection
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            transMatrix = GetTarnsMatrix();
        }

        private const double Phi = 40*Math.PI/180;

        private const double Theta = 40 * Math.PI / 180;

        private const int Ro = 25*L;

        private const int D = 10 * L;
        
        //Prism parametrs
        private const int L = 500;

        private const int X0 = 100;

        private const int Y0 = 100;

        private const int Z0 =0;

        private const int TwoDlen = L;

        private const int Zlen =  L;

        private const double Sin60 = 0.86602540378443864676372317075294;
        private const double Sin120 = 0.84602540378443864676372317075294;

        private double currentAlpha = 0;

        private const int TriangleRad = TwoDlen / 2;
        
        private static readonly Color ImageBackColor = Color.White;

        private readonly Pen mainPen = new Pen(Color.Purple, 3);

        private readonly Pen backPen = new Pen(Color.Black, 3);
        private readonly Pen osPen = new Pen(Color.Black, 2);

        //задаём координаты призмы
        private readonly double[,] startPrism =
       {
            {X0 - TwoDlen/2, Y0                  , Z0  , 1 },
            {X0            , Y0 + TwoDlen/2      , Z0  , 1 },
            {X0 + TwoDlen/2, Y0                  , Z0       , 1 },
            {X0 + TwoDlen/4, Y0 - TwoDlen/2*Sin60, Z0       , 1 },
            {X0 - TwoDlen/4, Y0 - TwoDlen/2*Sin60, Z0       , 1 },
            {X0 - TwoDlen/2, Y0                  , Z0 + Zlen, 1 },
            {X0            , Y0 + TwoDlen/2      , Z0 + Zlen, 1 },
            {X0 + TwoDlen/2, Y0                  , Z0 + Zlen, 1 },
            {X0 + TwoDlen/4, Y0 - TwoDlen/2*Sin60, Z0 + Zlen, 1 },
            {X0 - TwoDlen/4, Y0 - TwoDlen/2*Sin60, Z0 + Zlen, 1 },
        };

        private double[,] triangle = new double[3, 4];

        private double[,] transMatrix = new double[4, 4];

        //грани
        private readonly int[,] edges =
        {
            {0, 1}, {1, 2}, {2, 3}, {3, 4}, {4, 0}, {5, 6}, {6, 7}, {7, 8}, {8, 9}, {9, 5}, {0, 5}, {1, 6}, {2, 7},
            {3, 8}, {4, 9}
        };

        private Bitmap image;

        private Graphics graphics;

        private void goButton_Click(object sender, EventArgs e)
        {
            Draw();
        }

        //расчёты транс матрицы
        private double[,] GetTarnsMatrix()
        {
            double a = Math.Cos(Phi);
            double b = Math.Sin(Phi);
            double c = Math.Cos(Theta);
            double d = Math.Sin(Theta);
            return new double[,] {{-d, -a*c, -b*d,        0}, 
                                  {c,  -a*d, -b*d,        0}, 
                                  {0,  b,    -a,          0}, 
                                  {0,  0,    Ro,   1}};
        }

        private void Draw()
        {
            currentAlpha = 0;
            image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            graphics = Graphics.FromImage(image);
            graphics.ScaleTransform(1,-1);
            graphics.TranslateTransform(pictureBox1.Width / 2, -pictureBox1.Height / 2);
            graphics.Clear(ImageBackColor);
            transMatrix = GetTarnsMatrix();
            DoAnimation(graphics);
            pictureBox1.Image = image;
        }

        //построение матрицы треугольника
        private double[,] GetTriangleMatrix()
        {
           return new double[,]
            {{X0, Y0, Z0,1}, {X0+ TwoDlen/4, Y0, Z0,1}, {X0 + TwoDlen/2, Y0, Z0,1}};
        }

        //рисуем призму
        private void DrawPrism(Graphics graphics1)
        {
            double[,] viewCoords = Multiplication(startPrism, transMatrix);// {{0,0,10000,1},{0,0,110000,1},{0,10000,10000,1},{10000,0,10000,1}};
            List<PointF> twoDCoordinate = Get2DCoordinate(viewCoords);
            WriteFigure(twoDCoordinate, graphics1, mainPen, /*GetHidenPoint(viewCoords)*/0);
        }

        //анимируем
        private void DoAnimation(Graphics graphics1)
        {
            DrawPrism(graphics1);
            timer1.Enabled = true;
        }

        //перевод в координат на плоскость
        private PointF Get2DCoordinate(double x, double y, double z)
        {
            return new PointF((float) (D*x/z), (float) (D*y/z));
        }

        private List<PointF> Get2DCoordinate(double[,] viewCoordinats)
        {
            if (viewCoordinats.GetLength(1) < 3) throw new ArgumentException("Invalid view coordinates");
            var result = new List<PointF>();
            for (int i = 0; i < viewCoordinats.GetLength(0); i++)
            {
                result.Add(Get2DCoordinate(viewCoordinats[i, 0], viewCoordinats[i, 1], viewCoordinats[i, 2]));
            }
            return result;
        }

        //рисуем фигуру
        private void WriteFigure(IReadOnlyList<PointF> points, Graphics gr, Pen pen, int hidenPoint)
        {
            for (int i = 0; i < edges.GetLength(0); i++)
            {
                if ((edges[i, 0] != 4 && edges[i, 1] != 4) && (edges[i, 0] != 0 && edges[i, 1] != 0))
                {
                    pen.DashStyle = DashStyle.Solid;
                    gr.DrawLine(pen, points[edges[i, 0]], points[edges[i, 1]]);
                }
                else
                {
                    pen.DashStyle = DashStyle.Dash;
                    gr.DrawLine(pen, points[edges[i, 0]], points[edges[i, 1]]);
                }
            }
            pen.DashStyle = DashStyle.Solid;
            var bottom = new PointF(0+5, image.Height -507);
            var top = new PointF(0+5, image.Width / 2);
            gr.DrawLine(osPen, top, bottom);
            bottom = new PointF(0 + 5, image.Height -585 * (float)Sin60);
            top = new PointF(0 + 200, image.Height -630);
            gr.DrawLine(osPen, top, bottom);
            bottom = new PointF(0+5, image.Height - 600 * (float)Sin120);
            top = new PointF(0 - 200, image.Height - 700);
            gr.DrawLine(osPen, top, bottom);
        }

       
        //рисуем треугольник
        private void DrawTriangle(Graphics graphics1)
        {
            double[,] viewCoords = Multiplication(triangle, transMatrix);
            viewCoords[1, 0] += TriangleRad*Math.Cos(currentAlpha);
            viewCoords[1, 2] += TriangleRad * Math.Sin(currentAlpha) ;
            List<PointF> twoDCoordinate = Get2DCoordinate(viewCoords);
            mainPen.DashStyle = DashStyle.Dash;
            graphics1.DrawLine(mainPen,twoDCoordinate[0],twoDCoordinate[1]);
            graphics1.DrawLine(mainPen, twoDCoordinate[1], twoDCoordinate[2]);
            graphics1.DrawLine(mainPen, twoDCoordinate[2], twoDCoordinate[0]);
            mainPen.DashStyle = DashStyle.Solid;
        }

       
        private void DrawSimpleVisibleTriangle(Graphics graphics1)
        {
            double[,] viewCoords = Multiplication(triangle, transMatrix);
            viewCoords[1, 0] += TriangleRad * Math.Cos(currentAlpha);
            viewCoords[1, 2] += TriangleRad * Math.Sin(currentAlpha);
            List<PointF> twoDCoordinate = Get2DCoordinate(viewCoords);
            graphics1.DrawLine(mainPen, twoDCoordinate[2], twoDCoordinate[1]);
            PointF prismPoint = Get2DCoordinate(Multiplication(startPrism, transMatrix))[2];
            double k = (twoDCoordinate[0].Y - twoDCoordinate[0].Y)/(twoDCoordinate[0].X - twoDCoordinate[0].X);
            double b = twoDCoordinate[0].Y - k*twoDCoordinate[0].X;
            graphics1.DrawLine(mainPen, twoDCoordinate[1],  new PointF{X = (float)((prismPoint.Y - b)/k), Y = (float)prismPoint.Y});
            graphics1.DrawLine(mainPen, twoDCoordinate[1], new PointF { X = (float)(prismPoint.Y/1.75), Y = (float)(prismPoint.Y/1.75) });

        }

        private void DrawVisibleTriangle(Graphics graphics1)
        {
            double[,] viewTriangle = Multiplication(triangle, transMatrix);

            viewTriangle[1, 0] += TriangleRad * Math.Cos(currentAlpha) ;
            viewTriangle[1, 2] += TriangleRad * Math.Sin(currentAlpha) ;
            var lines = new List<HidenLines.Line>
            {
                new HidenLines.Line
                {
                    Start = new Point3D {X = viewTriangle[0, 0], Y = viewTriangle[0, 1], Z = viewTriangle[0, 2]},
                    End = new Point3D {X = viewTriangle[1, 0], Y = viewTriangle[1, 1], Z = viewTriangle[1, 2]}
                },
                new HidenLines.Line
                {
                    Start = new Point3D {X = viewTriangle[1, 0], Y = viewTriangle[1, 1], Z = viewTriangle[1, 2]},
                    End = new Point3D {X = viewTriangle[2, 0], Y = viewTriangle[2, 1], Z = viewTriangle[2, 2]}
                },
                new HidenLines.Line
                {
                    Start = new Point3D {X = viewTriangle[2, 0], Y = viewTriangle[2, 1], Z = viewTriangle[2, 2]},
                    End = new Point3D {X = viewTriangle[0, 0], Y = viewTriangle[0, 1], Z = viewTriangle[0, 2]}
                },
            };
            List<HidenLines.Line> visibleParts = HidenLines.GetVisiblePart(lines,  Multiplication(startPrism, transMatrix) );
            var visibleArray = new double[visibleParts.Count*2, 3];
            for (int i = 0; i < visibleParts.Count; i++)
            {
                visibleArray[i*2, 0] = visibleParts[i].Start.X;
                visibleArray[i*2, 1] = visibleParts[i].Start.Y;
                visibleArray[i*2, 2] = visibleParts[i].Start.Z;

                visibleArray[i*2 + 1, 0] = visibleParts[i].End.X;
                visibleArray[i*2 + 1, 1] = visibleParts[i].End.Y;
                visibleArray[i*2 + 1, 2] = visibleParts[i].End.Z;
            }
            List<PointF> twoDvisible = Get2DCoordinate(visibleArray);
            for (int i = 0; i < twoDvisible.Count; i += 2)
            {
                graphics1.DrawLine(mainPen, twoDvisible[i], twoDvisible[i + 1]);
            }
        }

        
        private void timer1_Tick(object sender, EventArgs e)
        {
            currentAlpha += 0.1;
            if (currentAlpha > 2 * Math.PI)
            {
                timer1.Enabled = false;
            }
            else
            {
                WriteNewFigure();
            }
        }

        //прорисовка новой фигуры
        private void WriteNewFigure()
        {
            graphics.Clear(ImageBackColor);
            transMatrix = GetTarnsMatrix();
            triangle = GetTriangleMatrix();
            DrawPrism(graphics);
            DrawTriangle(graphics);
            if (currentAlpha >4.1 && currentAlpha <5.5)
                DrawVisibleTriangle(graphics);
            if (currentAlpha > 1.9 && currentAlpha <4.1 )
                DrawSimpleVisibleTriangle(graphics);
            pictureBox1.Image = image;
        }

        //перемножение матриц
        static double[,] Multiplication(double[,] a, double[,] b)
        {
            if (a.GetLength(1) != b.GetLength(0)) throw new Exception("Матрицы нельзя перемножить");
            var r = new double[a.GetLength(0), b.GetLength(1)];
            for (int i = 0; i < a.GetLength(0); i++)
            {
                for (int j = 0; j < b.GetLength(1); j++)
                {
                    for (int k = 0; k < b.GetLength(0); k++)
                    {
                        r[i, j] += a[i, k] * b[k, j];
                    }
                }
            }
            return r;
        }
    }
    public class Point3D
    {
        public double X { get; set; }

        public double Y { get; set; }

        public double Z { get; set; }
    }
}
