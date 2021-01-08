using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Stationery_Management
{
    class MainMenu
    {
        public StockManager StockMgr = new StockManager();
        public TransactionLogManager LogMgr = new TransactionLogManager();

        public void GetDataFromTextFile()
        {
            try
            {
                FileInfo stockfile = new FileInfo("StockList.txt");
                if (!stockfile.Exists)
                {
                    File.Create("StockList.txt").Dispose();
                }
                else
                {
                    FileStream filestream = stockfile.OpenRead();
                    StreamReader streamreader = new StreamReader(filestream);
                    while (!streamreader.EndOfStream)
                    {
                        string[] line = streamreader.ReadLine().Split(",");
                        string code = line[0];
                        string name = line[1];
                        float price = float.Parse(line[2]);
                        int quantity = Int32.Parse(line[3]);
                        StockItem item = new StockItem(code, name, price, quantity);
                        StockMgr.Items.Add(item);
                    }
                    streamreader.Close();
                }

                FileInfo logfile = new FileInfo("LogFile.txt");
                if (!logfile.Exists)
                {
                    File.Create("LogFile.txt").Dispose();
                }
                else
                {
                    FileStream filestream = logfile.OpenRead();
                    StreamReader streamreader = new StreamReader(filestream);
                    while (!streamreader.EndOfStream)
                    {
                        string[] line = streamreader.ReadLine().Split(",");
                        DateTime date = DateTime.Parse(line[0]);
                        string code = line[1];
                        string name = line[2];
                        string type = line[3];
                        if (type == "Add")
                        {
                            float price = float.Parse(line[4]);
                            TransactionLogEntryAdd addentry = new TransactionLogEntryAdd(date, code, name, type, price);
                            LogMgr.TransactionLogEntries.Add(addentry);
                        }
                        else if (type == "Remove")
                        {
                            string person = line[5];
                            TransactionLogEntryRemove removeentry = new TransactionLogEntryRemove(date, code, name, type, person);
                            LogMgr.TransactionLogEntries.Add(removeentry);
                        }
                    }
                    streamreader.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(" Error: " + e.Message);
                Environment.Exit(0);
            }
        }

        public void AddToStock(string code, string name, float price, int quantity)
        {
            StockMgr.AddItem(code, name, price, quantity);
            LogMgr.AddEntry(DateTime.Now, "Add", code, name, price, "");
            Console.WriteLine(" Item Added");
        }

        public void TakeFromStock(string code, string person)
        {
            try
            {
                if (StockMgr.Items.Count != 0)
                {
                    bool found = false;
                    foreach (StockItem i in StockMgr.Items)
                    {
                        if (i.ItemCode == code)
                        {
                            StockMgr.RemoveItem(code);
                            LogMgr.AddEntry(DateTime.Now, "Remove", code, i.ItemName, 0, person);
                            found = true;
                            break;
                        }
                    }
                    if (found == true)
                    {
                        Console.WriteLine(" Item removed.");
                    }
                    else
                    {
                        Console.WriteLine(" Item not found.");
                    }
                }
                else
                {
                    Console.WriteLine(" There are no items");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(" Error: " + e.Message);
            }


        }

        public string InventoryReport()
        {
            try
            {
                if (StockMgr.Items.Count != 0)
                {
                    string items = String.Format(" {0,10} - {1,-10} - {2,-10}\n", "Code", "Item", "Quantity");
                    foreach (StockItem i in StockMgr.Items)
                    {
                        string code = i.ItemCode;
                        string name = i.ItemName;
                        string quantity = i.Quantity.ToString();
                        string item = String.Format(" {0, 10} - {1,-10} - {2,-10}", code, name, quantity);
                        items += item + "\n";
                    }
                    return items;
                }
                else
                {
                    return " There are no items";
                }
            }
            catch (Exception e)
            {
                return (" Error: " + e.Message);
            }
        }

        public string FinancialReport()
        {
            try
            {
                if (LogMgr.TransactionLogEntries.Count != 0)
                {
                    float itemtotal = 0;
                    float total = 0;
                    string entries = String.Format(" {0,10} - {1,-10} - {2,-11}\n", "Code", "Item", "Total price"); ;
                    foreach (StockItem item in StockMgr.Items)
                    {
                        itemtotal += item.Price * item.Quantity;
                        total += itemtotal;

                        string entrystring = String.Format(" {0, 10} - {1,-10} - {2,-11}", item.ItemCode, item.ItemName, itemtotal.ToString());
                        entries += entrystring + "\n";
                    }
                    entries += " Total spent on all items: " + itemtotal.ToString();
                    return entries;
                }
                else
                {
                    return " The transaction log is empty";
                }
            }
            catch (Exception e)
            {
                return (" Error: " + e.Message);
            }
        }

        public string DisplayTransactionLog()
        {
            try
            {
                if (LogMgr.TransactionLogEntries.Count != 0)
                {
                    List<string[]> entries = new List<string[]>();
                    foreach (TransactionLogEntry i in LogMgr.TransactionLogEntries)
                    {
                        string[] entry;
                        string date = i.Date.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                        string type = i.Type;
                        string name = i.ItemName;

                        if (i.Type == "Add")
                        {
                            TransactionLogEntryAdd a = (TransactionLogEntryAdd)i;
                            string price = a.Price.ToString();
                            entry = new string[] { date, type, name, price };
                        }
                        else
                        {
                            TransactionLogEntryRemove a = (TransactionLogEntryRemove)i;
                            string person = a.Person;
                            entry = new string[] { date, type, name, person };
                        }
                        entries.Add(entry);
                    }
                    entries = entries.OrderBy(x => x.FirstOrDefault()).ToList();
                    string entriesstring = String.Format(" {0,10} - {1,-10} - {2,-10} - {3,-10} - {4,-10}\n", "Date", "Type", "Item", "Price", "Person"); ;
                    string entrystring;
                    foreach (string[] i in entries)
                    {
                        if (i[1] == "Add")
                        {
                            entrystring = String.Format(" {0, 10} - {1,-10} - {2,-10} - {3, -10} -", i[0], i[1], i[2], i[3]);
                        }
                        else if (i[1] == "Remove")
                        {
                            entrystring = String.Format(" {0, 10} - {1,-10} - {2,-10} -            - {3, -10}", i[0], i[1], i[2], i[3]);
                        }
                        else
                        {
                            entrystring = " Error";
                        }
                        entriesstring += entrystring + "\n";
                    }
                    return entriesstring;
                }
                else
                {
                    return " The transaction log is empty";
                }
            }
            catch (Exception e)
            {
                return (" Error: " + e.Message);
            }
        }

        public string ReportPersonalUsage(string person)
        {
            try
            {
                string entries = String.Format(" {0,10} - {1,-10} - {2,-10}\n", "Date taken", "Item Code", "Item");

                foreach (TransactionLogEntry i in LogMgr.TransactionLogEntries)
                {
                    if (i.Type == "Remove")
                    {
                        TransactionLogEntryRemove a = (TransactionLogEntryRemove)i;
                        if (a.Person == person)
                        {
                            string code = a.ItemCode;
                            string name = a.ItemName;
                            string date = a.Date.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                            string entry = String.Format(" {0, 10} - {1,-10} - {2,-10}", date, code, name);
                            entries += entry + "\n";
                        }
                    }
                }
                if (entries == String.Format(" {0,10} - {1,-10} - {2,-10}\n", "Date taken", "Item Code", "Item"))
                {
                    return " No entries found";
                }
                else
                {
                    return entries;
                }
            }
            catch (Exception e)
            {
                return (" Error: " + e.Message);
            }
        }
    }
}
