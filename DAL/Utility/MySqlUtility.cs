using MySql.Data.MySqlClient;

namespace DAL.Utility;

public static class MySqlUtility
{
    public static MySqlConnection OpenConnection()
    {
        MySqlConnection connection = new MySqlConnection("Server=localhost;Database=legoapp;User=root;Password=root");
        connection.Open();
        return connection;
    }
}