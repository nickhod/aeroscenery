﻿using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using AeroScenery.Data.Models;
using System.Globalization;

namespace AeroScenery.Data
{
    public class SchemaUpgrader
    {
        private string dbPath;

        public SchemaUpgrader(string dbPath)
        {
            this.dbPath = dbPath;
        }

        public void UpgradeToLatestSchema(int currentSchemaVersion)
        {
            switch (currentSchemaVersion)
            {
                case 1:
                    this.UpgradeToVersion2();
                    break;
                case 2:
                    // Version 3 added as a reminder of how this will work
                    //this.UpgradeToVersion3();
                    break;
            }
        }

        private void SaveNewSchemaVersion(int newSchemaVersion)
        {
            using (var con = DbConnection())
            {
                var dbVersion = new DatabaseVersion();
                dbVersion.DatabaseVersionId = newSchemaVersion;
                dbVersion.UpgradedOn = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);

                var insertSql = @"INSERT INTO DatabaseVersion (DatabaseVersionId, UpgradedOn) VALUES (@DatabaseVersionId, @UpgradedOn);";

                // Add a column to store GridSquare level
                con.Open();
                con.Query(insertSql, dbVersion);
                con.Close();
            }
        }

        private void UpgradeToVersion2()
        {
            using (var con = DbConnection())
            {
                // Add a column to store GridSquare level
                con.Open();
                con.Execute(@"ALTER TABLE GridSquares ADD COLUMN Level INTEGER DEFAULT 9;");
                con.Close();
            }


            this.SaveNewSchemaVersion(2);
            //this.UpgradeToVersion3();
        }

        //private void UpgradeToVersion3()
        //{

        //}

        private SQLiteConnection DbConnection()
        {
            return new SQLiteConnection("Data Source=" + dbPath);
        }
    }
}