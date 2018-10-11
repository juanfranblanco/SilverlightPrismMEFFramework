using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;

using UI.Infrastructure.State.Contracts.Services;

using System.Linq;

namespace Infrastructure.State.Service.Services
{
    [Export(typeof(IStateService))]
    public class StateService : IStateService
    {
        private readonly IStorageStateService storageStateService;

        [ImportingConstructor]
        public StateService(IStorageStateService storageStateService)
        {
            if (storageStateService == null) throw new ArgumentNullException("storageStateService");
            this.storageStateService = storageStateService;
            InitisaliseFromStorage();
        }

        public List<ContextState> ContextStates { get; set; }

        private void InitisaliseFromStorage()
        {
            ContextStates = storageStateService.InitisaliseFromStorage();
        }

        public void RegisterSaveableObject(object saveableObject, string contextStateKey)
        {
            FindContextState(contextStateKey).AddContextStateItem(saveableObject);
        }

        public void SaveAll()
        {
           this.SaveAll(ContextStates);
        }

        public void SaveAll(List<ContextState> contextStates)
        {
            contextStates.ForEach((contextState) => contextState.SerialiseStateItemsIntoXml());
            storageStateService.SaveToStorage(contextStates);
        }

        public void Save(string contextStateKey)
        {
            var contextState = FindContextState(contextStateKey);
            contextState.SerialiseStateItemsIntoXml();
            //Refactor to storage
            var savedContextStates = this.storageStateService.InitisaliseFromStorage();
            savedContextStates.RemoveContextState(contextStateKey);
            savedContextStates.Add(contextState);
            storageStateService.SaveToStorage(savedContextStates);
        }       

        public T RestoreValues<T>(string contextStateKey) where T : class
        {
            if (ExistsContextState(contextStateKey))
            {
                var contextState = FindContextState(contextStateKey);
                return contextState.RestoreStateItem<T>();
            }
            return null;
        }

        public ContextState FindContextState(string contextStateKey)
        {
            return ContextStates.FindContextState(contextStateKey);
        }


        public bool ExistsContextState(string contextStateKey)
        {
            return ContextStates.ExistsContextState(contextStateKey);
        }

        public void RegisterContextState(string contextStateKey, string contextStateName, string contextStateDescription)
        {
            if (ExistsContextState(contextStateKey)) return;
            var contextState = new ContextState
                                   {
                                       Key = contextStateKey,
                                       Name = contextStateName,
                                       Description = contextStateDescription
                                   };
            this.ContextStates.Add(contextState);
        }

        public void RemoveContextState(string contextStateKey)
        {
            ContextStates.RemoveContextState(contextStateKey);
            DeleteFromStorage(contextStateKey);
        }

        //Refactor to storage
        private void DeleteFromStorage(string contextStateKey)
        {
            var savedContextStates = this.storageStateService.InitisaliseFromStorage();
            savedContextStates.RemoveContextState(contextStateKey);
            this.storageStateService.SaveToStorage(savedContextStates);
        }
    }


    public static class ContextStateExtenstions
    {

        public static ContextState FindContextState(this List<ContextState> contextStates, string contextStateKey)
        {
            return contextStates.FirstOrDefault((context) => context.Key == contextStateKey);
        }

        public static bool ExistsContextState(this List<ContextState> contextStates, string contextStateKey)
        {
            return contextStates.FindContextState(contextStateKey) != null;
        }

        public static void RemoveContextState(this List<ContextState> contextStates, string contextStateKey)
        {
            if (!contextStates.ExistsContextState(contextStateKey)) return;
            var contextState = contextStates.FindContextState(contextStateKey);
            contextStates.Remove(contextState);
        }
    }
}