namespace Assets.Scripts.Model
{
    public interface ICommand
    {
        void  Do();
        void Undo();
    }
}