using System.ComponentModel;

// ReSharper disable UnusedMember.Global

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