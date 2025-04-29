using System;
using System.IO;


class Sudoku
{
    private int[,]? puzzle;
    private bool solved;

    public Sudoku() { }

    public void UserInput()
    {
        Console.WriteLine("Enter the puzzle file name: ");
        string? fileName = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(fileName))
        {
            Console.WriteLine("Invalid input. Exiting.");
            return;
        }
        puzzle = ReadPuzzle(fileName!);
    }

    public int[,]? ReadPuzzle(string filename)
    {
        int[,] puzzle = new int[9, 9];
        try
        {
            using StreamReader fileText = new StreamReader(filename);

            for (int i = 0; i < 9; i++)
            {
                string line = fileText.ReadLine();
                string[] nums = line!.Split(' ');

                for (int j = 0; j < 9; j++)
                {
                    puzzle[i, j] = int.Parse(nums[j]);
                }
            }

            return puzzle;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error reading file: " + ex.Message);
            return null;
        }
    }

    private bool ConstraintCheck(int row, int col, int num)
    {
        for (int i = 0; i < 9; i++)
        {
            if (puzzle![row, i] == num || puzzle[i, col] == num)
                return false;
        }

        int startRow = row - row % 3;
        int startCol = col - col % 3;
        for (int i = startRow; i < startRow + 3; i++)
        {
            for (int j = startCol; j < startCol + 3; j++)
            {
                if (puzzle![i, j] == num)
                    return false;
            }
        }

        return true;
    }

    public bool Solve()
    {
        if (puzzle == null) return false;

        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                if (puzzle[i, j] == 0)
                {
                    for (int num = 1; num <= 9; num++)
                    {
                        if (ConstraintCheck(i, j, num))
                        {
                            puzzle[i, j] = num;
                            if (Solve())
                                return true;
                            puzzle[i, j] = 0;
                        }
                    }
                    return false;
                }
            }
        }

        solved = true;
        return true;
    }


    public void printPuzzle()
    {
        if (puzzle == null)
        {
            Console.WriteLine("Puzzle not loaded.");
            return;
        }


        if (!solved)
        {
            Solve();
        }


        Console.WriteLine("Puzzle after solving:");
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {

                Console.Write(puzzle[i, j].ToString() + " ");
            }
            Console.WriteLine();
        }
    }
}