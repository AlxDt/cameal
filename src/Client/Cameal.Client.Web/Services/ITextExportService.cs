using Cameal.Client.Web.Models;

namespace Cameal.Client.Web.Services;

public interface ITextExportService
{
    string ExportGroceryListToSamsungNotes(IEnumerable<GroceryListItemDto> groceryList);
}
