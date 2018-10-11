namespace Infrastructure.DataMapper
{
    using System.Collections.ObjectModel;

    using AutoMapper;

    public class GenericUIDataMapper<TDataContract, TModel>
    {
        public GenericUIDataMapper()
        {
            InitialiseMappingRules();
        }

        public virtual void InitialiseMappingRules()
        {
            Mapper.CreateMap<TDataContract, TModel>();
            Mapper.CreateMap<TModel, TDataContract>();
        }

        public virtual ObservableCollection<TDataContract> MapModelToDataContract(ObservableCollection<TModel> entities)
        {
            return Mapper.Map<ObservableCollection<TModel>, ObservableCollection<TDataContract>>(entities);
        }

        public virtual ObservableCollection<TModel> MapDataContactToModel(ObservableCollection<TDataContract> entities)
        {
            return Mapper.Map<ObservableCollection<TDataContract>, ObservableCollection<TModel>>(entities);
        }

        public virtual TDataContract MapModelToDataContract(TModel entity)
        {
            return Mapper.Map<TModel, TDataContract>(entity);
        }

        public virtual TModel MapDataContactToModel(TDataContract entity)
        {
            return Mapper.Map<TDataContract, TModel>(entity);
        }
    }

    
}
