﻿using System.Collections.Generic;

namespace EPAM.CSCourse2016.AlyoshkinZakhar.JsonParserUI
{
    public interface IObjectProperties
    {
        void AddProperty(string name, object value = null);
        List<Property> GetProperties();
    }
}