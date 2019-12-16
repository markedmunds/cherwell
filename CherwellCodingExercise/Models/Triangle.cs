using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace CherwellCodingExercise.Models
{
    public class Triangle
    {
        public Triangle() { }
        public Triangle(Point a, Point b, Point c)
        {
            A = a;
            B = b;
            C = c;
        }

        public Point A { get; set; }
        public Point B { get; set; }
        public Point C { get; set; }
    }
}
