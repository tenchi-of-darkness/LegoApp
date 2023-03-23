using BLL.Repositories;
using BLL.Services;
using Microsoft.AspNetCore.Mvc;
namespace UIL.Controllers;

public class InventoryController : Controller
{
    private readonly IInventoryRepository _inventoryRepository;
    public InventoryController (IInventoryRepository inventoryRepository)
    {
        _inventoryRepository = inventoryRepository;
    }
    
    // GET
    public IActionResult Index(int id)
    {
        InventoryService service = new (_inventoryRepository);
        return View(service.GetInventory(id));
    }
}