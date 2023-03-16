using System;
using UnityEngine;
using System.Collections.Generic;

namespace Assets.Scripts.Model
{
    public class GameManager // TODO: Interface !!!!
    {
        private List<ICommand> playedCommands;
        private int previousCommandIndex; // TODO: Extract to custom commands history manager
        private Piece selectedPiece;
        private List<ICommand> selectedPieceAvailableMoves;
        private Board board;
        private Team currentTeam;

        public GameManager(Board startingBoardLayout, Team startingTeam = Team.White)
        {
            board = startingBoardLayout;
            playedCommands = new List<ICommand>();
            previousCommandIndex = -1;
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

        public void OnUndoSelected() {
            if (previousCommandIndex >= 0) 
            {
                ICommand previousCommand = playedCommands[previousCommandIndex];
                previousCommand.Undo();
                SwitchCurrentTeam();
                previousCommandIndex -= 1;
            }
        }

        public void OnRedoSelected() {
            int nextCommandIndex = previousCommandIndex + 1;
            if (nextCommandIndex < playedCommands.Count) 
            {
                ICommand nextCommand = playedCommands[nextCommandIndex];
                nextCommand.Do();
                SwitchCurrentTeam();
                previousCommandIndex += 1;
            }
        }

        private void RemoveUpcomingCommands() {
            int nextCommandIndex = previousCommandIndex + 1;
            if (nextCommandIndex < playedCommands.Count) 
            {
                playedCommands.RemoveRange(nextCommandIndex, playedCommands.Count - nextCommandIndex);
            }
        }

        private void TryToMoveSelectedPiece(Vector2Integer destinationSquare)
        {
            ICommand availableCommand = AvailableMoveForDestinationSquare(destinationSquare);
            if (availableCommand != null)
            {
                availableCommand.Do();
                RemoveUpcomingCommands();
                playedCommands.Add(availableCommand);
                previousCommandIndex += 1;
                Deselect();
                SwitchCurrentTeam();
            }
        }

        private ICommand AvailableMoveForDestinationSquare(Vector2Integer destinationSquare)
        {
            foreach (var move in selectedPieceAvailableMoves)
            {
                if (move.SquareClicked().Equals(destinationSquare))
                    return move;
            }
            return null;
        }

        private void Select(Piece piece)
        {
            Deselect();
            piece.SetIsSelected(true);
            selectedPiece = piece;
            selectedPieceAvailableMoves = MovingService.AvailableMoves(piece, board);
            board.SetAvailableMoves(selectedPieceAvailableMoves);
        }

        private void Deselect()
        {
            if (selectedPiece != null)
            {
                selectedPiece.SetIsSelected(false);
                selectedPiece = null;
                board.SetAvailableMoves(new List<ICommand>());
            }
        }

        private void SwitchCurrentTeam()
        {
            Debug.Log("Switch teams");
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

