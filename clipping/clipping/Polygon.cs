using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace clipping
{
    //наследуемся от списка структур точки с плав. запятой на плоскости
    //public class Polygonal :  List<PointF> 
    //{
    //    public Polygonal(IEnumerable<PointF> collection)
    //        : base(collection)
    //    {
    //    }

    //    public bool IsConvex //проверка фигуры на выпуклость
    //    {
    //        get
    //        {
    //            if (Count >= 3) //если это замкнутая фигура (число точек >= 3)
    //                for (int a = Count - 2, b = Count - 1, c = 0; c < Count; a = b, b = c, ++c) //обходим углы
    //                    if (!new LineSegment(this[a], this[b])
    //                            .TraversingCornersInOneDirection(this[c])) //если знаки векторных произведений для отдельных углов не совпадают
    //                        return false; //фигура не выпуклая
    //            return true; //иначе - выпуклая
    //        }
    //    }

    //    public IEnumerable<LineSegment> Ends //генерация ребер (отрезков) из фигуры для перечисления через foreach
    //    {
    //        get
    //        {
    //            if (Count >= 2)
    //                for (int a = Count - 1, b = 0; b < Count; a = b, ++b)
    //                    yield return new LineSegment(this[a], this[b]);
    //        }
    //    }

    //    private bool CyrusBeckClip(ref LineSegment subject) //действия выполняемы для отсекаемого отрезка
    //    {
    //        var subjDir = subject.Direction;
    //        var tA = 0.0f;
    //        var tB = 1.0f;
    //        foreach (var edge in Ends)
    //            switch (Math.Sign(edge.Normal.GetDot(subjDir))) //скалярное произведение вектора ребра и внешней нормали
    //            {
    //                case -1: //отрезок направлен с внутренней на внешнюю сторону ребра (ребро тыльное)
    //                {
    //                    var t = subject.InteractionParameter(edge);
    //                    if (t > tA) tA = t; //заменяем параметр
    //                    break;
    //                }
    //                case 0: //отрезок параллерен ребру
    //                {
    //                    if (!edge.TraversingCornersInOneDirection(subject.A)) return false;
    //                    break;
    //                }
    //                case 1: //отрезок направлен с внешней на внутреннюю сторону ребра (ребро фронтальное)
    //                {
    //                    var t = subject.InteractionParameter(edge);
    //                    if (t < tB) tB = t;
    //                    break;
    //                }
    //            }

    //        if (tA > tB) //если отрезок полностью невидим
    //            return false;
    //        subject = subject.Transform(tA, tB); //заданная параметрами tA и tB часть отрезка видима
    //        return true;
    //    }

    //    public List<LineSegment> CyrusBeckClip(List<LineSegment> subjects)
    //    {
    //        if (!IsConvex) //если многоугольник не выпуклый
    //        {
    //            Reverse(); //реверс элементов List<PointF>
    //            if (!IsConvex)
    //            {
    //                MessageBox.Show("Отсекающий многоугольник должен быть выпуклым");
    //                return subjects;
    //                //throw new InvalidOperationException(""); //
    //            }
    //        }

    //        var clippedSubjects = new List<LineSegment>();
    //        foreach (var subject in subjects) //отсекаем все наши отрезки
    //        {
    //            var clippedSubject = subject;
    //            if (CyrusBeckClip(ref clippedSubject)) clippedSubjects.AddPoint(clippedSubject);
    //        }

    //        return clippedSubjects;
    //    }
    //}
}