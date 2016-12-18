using CourseEPAM_Zakhar.Json.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CourseEPAM_Zakhar.Json
{
    public class JsonSerializer : IJsonSerializer
    {
        private List<IObjectProperties> _objectsProperties = new List<IObjectProperties>();

        private Dictionary<char, char> Dictionary = new Dictionary<char, char>()
        {
             {Consts.BracketOpenSquare, Consts.BracketCloseSquare}, // [ ]
             {Consts.BracketOpenBrace, Consts.BracketCloseBrace}, // { }
        };

        public object JsonDeserializer(string json)
        {
            json = json.Trim();
            bool b;
            decimal d;

            if (String.IsNullOrWhiteSpace(json)) return Consts.Error; //если пустая строка
            else if (bool.TryParse(json, out b)) return b; //если true или false
            else if (decimal.TryParse(json, out d)) return d; //если число
            else if (json.ToLower() == "null") return "null"; //если null
            else if (json[0] == Consts.CharQuotes && json[json.Length - 1] == Consts.CharQuotes) return json; //если строка

            else
            {
                if (!json.Contains(Consts.BracketOpenBrace) && !json.Contains(Consts.BracketCloseBrace)) //если просто массив
                {
                    var arr = json.Substring(1, json.Length - 2).Split(',');
                    foreach (var a in arr)
                    {
                        var objectProperty = new ObjectProperties();
                        objectProperty.AddProperty(a.Trim());
                        _objectsProperties.Add(objectProperty);
                    }
                    return _objectsProperties;
                }
                //если объект(ы)
                var name = new StringBuilder();
                var value = new StringBuilder();
                var objectProperties = new ObjectProperties();
                var isName = false;
                var isValue = false;
                var isArray = false;
                var isObject = false;

                for (var i = 0; i < json.Length; i++)
                {
                    if (json[i] == Consts.BracketOpenBrace && !isValue) objectProperties = new ObjectProperties(); //создаем объект, если встречаем {
                    else if(json[i] == Consts.CharQuotes && !isName && !isValue) isName = true; //начинаем запоминать имя свойства
                    else if (json[i] == Consts.CharQuotes && isName && name.Length > 0) //начинаем запоминать значение свойства
                    {
                        i++;
                        isName = false;
                        isValue = true;
                    }
                    else if (isName) name.Append(json[i]); //выяснение имени свойства
                    else if (json[i] == ',' && name.Length > 0 && !isArray && !isObject) //добавляем свойство и начинаем обрабатывать следующее свойство
                    {
                        objectProperties.AddProperty(name.ToString().Trim(), (object)value.ToString().Trim());
                        isName = false;
                        isValue = false;
                        name = new StringBuilder();
                        value = new StringBuilder();
                    }
                    else if (json[i] == Consts.BracketCloseBrace && name.Length > 0 && !isObject) //добавляем объект, если встречаем } не в значении свойства
                    {
                        objectProperties.AddProperty(name.ToString().Trim(), (object)value.ToString().Trim());
                        _objectsProperties.Add(objectProperties);
                        isName = false;
                        isValue = false;
                        name = new StringBuilder();
                        value = new StringBuilder();
                        objectProperties = new ObjectProperties();
                    }
                    else if (isValue) //выяснение значения свойства
                    {
                        if (json[i] == Consts.BracketOpenSquare) isArray = true;
                        if (json[i] == Consts.BracketOpenBrace) isObject = true;
                        value.Append(json[i]);
                        if (json[i] == Consts.BracketCloseSquare) isArray = false;
                        if (json[i] == Consts.BracketCloseBrace) isObject = false;
                    }
                }
                if (name.Length != 0)
                {
                    objectProperties.AddProperty(name.ToString().Trim(), (object)value.ToString().Trim());
                    _objectsProperties.Add(objectProperties);
                }
                return _objectsProperties; //возвращаем все объекты
            }
        }
    }
}
