using BlazorDapper.Model;
using Dapper;
using Microsoft.Data.Sqlite;
using System.Data;
using System.Data.Common;

namespace BlazorDapper.Data
{
    public class CarService
    {
        private readonly SqliteConnection _dbConnection;

        public CarService(SqliteConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }
        public void InitializeDatabase()
        {
            _dbConnection.Open();
            _dbConnection.Execute(@"
            CREATE TABLE IF NOT EXISTS Cars(
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Name TEXT ,
                Price TEXT,
                Image TEXT ,
                Description TEXT)");
        }
        public IEnumerable<Car> GetAllCars()
        {
            _dbConnection.Open();
            var cars = _dbConnection.Query<Car>("SELECT * FROM Cars");
            _dbConnection.Close();
            return cars;
        }

        public void InsertCar(Car car)
        {
            _dbConnection.Execute("INSERT INTO Cars (Name, Price, Image, Description) VALUES (@Name, @Price, @Image, @Description)", car);
            
        }


        public void UpdateCar(Car car)
        {
            _dbConnection.Open();
            _dbConnection.Execute("UPDATE Cars SET Name = @Name, Price = @Price, Image = @Image, Description = @Description  WHERE Id = @Id", car);
            _dbConnection.Close();
        }
        public void DeleteCar(int Id)
        {
            _dbConnection.Open();
            _dbConnection.Execute("DELETE FROM Cars WHERE Id = @Id", new { Id = Id });
            _dbConnection.Close();
        }

        public IEnumerable<Car> GetDetailCar()
        {
            _dbConnection.Open();
            var detailcar = _dbConnection.Query<Car>("SELECT * FROM Cars");
            _dbConnection.Close();
            return detailcar;
        }

    }


}
