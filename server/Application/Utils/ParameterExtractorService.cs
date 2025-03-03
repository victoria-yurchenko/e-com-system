using Application.Interfaces.Utils;

namespace Application.Utils
{
    public class ParameterExtractorService : IParameterExtractorService
    {
        public object? TryExtractValue(IDictionary<string, object> parameters, string key)
            => parameters.TryGetValue(key, out var value) ? value : null;

        public TEnum GetEnumParameter<TEnum>(IDictionary<string, object> parameters, string key) where TEnum : struct, Enum
            => parameters.TryGetValue(key, out var value) && value is TEnum result
                ? result
                : throw new ArgumentException($"Missing or invalid '{key}' parameter.");


        public Action? TryParseAction(object? value)
            => value is Action actionEnum ? actionEnum
                : value is string actionString && Enum.TryParse(typeof(Action), actionString, out var result) ? (Action)result
                : null;
    }

}