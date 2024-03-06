namespace MVPFramework.Model
{
    public interface IModel<out TData>
    {
        TData Capture();
    }
}