using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BigSnow.UnitTestHelpers;
using CherwellCodingExercise.Models;

namespace CherwellCodingExercise.Tests.Models
{
    [TestClass]
    public class TriangleTests
    {
        [TestMethod]
        public void ConstructTriangle()
        {
            var a = GetRandomPoint();
            var b = GetRandomPoint();
            var c = GetRandomPoint();

            var result = new Triangle(a, b, c);

            Assert.AreEqual(a, result.A);
            Assert.AreEqual(b, result.B);
            Assert.AreEqual(c, result.C);
        }

        private Point GetRandomPoint()
        {
            return new Point(RandomValueGenerator.Instance.GetRandomInteger(), RandomValueGenerator.Instance.GetRandomInteger());
        }
    }
}
