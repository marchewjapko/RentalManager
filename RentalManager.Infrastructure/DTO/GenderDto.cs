using System.ComponentModel;

// ReSharper disable UnusedMember.Global

namespace RentalManager.Infrastructure.DTO;

public enum GenderDto
{
    [Description("Man")]
    Man,

    [Description("Woman")]
    Woman,

    [Description("OtherPreferNotSay")]
    OtherPreferNotSay
}