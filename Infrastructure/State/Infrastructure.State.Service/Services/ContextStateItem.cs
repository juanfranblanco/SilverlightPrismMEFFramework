using System;
using System.IO;
using System.Xml.Serialization;

namespace Infrastructure.State.Service.Services
{
    public class ContextStateItem
    {
        public string TypeName { get; set; }
        private object state;

        public void  SetState(object objectState)
        {
            state = objectState;
            SetTypeName();
           
        }

        private void SetTypeName()
        {
            TypeName = state.GetType().ToString();
        }

        public string StatetXml { get; set; }
        
        public void SerialiseStateIntoXml()
        {
            if (state == null) return;
            var serializer = new XmlSerializer(state.GetType());
            var stringWriter = new StringWriter();
            serializer.Serialize(stringWriter, state);
            StatetXml = stringWriter.ToString();
        }

        public bool IsOfType(Type type)
        {
            return type.ToString() == TypeName;
        }

        public bool IsForObject(object objectState)
        {
            return IsOfType(objectState.GetType());
        }

        public object DeserialiseState(Type type)
        {
            var serializer = new XmlSerializer(type);
            var stringReader = new StringReader(StatetXml);
            state = serializer.Deserialize(stringReader);
            return state;
        }
    }
}