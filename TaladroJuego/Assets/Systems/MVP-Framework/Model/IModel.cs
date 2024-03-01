namespace MVPFramework.Model
{
    public interface IModel<out TState>
    {
        TState Capture();
    }
}