using System.Collections.Generic;

namespace CourseEPAM_Zakhar.Json
{
    public interface IObjectProperties
    {
        void AddProperty(string name, object value = null);
        Property GetPropertyByName(string name);
        List<Property> GetProperties();
    }
}