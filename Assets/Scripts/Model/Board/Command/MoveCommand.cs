using System;
namespace Assets.Scripts.Model
{
    public class MoveCommand : ICommand
    {
        public Piece SelectedPiece { get; private set; } 
        public Vector2Integer StartSquareLocation { get; private set; }
        
        public Piece PieceToBeCaptured { get; private set; }
        public Vector2Integer EndSquareLocation { get; private set; }

        public MoveCommand(Piece selectedPiece, Vector2Integer startSquareLocation, Piece pieceToBeCaptured, Vector2Integer endSquareLocation)
        {
            this.SelectedPiece = selectedPiece;
            this.StartSquareLocation = startSquareLocation;
            this.PieceToBeCaptured = pieceToBeCaptured;
            this.EndSquareLocation = endSquareLocation;
        }

        public virtual void Do(Boolean show = true)
        {
            SelectedPiece.MoveTo(EndSquareLocation, false, show);
            PieceToBeCaptured?.SetIsAlive(false, show);
        }

        public virtual void Undo(Boolean show = true)
        {
            SelectedPiece.MoveTo(StartSquareLocation, true, show);
            PieceToBeCaptured?.SetIsAlive(true, show);
        }

        public Vector2Integer SquareClicked()
        {
            return EndSquareLocation;
        }
    }
}