using AeroScenery.Common;
using AeroScenery.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroScenery.Data
{
    public interface IDataRepository
    {
        Settings Settings { get; set; }
        /// <summary>
        /// 
        /// </summary>
        void CreateDatabase();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        List<GridSquare> GetAllGridSquares();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gridSquare"></param>
        void UpdateGridSquare(GridSquare gridSquare);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gridSquare"></param>
        void CreateGridSquare(GridSquare gridSquare);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gridSquare"></param>
        void DeleteGridSquare(GridSquare gridSquare);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        GridSquare FindGridSquare(string key);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        void UpgradeDatabase();
    }
}
