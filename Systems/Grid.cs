#region File description
/* File name: Grid.cs
 * 
 * Location: ./Systems/Grid.cs
 * 
 * Purpose: Provide system for snapping elements to grids.
 * 
 * Author: Mohammed "Tanjoodo" Arabiat
 */
#endregion 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace Systems
{
    public class Grid
    { 
        Vector2[,] _gridArray;
        public readonly int _columns, _rows;
        /// <summary>
        /// Initialize Grid object
        /// </summary>
        /// <param name="origin">Coordinates of the top left corner of the grid</param>
        /// <param name="size">Size of the grid</param>
        /// <param name="columns">Number of columns</param>
        /// <param name="rows">Number of rows</param>
        public Grid(Vector2 origin, Vector2 size, int columns, int rows)
        {
            _columns = columns;
            _rows = rows;

            //Figure out the size of each cell, basically just divide size by Columns/Rows
            Vector2 cell_size = new Vector2(size.X / columns, size.Y / rows);
             
            _gridArray = new Vector2[columns, rows]; //2D array that holds coordinates to every cell in the grid
           
            //Now that we got all the relevant information, we can start constructing the grid.
            PopulateArray(columns, rows, cell_size, origin);
        }

        /// <summary>
        /// Initialize Grid object
        /// </summary>
        /// <param name="origin">Coordinates of the top left corner of the grid</param>
        /// <param name="size">Size of the grid</param>
        /// <param name="cellSize">Size of each cell</param>
        public Grid(Vector2 origin, Vector2 size, Vector2 cellSize)
        {
            _columns = (int)(size.X / cellSize.X); _rows = (int)(size.Y / cellSize.Y); //To be used outside the scope of this method

            _gridArray = new Vector2[_columns, _rows];

            // Now that we got all the relevant information we can start constructing the grid.
            PopulateArray(_columns, _rows, cellSize, origin);
           
        }

        void PopulateArray(int columns, int rows, Vector2 cellSize, Vector2 origin)
        {
            //Iterate through the array and fill it with coordinates, we will use a nested for loop for this.
            for (int i = 0; i < columns; i++)
                {
                    for (int y = 0; y < rows; y++)
                    {                
                         _gridArray[i, y] = new Vector2(i * cellSize.X + origin.X, y * cellSize.Y + origin.Y); 
                    }
                }
        }

        /// <summary>
        /// Gets screen coordinates of cell
        /// </summary>
        /// <param name="x">Cell's X position in the grid</param>
        /// <param name="y">Cell's Y position in the grid</param>
        /// <returns>Returns the screen coordinates for the required cell addressed by its grid coordinates</returns>
        public Vector2 GetCoordinates(int x, int y)
        {
            return _gridArray[x, y];
        }

        /// <summary>
        /// Get screen coordinates of cell
        /// </summary>
        /// <param name="gridPosition">Cell's position in the grid</param>
        /// <returns>Returns the screen coordinates for the required cell addressed by its grid coordinates</returns>
        public Vector2 GetCoordinates(Vector2 gridPosition)
        {
            return _gridArray[(int)gridPosition.X, (int)gridPosition.Y];
        }

        public void ApplyTranformationMatrix(Matrix tranformationMatrix)
        {
            for (int i = 0; i < _columns; i++)
            {
                for (int k = 0; k < _rows; k++)
                {
                    _gridArray[i, k] = Vector2.Transform(_gridArray[i, k], tranformationMatrix);
                }
            }
        }
    }
}
