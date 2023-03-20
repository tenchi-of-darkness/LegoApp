using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Threading.Channels;
using BLL;
using Microsoft.VisualBasic.CompilerServices;
using MySql.Data.MySqlClient;
using Mysqlx.Crud;

namespace DAL;

public class InventoryRepository
{
    public async Task<InventoryEntity?> GetInventoryById(int id)
    {
        try
        {
            await using MySqlConnection connection = new("Server=localhost;Database=legoapp;User=root;Password=root");

            await connection.OpenAsync();
            InventoryEntity inventory = new()
            {
                Id = id
            };

            var setIds = new List<int>();
            {
                await using MySqlCommand command = connection.CreateCommand();

                command.CommandText = $"SELECT * FROM inventory_lego_set WHERE inventory_id = @inventoryId";
                command.Parameters.AddWithValue("@inventoryId", inventory.Id);
                await using DbDataReader reader = await command.ExecuteReaderAsync();
            
                if (!reader.HasRows) return inventory;
                while (await reader.ReadAsync())
                {
                    setIds.Add(reader.GetInt32("lego_set_id"));
                }
            }
            {
                foreach (var setId in setIds)
                {
                    await using MySqlCommand command = connection.CreateCommand();

                    command.CommandText = $"SELECT * FROM lego_set WHERE id = @id";
                    command.Parameters.AddWithValue("@id", setId);
                    await using DbDataReader reader = await command.ExecuteReaderAsync();
                    
                    if (!reader.HasRows) continue;
                    await reader.ReadAsync();
                    
                    LegoSetEntity set = new ()
                    {
                        Id = setId,
                        HeightInCm = reader.GetFloat("height_in_cm"),
                        LengthInCm = reader.GetFloat("length_in_cm"),
                        WidthInCm = reader.GetFloat("width_in_cm"),
                        Name = reader.GetString("name"),
                        Price = !await reader.IsDBNullAsync("price") ? reader.GetDecimal("price") : null,
                        Retired = reader.GetBoolean("retired"),
                        ThemeId = !await reader.IsDBNullAsync("theme_id") ? reader.GetInt32("theme_id") : null,
                        TotalPieces = reader.GetInt32("total_pieces")
                    };
                    inventory.LegoSets.Add(set);
                }
            }
            {
                IEnumerable<LegoSetEntity> setsWithThemes = inventory.LegoSets.Where(legoSet => legoSet.ThemeId!=null);
                IEnumerable<int> themeIds = setsWithThemes.Select(legoSet => legoSet.ThemeId!.Value).Distinct();

                foreach (var themeId in themeIds)
                {
                    await using MySqlCommand command = connection.CreateCommand();

                    command.CommandText = $"SELECT * FROM lego_theme WHERE id = @id";
                    command.Parameters.AddWithValue("@id", themeId);
                    await using DbDataReader reader = await command.ExecuteReaderAsync();
                    
                    if (!reader.HasRows) continue;
                    await reader.ReadAsync();
                    
                    LegoThemeEntity theme = new ()
                    {
                        Id = themeId,
                        Name = reader.GetString("name"),
                        Retired = reader.GetBoolean("retired"),
                    };
                    
                    foreach (LegoSetEntity set in setsWithThemes.Where(legoSet => legoSet.ThemeId == themeId))
                    {
                        set.Theme = theme;
                    }
                }
            }
            return inventory;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }

    public void UpdateInventory(int legoSetId, int inventoryId)
    {
    }
}