namespace WristbandRadio.FileServer.Common.Domain.Utilities.Custom_Attributes;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
public class NavigationAttribute : Attribute
{
    public Type? AssociatedType { get; }
    public string AssociatedProperty { get; }

    public NavigationAttribute(Type? associatedType, string associatedProperty)
    {
        AssociatedType = associatedType;
        AssociatedProperty = associatedProperty;
    }
}
