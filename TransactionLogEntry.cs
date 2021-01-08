using System;

namespace Stationery_Management
{
    abstract class TransactionLogEntry
    {
        public DateTime Date { get; }
        public string ItemCode { get; }
        public string ItemName { get; }
        public string Type { get; }
        public TransactionLogEntry(DateTime date, string code, string name, string type)
        {
            this.Date = date;
            this.ItemCode = code;
            this.ItemName = name;
            this.Type = type;
        }
    }
}
