using System;

namespace Stationery_Management
{
    class Program
    {
        private static MainMenu Menu = new MainMenu();
        static void Main(string[] args)
        {
            Menu.GetDataFromTextFile();

            bool exit = false;
            do
            {
                Console.Write("----- MAIN MENU -----\n 1. Add to stock\n 2. Take from stock\n 3. Inventory report\n 4. Financial report\n 5. Transaction log\n 6. Personal report\n 7. Exit\n");
                Console.Write(" Choose: ");
                string input = Console.ReadLine();
                string code;
                string name;
                string person;
                float price;
                int quantity;
                bool valid = true;
                switch (input)
                {
                    case "1":
                        do
                        {
                            Console.Write("    Enter item code: ");
                            code = Console.ReadLine();
                            if (code == "")
                            {
                                valid = false;
                            }
                            else
                            {
                                valid = true;
                            }
                        } while (valid == false);
                        do
                        {
                            Console.Write("    Enter item name: ");
                            name = Console.ReadLine();
                            if (name == "")
                            {
                                valid = false;
                            }
                            else
                            {
                                valid = true;
                            }
                        } while (valid == false);
                        do
                        {
                            Console.Write("    Enter price: ");
                            valid = float.TryParse(Console.ReadLine(), out price);
                            if (price <= 0)
                            {
                                valid = false;
                            }
                            else
                            {
                                valid = true;
                            }
                        } while (valid == false);
                        do
                        {
                            Console.Write("    Enter quantity: ");
                            valid = int.TryParse(Console.ReadLine(), out quantity);
                            if (quantity <= 0)
                            {
                                valid = false;
                            }
                            else
                            {
                                valid = true;
                            }
                        } while (valid == false);
                        Menu.AddToStock(code, name, price, quantity);
                        break;
                    case "2":
                        do
                        {
                            Console.Write("    Enter item code: ");
                            code = Console.ReadLine();
                            if (code == "")
                            {
                                valid = false;
                            }
                            else
                            {
                                valid = true;
                            }
                        } while (valid == false);
                        do
                        {
                            Console.Write("    Enter person name: ");
                            person = Console.ReadLine();
                            if (person == "")
                            {
                                valid = false;
                            }
                            else
                            {
                                valid = true;
                            }
                        } while (valid == false);
                        Menu.TakeFromStock(code, person);
                        break;
                    case "3":
                        Console.WriteLine(Menu.InventoryReport());
                        break;
                    case "4":

                        Console.WriteLine(Menu.FinancialReport());
                        break;
                    case "5":
                        Console.WriteLine(Menu.DisplayTransactionLog());
                        break;
                    case "6":
                        do
                        {
                            Console.Write("    Enter person name: ");
                            person = Console.ReadLine();
                            if (person == "")
                            {
                                valid = false;
                            }
                            else
                            {
                                valid = true;
                            }
                        } while (valid == false);
                        Console.WriteLine(Menu.ReportPersonalUsage(person));
                        break;
                    case "7":
                        exit = true;
                        break;
                }
            } while (exit == false);
            Environment.Exit(0);
        }
    }
}