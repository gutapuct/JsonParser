using System;
using System.Collections.Generic;
using System.Linq;

namespace EPAM.CSCourse2016.AlyoshkinZakhar.JsonParserUI
{
    public class JsonDeserializerZakhar: EPAM.CSCourse2016.ParserPerfTester.Common.IParser
    {
        public string ToTestString(string json)
        {
            var outputText = String.Empty;

            var jsonSerializer = new JsonSerializer();
            var objectsProperties = jsonSerializer.JsonDeserializer(json);

            if (objectsProperties is bool || objectsProperties is Decimal || objectsProperties is string) //если простой формат
            {
                return objectsProperties.ToString();
            }
            else if (objectsProperties == null) //если null
            {
                return null;
            }
            else
            {
                var objects = objectsProperties as List<IObjectProperties>;
                if (objects == null) return Consts.Error; //если ошибка в преобразовании

                if (!objects.Any(i => i.GetProperties().Any(j => j.Value != null))) //вывод, если это просто массив элементов
                {
                    foreach (var obj in objects)
                    {
                        var objs = obj.GetProperties();
                        foreach (var o in objs)
                        {
                            outputText += String.Format("{0},", o.Name);
                        }
                    }
                    outputText = Consts.BracketOpenSquare + outputText.Substring(0, outputText.Length - 1) + Consts.BracketCloseSquare;
                }
                else //вывод, если это НЕ просто массив элементов
                {
                    foreach (var ListObjects in objects)
                    {
                        var obj = ListObjects.GetProperties();
                        if (obj.Where(i => i.Value != null && !String.IsNullOrWhiteSpace(i.Value.ToString())).Any()) //если это проперти
                        {
                            outputText += Consts.BracketOpenBrace;
                            foreach (var o in obj) //проходим по всем свойствам
                            {
                                outputText += String.Format("\"{0}\":{1},", o.Name, o.Value);
                            }
                            outputText = outputText.Substring(0, outputText.Length - 1) + Consts.BracketCloseBrace + ",";
                        }
                        else //если это просто элемент
                        {
                            foreach (var o in obj)
                            {
                                outputText += String.Format("{0},", o.Name);
                            }
                        }
                    }
                    if (objects.Count > 1) //если элементов более одного, то добавляем скобки, указывая, что это массив
                    {
                        outputText = Consts.BracketOpenSquare + outputText.Substring(0, outputText.Length - 1) + Consts.BracketCloseSquare;
                    }
                    else
                    {
                        outputText = outputText.Substring(0, outputText.Length - 1);
                    }
                }
            }
            return outputText;
        }

    }
}
