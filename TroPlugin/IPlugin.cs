namespace TroPlugin
{
    public interface IPlugin
    {
        string Name { get; }
        void Do();
    }
}