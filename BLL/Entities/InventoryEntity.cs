namespace BLL.Entities;

public class InventoryEntity
{
    public int Id { get; set; }
    public List<InventoryLegoSetEntity> LegoSets { get; set; } = new();
}