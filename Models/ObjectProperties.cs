using System.Collections.Generic;
using System.Linq;

namespace CourseEPAM_Zakhar.Json
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
