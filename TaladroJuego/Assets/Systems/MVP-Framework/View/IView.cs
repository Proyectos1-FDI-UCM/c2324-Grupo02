namespace MVPFramework.View
{
    public interface IView<in TModel>
    {
        bool TryUpdateWith(TModel model);
    }
}