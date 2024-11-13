using System.Runtime.CompilerServices;

namespace SudokuSolver
{
    internal class Program
    {
        
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            Sudoku myGame = new Sudoku();
            myGame.initTestLevel();
            myGame.slow = true;
            myGame.trySolver();
        }

    }
}