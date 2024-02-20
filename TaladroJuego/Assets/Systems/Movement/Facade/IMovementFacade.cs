namespace MovementSystem.Facade
{
    public interface IMovementFacade<TInput> 
    {
        void Move(TInput input);
    }
}

