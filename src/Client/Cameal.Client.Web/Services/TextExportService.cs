using System.Text;
using Cameal.Client.Web.Models;

namespace Cameal.Client.Web.Services;

public class TextExportService : ITextExportService
{
    public string ExportGroceryListToSamsungNotes(IEnumerable<GroceryListItemDto> groceryList)
    {
        var sb = new StringBuilder();
        sb.AppendLine("Grocery List");
        sb.AppendLine($"Generated: {DateTime.Now:yyyy-MM-dd HH:mm}");
        sb.AppendLine();
        
        var sortedList = groceryList.OrderBy(i => i.Name).ToList();
        
        foreach (var item in sortedList)
        {
            sb.AppendLine($"- [ ] {item.Name} - {item.TotalQuantity} {item.Unit}");
        }
        
        return sb.ToString();
    }
}
