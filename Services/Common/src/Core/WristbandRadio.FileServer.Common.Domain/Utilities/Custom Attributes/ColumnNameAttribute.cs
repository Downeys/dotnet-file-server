namespace WristbandRadio.FileServer.Common.Domain.Utilities.Custom_Attributes;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
public class ColumnNameAttribute : Attribute
{
    public string NameValue { get; }

    public ColumnNameAttribute(string nameValue)
    {
        NameValue = nameValue;
    }
}
