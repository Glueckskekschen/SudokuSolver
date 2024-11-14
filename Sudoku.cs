using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver
{
    internal class Sudoku
    {
        private String[,] sudokuField = new String[9, 9];
        public bool slow { get; set; }

        
        public Sudoku() 
        {
            if(!MapLoad())
                initTestLevel();            
        }

        public void printField()
        {
            Console.Clear();
            for (int i = 0; i < sudokuField.GetLength(0); i++)
            {
                for (int y = 0; y < sudokuField.GetLength(1); y++)
                {
                    if (y == 0)
                    {
                        Console.Write("|" + sudokuField[i, y] + " ");

                    }
                    else if ((y + 1) % 3 == 0)
                    {
                        Console.Write(sudokuField[i, y] + "|");
                    }
                    else
                    {
                        Console.Write(sudokuField[i, y] + " ");
                    }
                }
                Console.WriteLine();
            }
        }

        public void initTestLevel()
        {
            for (int i = 0; i < sudokuField.GetLength(0); i++)
            {
                for (int y = 0; y < sudokuField.GetLength(1); y++)
                {
                    sudokuField[i, y] = "x";
                }
            }
            sudokuField[0, 0] = "5";
            sudokuField[0, 1] = "3";
            sudokuField[0, 4] = "7";
            sudokuField[1, 0] = "6";
            sudokuField[1, 3] = "1";
            sudokuField[1, 4] = "9";
            sudokuField[1, 5] = "5";
            sudokuField[2, 1] = "9";
            sudokuField[2, 2] = "8";
            sudokuField[2, 7] = "6";
            sudokuField[3, 0] = "8";
            sudokuField[3, 4] = "6";
            sudokuField[3, 8] = "3";
            sudokuField[4, 0] = "4";
            sudokuField[4, 3] = "8";
            sudokuField[4, 5] = "3";
            sudokuField[4, 8] = "1";
            sudokuField[5, 0] = "7";
            sudokuField[5, 4] = "2";
            sudokuField[5, 8] = "6";
            sudokuField[6, 1] = "6";
            sudokuField[6, 6] = "2";
            sudokuField[6, 7] = "8";
            sudokuField[7, 3] = "4";
            sudokuField[7, 4] = "1";
            sudokuField[7, 5] = "9";
            sudokuField[7, 8] = "5";
            sudokuField[8, 4] = "8";
            sudokuField[8, 7] = "7";
            sudokuField[8, 8] = "9";
        }

        
        
        public bool trySolver()
        {
            for (int i = 0; i < sudokuField.GetLength(0); i++)
            {
                for (int y = 0; y < sudokuField.GetLength(1); y++)
                {
                    if (sudokuField[i, y] == "x")
                    {
                        for (int test = 1; test <= 9; test++)
                        {
                            sudokuField[i, y] = test.ToString();
                            if (checkFinish())
                            {
                                if(slow)
                                    printField();
                                if (trySolver())
                                {
                                    return true;
                                }
                                else { sudokuField[i, y] = "x"; }
                                
                            }
                            sudokuField[i, y] = "x";
                        }
                        return false;
                    }                                       
                }
            }
            printField();
            return true;
        }

        
        public bool checkFinish()
        {
            for (int i = 0; i < sudokuField.GetLength(0); i++)
            {
                for (int y = 0; y < sudokuField.GetLength(1); y++)
                {
                    if(sudokuField[i, y] != "x")
                    {
                        if(gameRuleCheckField(i, y) == false)
                        {
                            return false;
                        }
                    }                    
                }
            }
            return true;
        }

        private bool gameRuleCheckField(int iCheck, int yCheck)
        {
            int value = int.Parse(sudokuField[iCheck, yCheck]);

            // Row check
            for (int i = 0; i < 9; i++)
            {
                if (i != iCheck && sudokuField[i, yCheck] == value.ToString())
                    return false;
            }

            // Column check
            for (int j = 0; j < 9; j++)
            {
                if (j != yCheck && sudokuField[iCheck, j] == value.ToString())
                    return false;
            }

            // 3x3 box check
            int boxStartRow = (iCheck / 3) * 3;
            int boxStartCol = (yCheck / 3) * 3;
            for (int i = boxStartRow; i < boxStartRow + 3; i++)
            {
                for (int j = boxStartCol; j < boxStartCol + 3; j++)
                {
                    if ((i != iCheck || j != yCheck) && sudokuField[i, j] == value.ToString())
                        return false;
                }
            }
            return true;
        }

        private bool MapLoad()
        {
            char[] validChars = { '1', '2', '3', '4', '5', '6', '7', '8', '9', 'x', ',' };
            try
            {
                string contents = File.ReadAllText(@"Sudoku.txt").Replace(Environment.NewLine,",");

                if (contents.Count(f => f == ',') != 80)
                    throw new Exception("Sudoku.txt is not valid!\nCheck ','");
                if(!contents.All(c => validChars.Contains(c)))
                    throw new Exception("Sudoku.txt is not valid!\nCheck characters");

                contents = contents.Replace(",", "");
                int iCounter = 0;
                for (int i = 0; i < sudokuField.GetLength(0); i++)
                {
                    for (int y = 0; y < sudokuField.GetLength(1); y++)
                    {
                        sudokuField[i, y] = contents[iCounter].ToString();
                        iCounter++;
                    }
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR while try to loading map!!!");
                Console.WriteLine("Message: " + ex.Message);
                Console.ReadKey();
                return false;
            }
            return true;
        }
    }
}
