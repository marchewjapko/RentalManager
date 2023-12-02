using System.ComponentModel;

namespace RentalManager.Core.Domain;

public enum Gender
{
    [Description("Man")]
    Man,
    [Description("Woman")]
    Woman,
    [Description("OtherPreferNotSay")]
    OtherPreferNotSay
}