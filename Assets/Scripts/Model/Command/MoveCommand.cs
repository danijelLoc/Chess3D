using System;
namespace Assets.Scripts.Model
{
    public class MoveCommand : ICommand
    {
        Vector2Integer startLocation;
        Vector2Integer endLocation;

        public MoveCommand(Vector2Integer startLocation, Vector2Integer endLocation)
        {
            this.startLocation = startLocation;
            this.endLocation = endLocation;
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