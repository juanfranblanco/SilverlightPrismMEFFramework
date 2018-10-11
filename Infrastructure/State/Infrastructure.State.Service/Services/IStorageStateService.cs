using System.Collections.Generic;

namespace Infrastructure.State.Service.Services
{
    public interface IStorageStateService
    {
        List<ContextState> InitisaliseFromStorage();
        void SaveToStorage(List<ContextState> contextStates);
    }
}