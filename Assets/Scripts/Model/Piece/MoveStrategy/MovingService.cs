using System;
using System.Collections;
using System.Collections.Generic;

namespace Assets.Scripts.Model
{
    public class MovingService
    {
        public static List<Vector2Integer> AvailableMoves(Piece piece, BoardLayout boardLayout)
        {
            switch(piece.Type) {
                case PieceType.Pawn:
                    return PawnAvailableMoves(piece, boardLayout);
                case PieceType.Knight:
                    return KnightAvailableMoves(piece, boardLayout);
                case PieceType.Bishop:
                    return BishopAvailableMoves(piece, boardLayout);
                case PieceType.Rook:
                    return RookAvailableMoves(piece, boardLayout);
                case PieceType.Queen:
                    return QueenAvailableMoves(piece, boardLayout);
                case PieceType.King:
                    return KingAvailableMoves(piece, boardLayout);
                default:
                    throw new NotImplementedException("Moving strategy for piece type is not implemented");
            }
        }

        public static List<Vector2Integer> PawnAvailableMoves(Piece piece, BoardLayout boardLayout)
        {
            // TODO check if is shielding King
            return new List<Vector2Integer> { new Vector2Integer(piece.CurrentSquare.X, piece.CurrentSquare.Y + 1) };
        }

        public static List<Vector2Integer> RookAvailableMoves(Piece piece, BoardLayout boardLayout)
        {
            throw new NotImplementedException("Moving strategy for piece type is not implemented");
        }

        public static List<Vector2Integer> BishopAvailableMoves(Piece piece, BoardLayout boardLayout)
        {
            throw new NotImplementedException("Moving strategy for piece type is not implemented");
        }

        public static List<Vector2Integer> KnightAvailableMoves(Piece piece, BoardLayout boardLayout)
        {
            throw new NotImplementedException("Moving strategy for piece type is not implemented");
        }

        public static List<Vector2Integer> QueenAvailableMoves(Piece piece, BoardLayout boardLayout)
        {
            throw new NotImplementedException("Moving strategy for piece type is not implemented");
        }

        public static List<Vector2Integer> KingAvailableMoves(Piece piece, BoardLayout boardLayout)
        {
            throw new NotImplementedException("Moving strategy for piece type is not implemented");
        }
    }
}
