namespace MVPFramework.Model
{
    public readonly struct Model<TState> : IModel<TState>
    {
        private readonly TState _state;

        public Model(TState model) => _state = model;

        public TState Capture() => _state;

        public static implicit operator TState(Model<TState> model) => model._state;
        public static implicit operator Model<TState>(TState model) => new Model<TState>(model);
    }
}