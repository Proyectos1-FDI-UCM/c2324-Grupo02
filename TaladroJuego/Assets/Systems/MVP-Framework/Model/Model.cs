namespace MVPFramework.Model
{
    public readonly struct Model<TData> : IModel<TData>
    {
        private readonly TData _state;

        public Model(TData model) => _state = model;

        public TData Capture() => _state;

        public static implicit operator TData(Model<TData> model) => model._state;
        public static implicit operator Model<TData>(TData model) => new Model<TData>(model);
    }
}