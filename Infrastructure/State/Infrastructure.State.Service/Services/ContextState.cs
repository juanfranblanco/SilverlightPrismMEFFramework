using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.State.Service.Services
{
    public class ContextState
    {
        public string Key { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime LastTimeSaved { get; set; }
        public List<ContextStateItem> Items { get; set; }

        public ContextState()
        {
            Items = new List<ContextStateItem>();
        }

        public void AddContextStateItem(object state)
        {
            var contextStateItem = new ContextStateItem();
            contextStateItem.SetState(state);
            this.Items.Add(contextStateItem);
        }

        public void SerialiseStateItemsIntoXml()
        {
            Items.ForEach((item) => item.SerialiseStateIntoXml());
        }

        public bool ContainsStateItem(object objectState)
        {
            return Items.Where((item) => item.IsForObject(objectState)).Count() > 0;
        }

        public bool ContainsStateItemType(Type type)
        {
            return Items.Where((item) => item.IsOfType(type)).Count() > 0;
        }

        public T RestoreStateItem<T>() where T: class
        {
            if(ContainsStateItemType(typeof(T)))
            {
                var contextStateItem = Items.Where((item) => item.IsOfType(typeof (T))).First();
                var state = contextStateItem.DeserialiseState(typeof(T));
                return (state == null) ? default(T): (T) state;
            }
            return null;
        }
    }
}