using System.Threading.Tasks.Dataflow;
using BLL.Entities;
using BLL.Repositories;

namespace BLL.Services;

public class InventoryService
{
    private readonly IInventoryRepository _inventoryRepository;
    public InventoryEntity? GetInventory(int id)
    {
       return _inventoryRepository.GetInventoryById(id);
    }

    public InventoryService(IInventoryRepository inventoryRepository)
    {
        _inventoryRepository = inventoryRepository;
    }
}