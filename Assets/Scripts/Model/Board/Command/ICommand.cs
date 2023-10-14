using System;

namespace Assets.Scripts.Model
{
    public interface ICommand
    {
        Vector2Integer SquareClicked(); 
        void Do(Boolean show = true);
        void Undo(Boolean show = true);
    }
}