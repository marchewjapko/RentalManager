﻿namespace RentalManager.Core.Domain;

public class Payment : DomainBase
{
    public int AgreementId { get; set; }

    public Agreement Agreement { get; set; }

    public string? Method { get; set; }

    public int Amount { get; set; }

    public DateTime DateFrom { get; set; }

    public DateTime DateTo { get; set; }
}