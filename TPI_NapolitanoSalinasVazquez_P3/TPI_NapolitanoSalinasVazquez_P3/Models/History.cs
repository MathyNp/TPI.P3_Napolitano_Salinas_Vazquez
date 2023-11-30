﻿namespace TPI_NapolitanoSalinasVazquez_P3.Models
{
    public class History
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime Date { get; set; }

        public string ProductNames { get; set; }
        public string ProductIds { get; set; }

        public decimal Amount { get; set; }

    }
}
