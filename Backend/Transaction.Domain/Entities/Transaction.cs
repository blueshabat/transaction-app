﻿namespace Transaction.Domain.Entities
{
    public class Transaction
    {
        public int Id { get; set; }

        public string Type { get; set; } = string.Empty;

        public decimal Amount { get; set; }

        public DateTime Date { get; set; }

        public string Description { get; set; } = string.Empty;

        public string Category { get; set; } = string.Empty;

        public decimal Balance { get; set; }
    }
}
