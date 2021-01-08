using System;

namespace Stationery_Management
{
    class TransactionLogEntryAdd : TransactionLogEntry
    {
        public float Price { get; }
        public TransactionLogEntryAdd(DateTime date, string code, string name, string type, float price) : base(date, code, name, type)
        {
            this.Price = price;
        }
    }
}
