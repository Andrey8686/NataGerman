using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace WebSite.Extensions
{
    public static class ExcelExtensions
    {
        public static ExcelRange SetValue(this ExcelRange cells, string val)
        {
            cells.Value = val;
            return cells;
        }

        public static ExcelRange SetColor(this ExcelRange cells, Color val)
        {
            cells.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
            cells.Style.Fill.BackgroundColor.SetColor(val);
            return cells;
        }

        public static ExcelRange SetFontColor(this ExcelRange cells, Color val)
        {
            cells.Style.Font.Color.SetColor(val);
            return cells;
        }

        public static ExcelRange Bold(this ExcelRange cells)
        {
            cells.Style.Font.Bold = true;
            return cells;
        }

        public static ExcelRange Center(this ExcelRange cells)
        {
            cells.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            return cells;
        }

        public static ExcelRange Left(this ExcelRange cells)
        {
            cells.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
            return cells;
        }

        public static ExcelRange Right(this ExcelRange cells)
        {
            cells.Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
            return cells;
        }

        public static ExcelRange Border(this ExcelRange cells, Color color, ExcelBorderStyle style = ExcelBorderStyle.Thin)
        {
            cells.Style.Border.BorderAround(style, color);
            return cells;
        }

        public static ExcelRange Merge(this ExcelRange cells)
        {
            cells.Merge = true;
            return cells;
        }

        public static ExcelRange DateFormat(this ExcelRange cells)
        {
            cells.Style.Numberformat.Format = "dd mm";
            return cells;
        }

        public static ExcelWorksheet AutoFit(this ExcelWorksheet ws, int cnt)
        {
            for (var col = 1; col < cnt; col++)
            {
                ws.Column(col).AutoFit();
            }

            return ws;
        }
    }
}