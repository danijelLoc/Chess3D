using System;
using System.Collections.Generic;

namespace Assets.Scripts.Model
{
    public class GameManager
    {
        private Piece selectedPiece;
        private List<Vector2Integer> selectedPieceAvailableMoves;
        private Board board;
        private Team currentTeam;

        public GameManager(Board startingBoardLayout, Team startingTeam = Team.White)
        {
            this.board = startingBoardLayout;
            currentTeam = startingTeam;
        }

        public void OnSquareSelected(Vector2Integer squareLocation)
        {
            Piece piece = board.Layout[squareLocation.X, squareLocation.Y];
            if (selectedPiece != null)
            {
                if (piece != null && selectedPiece == piece)
                    Deselect();
                else if (piece != null && selectedPiece != piece && piece.Team == currentTeam)
                    Select(piece);
                else
                    TryToMoveSelectedPiece(squareLocation);
            }
            else
            {
                if (piece != null && piece.Team == currentTeam)
                    Select(piece);
            }
        }

        private void TryToMoveSelectedPiece(Vector2Integer destinationSquare)
        {
            // TODO check is it allowed by piece type, and defending king
            Piece enemyPiece = board.Layout[destinationSquare.X, destinationSquare.Y];
            if (enemyPiece != null)
                MoveAndCapture(destinationSquare, enemyPiece);
            else
                MoveSelectedPiece(destinationSquare);
        }

        private void MoveAndCapture(Vector2Integer destinationSquare, Piece enemyPiece)
        {
            selectedPiece.MoveTo(destinationSquare);
            enemyPiece.SetIsAlive(false);
            Deselect();
            SwitchCurrentTeam();
        }

        private void MoveSelectedPiece(Vector2Integer destinationSquare)
        {
            selectedPiece.MoveTo(destinationSquare);
            Deselect();
            SwitchCurrentTeam();
        }

        private void Select(Piece piece)
        {
            Deselect();
            piece.SetIsSelected(true);
            selectedPiece = piece;
            List<MoveCommand> availableMoves = MovingService.AvailableMoves(piece, board);
            board.SetAvailableMoves(availableMoves);
        }

        private void Deselect()
        {
            if (selectedPiece != null)
            {
                selectedPiece.SetIsSelected(false);
                selectedPiece = null;
                board.SetAvailableMoves(new List<MoveCommand>());
            }
        }

        private void SwitchCurrentTeam()
        {
            switch (currentTeam)
            {
                case Team.White:
                    currentTeam = Team.Black; break;

                case Team.Black:
                    currentTeam = Team.White; break;
            }
        }
    }
}

