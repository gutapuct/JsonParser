using System.Collections;
using System.Text;

namespace EPAM.CSCourse2016.AlyoshkinZakhar.JsonParserUI
{
    class ObjectDeserializer : IObjectDeserializerZakhar
    {
        public StringBuilder result;
        public string DeserializeZakhar(IObjectProperties objectProperties)
        {
            result = new StringBuilder("{\r\n");
            foreach (var property in objectProperties.GetProperties())
            {
                var arrayObject = property.Value as IEnumerable;
                if (arrayObject != null && !(property.Value is string))
                {
                    result.AppendFormat("\t{0} :\r\n\t[\r\n", property.Name);
                    foreach (var obj in arrayObject)
                    {
                        ParseAndSaveValueObject(obj);
                    }
                    result.Remove(result.Length - 3, 3).Append("\r\n\t],\r\n");
                }
                else
                {
                    result.AppendFormat("\t{0} : {1},\r\n", property.Name, property.Value ?? "null");
                }
            }

            return result.Remove(result.Length - 3, 3).Append("\r\n}").ToString();
        }

        private void ParseAndSaveValueObject(object value)
        {
            if (value is IObjectProperties)
            {
                var objectProperties = (IObjectProperties)value;
                foreach (var p in objectProperties.GetProperties())
                {
                    if (p is IObjectProperties)
                    {
                        ParseAndSaveValueObject(p);
                    }
                    else
                    {
                        result.AppendFormat("\t\t{0} : {1},\r\n", p.Name, p.Value ?? "null");
                    }
                }
            }
            else
            {
                result.AppendFormat("\t\t{0},\r\n", value.ToString());
            }
        }
    }
}
