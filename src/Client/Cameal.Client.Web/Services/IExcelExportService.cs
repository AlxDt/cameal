using Cameal.Client.Web.Models;

namespace Cameal.Client.Web.Services;

public interface IExcelExportService
{
    byte[] ExportGroceryListToExcel(IEnumerable<GroceryListItemDto> groceryList);
}
