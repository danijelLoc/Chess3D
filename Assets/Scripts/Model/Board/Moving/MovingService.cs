using System;
using System.Collections;
using System.Collections.Generic;

namespace Assets.Scripts.Model
{
    public class MovingService
    {
        private static readonly List<Vector2Integer> diagonalDirections = new List<Vector2Integer> {
                new Vector2Integer(1, 1), new Vector2Integer(-1, -1),
                new Vector2Integer(1, -1), new Vector2Integer(-1, 1)
        };

        private static readonly List<Vector2Integer> lineDirections = new List<Vector2Integer> {
                new Vector2Integer(0, 1), new Vector2Integer(0, -1),
                new Vector2Integer(1, 0), new Vector2Integer(-1, 0)
        };

        private static readonly List<Vector2Integer> knightDirections = new List<Vector2Integer> {
                new Vector2Integer(2,1), new Vector2Integer(2,-1),
                new Vector2Integer(-2,1), new Vector2Integer(-2,-1),
                new Vector2Integer(1,2), new Vector2Integer(1,-2),
                new Vector2Integer(-1,2), new Vector2Integer(-1,-2),
        };

        public static List<MoveCommand> AvailableMoves(Piece piece, Board biard)
        {
            switch (piece.Type)
            {
                case PieceType.Pawn:
                    return PawnAvailableMoves(piece, biard);
                case PieceType.Knight:
                    return KnightAvailableMoves(piece, biard);
                case PieceType.Bishop:
                    return BishopAvailableMoves(piece, biard);
                case PieceType.Rook:
                    return RookAvailableMoves(piece, biard);
                case PieceType.Queen:
                    return QueenAvailableMoves(piece, biard);
                case PieceType.King:
                    return KingAvailableMoves(piece, biard);
                default:
                    throw new NotImplementedException("Moving strategy for piece type is not implemented");
            }
        }


        private static List<MoveCommand> RookAvailableMoves(Piece selectedPiece, Board board)
        {
            return ContinuousMoves(lineDirections, selectedPiece, board, Board.SquaresInRow);
        }

        private static List<MoveCommand> BishopAvailableMoves(Piece selectedPiece, Board board)
        {
            return ContinuousMoves(diagonalDirections, selectedPiece, board, Board.SquaresInRow);
        }

        private static List<MoveCommand> QueenAvailableMoves(Piece selectedPiece, Board board)
        {
            List<MoveCommand> diagonalMoves = ContinuousMoves(diagonalDirections, selectedPiece, board, Board.SquaresInRow);
            List<MoveCommand> lineMoves = ContinuousMoves(lineDirections, selectedPiece, board, Board.SquaresInRow);
            diagonalMoves.AddRange(lineMoves);
            return diagonalMoves;
        }

        private static List<MoveCommand> KingAvailableMoves(Piece selectedPiece, Board board)
        {
            return ContinuousMoves(lineDirections, selectedPiece, board, 1);
        }

        private static List<MoveCommand> KnightAvailableMoves(Piece selectedPiece, Board board)
        {
            List<MoveCommand> moves = new List<MoveCommand>();
            foreach (var direction in knightDirections)
            {
                Vector2Integer potentialSquare = selectedPiece.CurrentSquare + direction;
                Piece pieceOnPotentialSquare = board.Layout[potentialSquare.X, potentialSquare.Y];
                if (!Board.IsSquareInsideBoard(potentialSquare))
                    break;
                if (pieceOnPotentialSquare == null)
                {
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
            return moves;
        }

        private static List<MoveCommand> ContinuousMoves(List<Vector2Integer> directions, Piece selectedPiece, Board board, int range)
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

        private static List<MoveCommand> PawnAvailableMoves(Piece selectedPiece, Board board)
        {
            // TODO check if is shielding King
            return new List<MoveCommand>();
        }
    }
}
