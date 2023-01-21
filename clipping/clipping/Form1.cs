using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace clipping
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private const int Fig1 = 3, Fig2 = 4; // число вершин фигур
        private int Step; // число шагов и счетчик
        private int StepCount = 2; // число шагов и счетчик

        // цвет фона
        private static readonly Color BackColor = Color.White;

        // кисти для рисования
        private readonly SolidBrush MainBrush = new SolidBrush(Color.BlueViolet);
        private readonly SolidBrush BackBrush = new SolidBrush(BackColor);

        // размеры длины линий фигур
        private int Size1 = 50;
        private int Size2 = 100;

        private readonly PointF[] Offset = new PointF[Fig2]; //под смещение координат
        private readonly PointF[] CurrentPoints = new PointF[Fig2]; // текущие координаты фигуры
        private PointF[] StartPoints = new PointF[Fig2]; // начальные координаты фигуры
        private PointF[] EndPoints = new PointF[Fig2]; // конечные координаты фигуры   

        private Bitmap image;
        private Graphics graphics;
        private Pen pen = new Pen(Color.Black, 3); //перо рисования

        // запустить отрисовку по таймеру
        private void StartButton_Click(object sender, EventArgs e)
        {
            // задать изображение
            image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            graphics = Graphics.FromImage(image);
            graphics.Clear(BackColor); // очистить
            Step = 0; // номер шага в 0
            // задать начальные координаты фигур
            SetFirstPosition(graphics);
            // запустить таймер
            timer1.Enabled = true;
        }

        // задать начальные координаты фигур
        private void SetFirstPosition(Graphics graphics)
        {
            // задать начальные координаты фигур
            // стартовая фигура    
            StartPoints = GetStartPoints(
                GetFirstFigurePoints(new Point(Size1 / 2 + 20, Size1 / 2 + 20)));
            // конечная фигура
            EndPoints = GetSecondFigurePoints(
                new Point(pictureBox1.Width - Size2 - 20, pictureBox1.Height - Size2 - 20));
            // копировать начальну фигуру в текущую
            StartPoints.CopyTo(CurrentPoints, 0);

            // Рисуем конечную фигуру только для 1 лабы
            if (FirstLabaRadio.Checked)
                // отрисовать конечную фигуры
                DrawFigure(EndPoints, graphics, MainBrush);

            SetOffsets(CurrentPoints, EndPoints);
        }

        // найти координаты первой фигуры через аффинные преобразования
        private PointF[] GetFirstFigurePoints(Point startPosition)
        {
            // угол положительный ( схема связи 1 по часовой )
            var ang = 2 * Math.PI / Fig1;
            var f = new PointF[Fig1];
            f[0].X = Size1 - 20;
            for (var i = 1; i < f.Length; i++)
            {
                f[i].X = (float)(f[0].X * Math.Cos(i * ang) - f[0].Y * Math.Sin(i * ang)) + startPosition.X;
                f[i].Y = (float)(f[0].X * Math.Sin(i * ang) + f[0].Y * Math.Cos(i * ang)) + startPosition.Y;
            }

            f[0].X += startPosition.X;
            f[0].Y += startPosition.Y;
            return f;
        }

        // найти координаты второй фигуры через аффинные преобразования
        private PointF[] GetSecondFigurePoints(Point startPosition)
        {
            // угол положительный ( схема связи 1 по часовой)
            var ang = 2 * Math.PI / Fig2;
            var f = new PointF[Fig2];
            f[0].X = Size2;
            for (var i = 1; i < f.Length; i++)
            {
                f[i].X = (float)(f[0].X * Math.Cos(i * ang) - f[0].Y * Math.Sin(i * ang)) + startPosition.X;
                f[i].Y = (float)(f[0].X * Math.Sin(i * ang) + f[0].Y * Math.Cos(i * ang)) + startPosition.Y;
            }

            f[0].X += startPosition.X;
            f[0].Y += startPosition.Y;
            return f;
        }

        // нарисовать фигуру по координатам
        private void DrawFigure(PointF[] points, Graphics gr, SolidBrush br)
        {
            gr.DrawPolygon(new Pen(br, 4), points);
        }

        // взять массив начальной фигуры
        private PointF[] GetStartPoints(PointF[] fisrtFigurePoints)
        {
            return new[] { fisrtFigurePoints[0], fisrtFigurePoints[1], fisrtFigurePoints[2], fisrtFigurePoints[2] };
        }

        private void FirstLabaRadio_CheckedChanged(object sender, EventArgs e)
        {
            //this.FirstLabaRadio.Checked = true;
            //this.ThirdLabRadio.Checked = false;
        }

        private void ThirdLabRadio_CheckedChanged(object sender, EventArgs e)
        {
            //this.FirstLabaRadio.Checked = false;
            //this.ThirdLabRadio.Checked = true;
        }

        // установить новые точки перехода текущей фигуры относительно второй 
        private void SetOffsets(PointF[] firstFigurePoints, PointF[] secondFigurePoints)
        {
            for (var i = 0; i < firstFigurePoints.Length; i++)
            {
                if (i == firstFigurePoints.Length - 1)
                {
                    Offset[i].X = Math.Abs(firstFigurePoints[i].X - secondFigurePoints[0].X) / StepCount;
                    Offset[i].Y = Math.Abs(firstFigurePoints[i].Y - secondFigurePoints[0].Y) / StepCount;
                    break;
                }

                Offset[i].X = Math.Abs(firstFigurePoints[i].X - secondFigurePoints[i + 1].X) / StepCount;
                Offset[i].Y = Math.Abs(firstFigurePoints[i].Y - secondFigurePoints[i + 1].Y) / StepCount;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // остановить таймер после StepCount шагов
            if (Step++ == StepCount)
            {
                timer1.Enabled = false;
            }
            else // иначе
            {
                if (ThirdLabRadio.Checked)
                {
                    if (!checkBox1.Checked) graphics.Clear(BackColor);

                    //начинаем алгоритм отсечения Кируса-Бека 
                    //рисуем форму окна вариант a
                    pen = new Pen(Color.Gray, 3);


                    //выставлям точки для отрисовки фигуры
                    var A1 = new PointF(400, 100);
                    var A2 = new PointF(200, 400);
                    var A3 = new PointF(600, 400);

                    var A4 = new PointF(400, 200);
                    var A5 = new PointF(280, 350);
                    var A6 = new PointF(520, 350);

                    //Отрисовка линий по точкам
                    DrawLinePointF(A1, A2);
                    DrawLinePointF(A2, A3);
                    DrawLinePointF(A3, A1);
                    DrawLinePointF(A4, A5);
                    DrawLinePointF(A5, A6);
                    DrawLinePointF(A6, A4);
                    pen = new Pen(Color.BlueViolet, 3);

                    //разбиваем наш невыпуклый многоульник на несколько выпуклых
                    var convexPolygons = new List<Polygonal>
                    {
                        new Polygonal(new List<PointF> { A1, A2, A5, A4 }),
                        new Polygonal(new List<PointF> { A1, A4, A6, A3 }),
                        new Polygonal(new List<PointF> { A2, A5, A6, A3 }),
                        //new Polygonal(new List<PointF> { A4, A5, A6 }),
                    };

                    //получаем три сегмента (три отрезка составляющие наш четырехугольник) которые нужно отсечь окном с исп. алг. Кируса-Бека
                    var triangleSegments = new List<LineSegment>
                    {
                        GetSegmentFromLine((int)CurrentPoints[0].X, (int)CurrentPoints[0].Y, (int)CurrentPoints[1].X,
                            (int)CurrentPoints[1].Y),
                        GetSegmentFromLine((int)CurrentPoints[1].X, (int)CurrentPoints[1].Y, (int)CurrentPoints[2].X,
                            (int)CurrentPoints[2].Y),
                        GetSegmentFromLine((int)CurrentPoints[0].X, (int)CurrentPoints[0].Y, (int)CurrentPoints[3].X,
                            (int)CurrentPoints[3].Y),
                        GetSegmentFromLine((int)CurrentPoints[3].X, (int)CurrentPoints[3].Y, (int)CurrentPoints[2].X,
                            (int)CurrentPoints[2].Y)
                    };

                    var result = new List<LineSegment>(); //результат отсечения: сегменты
                   
                    bool isDraw = true;

                    //проходим по всем выпуклым многоугольникам составляющим наше окно и отсекаем отрезки составляющие четырехугольник
                    foreach (var polygon in convexPolygons) //для каждого выпуклого многоугольника
                    {
                        //для каждого отсекаемого отрезка 
                        //применяем алгоритм внутреннего отсечения окном Кируса-Бека
                        result = polygon.CyrusBeckClip(triangleSegments);
                        //if (result.Any())
                        //{
                        //    isDraw = false;
                        //}
                        //рисуем отсеченные отрезки на экране
                        DrawSegments(result);

                        //обнуляем результат
                        result.Clear();
                    }

                    //if (isDraw)
                    //{
                    //    DrawNewFigure();
                        
                    //}
                    // берем новые координаты
                    SetNextPosition();

                    pictureBox1.Image = image;
                }
                //DrawFigure(StartPoints, graphics, MainBrush);
                //// и конечную фигуру
                //DrawFigure(EndPoints, graphics, MainBrush);

                // Рисуем Начальную фигуру только для 1 лабы
                if (FirstLabaRadio.Checked)
                {
                    // отрисовать текущую фигуры
                    DrawNewFigure();
                    // стартовое положение фигуры
                    DrawFigure(StartPoints, graphics, MainBrush);
                    // и конечную фигуру
                    DrawFigure(EndPoints, graphics, MainBrush);
                }
            }
        }

        //рисуем отрезки на экране по полученному массиву сегментов
        private void DrawSegments(List<LineSegment> listSegments)
        {
            foreach (var segment in listSegments)
                DrawLinePointF(segment.A, segment.B);
        }

        //возвращаем LineSegment по полученным целочисленным координатам отрезка
        private LineSegment GetSegmentFromLine(int x1, int y1, int x2, int y2)
        {
            var p1 = new PointF(x1, y1);
            var p2 = new PointF(x2, y2);

            var segment = new LineSegment(p1, p2);

            return segment;
        }


        //алгоритм Кируса-Бека вспомогательные функции
        private void DrawLinePointF(PointF A, PointF B)
        {
            var x1 = (int)Math.Round(A.X);
            var x2 = (int)Math.Round(B.X);
            var y1 = (int)Math.Round(A.Y);
            var y2 = (int)Math.Round(B.Y);
            DrawLine(x1, y1, x2, y2);
        }

        //рисуем линию
        private void DrawLine(int x1, int y1, int x2, int y2)
        {
            //т.к. координаты ординат панели перевернуты относительно человеческого восприятия - переворачиваем их
            //рисуем линию
            graphics.DrawLine(pen, x1, y1, x2, y2);
        }


        private void DrawNewFigure()
        {
            // если со шлейфом
            if (!checkBox1.Checked)
                // рисуем текущую
                DrawFigure(CurrentPoints, graphics, BackBrush);
            // берем новые координаты
            SetNextPosition();
            // отрисовка
            DrawFigure(CurrentPoints, graphics, MainBrush);
            pictureBox1.Image = image;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void CountStepBar_Scroll(object sender, EventArgs e)
        {
            this.StepCount = CountStepBar.Value;
        }

        // установить новые координаты 
        // для текущей фигуры
        private void SetNextPosition()
        {
            for (var j = 0; j < CurrentPoints.Length; j++)
            {
                CurrentPoints[j].X += Offset[j].X;
                CurrentPoints[j].Y += Offset[j].Y;
            }
        }
    }

    public class Polygonal : List<PointF>
    {
        public Polygonal(IEnumerable<PointF> list) : base(list)
        {
        }

        public bool IsConvex //проверка фигуры на выпуклость
        {
            get
            {
                if (Count >= 3) //если это замкнутая фигура (число точек >= 3)
                    for (int a = Count - 2, b = Count - 1, c = 0; c < Count; a = b, b = c, ++c) //обходим углы
                        if (!new LineSegment(this[a], this[b])
                                .TraversingCornersInOneDirection(this[c])) //если знаки векторных произведений для отдельных углов не совпадают
                            return false; //фигура не выпуклая
                return true; //иначе - выпуклая
            }
        }

        public IEnumerable<LineSegment> Ends //генерация ребер (отрезков) из фигуры для перечисления через foreach
        {
            get
            {
                if (Count >= 2)
                    for (int a = Count - 1, b = 0; b < Count; a = b, ++b)
                        yield return new LineSegment(this[a], this[b]);
            }
        }

        private bool CyrusBeckClip(ref LineSegment obj) //действия выполняемы для отсекаемого отрезка
        {
            var objDir = obj.Direction;
            var a = 0.0f;
            var b = 1.0f;

            foreach (var edge in Ends)
            {
                var dot = edge.Normal.GetDot(objDir);
                var integer = Math.Sign(dot);
                switch (integer) //скалярное произведение вектора ребра и внешней нормали
                {
                    case -1: //отрезок направлен с внутренней на внешнюю сторону ребра (ребро тыльное)
                    {
                        var t = obj.InteractionParameter(edge);
                        if (t > a) a = t; //заменяем параметр
                        break;
                    }
                    case 0: //отрезок параллерен ребру
                    {
                        if (!edge.TraversingCornersInOneDirection(obj.A)) return false;
                        break;
                    }
                    case 1: //отрезок направлен с внешней на внутреннюю сторону ребра (ребро фронтальное)
                    {
                        var t = obj.InteractionParameter(edge);
                        if (t < b) b = t;
                        break;
                    }
                }
            }

            if (a > b) //если отрезок полностью невидим
                return false;

            obj = obj.Transform(a, b); //заданная параметрами tA и tB часть отрезка видима
            return true;
        }

        public List<LineSegment> CyrusBeckClip(List<LineSegment> obj)
        {
            if (!IsConvex) //если многоугольник не выпуклый
            {
                Reverse(); //реверс элементов List<PointF>
                if (!IsConvex) return obj;
            }

            var clippedObjs = new List<LineSegment>();
            foreach (var subject in obj) //отсекаем все наши отрезки
            {
                var clippedObj = subject;
                if (CyrusBeckClip(ref clippedObj)) clippedObjs.Add(clippedObj);
            }

            return clippedObjs;
        }
    }

    //структура c ребром, отрезоком и операцииями.
    public struct LineSegment
    {
        public readonly PointF A, B;

        public LineSegment(PointF a, PointF b)
        {
            A = a;
            B = b;
        }

        //(обходятся ли углы в одном направлении)
        public bool TraversingCornersInOneDirection(PointF p)
        {
            var ab = new PointF(B.X - A.X, B.Y - A.Y);
            var ap = new PointF(p.X - A.X, p.Y - A.Y);
            return ab.GetCross(ap) >= 0;
        }
        //нормаль
        public PointF Normal => new PointF(B.Y - A.Y, A.X - B.X); 
        //направление
        public PointF Direction => new PointF(B.X - A.X, B.Y - A.Y); 

        //вычисление параметра t для представления отрезка (ребра) в параметрическом виде
        public float InteractionParameter(LineSegment that)
        {
            var segment = this;
            var edge = that;

            var segmentToEdge = edge.A.GetSub(segment.A);
            var segmentDir = segment.Direction;
            var edgeDir = edge.Direction;

            var t = edgeDir.GetCross(segmentToEdge) / edgeDir.GetCross(segmentDir);

            if (float.IsNaN(t)) //если не float-число
                t = 0;

            return t;
        }

        //отрезок полностью видим (внутри фигуры)
        public LineSegment Transform(float a, float b)
        {
            var d = Direction;
            return new LineSegment(A.AddPoint(d.GetMul(a)), A.AddPoint(d.GetMul(b)));
        }
    }

    public static class PointExtensions //векторные операции
    {
        public static PointF AddPoint(this PointF a, PointF b) //добавление
        {
            return new PointF(a.X + b.X, a.Y + b.Y);
        }

        public static PointF GetSub(this PointF a, PointF b) //разность
        {
            return new PointF(a.X - b.X, a.Y - b.Y);
        }

        public static PointF GetMul(this PointF a, float b)
        {
            return new PointF(a.X * b, a.Y * b);
        }

        public static float GetDot(this PointF a, PointF b) //скалярное произведение
        {
            return a.X * b.X + a.Y * b.Y;
        }

        public static float GetCross(this PointF a, PointF b) //пересечение
        {
            return a.X * b.Y - a.Y * b.X;
        }
    }
}