namespace Application.Interfaces.Utils
{
    public interface IParameterExtractorService
    {
        object? TryExtractValue(IDictionary<string, object> parameters, string key);
        Action? TryParseAction(object value);
        public TEnum GetEnumParameter<TEnum>(IDictionary<string, object> parameters, string key) where TEnum : struct, Enum;
    }
}