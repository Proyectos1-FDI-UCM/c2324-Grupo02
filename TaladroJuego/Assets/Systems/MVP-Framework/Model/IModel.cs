namespace MVPFramework.Model
{
    public interface IModel<out TModel>
    {
        TModel Capture();
    }
}