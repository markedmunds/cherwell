using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CherwellCodingExercise.Models;
using Microsoft.AspNetCore.Mvc;

namespace CherwellCodingExercise.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrianglesController : ControllerBase
    {
        /*
         A_____C
         |\    |
         | \   |
         |  \  |
         |   \ |
         |____\|
         C     B
        */

        private const int LEG_PIXEL_LENGTH = 10;
        private const int ROW_COUNT = 6;
        private const int COLUMN_COUNT = 12;

        private Regex _rowConstraint = new Regex("[A-F]", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        [HttpGet("rows")]
        public ActionResult<IEnumerable<string>> GetRows()
        {
            var result = new string[ROW_COUNT];
            for (int x = 0; x < ROW_COUNT; x++)
                result[x] = char.ConvertFromUtf32(65 + x);

            return result;
        }

        [HttpGet("columns")]
        public ActionResult<IEnumerable<int>> GetColumns()
        {
            var result = new int[COLUMN_COUNT];
            for (int x = 0; x < COLUMN_COUNT; x++)
                result[x] = x + 1;

            return result;
        }

        [HttpGet("coordinates")]
        public ActionResult<Triangle> GetCoordinates(string row, int column)
        {
            if (string.IsNullOrWhiteSpace(row) || !_rowConstraint.IsMatch(row))
                return BadRequest("Invalid row value.  Must be 'A-F'");
            if (column < 1 || column > 12)
                return BadRequest("Invalid column value.  Must be '1-12'");

            int xBase = GetXBase(column);
            int yBase = GetYBase(row);

            var a = new Point(xBase * LEG_PIXEL_LENGTH, yBase * LEG_PIXEL_LENGTH * -1);
            var b = new Point(a.X + LEG_PIXEL_LENGTH, a.Y - LEG_PIXEL_LENGTH);
            var c = (column % 2 == 0)
                ? new Point(a.X + LEG_PIXEL_LENGTH, a.Y)
                : new Point(a.X, a.Y - LEG_PIXEL_LENGTH);

            return new Triangle(a, b, c);
        }

        [HttpPost("row_and_column")]
        public ActionResult<string> GetRowAndColumn(Triangle triangle)
        {
            if (triangle == null || triangle.A == null || triangle.B == null || triangle.C == null)
                return BadRequest("Invalid triangle data");

            var row = char.ConvertFromUtf32((triangle.A.Y / LEG_PIXEL_LENGTH * -1) + 65);

            var baseColumn = (triangle.C.X / LEG_PIXEL_LENGTH * 2) + 1;

            var column = (triangle.A.Y == triangle.C.Y)
                ? baseColumn - 1
                : baseColumn;

            return $"{row}-{column}";
        }

        private int GetXBase(int column)
        {
            return (int)Math.Floor(((decimal)column - 1) / 2);
        }

        private int GetYBase(string row)
        {
            return row.ToUpper().ToCharArray()[0] - 65;
        }
    }
}
