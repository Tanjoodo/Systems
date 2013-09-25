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
        //Initialization requires origin coordinates (the top left corner) of the grid, grid size (in Vector2) and number of columns/rows.
        public Grid(Vector2 Origin, Vector2 Size, int Columns, int Rows)
        {
            columns = Columns; rows = Rows; //To be used outside the scope of this method.

            //Figure out the size of each cell, basically just divide size by Columns/Rows
            Vector2 cell_size = new Vector2(Size.X / Columns, Size.Y / Rows);
             
            grid_array = new Vector2[Columns, Rows]; //2D array that holds coordinates to every cell in the grid
           
            //Now that we got all the relevant information, we can start constructing the grid.
            PopulateArray(Columns, Rows, cell_size, Origin);
        
        }

        //Initialization requires origin coordinates (the top left corner) of the grid, grid size and cell size
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

        // Returns the coordinates for the required cell addressed by its grid coordinates (levels of abstraction FTW!)
        public Vector2 GetCoordinates(int X, int Y)
        {
            return grid_array[X, Y];
        }

        public Vector2 GetCoordinates(Vector2 GridPosition)
        {
            
            return grid_array[(int)GridPosition.X, (int)GridPosition.Y];
        }

    }
}
