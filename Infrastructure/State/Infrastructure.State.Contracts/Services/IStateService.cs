namespace UI.Infrastructure.State.Contracts.Services
{
    public interface IStateService
    {
        void RegisterSaveableObject(object saveableObject, string cotextStateKey);
        void SaveAll();
        T RestoreValues<T>(string cotextStateKey) where T : class;
        void RegisterContextState(string cotextStateKey, string contextName, string contextDescription);
        bool ExistsContextState(string cotextStateKey);
        void RemoveContextState(string contextStateKey);
        void Save(string contextStateKey);
    }
}
