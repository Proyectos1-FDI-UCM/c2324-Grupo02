namespace MVPFramework.Model
{
    public readonly struct Model<TModel> : IModel<TModel>
    {
        private readonly TModel _model;

        public Model(TModel model) => _model = model;

        public TModel Capture() => _model;

        public static implicit operator TModel(Model<TModel> model) => model._model;
        public static implicit operator Model<TModel>(TModel model) => new Model<TModel>(model);
    }
}