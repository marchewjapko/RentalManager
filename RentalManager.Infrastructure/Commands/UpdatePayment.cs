﻿namespace RentalManager.Infrastructure.Commands
{
    public class UpdatePayment
    {
        public string Method { get; set; }
        public int Amount { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
    }
}
