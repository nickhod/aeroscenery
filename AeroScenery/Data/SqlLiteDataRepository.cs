using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AeroScenery.Common;
using AeroScenery.Data.Models;
using System.IO;
using System.Data.SQLite;
using Dapper;

namespace AeroScenery.Data
{
    public class SqlLiteDataRepository : IDataRepository
    {
        private Settings settings;


        public List<GridSquare> GetAllGridSquares()
        {
            using (var con = DbConnection())
            {
                var query = @"SELECT * FROM GridSquares ORDER BY Name";

                con.Open();
                List<GridSquare> result = con.Query<GridSquare>(query).ToList();

                return result;
            }
        }

        public void CreateGridSquare(GridSquare gridSquare)
        {
            using (var con = DbConnection())
            {
                var query = @"INSERT INTO GridSquares (Name, NorthLatitude, EastLongitude, WestLongitude, SouthLatitude) VALUES 
                            (@Name, @NorthLatitude, @EastLongitude, @WestLongitude, @SouthLatitude);
                            SELECT last_insert_rowid();";

                con.Open();
                gridSquare.GridSquareId = con.Query<long>(query, gridSquare).First();
            }
        }

        public void UpdateGridSquare(GridSquare gridSquare)
        {
            using (var con = DbConnection())
            {
                var query = @"UPDATE GridSquares SET
                            Name=@Name,
                            NorthLatitude=@NorthLatitude,
                            EastLongitude=@EastLongitude,
                            WestLongitude=@WestLongitude,
                            SouthLatitude=@SouthLatitude 
                            WHERE GridSquareId=@GridSquareId";

                con.Open();
                con.Query(query, gridSquare);
            }
        }



        public void DeleteGridSquare(GridSquare gridSquare)
        {
            using (var con = DbConnection())
            {
                var query = @"DELETE FROM GridSquares WHERE GridSquareId=@GridSquareId;";

                con.Open();
                con.Query(query, gridSquare);
            }
        }

        public GridSquare FindGridSquare(string name)
        {
            using (var con = DbConnection())
            {
                var query = @"SELECT * FROM GridSquares WHERE Name = @name";

                con.Open();
                GridSquare result = con.Query<GridSquare>(query, new { name }).FirstOrDefault();

                return result;
            }
        }

        public void UpgradeDatabase()
        {
            var dbPath = this.settings.AeroSceneryDBDirectory + @"\aerofly.db";

            if (!File.Exists(dbPath))
            {
                CreateDatabase();
            }

        }

        public void CreateDatabase()
        {
            var dbPath = this.settings.AeroSceneryDBDirectory + @"\aerofly.db";

            if (!File.Exists(dbPath))
            {
                SQLiteConnection.CreateFile("aerofly.db");
                CreateSchema();
            }
        }

        private void CreateSchema()
        {
            using (var con = DbConnection())
            {
                con.Open();
                con.Execute(
                    @"create table GridSquares
                      (
                        GridSquareId         INTEGER PRIMARY KEY AUTOINCREMENT,
                        Name                 TEXT,
                        NorthLatitude        REAL,
                        EastLongitude        REAL,
                        WestLongitude        REAL,
                        SouthLatitude        REAL
                      )");

                con.Execute(@"CREATE UNIQUE INDEX ix_GridSquare_Name ON GridSquares (Name ASC);");
            }
        }

        private SQLiteConnection DbConnection()
        {
            var dbPath = this.settings.AeroSceneryDBDirectory + @"\aerofly.db";
            return new SQLiteConnection("Data Source=" + dbPath);
        }

        public Settings Settings
        {
            get
            {
                return this.settings;
            }

            set
            {
                this.settings = value;
            }
        }

    }
}
