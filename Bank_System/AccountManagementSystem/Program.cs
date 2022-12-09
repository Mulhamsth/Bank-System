using System.Text.RegularExpressions;
using System.Text;

namespace AccountManagementSystem
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
        }

    }
    public class Account
    {
        //readonly
        internal int ID;
        internal string Name { get; set; }
        internal int Balance { get; set; }

        private string _email { get; set; }
        internal string Email
        {
            get { return _email; }
            set
            {
                //A simple regex to validate string against email format and
                //catch the most obvious syntax errors:

                if (new Regex(@"^[a-zA-Z0-9]+@[a-zA-Z0-9]+\.[a-zA-Z0-9]+$").Match(value).Success)
                    this._email = value;
                else
                    throw new Exception("Invalid Email");
            }
        }

        private string _password { get; set; }
        internal string Password
        {
            get { return _password; }
            set
            {
                /*
                Has minimum 8 characters in length. Adjust it by modifying {8,}
                At least one uppercase English letter. You can remove this condition by removing (?=.*?[A-Z])
                At least one lowercase English letter.  You can remove this condition by removing (?=.*?[a-z])
                At least one digit. You can remove this condition by removing (?=.*?[0-9])
                At least one special character,  You can remove this condition by removing (?=.*?[#?!@$%^&*-])
                */

                if (new Regex(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$").Match(value).Success)
                    _password = value;
                else
                    throw new Exception("Invalid Password");
            }
        }
        public Account(string Name, string email, string password, int balance = 0)
        {
            this.Name = Name;
            this.Balance = balance;
            this.Email = email;
            this.Password = password;
        }

        public override string ToString()
        {
            /* for storing in Database
             * e.g.:     1;Max Mustermann;mustermann;Maxmuster1234!;100
             * the ; is used to make it readable for the Database when split it
             */
            return $"{ID};{Name};{_email};{_password};{Balance}";
        }
    }

    public class AccountsDB
    {
        public List<Account> _accounts = new List<Account>();
        List<Account> ExtractedAccounts = new List<Account>();
        public int Count { get { return _accounts.Count; } }


        #region Extracting Accounts
        private bool Extracted = false;
        public void ExtractAccounts(string filename)
        {
            StreamReader sr = new StreamReader(filename);
            while (sr.Peek() > 0)
            {
                string[] parts = sr.ReadLine().Split(";");
                int ID = int.Parse(parts[0]);
                string Name = parts[1];
                string Email = parts[2];
                string Password = parts[3];
                int Balance = int.Parse(parts[4]);
                Account account = new Account(Name, Email, Password, Balance) { ID = ID };
                ExtractedAccounts.Add(account);
            }
            sr.Close();
        }

        public void AddExtractedAccounts()
        {
            if (Extracted)
            {
                foreach (Account account in ExtractedAccounts) { _accounts.Add(account); }
                Extracted = false;
                ExtractedAccounts.Clear();
            }
            return;
        }
        public string ShowExtractedAccounts(string seperator = "\n")
        {
            StringBuilder strb = new StringBuilder();
            foreach (Account a in ExtractedAccounts)
            {
                strb.Append(a.ToString() + seperator);
            }
            string str = strb.ToString();
            return str;
        }
        #endregion
        public string ShowAccounts(string seperator = "\n")
        {
            StringBuilder strb = new StringBuilder();
            foreach (Account a in _accounts)
            {
                strb.Append(a.ToString() + seperator);
            }
            string str = strb.ToString();
            return str;
        }
        public void ExportAccounts(string filename)
        {
            StreamWriter sw = new StreamWriter(filename);
            foreach (Account account in _accounts) { sw.WriteLine(account); }
            sw.Close();
        }
        public void AddAccount(Account account)
        {
            account.ID = Count + 1;
            _accounts.Add(account);
        }

        public Account FindAccount(string Email)
        {
            foreach (Account account in _accounts)
            {
                if (account.Email == Email) return account;
            }
            return null;
        }
        public Account FindAccount(int id)
        {
            foreach (Account account in _accounts)
            {
                if (account.ID == id) return account;
            }
            return null;
        }
        public void RemoveAccount(Account account)
        {
            _accounts.Remove(account);
        }
        public bool DoesExist(int id)
        {
            if (FindAccount(id) == null) return false;
            return true;
        }
        public void ModifyAccount(Account acc, string? Name, string? Email, string? password)
        {
            if (Name != null)
                acc.Name = Name;
            if (Email != null)
                acc.Email = Email;
            if (password != null)
                acc.Password = password;
            return;
        }
    }

    public static class Login
    {
        public static bool IsValid(AccountsDB db, string Email, string Password)
        {
            Account acc = db.FindAccount(Email);
            if (acc != null)
                if (acc.Password == Password)
                    return true;
            return false;
        }
    }

    public static class Register
    {
        public static void NewAccount(AccountsDB DB, string Name, string Email, string Password)
        {
            if (new Regex(@"^[a-zA-Z0-9]+@[a-zA-Z0-9]+\.[a-zA-Z0-9]+$").Match(Email).Success)
                if (new Regex(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$").Match(Password).Success)
                {
                    Account acc = new Account(Name, Email, Password);
                    DB.AddAccount(acc);
                }
            return;
        }
    }
}