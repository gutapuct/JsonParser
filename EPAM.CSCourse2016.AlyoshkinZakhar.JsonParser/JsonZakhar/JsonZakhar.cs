using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EPAM.CSCourse2016.AlyoshkinZakhar.JsonParserUI
{
    public class JsonZakhar: EPAM.CSCourse2016.ParserPerfTester.Common.IParser
    {
        public string ToTestString(string json)
        {
            var outputText = new StringBuilder();
            var objectsProperties = new JsonSerializer().JsonSerializerZakhar(json);

            if (objectsProperties is bool || objectsProperties is Decimal || objectsProperties is string) //если простой формат
            {
                return objectsProperties.ToString();
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
            { //если это объект(ы)
                var objectsPropertiesList = objectsProperties as List<IObjectProperties>;

                OutputObjects(objectsPropertiesList, ref outputText); //выводим в строку объект(ы) - ключ-значение

                if (json[0] == Consts.BracketOpenSquare) //если это массив, то добавляем скобки и удаляем последню запятую
                {
                    outputText = new StringBuilder(Consts.BracketOpenSquare + outputText.Remove(outputText.Length - 1, 1).ToString() + Consts.BracketCloseSquare);
                }
                else
                {
                    outputText = outputText.Remove(outputText.Length - 1, 1); //удаляем лишнюю запятую
                }
                
            }
            return outputText.ToString(); //возвращаем string 
        }

        private void OutputObjects(List<IObjectProperties> objectsPropertiesList, ref StringBuilder outputText)
        {
            foreach (var objects in objectsPropertiesList) //пробегаемся по всем объектам
            {
                var propertiesList = objects.GetProperties();
                if (propertiesList.Where(i => i.Value != null && !String.IsNullOrWhiteSpace(i.Value.ToString())).Any()) //если это проперти
                {
                    outputText.Append(Consts.BracketOpenBrace); //добавляем {
                    foreach (var property in propertiesList) //проходим по всем свойствам
                    {
                        outputText.AppendFormat("\"{0}\":", property.Name);
                        if (property.Value is List<IObjectProperties>) //если в value объект(ы)
                        {
                            if ((property.Value as List<IObjectProperties>).Count > 1)//если массив объектов, то добавляем []
                            {
                                outputText.Append(Consts.BracketOpenSquare);
                                OutputObjects(property.Value as List<IObjectProperties>, ref outputText);
                                outputText = new StringBuilder(outputText.Remove(outputText.Length - 1, 1).ToString() + Consts.BracketCloseSquare + ",");
                            }
                            else
                            {
                                OutputObjects(property.Value as List<IObjectProperties>, ref outputText);
                            }
                        }
                        else if (property.Value is string[]) //если в value массив, то добавляем скобки и проходим по ним
                        {
                            outputText.Append(Consts.BracketOpenSquare);
                            foreach (var item in property.Value as string[])
                            {
                                outputText.AppendFormat("{0},", item.Trim());
                            }
                            outputText = new StringBuilder(outputText.Remove(outputText.Length - 1, 1).ToString() + "],");
                        }
                        else //если в value простое значение
                        {
                            outputText.AppendFormat("{0},", property.Value);
                        }
                    }
                    outputText = new StringBuilder(outputText.Remove(outputText.Length - 1, 1).ToString() + Consts.BracketCloseBrace + ","); //удаляем последнюю запятую и добавляем }
                }
                else //если это просто элемент
                {
                    foreach (var item in propertiesList)
                    {
                        outputText.AppendFormat("{0},", item.Name);
                    }
                }
            }
        }
    }
}
