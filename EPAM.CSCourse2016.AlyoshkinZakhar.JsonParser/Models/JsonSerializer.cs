using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EPAM.CSCourse2016.AlyoshkinZakhar.JsonParserUI
{
    public class JsonSerializer : IJsonSerializer
    {
        private List<IObjectProperties> _objectsProperties = new List<IObjectProperties>();

        public object JsonSerializerZakhar(string json)
        {
            bool b;
            decimal d;

            if (String.IsNullOrWhiteSpace(json)) return Consts.Error; //если пустая строка
            else if (bool.TryParse(json, out b)) return b; //если true или false
            else if (decimal.TryParse(json, out d)) return d; //если число
            else if (json.ToLower().Trim() == "null") return null; //если null
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
                var isValue = false;
                var isArray = false;
                var isObject = false;
                for (var i = 0; i < json.Length; i++)
                {
                    if (json[i] == Consts.BracketOpenBrace && !isValue)
                    {
                        objectProperties = new ObjectProperties(); //создаем объект, если встречаем '{'
                    }
                    else if (json[i] == Consts.CharQuotes && !isValue) //начинаем запоминать имя свойства
                    {
                        try
                        {
                            i++;
                            do
                            {
                                name.Append(json[i]);
                                i++;
                            } while (json[i] != '"'); //ищем конец ключа
                            while (json[i] != ':') //ищем начало значения
                            {
                                i++;
                            }
                            isValue = true;
                        }
                        catch (IndexOutOfRangeException)
                        {
                            return Consts.Error;
                        }
                    }
                    else if (json[i] == ',' && !isArray && !isObject) //добавляем свойство и начинаем обрабатывать следующее свойство
                    {
                        objectProperties.AddProperty(name.ToString().Trim(), (object)value.ToString().Trim());
                        isValue = false;
                        name = new StringBuilder();
                        value = new StringBuilder();
                    }
                    else if (json[i] == Consts.BracketCloseBrace && !isObject) //добавляем объект, если встречаем '}' не в значении свойства
                    {
                        objectProperties.AddProperty(name.ToString().Trim(), (object)value.ToString().Trim());
                        _objectsProperties.Add(objectProperties);
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
                if (isObject || isArray) return Consts.Error;

                return _objectsProperties; //возвращаем все объекты
            }
        }
    }
}
