using System.Data;
using BLL.Entities;
using BLL.Repositories;
using DAL.Utility;
using MySql.Data.MySqlClient;

namespace DAL;

public class InventoryRepository : IInventoryRepository
{
    public InventoryEntity GetInventoryById(int id)
    {
        InventoryEntity inventory = new()
        {
            Id = id
        };
        try
        {
            using MySqlConnection connection = MySqlUtility.OpenConnection();
            using MySqlCommand? command = connection.CreateCommand();
            command.CommandText = "SELECT amount_of_sets, inventory_id, lego_set.* FROM inventory_lego_set INNER JOIN lego_set ON inventory_lego_set.lego_set_id = lego_set.id WHERE inventory_id=@inventoryId";
            command.Parameters.AddWithValue("@inventoryId", id);

            using MySqlDataReader? reader = command.ExecuteReader();

            if (!reader.HasRows)
            {
                return inventory;
            }

            while (reader.Read())
            {
                int amount = reader.GetInt32("amount_of_sets");
                int setId = reader.GetInt32("id");
                string name = reader.GetString("name");
                
                inventory.LegoSets.Add(new InventoryLegoSetEntity
                {
                    Amount = amount,
                    LegoSetId = setId,
                    Name = name
                });
            }
            
            return inventory;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public InventoryEntity? AddToInventoryById(int legoSetId, int amountOfSets = 1, int? id = null)
    {
        using MySqlConnection connection = MySqlUtility.OpenConnection();
        using MySqlCommand? command = connection.CreateCommand();
        command.CommandText = "SELECT 1 FROM inventory_lego_set WHERE lego_set_id = @legoSetId";
        if ((int) command.ExecuteScalar() == 1)
        {
            using MySqlCommand? updateCommand = connection.CreateCommand();
            updateCommand.CommandText = "UPDATE";
        }
        else
        {
            
        }

        return null;
    }
}




    
        


