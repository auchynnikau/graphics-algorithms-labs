using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace projection
{
    public static class HidenLines
    {
        public class Line
        {
            public Point3D Start { get; set; }

            public Point3D End { get; set; }
        }

        private class Pare
        {
            public double Tau { get; set; }

            public double Lambda { get; set; }
        }

        public class Plane
        {
            public Point3D First { get; set; }

            public Point3D Second { get; set; }

            public Point3D Third { get; set; }
        }

        public class PlaneEquation
        {
            public double K1 { get; set; }

            public double K2 { get; set; }

            public double K3 { get; set; }

            public double K { get; set; }
        }

        public static List<Line> GetVisiblePart(List<Line> lines, double[,] prism)
        {
            var planes = /*GetPlanePlanes(prism);*/ GetPrismPlanes(prism);
            List<Line> result = lines;
            foreach (Plane plane in planes)
            {
                result = GetVisiblePart(result, plane);
            }
            return result;
        }


        private static List<Plane> GetPlanePlanes(double[,] prism)
        {
            return new List<Plane>
            {
                new Plane
                {
                    First = new Point3D {X = prism[0, 0], Y = prism[0, 1], Z = prism[0, 2]},
                    Second = new Point3D {X = prism[1, 0], Y = prism[1, 1], Z = prism[1, 2]},
                    Third = new Point3D {X = prism[2, 0], Y = prism[2, 1], Z = prism[2, 2]}
                },
                new Plane
                {
                    First = new Point3D {X = prism[3, 0], Y = prism[3, 1], Z = prism[3, 2]},
                    Second = new Point3D {X = prism[1, 0], Y = prism[1, 1], Z = prism[1, 2]},
                    Third = new Point3D {X = prism[2, 0], Y = prism[2, 1], Z = prism[2, 2]}
                }
            };
        }

        private static List<Plane> GetPrismPlanes(double[,] prism)
        {
            return new List<Plane>
            {
                /*new Plane
                {
                    First = new Point3D {X = prism[0, 0], Y = prism[0, 1], Z = prism[0, 2]},
                    Second = new Point3D {X = prism[1, 0], Y = prism[1, 1], Z = prism[1, 2]},
                    Third = new Point3D {X = prism[2, 0], Y = prism[2, 1], Z = prism[2, 2]}
                },
                new Plane
                {
                    First = new Point3D {X = prism[0, 0], Y = prism[0, 1], Z = prism[0, 2]},
                    Second = new Point3D {X = prism[4, 0], Y = prism[4, 1], Z = prism[4, 2]},
                    Third = new Point3D {X = prism[2, 0], Y = prism[2, 1], Z = prism[2, 2]}
                },
                new Plane
                {
                    First = new Point3D {X = prism[2, 0], Y = prism[2, 1], Z = prism[2, 2]},
                    Second = new Point3D {X = prism[4, 0], Y = prism[4, 1], Z = prism[4, 2]},
                    Third = new Point3D {X = prism[3, 0], Y = prism[3, 1], Z = prism[3, 2]}
                },
                new Plane
                {
                    First = new Point3D {X = prism[0, 0], Y = prism[0, 1], Z = prism[0, 2]},
                    Second = new Point3D {X = prism[1, 0], Y = prism[1, 1], Z = prism[1, 2]},
                    Third = new Point3D {X = prism[5, 0], Y = prism[5, 1], Z = prism[5, 2]}
                },
                new Plane
                {
                    First = new Point3D {X = prism[1, 0], Y = prism[1, 1], Z = prism[1, 2]},
                    Second = new Point3D {X = prism[5, 0], Y = prism[5, 1], Z = prism[5, 2]},
                    Third = new Point3D {X = prism[6, 0], Y = prism[6, 1], Z = prism[6, 2]}
                },*/
                new Plane
                {
                    First = new Point3D {X = prism[1, 0], Y = prism[1, 1], Z = prism[1, 2]},
                    Second = new Point3D {X = prism[6, 0], Y = prism[6, 1], Z = prism[6, 2]},
                    Third = new Point3D {X = prism[7, 0], Y = prism[7, 1], Z = prism[7, 2]}
                },
                new Plane
                {
                    First = new Point3D {X = prism[7, 0], Y = prism[7, 1], Z = prism[7, 2]},
                    Second = new Point3D {X = prism[1, 0], Y = prism[1, 1], Z = prism[1, 2]},
                    Third = new Point3D {X = prism[2, 0], Y = prism[2, 1], Z = prism[2, 2]}
                },
                //new Plane
                //{
                //    First = new Point3D {X = prism[7, 0], Y = prism[7, 1], Z = prism[7, 2]},
                //    Second = new Point3D {X = prism[8, 0], Y = prism[8, 1], Z = prism[8, 2]},
                //    Third = new Point3D {X = prism[2, 0], Y = prism[2, 1], Z = prism[2, 2]}
                //},
                //new Plane
                //{
                //    First = new Point3D {X = prism[2, 0], Y = prism[2, 1], Z = prism[2, 2]},
                //    Second = new Point3D {X = prism[3, 0], Y = prism[3, 1], Z = prism[3, 2]},
                //    Third = new Point3D {X = prism[8, 0], Y = prism[8, 1], Z = prism[8, 2]}
                //},
                /*new Plane
                {
                    First = new Point3D {X = prism[3, 0], Y = prism[3, 1], Z = prism[3, 2]},
                    Second = new Point3D {X = prism[8, 0], Y = prism[8, 1], Z = prism[8, 2]},
                    Third = new Point3D {X = prism[9, 0], Y = prism[9, 1], Z = prism[9, 2]}
                },
                new Plane
                {
                    First = new Point3D {X = prism[3, 0], Y = prism[3, 1], Z = prism[3, 2]},
                    Second = new Point3D {X = prism[4, 0], Y = prism[4, 1], Z = prism[4, 2]},
                    Third = new Point3D {X = prism[9, 0], Y = prism[9, 1], Z = prism[9, 2]}
                },
                new Plane
                {
                    First = new Point3D {X = prism[0, 0], Y = prism[0, 1], Z = prism[0, 2]},
                    Second = new Point3D {X = prism[4, 0], Y = prism[4, 1], Z = prism[4, 2]},
                    Third = new Point3D {X = prism[9, 0], Y = prism[9, 1], Z = prism[9, 2]}
                }, 
                new Plane
                {
                    First = new Point3D {X = prism[0, 0], Y = prism[0, 1], Z = prism[0, 2]},
                    Second = new Point3D {X = prism[5, 0], Y = prism[5, 1], Z = prism[5, 2]},
                    Third = new Point3D {X = prism[9, 0], Y = prism[9, 1], Z = prism[9, 2]}
                },*/
                //new Plane
                //{
                //    First = new Point3D {X = prism[5, 0], Y = prism[5, 1], Z = prism[5, 2]},
                //    Second = new Point3D {X = prism[6, 0], Y = prism[6, 1], Z = prism[6, 2]},
                //    Third = new Point3D {X = prism[7, 0], Y = prism[7, 1], Z = prism[7, 2]}
                //},
                //new Plane
                //{
                //    First = new Point3D {X = prism[5, 0], Y = prism[5, 1], Z = prism[5, 2]},
                //    Second = new Point3D {X = prism[7, 0], Y = prism[7, 1], Z = prism[7, 2]},
                //    Third = new Point3D {X = prism[9, 0], Y = prism[9, 1], Z = prism[9, 2]}
                //},
                //new Plane
                //{
                //    First = new Point3D {X = prism[7, 0], Y = prism[7, 1], Z = prism[7, 2]},
                //    Second = new Point3D {X = prism[8, 0], Y = prism[8, 1], Z = prism[8, 2]},
                //    Third = new Point3D {X = prism[9, 0], Y = prism[9, 1], Z = prism[9, 2]}
                //}
            };
        }

        private static List<Line> GetVisiblePart(IEnumerable<Line> lines, Plane plane)
        {
            var result = new List<Line>();
            foreach (var line in lines)
            {
                result.AddRange(GetVisiblePart(line,plane));
            }
            return result;
        }

        private static List<Line> GetVisiblePart(Line line, Plane plane)
        {
            var generalPoints = GetGeneralPoints(line, plane);
            Debug.WriteLine("r - " + GetPointHigthFormPlane(GetPlaneEquation(GetFirstPyramidSide(plane)), GetPointFromParametr(generalPoints[0].Lambda, line)));

            generalPoints =
                generalPoints.Where(x => x.Lambda <= 1 && x.Lambda >= 0 && x.Tau <= 1 && x.Tau >= 0).ToList();

            Point3D i, j;
            if (generalPoints.Count == 0)
            {
                if (!IsInside(line.Start, plane))
                {
                    return new List<Line> {line};
                }
                i = line.Start;
                j = line.End;
            }
            else if (generalPoints.Count == 1)
            {
                i = GetPointFromParametr(generalPoints[0].Lambda, line);
                j = IsInside(line.Start, plane) ? line.Start : line.End;
            }
            else
            {
                i = GetPointFromParametr(generalPoints[0].Lambda, line);
                j = GetPointFromParametr(generalPoints[1].Lambda, line);
            }
            PlaneEquation eq = GetPlaneEquation(plane);

            if (Math.Abs(line.Start.X - i.X) > Math.Abs(line.Start.X - j.X))
            {
                Point3D temp = i;
                i = j;
                j = temp;
            }

            double he = eq.K; 
            double hi = GetPointHigthFormPlane(eq, i);
            double hj = GetPointHigthFormPlane(eq, j);
            if (Math.Sign(hi) == Math.Sign(hj) && Math.Sign(hj) == Math.Sign(he))
            {
                return new List<Line> {new Line {Start = line.Start, End = line.End}};
            }
            if (Math.Sign(hi) == Math.Sign(hj) && Math.Sign(hj) != Math.Sign(he))
            {
                return new List<Line> {new Line {Start = line.Start, End = i}, new Line {Start = j, End = line.End}};
            }
            if (Math.Sign(hi) != Math.Sign(hj))
            {
                double param = GetGeneralPoint(line, plane);
                Point3D s = GetPointFromParametr(param, line);
                if (param < 0 || param > 1)
                {
                    double hs = GetPointHigthFormPlane(eq, s);
                    Debug.WriteLine(String.Format("hi - {0}\nhj - {1}\nhe - {2}\nhs - {3}\n\n", hi, hj, he, hs));
                    //Thread.Sleep(100000);
                }
                if (Math.Sign(hi) == Math.Sign(he))
                {
                    return new List<Line> {new Line {Start = line.Start, End = s}, new Line {Start = j, End = line.End}};
                }
                return new List<Line> {new Line {Start = line.Start, End = i}, new Line {Start = s, End = line.End}};
            }
            return null;
        }

        private static double GetPointHigthFormPlane(PlaneEquation eq, Point3D point)
        {
            return eq.K + eq.K1*point.X + eq.K2*point.Y + eq.K3*point.Z;
        }

        private static List<Pare> GetGeneralPoints(Line line, Plane plane)
        {
            return new List<Pare>
            {
                new Pare
                {
                    Lambda = GetGeneralPoint(line, GetFirstPyramidSide(plane)),
                    Tau = GetGeneralPoint(new Line {Start = plane.First, End = plane.Second}, GetLinePlane(line)),
                },
                new Pare
                {
                    Lambda = GetGeneralPoint(line, GetSecondPyramidSide(plane)),
                    Tau = GetGeneralPoint(new Line {Start = plane.Second, End = plane.Third}, GetLinePlane(line)),
                },
                new Pare
                {
                    Lambda = GetGeneralPoint(line, GetThirdPyramidSide(plane)),
                    Tau = GetGeneralPoint(new Line {Start = plane.Third, End = plane.First}, GetLinePlane(line)),
                }
            };
        }

        private static Plane GetThirdPyramidSide(Plane plane)
        {
            return new Plane
            {
                First = new Point3D {X = 0, Y = 0, Z = 0},
                Second = plane.Third,
                Third = plane.First
            };
        }

        private static Plane GetSecondPyramidSide(Plane plane)
        {
            return new Plane
            {
                First = new Point3D {X = 0, Y = 0, Z =0},
                Second = plane.Second,
                Third = plane.Third
            };
        }

        private static Plane GetFirstPyramidSide(Plane plane)
        {
            return new Plane
            {
                First = new Point3D {X = 0, Y = 0, Z = 0},
                Second = plane.First,
                Third = plane.Second
            };
        }

        private static Plane GetLinePlane(Line line)
        {
            return new Plane
            {
                First = new Point3D {X = 0, Y = 0, Z = 0},
                Second = line.Start,
                Third = line.End
            };
        }

        private static Point3D GetPointFromParametr(double param, Line line)
        {
            return new Point3D
            {
                X = line.Start.X + param*(line.End.X - line.Start.X),
                Y = line.Start.Y + param*(line.End.Y - line.Start.Y),
                Z = line.Start.Z + param*(line.End.Z - line.Start.Z)
            };
        }

        private static bool IsInside(Point3D point, Plane plane)
        {
            double k1 = plane.First.Y*plane.Second.Z - plane.Second.Y*plane.First.Z;
            double k2 = plane.First.X * plane.Second.Z - plane.Second.X * plane.First.Z;
            double k3 = plane.First.X * plane.Second.Y - plane.Second.X * plane.First.Y;
            double a1 = k1*point.X - k2*point.Y + k3*point.Z;

            k1 = plane.First.Y * plane.Third.Z - plane.Third.Y * plane.First.Z;
            k2 = plane.First.X * plane.Third.Z - plane.Third.X * plane.First.Z;
            k3 = plane.First.X * plane.Third.Y - plane.Third.X * plane.First.Y;
            double a2 = k1 * point.X - k2 * point.Y + k3 * point.Z;

            k1 = plane.Third.Y * plane.Second.Z - plane.Second.Y * plane.Third.Z;
            k2 = plane.Third.X * plane.Second.Z - plane.Second.X * plane.Third.Z;
            k3 = plane.Third.X * plane.Second.Y - plane.Second.X * plane.Third.Y;
            double a3 = k1 * point.X - k2 * point.Y + k3 * point.Z;

            /*PlaneEquation eq = GetPlaneEquation(GetFirstPyramidSide(plane));
            double a1 = -eq.K + eq.K1*point.X - eq.K2*point.Y + eq.K3*point.Z;
            eq = GetPlaneEquation(GetSecondPyramidSide(plane));
            double a2 = -eq.K + eq.K1 * point.X - eq.K2 * point.Y + eq.K3 * point.Z;
            eq = GetPlaneEquation(GetThirdPyramidSide(plane));
            double a3 = -eq.K + eq.K1 * point.X - eq.K2 * point.Y + eq.K3 * point.Z;*/

            return Math.Sign(a1) != Math.Sign(a2) || Math.Sign(a1) != Math.Sign(a3) || Math.Sign(a2) != Math.Sign(a3);
        }
         
        public static double GetGeneralPoint(Line line, Plane plane )
        {
            PlaneEquation eq = GetPlaneEquation(plane);
            return -(eq.K + eq.K1*line.Start.X + eq.K2*line.Start.Y + eq.K3*line.Start.Z)/
                   (eq.K1*(line.End.X - line.Start.X) + eq.K2*(line.End.Y - line.Start.Y) +
                    eq.K3*(line.End.Z - line.Start.Z));

        }

        public static PlaneEquation GetPlaneEquation(Plane plane)
        {
            double k1 = GetDeterminator(new double[,]
            {
                {plane.First.Y,  plane.First.Z,  1},
                {plane.Second.Y, plane.Second.Z, 1},
                {plane.Third.Y,  plane.Third.Z,  1}
            });
            double k2 = GetDeterminator(new double[,]
            {
                {plane.First.X,  plane.First.Z,  1},
                {plane.Second.X, plane.Second.Z, 1},
                {plane.Third.X,  plane.Third.Z,  1}
            });
            double k3 = GetDeterminator(new double[,]
            {
                {plane.First.X,  plane.First.Y,  1},
                {plane.Second.X, plane.Second.Y, 1},
                {plane.Third.X,  plane.Third.Y,  1}
            });
            double k = GetDeterminator(new double[,]
            {
                {plane.First.X,  plane.First.Y,  plane.First.Z },
                {plane.Second.X, plane.Second.Y, plane.Second.Z},
                {plane.Third.X,  plane.Third.Y,  plane.Third.Z }
            });
            return new PlaneEquation {K1 = k1, K2 = -k2, K3 = k3, K = -k};
        }

        public static double GetDeterminator(double[,] array)
        {
            if (array.GetLength(0) != 3 || array.GetLength(1) != 3)
            {
                throw new ArgumentException("Array have to have size 3x3");
            }
            double[,] m = array;
            return m[0, 0]*m[1, 1]*m[2, 2] + m[0, 1]*m[1, 2]*m[2, 0] + m[1, 0]*m[2, 1]*m[0, 2] - m[2, 0]*m[1, 1]*m[0, 2] -
                   m[0, 1]*m[1, 0]*m[2, 2] - m[0, 0]*m[1, 2]*m[2, 1];
        }

      
    }
}
