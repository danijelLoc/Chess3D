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

        public void Do()
        {
            throw new NotImplementedException();
        }

        public void Undo()
        {
            throw new NotImplementedException();
        }
    }
}