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

        public void CreateDatabase()
        {
            var dbPath = this.settings.AeroSceneryDBDirectory + @"\aerofly.db";

            if (!File.Exists(dbPath))
            {
                SQLiteConnection.CreateFile("aerofly.db");
                CreateSchema();
            }
        }

        public List<GridSquare> GetAllGridSquares()
        {
            throw new NotImplementedException();
        }

        public void UpdateGridSquare(GridSquare gridSquare)
        {
            throw new NotImplementedException();
        }

        public void CreateGridSquare(GridSquare gridSquare)
        {
            throw new NotImplementedException();
        }

        public void DeleteGridSquare(GridSquare gridSquare)
        {
            throw new NotImplementedException();
        }

        public GridSquare FindGridSquare(string key)
        {
            throw new NotImplementedException();
        }

        public void UpgradeDatabase()
        {
            var dbPath = this.settings.AeroSceneryDBDirectory + @"\aerofly.db";

            if (!File.Exists(dbPath))
            {
                CreateDatabase();
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
                         GridSquareId         INTEGER PRIMARY KEY AUTOINCREMENT
                      )");
            }
        }

        private SQLiteConnection DbConnection()
        {
            var dbPath = this.settings.AeroSceneryDBDirectory + @"\aerofly.db";
            return new SQLiteConnection("Data Source=" + dbPath);
        }

    }
}
