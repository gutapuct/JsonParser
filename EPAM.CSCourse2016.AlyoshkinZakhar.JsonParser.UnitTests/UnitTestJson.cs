using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EPAM.CSCourse2016.AlyoshkinZakhar.JsonParserUI.UnitTests
{
    [TestClass]
    public class UnitTestJson
    {
        [TestMethod]
        public void TestMethod1()
        {
            string ExternalValue = "true";
            var test = new JsonZakhar();

            var internalValue = test.ToTestString(ExternalValue);
            var expectation = "true";

            Assert.IsTrue(internalValue == expectation);
        }

        [TestMethod]
        public void TestMethod2()
        {
            string ExternalValue = "";
            var test = new JsonZakhar();

            var internalValue = test.ToTestString(ExternalValue);
            var expectation = "";

            Assert.IsTrue(internalValue == expectation);
        }

        [TestMethod]
        public void TestMethod3()
        {
            string ExternalValue = "{\r\n\"name\":\"Zakhar\",\r\n\"age\":18,\r\n\"IsAdmin\": true\r\n}   \r\n\r\n   ";
            var test = new JsonZakhar();

            var internalValue = test.ToTestString(ExternalValue);
            var expectation = "{\"name\":\"Zakhar\",\"age\":18,\"IsAdmin\":true}";

            Assert.IsTrue(internalValue == expectation);
        }

        [TestMethod]
        public void TestMethod4()
        {
            string ExternalValue = "[1, 2, 3, \"message\", true, null, [\"one\", \"two\", \"three\"], 15]";
            var test = new JsonZakhar();

            var internalValue = test.ToTestString(ExternalValue);
            var expectation = "[1,2,3,\"message\",true,null,[\"one\",\"two\",\"three\"],15]";

            Assert.IsTrue(internalValue == expectation);
        }

        [TestMethod]
        public void TestMethod5()
        {
            string ExternalValue = "null";
            var test = new JsonZakhar();

            var internalValue = test.ToTestString(ExternalValue);
            string expectation = "null";

            Assert.IsTrue(internalValue == expectation);
        }

        [TestMethod]
        public void TestMethod6()
        {
            string ExternalValue = @"{""FirstName"":""Ivan"",""LastName"":""Ivanov""}";
            var test = new JsonZakhar();

            var internalValue = test.ToTestString(ExternalValue);
            var expectation = "{\"FirstName\":\"Ivan\",\"LastName\":\"Ivanov\"}";

            Assert.IsTrue(internalValue == expectation);
        }

        [TestMethod]
        public void TestMethod7()
        {
            string ExternalValue = @"[{""FirstName"":""Ivan"",""LastName"":""Ivanov""},{""FirstName"":""Petr"",""LastName"":""Petrov""}]";
            var test = new JsonZakhar();

            var internalValue = test.ToTestString(ExternalValue);
            var expectation = "[{\"FirstName\":\"Ivan\",\"LastName\":\"Ivanov\"},{\"FirstName\":\"Petr\",\"LastName\":\"Petrov\"}]";

            Assert.IsTrue(internalValue == expectation);
        }

        [TestMethod]
        public void TestMethod8()
        {
            string ExternalValue = @"[{""Author"":""Ivanov"",""Year"":2015,""Text"":""Text-1""},{""Author"":""Petrov"",""Year"":2016,""Text"":""Text-2""},{""Author"":""Sidorov"",""Year"":2007,""Text"":""Text-3""}]";
            var test = new JsonZakhar();

            var internalValue = test.ToTestString(ExternalValue);
            var expectation = "[{\"Author\":\"Ivanov\",\"Year\":2015,\"Text\":\"Text-1\"},{\"Author\":\"Petrov\",\"Year\":2016,\"Text\":\"Text-2\"},{\"Author\":\"Sidorov\",\"Year\":2007,\"Text\":\"Text-3\"}]";

            Assert.IsTrue(internalValue == expectation);
        }

        [TestMethod]
        public void TestMethod9()
        {
            string ExternalValue = @"[{""FullName"":{""LastName"":""Ivanov"",""FirstName"":""Ivan""},""Age"":27,""IsMarried"":true,""CountChildren"":2},{""FullName"":{""LastName"":""Patrov"",""FirstName"":""Petr""},""Age"":16,""IsMarried"":false,""CountChildren"":null}]";
            var test = new JsonZakhar();

            var internalValue = test.ToTestString(ExternalValue);
            var expectation = "[{\"FullName\":{\"LastName\":\"Ivanov\",\"FirstName\":\"Ivan\"},\"Age\":27,\"IsMarried\":true,\"CountChildren\":2},{\"FullName\":{\"LastName\":\"Patrov\",\"FirstName\":\"Petr\"},\"Age\":16,\"IsMarried\":false,\"CountChildren\":null}]";

            Assert.IsTrue(internalValue == expectation);
        }

        [TestMethod]
        public void TestMethod10()
        {
            string ExternalValue = "[1,2,3]";
            var test = new JsonZakhar();

            var internalValue = test.ToTestString(ExternalValue);
            var expectation = "[1,2,3]";

            Assert.IsTrue(internalValue == expectation);
        }

        [TestMethod]
        public void TestMethod11()
        {
            string ExternalValue = "[{\"name\":\"Zakhar\"}]";
            var test = new JsonZakhar();

            var internalValue = test.ToTestString(ExternalValue);
            var expectation = "[{\"name\":\"Zakhar\"}]";

            Assert.IsTrue(internalValue == expectation);
        }

        [TestMethod]
        public void TestMethod12()
        {
            string ExternalValue = "{\"name\":\"Zakhar\"}";
            var test = new JsonZakhar();

            var internalValue = test.ToTestString(ExternalValue);
            var expectation = "{\"name\":\"Zakhar\"}";

            Assert.IsTrue(internalValue == expectation);
        }
    }
}
