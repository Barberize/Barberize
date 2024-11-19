namespace Barberize.Settings.Core.Models;

public class Parameter : Aggregate<Guid>
{
    public string Key { get; private set; } = default!;
    public ParameterDataType DataType { get; private set; } = ParameterDataType.String;
    public string? DefaultValue { get; private set; }
    public string? Value { get; private set; }

    public static Parameter Create(string key, ParameterDataType dataType, string? defaultValue, string? value)
    {
        ArgumentException.ThrowIfNullOrEmpty("key");
        VerifyValueType(value, dataType);
        VerifyValueType(defaultValue, dataType);

        var parameter = new Parameter
        {
            Key = key,
            DataType = dataType,
            DefaultValue = defaultValue,
            Value = value
        };

        return parameter;
    }

    public void SetParameterValue(string? value)
    {
        VerifyValueType(value, DataType);
        Value = value;
    }

    public void SetParameterDefaultValue(string? defaultValue)
    {
        VerifyValueType(defaultValue, DataType);
        DefaultValue = defaultValue;
    }


    private static void VerifyValueType(string? value, ParameterDataType dataType)
    {
        if (dataType == ParameterDataType.Int)
        {
            if (value != null && !int.TryParse(value, out _)) throw new Exception("Wrong parameter type, not an Int");
        }

        if (dataType == ParameterDataType.Double)
        {
            if (value != null && !double.TryParse(value, out _)) throw new Exception("Wrong parameter type, not a Double");
        }

        if (dataType == ParameterDataType.DateOnly)
        {
            if (value != null && !DateOnly.TryParse(value, out _)) throw new Exception("Wrong parameter type, not a Date");
        }
    }

    // Helpers

    public string? GetStringValue()
    {
        if (DataType != ParameterDataType.String) throw new Exception("Wrong parameter type, not a String");
        return Value ?? DefaultValue;
    }

    public int? GetIntValue() 
    {
        if (DataType != ParameterDataType.Int) new Exception("Wrong parameter type, not an Int");

        var val = Value ?? DefaultValue;
        if (val == null) return null;

        return int.Parse(val);
    }

    public double? GetDoubleValue()
    {
        if (DataType != ParameterDataType.Double) new Exception("Wrong parameter type, not a Double");

        var val = Value ?? DefaultValue;
        if (val == null) return null;

        return double.Parse(val, CultureInfo.InvariantCulture);
    }

    public DateOnly? GetDateValue()
    {
        if (DataType != ParameterDataType.DateOnly) new Exception("Wrong parameter type, not a Date");

        var val = Value ?? DefaultValue;
        if (val == null) return null;

        return DateOnly.Parse(val);
    }
}
