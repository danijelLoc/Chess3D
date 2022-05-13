using System;
using System.Collections;
using System.Collections.Generic;

namespace Assets.Scripts.Model
{
    public class MovingService
    {

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
            return MovingUtils.ContinuousMoves(MovingUtils.lineDirections, selectedPiece, board, Board.SquaresInRow);
        }

        private static List<MoveCommand> BishopAvailableMoves(Piece selectedPiece, Board board)
        {
            return MovingUtils.ContinuousMoves(MovingUtils.diagonalDirections, selectedPiece, board, Board.SquaresInRow);
        }

        private static List<MoveCommand> QueenAvailableMoves(Piece selectedPiece, Board board)
        {
            List<MoveCommand> diagonalMoves = MovingUtils.ContinuousMoves(MovingUtils.diagonalDirections, selectedPiece, board, Board.SquaresInRow);
            List<MoveCommand> lineMoves = MovingUtils.ContinuousMoves(MovingUtils.lineDirections, selectedPiece, board, Board.SquaresInRow);
            diagonalMoves.AddRange(lineMoves);
            return diagonalMoves;
        }

        private static List<MoveCommand> KingAvailableMoves(Piece selectedPiece, Board board)
        {
            List<MoveCommand> diagonalMoves = MovingUtils.ContinuousMoves(MovingUtils.diagonalDirections, selectedPiece, board, 1);
            List<MoveCommand> lineMoves = MovingUtils.ContinuousMoves(MovingUtils.lineDirections, selectedPiece, board, 1);
            diagonalMoves.AddRange(lineMoves);
            return diagonalMoves;
        }

        private static List<MoveCommand> KnightAvailableMoves(Piece selectedPiece, Board board)
        {
            List<MoveCommand> moves = new List<MoveCommand>();
            foreach (var direction in MovingUtils.knightDirections)
            {
                Vector2Integer potentialSquare = selectedPiece.CurrentSquare + direction;
                if (!Board.IsSquareInsideBoard(potentialSquare))
                    continue;
                Piece pieceOnPotentialSquare = board.Layout[potentialSquare.X, potentialSquare.Y];
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
                }
                else if (pieceOnPotentialSquare.Team == selectedPiece.Team)
                    continue;
            }
            return moves;
        }

        private static List<MoveCommand> PawnAvailableMoves(Piece selectedPawn, Board board)
        {
            List<MoveCommand> moves = new List<MoveCommand>();

            // advance
            Vector2Integer direction = MovingUtils.PawnLineDirection(selectedPawn.Team);
            var lineAdvanceMoves = MovingUtils.ContinuousMoves(new List<Vector2Integer> { direction }, selectedPawn,
                board, selectedPawn.MoveCounter == 0 ? 2 : 1, false);
            moves.AddRange(lineAdvanceMoves);

            // atack
            List<Vector2Integer> attackDirections = MovingUtils.PawnAttackDirections(selectedPawn.Team);
            foreach (var attackDirection in attackDirections) {
                Vector2Integer destination = selectedPawn.CurrentSquare + attackDirection;
                if (!Board.IsSquareInsideBoard(destination))
                    continue;
                Piece pieceToCapture = board.Layout[destination.X, destination.Y];
                Piece pieceNextTo = board.Layout[destination.X, selectedPawn.CurrentSquare.Y];
                // pieceToCaptureEnPassant
                if (pieceNextTo != null && pieceNextTo.Type == PieceType.Pawn && pieceNextTo.MoveCounter == 1 &&
                    pieceNextTo.CurrentSquare.Y == MovingUtils.PawnTwoSquareAdvanceYLocation(pieceNextTo.Team))
                {
                    // TODO turn right after two square advance only
                    var enPassant = new MoveCommand(selectedPawn, selectedPawn.CurrentSquare, pieceNextTo, destination);
                    moves.Add(enPassant);
                }
                if (pieceToCapture != null && pieceToCapture.Team != selectedPawn.Team)
                {
                    moves.Add(new MoveCommand(selectedPawn, selectedPawn.CurrentSquare, pieceToCapture, destination));
                }
            }

            return moves;
        }
    }
}
