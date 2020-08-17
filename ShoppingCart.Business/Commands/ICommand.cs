namespace ShoppingCart.Business.Commands
{
    public interface ICommand
    {
        bool CanExecute();
        void Execute();
        void Undo();
    }
}