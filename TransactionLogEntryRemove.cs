using System;

namespace Stationery_Management
{
    class TransactionLogEntryRemove : TransactionLogEntry
    {
        public string Person { get; }
        public TransactionLogEntryRemove(DateTime date, string code, string name, string type, string person) : base(date, code, name, type)
        {
            this.Person = person;
        }
    }
}
