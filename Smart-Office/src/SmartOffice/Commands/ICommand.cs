namespace SmartOffice.Commands
{
    public interface ICommand
    {
        // Executes the command. Returns true if app should exit.
        bool Execute(string[] args);
    }
}
