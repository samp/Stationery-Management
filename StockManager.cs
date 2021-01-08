using System;
using System.Collections.Generic;
using System.IO;

namespace Stationery_Management
{
    class StockManager
    {
        public List<StockItem> Items = new List<StockItem>();
        public void AddItem(string code, string name, float price, int quantity)
        {
            try
            {
                bool found = false;
                foreach (StockItem i in Items)
                {
                    if (i.ItemCode == code)
                    {
                        found = true;
                        i.Quantity += quantity;
                        string[] filecontents = File.ReadAllLines("StockList.txt");
                        List<string> filelist = new List<string>(filecontents);
                        foreach (string line in filelist)
                        {
                            string[] splitline = line.Split(",");
                            if (splitline[0] == code)
                            {
                                filelist.Remove(line);
                                splitline[2] = i.Quantity.ToString();
                                string writestring = String.Format("{0},{1},{2},{3}", code, name, price, i.Quantity);
                                filelist.Add(writestring);
                                break;
                            }
                        }
                        File.WriteAllLines("StockList.txt", filelist.ToArray());
                    }
                }
                if (found == false)
                {
                    StreamWriter StockWriter = new StreamWriter("StockList.txt", true);
                    StockItem item = new StockItem(code, name, price, quantity);
                    Items.Add(item);
                    string writestring = String.Format("{0},{1},{2},{3}", code, name, price, quantity);
                    StockWriter.WriteLine(writestring);
                    StockWriter.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(" Error: " + e.Message);
            }
        }

        public void RemoveItem(string code)
        {
            try
            {
                foreach (StockItem i in Items)
                {
                    if (i.ItemCode == code)
                    {
                        string[] filecontents = File.ReadAllLines("StockList.txt");
                        List<string> filelist = new List<string>(filecontents);
                        foreach (string line in filelist)
                        {
                            string[] splitline = line.Split(",");
                            if (splitline[0] == code)
                            {
                                filelist.Remove(line);
                                if (i.Quantity > 1)
                                {
                                    i.Quantity -= 1;
                                    splitline[2] = i.Quantity.ToString();
                                    string writestring = String.Format("{0},{1},{2},{3}", code, i.ItemName, i.Price, i.Quantity);
                                    filelist.Add(writestring);
                                }
                                else
                                {
                                    Items.Remove(i);
                                }
                                File.WriteAllLines("StockList.txt", filelist.ToArray());
                                break;
                            }
                        }
                    }
                    break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(" Error: " + e.Message);
            }
        }
    }
}
