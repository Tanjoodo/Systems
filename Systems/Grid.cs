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

#region Using statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
#endregion

namespace Systems
{
    public class Grid
    { 
        Vector2[,] grid_array;
        public readonly int columns, rows;
        /// <summary>
        /// Initialize Grid object
        /// </summary>
        /// <param name="Origin">Coordinates of the top left corner of the grid</param>
        /// <param name="Size">Size of the grid</param>
        /// <param name="Columns">Number of columns</param>
        /// <param name="Rows">Number of rows</param>
        public Grid(Vector2 Origin, Vector2 Size, int Columns, int Rows)
        {
            columns = Columns; rows = Rows; //To be used outside the scope of this method.

            //Figure out the size of each cell, basically just divide size by Columns/Rows
            Vector2 cell_size = new Vector2(Size.X / Columns, Size.Y / Rows);
             
            grid_array = new Vector2[Columns, Rows]; //2D array that holds coordinates to every cell in the grid
           
            //Now that we got all the relevant information, we can start constructing the grid.
            PopulateArray(Columns, Rows, cell_size, Origin);
        
        }

        /// <summary>
        /// Initialize Grid object
        /// </summary>
        /// <param name="Origin">Coordinates of the top left corner of the grid</param>
        /// <param name="Size">Size of the grid</param>
        /// <param name="CellSize">Size of each cell</param>
        public Grid(Vector2 Origin, Vector2 Size, Vector2 CellSize)
        {
            columns = (int)(Size.X / CellSize.X); rows = (int)(Size.Y / CellSize.Y); //To be used outside the scope of this method

            grid_array = new Vector2[columns, rows];

            // Now that we got all the relevant information we can start constructing the grid.
            PopulateArray(columns, rows, CellSize, Origin);
           
        }

        void PopulateArray(int Columns, int Rows, Vector2 CellSize, Vector2 Origin)
        {

            //Iterate through the array and fill it with coordinates, we will use a nested for loop for this.
            for (int i = 0; i < Columns; i++)
                {
                    for (int y = 0; y < Rows; y++)
                    {                
                         grid_array[i, y] = new Vector2(i * CellSize.X + Origin.X, y * CellSize.Y + Origin.Y); 
                    }
                }
        }

        /// <summary>
        /// Gets screen coordinates of cell
        /// </summary>
        /// <param name="X">Cell's X position in the grid</param>
        /// <param name="Y">Cell's Y position in the grid</param>
        /// <returns>Returns the screen coordinates for the required cell addressed by its grid coordinates</returns>
        public Vector2 GetCoordinates(int X, int Y)
        {
            return grid_array[X, Y];
        }

        /// <summary>
        /// Get screen coordinates of cell
        /// </summary>
        /// <param name="GridPosition">Cell's position in the grid</param>
        /// <returns>Returns the screen coordinates for the required cell addressed by its grid coordinates</returns>
        public Vector2 GetCoordinates(Vector2 GridPosition)
        {
            return grid_array[(int)GridPosition.X, (int)GridPosition.Y];
        }

        public void ApplyTranformationMatrix(Matrix TranformationMatrix)
        {
            for (int i = 0; i < columns; i++)
            {
                for (int k = 0; k < rows; k++)
                {
                    grid_array[i, k] = Vector2.Transform(grid_array[i, k], TranformationMatrix);
                }
            }
 
        }
    }
}
