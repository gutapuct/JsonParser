using System.Collections.Generic;
using System.Linq;

namespace CourseEPAM_Zakhar.Json
{
    public class ObjectProperties : IObjectProperties
    {
        private List<Property> _properties = new List<Property>();

        public void AddProperty(string name, object value = null)
        {
            var p = _properties.Where(i => i.Name == name).FirstOrDefault();
            if (p != null)
            {
                p.Value = value;
            }
            else
            {
                _properties.Add(new Property { Name = name, Value = value });
            }
        }

        public Property GetPropertyByName(string name)
        {
            return _properties.Where(i => i.Name == name).FirstOrDefault();
        }

        public List<Property> GetProperties()
        {
            return _properties;
        }
    }
}
