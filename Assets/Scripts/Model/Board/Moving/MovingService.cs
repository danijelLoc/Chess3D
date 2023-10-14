using System;
using System.Collections;
using System.Collections.Generic;

namespace Assets.Scripts.Model
{
    public class MovingService
    {

        public static List<ICommand> AvailableMoves(Piece piece, Board board)
        {
            switch (piece.Type)
            {
                case PieceType.Pawn:
                    return PawnAvailableMoves(piece, board);
                case PieceType.Knight:
                    return KnightAvailableMoves(piece, board);
                case PieceType.Bishop:
                    return BishopAvailableMoves(piece, board);
                case PieceType.Rook:
                    return RookAvailableMoves(piece, board);
                case PieceType.Queen:
                    return QueenAvailableMoves(piece, board);
                case PieceType.King:
                    return KingAvailableMoves(piece, board);
                default:
                    throw new NotImplementedException("Moving strategy for piece type is not implemented");
            }
        }

        private static List<ICommand> RookAvailableMoves(Piece selectedPiece, Board board)
        {
            return ContinuousMoves(MovingUtils.lineDirections, selectedPiece, board, Board.SquaresInRow);
        }

        private static List<ICommand> BishopAvailableMoves(Piece selectedPiece, Board board)
        {
            return ContinuousMoves(MovingUtils.diagonalDirections, selectedPiece, board, Board.SquaresInRow);
        }

        private static List<ICommand> QueenAvailableMoves(Piece selectedPiece, Board board)
        {
            List<ICommand> diagonalMoves = ContinuousMoves(MovingUtils.diagonalDirections, selectedPiece, board, Board.SquaresInRow);
            List<ICommand> lineMoves = ContinuousMoves(MovingUtils.lineDirections, selectedPiece, board, Board.SquaresInRow);
            diagonalMoves.AddRange(lineMoves);
            return diagonalMoves;
        }

        private static List<ICommand> KingAvailableMoves(Piece selectedPiece, Board board)
        {
            List<ICommand> diagonalMoves = ContinuousMoves(MovingUtils.diagonalDirections, selectedPiece, board, 1);
            List<ICommand> lineMoves = ContinuousMoves(MovingUtils.lineDirections, selectedPiece, board, 1);

            List<ICommand> lineMovesCheckedForCastling = new List<ICommand>();
            foreach (MoveCommand move in lineMoves) {
                var castlingCommand = GetCastlingCommandInThisMoveDirection(move, board);
                if (castlingCommand != null)
                    lineMovesCheckedForCastling.Add(castlingCommand);
                lineMovesCheckedForCastling.Add(move);
            }

            diagonalMoves.AddRange(lineMovesCheckedForCastling);
            return diagonalMoves;
        }

        private static CastlingCommand GetCastlingCommandInThisMoveDirection(MoveCommand move, Board board)
        {
            Vector2Integer leftDirection = new Vector2Integer(-1, 0);
            Vector2Integer rightDirection = new Vector2Integer(1, 0);
            int firstRow = move.SelectedPiece.Team == Team.White ? 0 : 7;

            var LeftCastlingKingLocation = new Vector2Integer(MovingUtils.LeftCastlingKingXLocation, firstRow);
            var LeftCastlingRookLocation = new Vector2Integer(MovingUtils.LeftCastlingRookXLocation, firstRow);
            var RightCastlingKingLocation = new Vector2Integer(MovingUtils.RightCastlingKingXLocation, firstRow);
            var RightCastlingRookLocation = new Vector2Integer(MovingUtils.RightCastlingRookXLocation, firstRow);

            // TODO can't castle under attack, also if new position is under attack, or if path between is under attack
            var direction = move.EndSquareLocation - move.StartSquareLocation;
            var rookPiece = MovingUtils.FirstPieceInDirection(direction, move.SelectedPiece, board);
            if (move.SelectedPiece.MoveCounter == 0 && rookPiece.Type == PieceType.Rook &&
                rookPiece.MoveCounter == 0 && move.StartSquareLocation.Y == firstRow && rookPiece.Team == move.SelectedPiece.Team)
            {
                // directly checking x cordinate because of possible quizz mode where rook is on different position but has 0 in move counter
                if (direction.Equals(leftDirection) && rookPiece.CurrentSquare.X == 0)
                {
                    return new CastlingCommand(move.SelectedPiece, move.StartSquareLocation, LeftCastlingKingLocation,
                        move.EndSquareLocation-new Vector2Integer(1,0), rookPiece, rookPiece.CurrentSquare, LeftCastlingRookLocation);
                }
                else if (direction.Equals(rightDirection) && rookPiece.CurrentSquare.X == 7) {
                    return new CastlingCommand(move.SelectedPiece, move.StartSquareLocation, RightCastlingKingLocation,
                        move.EndSquareLocation+new Vector2Integer(1,0), rookPiece, rookPiece.CurrentSquare, RightCastlingRookLocation);
                }
                    
            }
            return null;
        }


        private static List<ICommand> KnightAvailableMoves(Piece selectedPiece, Board board)
        {
            List<ICommand> moves = new List<ICommand>();
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


        private static List<ICommand> PawnAvailableMoves(Piece selectedPawn, Board board)
        {
            List<ICommand> moves = new List<ICommand>();

            // advance
            Vector2Integer direction = MovingUtils.PawnLineDirection(selectedPawn.Team);

            var lineAdvanceMoves = ContinuousMoves(
                new List<Vector2Integer> { direction }, 
                selectedPawn, board, selectedPawn.MoveCounter == 0 ? 2 : 1, false
            );
            moves.AddRange(lineAdvanceMoves);

            // attack
            List<Vector2Integer> attackDirections = MovingUtils.PawnAttackDirections(selectedPawn.Team);
            foreach (var attackDirection in attackDirections) {
                Vector2Integer destination = selectedPawn.CurrentSquare + attackDirection;
                if (!Board.IsSquareInsideBoard(destination))
                    continue;
                Piece pieceToCapture = board.Layout[destination.X, destination.Y];
                Piece pieceNextTo = board.Layout[destination.X, selectedPawn.CurrentSquare.Y];
                // pieceToCaptureEnPassant
                if (pieceNextTo != null && pieceNextTo.Type == PieceType.Pawn && pieceNextTo.Team != selectedPawn.Team
                    && pieceNextTo.MoveCounter == 1 && pieceNextTo.CurrentSquare.Y == MovingUtils.PawnTwoSquareAdvanceYLocation(pieceNextTo.Team))
                {
                    // TODO: EnPassant must be next move after enemy pawn moves FIX !!!! 
                    var enPassant = new MoveCommand(selectedPawn, selectedPawn.CurrentSquare, pieceNextTo, destination);
                    moves.Add(enPassant);
                }
                if (pieceToCapture != null && pieceToCapture.Team != selectedPawn.Team)
                {
                    moves.Add(new MoveCommand(selectedPawn, selectedPawn.CurrentSquare, pieceToCapture, destination));
                }
            }

            List<ICommand> movesCheckedForPromotion = new List<ICommand>();
            foreach (MoveCommand move in moves)
            {
                if (move.EndSquareLocation.Y == MovingUtils.PawnPromotionYLocation(move.SelectedPiece.Team))
                    movesCheckedForPromotion.Add(new PromotionCommand(move.SelectedPiece, move.StartSquareLocation, move.PieceToBeCaptured, move.EndSquareLocation));
                else
                    movesCheckedForPromotion.Add(move);
            }

            return (List<ICommand>)movesCheckedForPromotion;
        }

        public static List<ICommand> ContinuousMoves(List<Vector2Integer> directions, Piece selectedPiece, Board board, int range, bool withAttack = true)
        {
            List<ICommand> moves = new List<ICommand>();

            foreach (var direction in directions)
            {
                for (int i = 1; i <= range; i++)
                {
                    Vector2Integer potentialSquare = selectedPiece.CurrentSquare + direction * i;
                    if (!Board.IsSquareInsideBoard(potentialSquare))
                        break;
                    Piece pieceOnPotentialSquare = board.Layout[potentialSquare.X, potentialSquare.Y];
                    if (pieceOnPotentialSquare == null)
                    {
                        MoveCommand move = new MoveCommand(selectedPiece, selectedPiece.CurrentSquare, null, potentialSquare);
                        moves.Add(move);
                    }
                    else if (pieceOnPotentialSquare.Team != selectedPiece.Team)
                    {
                        if (withAttack)
                        {
                            MoveCommand attackMove = new MoveCommand(selectedPiece, selectedPiece.CurrentSquare, pieceOnPotentialSquare, potentialSquare);
                            moves.Add(attackMove);
                        }
                        break;
                    }
                    else if (pieceOnPotentialSquare.Team == selectedPiece.Team)
                        break;
                }
            }
            return moves;
        }
    }
}
