using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO.IsolatedStorage;

namespace Infrastructure.State.Service.Services
{
    [Export(typeof(IStorageStateService))]
    public class IsolatedStorageStateService : IStorageStateService
    {
        public const string ISOLATED_STORAGE_KEY = "ContextState";

        public List<ContextState> InitisaliseFromStorage()
        {
            if (IsolatedStorageSettings.ApplicationSettings.Contains(ISOLATED_STORAGE_KEY))
            {
                return (List<ContextState>)IsolatedStorageSettings.ApplicationSettings[ISOLATED_STORAGE_KEY];
            }
            return new List<ContextState>();
        }

        public void SaveToStorage(List<ContextState> contextStates)
        {
            if (IsolatedStorageSettings.ApplicationSettings.Contains(ISOLATED_STORAGE_KEY))
            {
                IsolatedStorageSettings.ApplicationSettings[ISOLATED_STORAGE_KEY] = contextStates;
            }
            else
            {
                IsolatedStorageSettings.ApplicationSettings.Add(ISOLATED_STORAGE_KEY, contextStates);
            }

            IsolatedStorageSettings.ApplicationSettings.Save();
        }

    }
}