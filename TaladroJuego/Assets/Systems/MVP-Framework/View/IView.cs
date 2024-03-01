namespace MVPFramework.View
{
    public interface IView<in TUpdate>
    {
        bool TryUpdateWith(TUpdate status);
    }
}