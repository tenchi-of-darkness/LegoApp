namespace BLL.Entities;

public class LegoThemeEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool Retired { get; set; }
    public List<LegoSetEntity> LegoSets { get; set; } = new();
}