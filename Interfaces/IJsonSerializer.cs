namespace CourseEPAM_Zakhar.Json
{
    public interface IJsonSerializer
    {
        object JsonDeserializer(string json);
    }
}
