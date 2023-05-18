using System;
using System.Collections.Generic;

class Program
{
    static int currentRow = 1;
    static char currentColumn = 'A';
    static List<Move> moves = new List<Move>();
    static Stack<Move> undoMoves = new Stack<Move>();
    static Stack<Move> redoMoves = new Stack<Move>();

    static void Main()
    {
        while (true)
        {
            int move = int.Parse(Console.ReadLine());

            if (move >= 1 && move <= 8)
            {
                int steps = int.Parse(Console.ReadLine());

                if (PerformMove(move, steps))
                {
                    ClearRedoHistory();
                }
                else
                {
                    Console.WriteLine("Move unsuccessful.");
                }
            }
            else if (move == 9)
            {
                if (UndoMove())
                {
                    
                }
                else
                {
                    Console.WriteLine("Undo unsuccessful.");
                }
            }
            else if (move == 10)
            {
                if (RedoMove())
                {
                    
                }
                else
                {
                    Console.WriteLine("Redo unsuccessful.");
                }
            }
            else if (move == 11)
            {
                PrintCurrentPosition();
                break;
            }
        }
    }

    static bool PerformMove(int direction, int steps)
    {
        Move move = new Move(direction, steps);

        int newRow = currentRow;
        char newColumn = currentColumn;

        switch (direction)
        {
            case 1: // Move up
                newRow += steps;
                break;
            case 2: // Move up and left
                newRow += steps;
                newColumn -= (char)steps;
                break;
            case 3: // Move left
                newColumn -= (char)steps;
                break;
            case 4: // Move down and left
                newRow -= steps;
                newColumn -= (char)steps;
                break;
            case 5: // Move down
                newRow -= steps;
                break;
            case 6: // Move down and right
                newRow -= steps;
                newColumn += (char)steps;
                break;
            case 7: // Move right
                newColumn += (char)steps;
                break;
            case 8: // Move up and right
                newRow += steps;
                newColumn += (char)steps;
                break;
            default:
                return false;
        }

        if (IsValidPosition(newRow, newColumn))
        {
            currentRow = newRow;
            currentColumn = newColumn;
            AddUndoMove(direction, steps);
            return true;
        }

        return false;

        moves.Add(move);
        undoMoves.Push(move);
        redoMoves.Clear();
    }

    static bool UndoMove()
    {
        if (undoMoves.Count == 0)
        {
            return false;
        }

        Move lastMove = undoMoves.Pop();

        int newRow = currentRow;
        char newColumn = currentColumn;

        switch (lastMove.Direction)
        {
            case 1: // Move up
                newRow += lastMove.Steps;
                newColumn -= (char)lastMove.Steps;
                break;
            case 2: // Move up and left
                newRow -= lastMove.Steps;
                newColumn += (char)lastMove.Steps;
                break;
            case 3: // Move left
                newColumn += (char)lastMove.Steps;
                break;
            case 4: // Move down and left
                newRow += lastMove.Steps;
                newColumn += (char)lastMove.Steps;
                break;
            case 5: // Move down
                newRow += lastMove.Steps;
                break;
            case 6: // Move down and right
                newRow += lastMove.Steps;
                newColumn -= (char)lastMove.Steps;
                break;
            case 7: // Move right
                newColumn -= (char)lastMove.Steps;
                break;
            case 8: // Move up and right
                newRow -= lastMove.Steps;
                newColumn -= (char)lastMove.Steps;
                break;
            default:
                return false;
        }

        if (IsValidPosition(newRow, newColumn))
        {
            redoMoves.Push(lastMove);
            currentRow = newRow;
            currentColumn = newColumn;
            return true;
        }

        return false;
    }

    static bool RedoMove()
    {
        if (undoMoves.Count == 0)
        {
            return false;
        }

        Move nextMove = redoMoves.Pop();

        int newRow = currentRow;
        char newColumn = currentColumn;

        switch (nextMove.Direction)
        {
            case 1: // Move up
                newRow += nextMove.Steps;
                break;
            case 2: // Move up and left
                newRow += nextMove.Steps;
                newColumn -= (char)nextMove.Steps;
                break;
            case 3: // Move left
                newColumn -= (char)nextMove.Steps;
                break;
            case 4: // Move down and left
                newRow -= nextMove.Steps;
                newColumn -= (char)nextMove.Steps;
                break;
            case 5: // Move down
                newRow -= nextMove.Steps;
                break;
            case 6: // Move down and right
                newRow -= nextMove.Steps;
                newColumn += (char)nextMove.Steps;
                break;
            case 7: // Move right
                newColumn += (char)nextMove.Steps;
                break;
            case 8: // Move up and right
                newRow += nextMove.Steps;
                newColumn += (char)nextMove.Steps;
                break;
            default:
                return false;
        }

        if (IsValidPosition(newRow, newColumn))
        {
            undoMoves.Push(nextMove);
            currentRow = newRow;
            currentColumn = newColumn;
            return true;
        }

        return false;
    }

    static void AddUndoMove(int direction, int steps)
    {
        undoMoves.Push(new Move(direction, steps));
    }

    static bool IsValidPosition(int row, char column)
    {
        return row >= 1 && row <= 8 && column >= 'A' && column <= 'H';
    }

    static void PrintCurrentPosition()
    {
        Console.WriteLine("{0}{1}", currentColumn, currentRow);
    }

    static void ClearRedoHistory()
    {
        redoMoves.Clear();
    }
}

class Move
{
    public int Direction { get; }
    public int Steps { get; }

    public Move(int direction, int steps)
    {
        Direction = direction;
        Steps = steps;
    }
}