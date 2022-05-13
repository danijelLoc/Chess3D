using System;
using System.Collections;
using System.Collections.Generic;

namespace Assets.Scripts.Model
{
    public class MovingUtils
    {
        public static readonly List<Vector2Integer> diagonalDirections = new List<Vector2Integer> {
                new Vector2Integer(1, 1), new Vector2Integer(-1, -1),
                new Vector2Integer(1, -1), new Vector2Integer(-1, 1)
        };

        public static readonly List<Vector2Integer> lineDirections = new List<Vector2Integer> {
                new Vector2Integer(0, 1), new Vector2Integer(0, -1),
                new Vector2Integer(1, 0), new Vector2Integer(-1, 0)
        };

        public static readonly List<Vector2Integer> knightDirections = new List<Vector2Integer> {
                new Vector2Integer(2,1), new Vector2Integer(2,-1),
                new Vector2Integer(-2,1), new Vector2Integer(-2,-1),
                new Vector2Integer(1,2), new Vector2Integer(1,-2),
                new Vector2Integer(-1,2), new Vector2Integer(-1,-2),
        };

        public static List<MoveCommand> ContinuousMoves(List<Vector2Integer> directions, Piece selectedPiece, Board board, int range)
        {
            List<MoveCommand> moves = new List<MoveCommand>();

            foreach (var direction in directions)
            {
                for (int i = 1; i <= range; i++)
                {
                    Vector2Integer potentialSquare = selectedPiece.CurrentSquare + direction * i;
                    Piece pieceOnPotentialSquare = board.Layout[potentialSquare.X, potentialSquare.Y];
                    if (!Board.IsSquareInsideBoard(potentialSquare))
                        break;
                    if (pieceOnPotentialSquare == null)
                    {
                        // TODO undo, start changes maybe
                        MoveCommand move = new MoveCommand(selectedPiece, selectedPiece.CurrentSquare, null, potentialSquare);
                        moves.Add(move);
                    }
                    else if (pieceOnPotentialSquare.Team != selectedPiece.Team)
                    {
                        // TODO check king shielding
                        MoveCommand move = new MoveCommand(selectedPiece, selectedPiece.CurrentSquare, pieceOnPotentialSquare, potentialSquare);
                        moves.Add(move);
                        break;
                    }
                    else if (pieceOnPotentialSquare.Team == selectedPiece.Team)
                        break;
                }
            }
            return moves;
        }

        public static int PawnYDirection(Team team)
        {
            return team == Team.White ? 1 : -1;
        }

        public static Vector2Integer PawnLineDirection(Team team)
        {
            return new Vector2Integer(0, PawnYDirection(team));
        }

        public static List<Vector2Integer> PawnAttackDirections(Team team)
        {
            return new List<Vector2Integer> { new Vector2Integer(-1, PawnYDirection(team)), new Vector2Integer(1, PawnYDirection(team)) };
        }

        public static int PawnTwoSquareAdvanceYLocation(Team team)
        {
            return (Board.SquaresInRow + PawnYDirection(team) * 2) % Board.SquaresInRow;
        }
    }
}
