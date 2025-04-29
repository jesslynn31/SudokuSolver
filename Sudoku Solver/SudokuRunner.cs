using System;

public class SudokuRunner
{
        static void Main(string[] args)
        {
            Sudoku sudoku = new Sudoku();
   
            sudoku.UserInput();    
            sudoku.Solve();        
            sudoku.printPuzzle();  
        }
    }


