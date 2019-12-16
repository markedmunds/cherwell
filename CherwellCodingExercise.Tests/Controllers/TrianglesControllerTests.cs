using CherwellCodingExercise.Controllers;
using CherwellCodingExercise.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Drawing;

namespace CherwellCodingExercise.Tests.Controllers
{
    [TestClass]
    public class TrianglesControllerTests
    {
        TrianglesController _controller;

        public TrianglesControllerTests()
        {
            _controller = new TrianglesController();
        }

        [TestMethod]
        public void GetCoordinatesTest_InvalidRow()
        {
            var result = _controller.GetCoordinates("Z", 4);
            Assert.IsInstanceOfType(result.Result, typeof(BadRequestObjectResult));
            Assert.AreEqual("Invalid row value.  Must be 'A-F'", ((BadRequestObjectResult)result.Result).Value);
        }

        [TestMethod]
        public void GetCoordinatesTest_InvalidColumn()
        {
            var result = _controller.GetCoordinates("D", 25);
            Assert.IsInstanceOfType(result.Result, typeof(BadRequestObjectResult));
            Assert.AreEqual("Invalid column value.  Must be '1-12'", ((BadRequestObjectResult)result.Result).Value);
        }

        [TestMethod]
        public void GetCoordinatesTests()
        {
            //Check the corners
            var result = _controller.GetCoordinates("A", 1);
            Assert.AreEqual(new Point(0, 0), result.Value.A);
            Assert.AreEqual(new Point(10, -10), result.Value.B);
            Assert.AreEqual(new Point(0, -10), result.Value.C);

            result = _controller.GetCoordinates("A", 12);
            Assert.AreEqual(new Point(50, 0), result.Value.A);
            Assert.AreEqual(new Point(60, -10), result.Value.B);
            Assert.AreEqual(new Point(60, 0), result.Value.C);

            result = _controller.GetCoordinates("F", 1);
            Assert.AreEqual(new Point(0, -50), result.Value.A);
            Assert.AreEqual(new Point(10, -60), result.Value.B);
            Assert.AreEqual(new Point(0, -60), result.Value.C);

            result = _controller.GetCoordinates("F", 12);
            Assert.AreEqual(new Point(50, -50), result.Value.A);
            Assert.AreEqual(new Point(60, -60), result.Value.B);
            Assert.AreEqual(new Point(60, -50), result.Value.C);

            //spot check a few
            result = _controller.GetCoordinates("B", 7);
            Assert.AreEqual(new Point(30, -10), result.Value.A);
            Assert.AreEqual(new Point(40, -20), result.Value.B);
            Assert.AreEqual(new Point(30, -20), result.Value.C);

            result = _controller.GetCoordinates("E", 9);
            Assert.AreEqual(new Point(40, -40), result.Value.A);
            Assert.AreEqual(new Point(50, -50), result.Value.B);
            Assert.AreEqual(new Point(40, -50), result.Value.C);

            result = _controller.GetCoordinates("D", 4);
            Assert.AreEqual(new Point(10, -30), result.Value.A);
            Assert.AreEqual(new Point(20, -40), result.Value.B);
            Assert.AreEqual(new Point(20, -30), result.Value.C);
        }

        [TestMethod]
        public void GetRowAndColumnTest_InvalidTriangle()
        {
            var result = _controller.GetRowAndColumn(null);
            Assert.IsInstanceOfType(result.Result, typeof(BadRequestObjectResult));
            Assert.AreEqual("Invalid triangle data", ((BadRequestObjectResult)result.Result).Value);
        }

        [TestMethod]
        public void GetRowAndColumnTests()
        {
            //Check the corners
            var triangle = new Triangle(new Point(0, 0), new Point(10, -10), new Point(0, -10));
            var result = _controller.GetRowAndColumn(triangle);
            Assert.AreEqual("A-1", result.Value);

            triangle = new Triangle(new Point(50, 0), new Point(60, -10), new Point(60, 0));
            result = _controller.GetRowAndColumn(triangle);
            Assert.AreEqual("A-12", result.Value);

            triangle = new Triangle(new Point(0, -50), new Point(10, -60), new Point(0, -60));
            result = _controller.GetRowAndColumn(triangle);
            Assert.AreEqual("F-1", result.Value);

            triangle = new Triangle(new Point(50, -50), new Point(60, -60), new Point(60, -50));
            result = _controller.GetRowAndColumn(triangle);
            Assert.AreEqual("F-12", result.Value);


            //spot check a few
            triangle = new Triangle(new Point(30, -10), new Point(40, -20), new Point(30, -20));
            result = _controller.GetRowAndColumn(triangle);
            Assert.AreEqual("B-7", result.Value);

            triangle = new Triangle(new Point(40, -40), new Point(50, -50), new Point(40, -50));
            result = _controller.GetRowAndColumn(triangle);
            Assert.AreEqual("E-9", result.Value);

            triangle = new Triangle(new Point(10, -30), new Point(20, -40), new Point(20, -30));
            result = _controller.GetRowAndColumn(triangle);
            Assert.AreEqual("D-4", result.Value);
        }
    }
}
