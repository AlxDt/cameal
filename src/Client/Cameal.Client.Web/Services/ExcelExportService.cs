using Cameal.Client.Web.Models;
using ClosedXML.Excel;

namespace Cameal.Client.Web.Services;

public class ExcelExportService : IExcelExportService
{
    public byte[] ExportGroceryListToExcel(IEnumerable<GroceryListItemDto> groceryList)
    {
        using var workbook = new XLWorkbook();
        var worksheet = workbook.Worksheets.Add("Grocery List");

        worksheet.Cell(1, 1).Value = "Ingredient";
        worksheet.Cell(1, 2).Value = "Quantity";
        worksheet.Cell(1, 3).Value = "Unit";

        var headerRange = worksheet.Range(1, 1, 1, 3);
        headerRange.Style.Font.Bold = true;
        headerRange.Style.Fill.BackgroundColor = XLColor.LightBlue;
        headerRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

        var sortedList = groceryList.OrderBy(i => i.Name).ToList();
        
        for (int i = 0; i < sortedList.Count; i++)
        {
            var item = sortedList[i];
            var row = i + 2;
            
            worksheet.Cell(row, 1).Value = item.Name;
            worksheet.Cell(row, 2).Value = item.TotalQuantity;
            worksheet.Cell(row, 3).Value = item.Unit;
        }

        worksheet.Columns().AdjustToContents();

        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        return stream.ToArray();
    }
}
