using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EPAM.CSCourse2016.AlyoshkinZakhar.JsonParserUI
{
    public class JsonSerializer : IJsonSerializer
    {
        private System.Globalization.CultureInfo _cultureInfo = new System.Globalization.CultureInfo("en-US");
        public object JsonSerializerZakhar(string json)
        {
            bool b;
            decimal d;

            if (String.IsNullOrWhiteSpace(json)) return String.Empty;
            else if (bool.TryParse(json, out b)) return b.ToString().ToLower(); //если true или false
            else if (Decimal.TryParse(json, System.Globalization.NumberStyles.Number, _cultureInfo, out d)) return json; //если число
            else if (json.ToLower().Trim() == "null") return "null"; //если null
            else if (json[0] == Consts.CharQuotes && json[json.Length - 1] == Consts.CharQuotes) return json; //если строка
            else if (json[0] == Consts.BracketOpenSquare && json[json.Length - 1] == Consts.BracketCloseSquare && !json.Contains(Consts.BracketOpenBrace))
            { //если просто массив
                return json.Substring(1, json.Length - 2).Split(',');
            }
            else
            { //если объект(ы)
                var objectsProperties = new List<IObjectProperties>();
                var name = new StringBuilder();
                var value = new StringBuilder();
                var objectProperties = new ObjectProperties();
                var isValue = false;
                short arrayInCount = 0; //счетчик вложенностей массива
                short objectInCount = 0; //счетчик вложенностей объектов
                var isStringValue = false;

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
                            } while (json[++i] != Consts.CharQuotes); //ищем кавычки, означающие конец ключа

                            while (json[i] != ':') //ищем начало значения
                            {
                                i++;
                            }

                            var whiteSpaces = new char[] { ' ', '\r', '\n', '\t' };
                            while (whiteSpaces.Contains(json[i])) //удаляем перед значением пустые символы
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
                    else if (json[i] == ',' && arrayInCount == 0 && objectInCount == 0 && isValue) //добавляем свойство и начинаем обрабатывать следующее свойство
                    {
                        objectProperties.AddProperty(name.ToString().Trim(), JsonSerializerZakhar(value.ToString().Trim()));
                        isValue = false;
                        name = new StringBuilder();
                        value = new StringBuilder();
                    }
                    else if (json[i] == Consts.BracketCloseBrace && objectInCount == 0) //добавляем объект, если встречаем '}' не в значении свойства
                    {
                        objectProperties.AddProperty(name.ToString().Trim(), JsonSerializerZakhar(value.ToString().Trim()));
                        objectsProperties.Add(objectProperties);
                        isValue = false;
                        name = new StringBuilder();
                        value = new StringBuilder();
                        objectProperties = new ObjectProperties();
                    }
                    else if (isValue) //выяснение значения свойства
                    {
                        if (json[i] == Consts.CharQuotes) //если значение - строка
                        {
                            try
                            {
                                isStringValue = true;
                                while (isStringValue)
                                {
                                    value.Append(json[i++]);
                                    if (json[i] == Consts.CharQuotes) isStringValue = false;
                                }
                            }
                            catch (IndexOutOfRangeException)
                            {
                                return Consts.Error;
                            }
                        }

                        if (json[i] == Consts.BracketOpenSquare) arrayInCount++; //если в строке [, то увеличиваем счетчик вложенносте
                        if (json[i] == Consts.BracketOpenBrace) objectInCount++; //если в строке {, то увеличиваем счетчик вложенносте
                        value.Append(json[i]);
                        if (json[i] == Consts.BracketCloseSquare) arrayInCount--; //если в строке ], то уменьшаем счетчик вложенносте
                        if (json[i] == Consts.BracketCloseBrace) objectInCount--; //если в строке }, то уменьшаем счетчик вложенносте
                    }
                }
                if (name.Length != 0)
                {
                    objectProperties.AddProperty(name.ToString().Trim(), JsonSerializerZakhar(value.ToString().Trim()));
                    objectsProperties.Add(objectProperties);
                }
                if (objectInCount != 0 || arrayInCount != 0) return Consts.Error;

                return objectsProperties; //возвращаем все объекты
            }
        }
    }
}
