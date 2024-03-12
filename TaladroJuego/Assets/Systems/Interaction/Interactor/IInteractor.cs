namespace InteractionSystem.Interactor
{
    public interface IInteractor<in T>
    {
        bool InteractWith(T resource);
    }
}