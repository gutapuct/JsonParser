using System.Collections.Generic;

namespace EPAM.CSCourse2016.AlyoshkinZakhar.JsonParser
{
    public class ObjectProperties : IObjectProperties
    {
        private List<Property> _properties = new List<Property>();

        public void AddProperty(string name, object value = null)
        {
            _properties.Add(new Property { Name = name, Value = value });
        }

        public List<Property> GetProperties()
        {
            return _properties;
        }
    }
}
