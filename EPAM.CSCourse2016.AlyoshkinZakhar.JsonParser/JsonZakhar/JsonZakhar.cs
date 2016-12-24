﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EPAM.CSCourse2016.AlyoshkinZakhar.JsonParserUI
{
    public class JsonZakhar: EPAM.CSCourse2016.ParserPerfTester.Common.IParser
    {
        public string ToTestString(string json)
        {
            json = json.Trim();
            var outputText = String.Empty;

            var jsonSerializer = new JsonSerializer();
            var objectsProperties = jsonSerializer.JsonSerializerZakhar(json);

            if (objectsProperties is bool || objectsProperties is Decimal || objectsProperties is string) //если простой формат
            {
                return objectsProperties.ToString();
            }
            else if (objectsProperties == null) //если null
            {
                return null;
            }
            else if (objectsProperties is string[]) // если просто массив
            {
                var result = new StringBuilder();
                foreach (var item in (objectsProperties as string[]))
                {
                    result.AppendFormat("{0},", item.Trim());
                }
                return String.Format("[{0}]", result.Remove(result.Length-1, 1));
            }
            else
            {
                var objectsPropertiesList = objectsProperties as List<IObjectProperties>;
                if (objectsPropertiesList == null) return Consts.Error; //если ошибка в преобразовании
                
                foreach (var objects in objectsPropertiesList)
                {
                    var propertiesList = objects.GetProperties();
                    if (propertiesList.Where(i => i.Value != null && !String.IsNullOrWhiteSpace(i.Value.ToString())).Any()) //если это проперти
                    {
                        outputText += Consts.BracketOpenBrace;
                        foreach (var property in propertiesList) //проходим по всем свойствам
                        {
                            outputText += String.Format("\"{0}\":{1},", property.Name, property.Value);
                        }
                        outputText = outputText.Substring(0, outputText.Length - 1) + Consts.BracketCloseBrace + ",";
                    }
                    else //если это просто элемент
                    {
                        foreach (var item in propertiesList)
                        {
                            outputText += String.Format("{0},", item.Name);
                        }
                    }
                }
                if (json[0] == Consts.BracketOpenSquare) //если это массив, то добавляем скобки
                {
                    outputText = Consts.BracketOpenSquare + outputText.Substring(0, outputText.Length - 1) + Consts.BracketCloseSquare;
                }
                else
                {
                    outputText = outputText.Substring(0, outputText.Length - 1);
                }
                
            }
            return outputText;
        }

    }
}
