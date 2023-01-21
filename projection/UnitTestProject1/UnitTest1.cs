using System;
using System.Diagnostics;
using projection;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {



        [TestMethod]
        public void TestGetPlaneEquation()
        {
            HidenLines.PlaneEquation eq =
                HidenLines.GetPlaneEquation(new HidenLines.Plane
                {
                    First = new Point3D {X = 1, Y = 0, Z = 0},
                    Second = new Point3D {X = 0, Y = 0, Z = 1},
                    Third = new Point3D {X = 0, Y = 1, Z = 0}
                });
            const double tolerance = 0.01;
            Debug.WriteLine("k - " + eq.K + "\nk1 - " + eq.K1 + "\nk2 - " + eq.K2 + "\nk3 - " + eq.K3);
            Assert.IsTrue(Math.Abs(eq.K - 1) < tolerance && Math.Abs(eq.K1+1) < tolerance && Math.Abs(eq.K2+1) < tolerance && Math.Abs(eq.K3+1) < tolerance);
        }

        [TestMethod]
        public void TestGetGeneralPoint()
        {
            double point =
                HidenLines.GetGeneralPoint(
                    new HidenLines.Line
                    {
                        Start = new Point3D {X = 0, Y = 0, Z = 0},
                        End = new Point3D {X = 1, Y = 0, Z = 1}
                    }, new HidenLines.Plane
                    {
                        First = new Point3D {X = 1, Y = 0, Z = 0},
                        Second = new Point3D {X = 0, Y = 0, Z = 1},
                        Third = new Point3D {X = 0, Y = 1, Z = 0}
                    });
            Assert.AreEqual(0.5,point);
        }

        [TestMethod]
        public void TestGetDeterminatorK()
        {
            double det =
                HidenLines.GetDeterminator(new double[,]
                {
                    {1, 0, 0},
                    {0, 0, 1},
                    {0, 1, 0}
                });
            Assert.AreEqual(-1,det);
        }
    }
}
