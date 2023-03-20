namespace BLL;

public class InventoryEntity
{
    public int Id { get; set; }
    public List<LegoSetEntity> LegoSets { get; set; } = new();
}