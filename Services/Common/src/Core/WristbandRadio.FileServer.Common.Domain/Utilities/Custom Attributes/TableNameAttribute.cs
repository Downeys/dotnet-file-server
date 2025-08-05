namespace WristbandRadio.FileServer.Common.Domain.Utilities.Custom_Attributes;

[AttributeUsage(AttributeTargets.Class)]
public  class TableNameAttribute : Attribute
{
    public string NameValue { get; }

    public TableNameAttribute(string nameValue)
    {
        NameValue = nameValue;
    }
}
