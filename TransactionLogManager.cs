using System;
using System.Collections.Generic;
using System.IO;

namespace Stationery_Management
{
    class TransactionLogManager
    {
        public List<TransactionLogEntry> TransactionLogEntries { get; } = new List<TransactionLogEntry>();

        public void AddEntry(DateTime date, string type, string code, string name, float price, string person)
        {
            try
            {
                StreamWriter LogWriter = new StreamWriter("LogFile.txt", true);
                string writestring = "";
                if (type == "Add")
                {
                    TransactionLogEntryAdd addentry = new TransactionLogEntryAdd(date, code, name, type, price);
                    TransactionLogEntries.Add(addentry);
                    writestring = String.Format("{0},{1},{2},{3},{4},", date, code, name, "Add", price);
                }
                else if (type == "Remove")
                {
                    TransactionLogEntryRemove removeentry = new TransactionLogEntryRemove(date, code, name, type, person);
                    TransactionLogEntries.Add(removeentry);
                    writestring = String.Format("{0},{1},{2},{3},0,{4}", date, code, name, "Remove", person);
                }
                else
                {
                    Console.WriteLine(" Entry type not valid");
                }
                LogWriter.WriteLine(writestring);
                LogWriter.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(" Error: " + e.Message);
            }
        }
    }
}
