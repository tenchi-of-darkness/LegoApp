using System.Reflection.Metadata.Ecma335;
using BLL.Entities;
using MySql.Data.MySqlClient;

namespace BLL.Repositories;

public interface IInventoryRepository
{
    public InventoryEntity? GetInventoryById(int id);
}