using System;
using System.IO;
using System.Collections.Generic;

namespace ATM
{
    internal class Program
    {
        static void Main(string[] args)
        {
            UI uI = new UI();
            uI.UserInterface();
        }
    }

    #region Admin UserInterface
    public class UI         //UserInterface Class for Admin
    {
        Accounttable at = new Accounttable();
        public void UserInterface()
        {
            while (true)
            {
                UIGetFormFile();
                Console.Clear();
                ShowOptions();
            }
        }
        public void ShowOptions()
        {
            bool loop = true;
            while (loop)
            {
                at.Display();
                Console.WriteLine("=============\n");
                Console.WriteLine("1. Add\t2. Delete\n3. Edit\t4. Quit");
                Console.WriteLine("=============\n");
                int ans = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine();
                switch (ans)
                {
                    case 1:
                        UIAdd();
                        break;
                    case 2:
                        UIDelete();
                        break;
                    case 3:
                        UIEdit();
                        break;
                    case 4:
                        loop = false;
                        break;
                    default:
                        Console.WriteLine("please choose one of the following options");
                        break;
                }
                at.save();
                System.Threading.Thread.Sleep(1000);
                Console.Clear();
            }
        }
        public void UIGetFormFile()
        {
            Console.WriteLine("Please enter your file name with .txt (if it does not exist, it will be created:\nif you want to edit the default file (data.txt) press the number 1");
            string ans = Console.ReadLine();
            if (ans != null && ans != "" && ans != " " && ans != "1")
                at.file = ans + ".txt";
            if (ans == "1")
                at.file = "data.txt";
            at.GetFormFile();
            at.save();
        }
        private void UIAdd()
        {
            Console.Write("ID:\t");
            string id = Console.ReadLine();
            Console.Write("Name:\t");
            string Name = Console.ReadLine();
            Console.Write("Password:\t");
            string Password = Console.ReadLine();
            Console.Write("Balance:\t");
            int balance = Convert.ToInt32(Console.ReadLine());
            at.Add(id, Name, Password, balance);
        }
        private void UIEdit()
        {
            Console.Write("Old ID:\t");
            string OldID = Console.ReadLine();
            Console.Write("New ID:\t");
            string NewID = Console.ReadLine();
            Console.Write("Name:\t");
            string Name = Console.ReadLine();
            Console.Write("Password:\t");
            string Password = Console.ReadLine();
            Console.Write("Balance:\t");
            int balance = Convert.ToInt32(Console.ReadLine());
            at.Edit(OldID, NewID, Name, Password, balance);
        }
        private void UIDelete()
        {
            Console.Write("ID:\t");
            string id = Console.ReadLine();
            at.Delete(id);
        }
    }

    #endregion

    #region Accounts Management

    class Accountfunc //Account functionalities
    {
        string AccID;
        public Account Account { 
            get { return at.account(AccID); }
            set { }
        }
        Accounttable at = new Accounttable();


        public void withdrawal(int amount)
        {

        }
    }

    class Accounttable  //contains all accounts
    {
        public List<Account> accounts = new List<Account>();
        public string file;

        public Account account(string id)   //search and return form the Accounts table a specific Account based on it's ID
        {

            for (int i = 0; i < accounts.Count; i++)
            {
                Account account = accounts[i];
                if (account.ID == id)
                    return account;
            }

            return null;
        }

        public void GetFormFile()   //To get the data out of an txt file
        {
            using (StreamReader sr = new StreamReader(file))
            {
                while (sr.Peek() > 0)
                {
                    string[] parts = sr.ReadLine().Split(';');
                    Account acc = new Account();
                    acc.ID = parts[0];
                    acc.Password = parts[1];
                    acc.Name = parts[2];
                    acc.balance = Convert.ToInt32(parts[3]);
                    accounts.Add(acc);
                }
            }
        }

        public void Display()       //displayes the list of Accounts
        {
            foreach (Account e in accounts)
                Console.WriteLine(e.ToString(" "));
        }

        public void save()      //to save the changes to the txt file
        {
            using (StreamWriter sw = new StreamWriter(file))
            {
                foreach (Account e in accounts)
                    sw.WriteLine(e.ToString(";"));
            }
        }

        public void Edit(string OldID, string NewID, string Name, string Password, int balance) //To change the data of an account
        {
            Account acc = account(OldID);
            if(NewID != "")
                acc.ID = NewID;
            if(Password != "")
                acc.Password = Password;
            if (Name != "")
                acc.Name = Name;
            if(balance >= 0)
                acc.balance = balance;

        }
        public void Add(string id, string Name, string Password, int balance)     //To add a new account
        {
            Account acc = new Account();
            acc.ID = id;
            acc.Password = Password;
            acc.Name = Name;
            acc.balance = balance;
            accounts.Add(acc);
        }

        public void Delete(string id)      //To delete an account
        {
            accounts.Remove(account(id));
        }
    }

    class Account   //store the information of an account
    {
        public string ID { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public int balance { get; set; }

        public new string ToString(string separator)
        {
            return ID + separator + Password + separator + Name + separator + balance;
        }

    }

    #endregion
}
