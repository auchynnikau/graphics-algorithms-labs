using System.Drawing;

namespace clipping
{
    //public struct LineSegment //структура кот. содержит ребро, отрезок и операции с ним.
    //{
    //    public readonly PointF A, B;

    //    public LineSegment(PointF a, PointF b)
    //    {
    //        A = a;
    //        B = b;
    //    }

    //    public bool TraversingCornersInOneDirection(PointF p) //совпадают ли знаки векторных произведений отдельных углов? (обходятся ли углы в одном направлении)
    //    {
    //        var ab = new PointF(B.X - A.X, B.Y - A.Y);
    //        var ap = new PointF(p.X - A.X, p.Y - A.Y);
    //        return ab.GetCross(ap) >= 0;
    //    }

    //    public PointF Normal => new PointF(B.Y - A.Y, A.X - B.X); //нормаль

    //    public PointF Direction => new PointF(B.X - A.X, B.Y - A.Y); //направление

    //    public float InteractionParameter(LineSegment that) //вычисление параметра t для представления отрезка (ребра) в параметрическом виде (см. лекции)
    //    {
    //        var segment = this;
    //        var edge = that;

    //        var segmentToEdge = edge.A.GetSub(segment.A);
    //        var segmentDir = segment.Direction;
    //        var edgeDir = edge.Direction;

    //        var t = edgeDir.GetCross(segmentToEdge) / edgeDir.GetCross(segmentDir);

    //        if (float.IsNaN(t)) //если не float-число
    //            t = 0;

    //        return t;
    //    }

    //    public LineSegment Transform(float tA, float tB) //отрезок полностью видим (внутри фигуры)
    //    {
    //        var d = Direction;
    //        return new LineSegment(A.AddPoint(d.GetMul(tA)), A.AddPoint(d.GetMul(tB)));
    //    }
    //}
}