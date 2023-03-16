using System;
using UnityEngine;
namespace Assets.Scripts.Model

{
    public class PromotionCommand : MoveCommand
    {

        public PromotionCommand(Piece selectedPiece, Vector2Integer startSquareLocation, Piece pieceToBeCaptured, Vector2Integer endSquareLocation) :
            base(selectedPiece, startSquareLocation, pieceToBeCaptured, endSquareLocation)
        { }

        public override void Do()
        {
            base.Do();
            SelectedPiece.Promote();
            Debug.Log("pawn promoted");
        }

        public override void Undo()
        {
            base.Undo();
            SelectedPiece.Demote();
            Debug.Log("queen demoted");
        }
    }
}