namespace InteractionSystem.Interactable
{
    public interface IInteractable<in TInteractor>
    {
        bool Accept(TInteractor interactor);
    }
}
