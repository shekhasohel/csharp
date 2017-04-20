using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knight
{
    class Program
    {
        int length, width; // declare length and width of board respectively.
        int knightX, knightY; // declare starting co-ordinates of Knight.

        int[,] board = new int[100, 100]; // Array to define board. 100*100 is maximum size of board
        bool[,] tmpBoard = new bool[100, 100]; // temporary board to help in calculating moves

        // declare list to keep track of remaining coordinates to make move
        struct coordinate
        {
            public int x,y,iteration;
        }
        List<coordinate> pendingList = new List<coordinate>();
        
        static void Main(string[] args)
        {
            Program newboard = new Program();

            // define length and width of board
            newboard.length = Convert.ToInt16(args[0]);
            newboard.width = Convert.ToInt16(args[1]);

            // define starting co-ordinates of Knight
            newboard.knightX = Convert.ToInt16(args[2]);
            newboard.knightY = Convert.ToInt16(args[3]);

            // start moves calculation process
            newboard.calculateMoves();
        }

        void calculateMoves()
        {
            // mark entire board as -1 initially (this leaves the unreached coordinates as -1 always)
            for (int j = 0; j < width; j++)
            {
                for (int i = 0; i < length; i++)
                {
                    board[i, j] = -1;
                }
            }

            // mark Knights initial co-ordinate as 0
            board[knightX, knightY] = 0;
            tmpBoard[knightX,knightY] = true;

            // add initial co-ordinate to list, starting from Knights first position
            pendingList.Add(new coordinate { x = knightX, y = knightY, iteration = 1 });

            // recursively make moves until entire board is reached
            while(pendingList.Count > 0)
            {
                makemoves(pendingList.FirstOrDefault());
            }

            // print calculated moves of the board
            for (int j = 0; j < width; j++)
            {
                for (int i = 0; i < length; i++)
                {
                    Console.Write(board[i, j] + " ");
                }
                Console.Write("\n");
            }
        }

        /// <summary>
        /// check and make all possible moves from current coordinate
        /// </summary>
        /// <param name="newCoordinate">current coordinate to iterate</param>
        void makemoves(coordinate newCoordinate)
        {
            int X, Y, Iteration;

            // initialize position of current coordinate
            X = newCoordinate.x;
            Y = newCoordinate.y;
            Iteration = newCoordinate.iteration;

            // remove processing coordinate from pending list
            pendingList.Remove(newCoordinate);

            // Move 1 (UP -> LEFT)
            // validate if move is valid from Knight's current position, and also Knight is reaching on that co-ordinate first time
            if (((X - 1 >= 0 && X - 1 < length) && (Y - 2 >= 0 && Y - 2 < width)) && (tmpBoard[X - 1, Y - 2] == false))
            {
                // if yes, then add number of iteration (number of moves) Knight took to reach this co-ordinate
                board[X-1, Y-2] = Iteration;
                tmpBoard[X-1, Y-2] = true;

                // add new co-ordinate in pending list to make future moves from
                pendingList.Add(new coordinate{x = X-1, y = Y-2, iteration = Iteration + 1});
            }

            // Move 2 (UP -> RIGHT)
            // validate if move is valid from Knight's current position, and also Knight is reaching on that co-ordinate first time
            if (((X + 1 >= 0 && X + 1 < length) && (Y - 2 >= 0 && Y - 2 < width)) && (tmpBoard[X + 1, Y - 2] == false))
            {
                // if yes, then add number of iteration (number of moves) Knight took to reach this co-ordinate
                board[X + 1, Y - 2] = Iteration;
                tmpBoard[X + 1, Y - 2] = true;

                // add new co-ordinate in pending list to make future moves from
                pendingList.Add(new coordinate { x = X + 1, y = Y - 2, iteration = Iteration + 1 });
            }

            // Move 3 (Right -> UP)
            // validate if move is valid from Knight's current position, and also Knight is reaching on that co-ordinate first time
            if (((X + 2 >= 0 && X + 2 < length) && (Y - 1 >= 0 && Y - 1 < width)) && (tmpBoard[X + 2, Y - 1] == false))
            {
                // if yes, then add number of iteration (number of moves) Knight took to reach this co-ordinate
                board[X + 2, Y - 1] = Iteration;
                tmpBoard[X + 2, Y - 1] = true;

                // add new co-ordinate in pending list to make future moves from
                pendingList.Add(new coordinate { x = X + 2, y = Y - 1, iteration = Iteration + 1 });
            }

            // Move 4 (RIGHT -> DOWN)
            // validate if move is valid from Knight's current position, and also Knight is reaching on that co-ordinate first time
            if (((X + 2 >= 0 && X + 2 < length) && (Y + 1 >= 0 && Y + 1 < width)) && (tmpBoard[X + 2, Y + 1] == false))
            {
                // if yes, then add number of iteration (number of moves) Knight took to reach this co-ordinate
                board[X + 2, Y + 1] = Iteration;
                tmpBoard[X + 2, Y + 1] = true;

                // add new co-ordinate in pending list to make future moves from
                pendingList.Add(new coordinate { x = X + 2, y = Y + 1, iteration = Iteration + 1 });
            }

            // Move 5 (DOWN -> RIGHT)
            // validate if move is valid from Knight's current position, and also Knight is reaching on that co-ordinate first time
            if (((X + 1 >= 0 && X + 1 < length) && (Y + 2 >= 0 && Y + 2 < width)) && (tmpBoard[X + 1, Y + 2] == false))
            {
                // if yes, then add number of iteration (number of moves) Knight took to reach this co-ordinate
                board[X + 1, Y + 2] = Iteration;
                tmpBoard[X + 1, Y + 2] = true;

                // add new co-ordinate in pending list to make future moves from
                pendingList.Add(new coordinate { x = X + 1, y = Y + 2, iteration = Iteration + 1 });
            }

            // Move 6 (DOWN -> LEFT)
            // validate if move is valid from Knight's current position, and also Knight is reaching on that co-ordinate first time
            if (((X - 1 >= 0 && X - 1 < length) && (Y + 2 >= 0 && Y + 2 < width)) && (tmpBoard[X - 1, Y + 2] == false))
            {
                // if yes, then add number of iteration (number of moves) Knight took to reach this co-ordinate
                board[X - 1, Y + 2] = Iteration;
                tmpBoard[X - 1, Y + 2] = true;

                // add new co-ordinate in pending list to make future moves from
                pendingList.Add(new coordinate { x = X - 1, y = Y + 2, iteration = Iteration + 1 });
            }

            // Move 7 (LEFT -> DOWN)
            // validate if move is valid from Knight's current position, and also Knight is reaching on that co-ordinate first time
            if (((X - 2 >= 0 && X - 2 < length) && (Y + 1 >= 0 && Y + 1 < width)) && (tmpBoard[X - 2, Y + 1] == false))
            {
                // if yes, then add number of iteration (number of moves) Knight took to reach this co-ordinate
                board[X - 2, Y + 1] = Iteration;
                tmpBoard[X - 2, Y + 1] = true;

                // add new co-ordinate in pending list to make future moves from
                pendingList.Add(new coordinate { x = X - 2, y = Y + 1, iteration = Iteration + 1 });
            }

            // Move 8 (LEFT -> UP)
            // validate if move is valid from Knight's current position, and also Knight is reaching on that co-ordinate first time
            if (((X - 2 >= 0 && X - 2 < length) && (Y - 1 >= 0 && Y - 1 < width)) && (tmpBoard[X - 2, Y - 1] == false))
            {
                // if yes, then add number of iteration (number of moves) Knight took to reach this co-ordinate
                board[X - 2, Y - 1] = Iteration;
                tmpBoard[X - 2, Y - 1] = true;

                // add new co-ordinate in pending list to make future moves from
                pendingList.Add(new coordinate { x = X - 2, y = Y - 1, iteration = Iteration + 1 });
            }
        }
    }
}