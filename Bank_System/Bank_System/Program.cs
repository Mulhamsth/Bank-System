namespace Bank_System
{
    internal class Program
    {
        static void Main(string[] args)
        {
            AccountsTable At = new AccountsTable();


            Account a = new Account(1,"Mulham Taylouni", "mulham@gmail.com", "Password!1234");
            Account a2 = new Account(2, "Morhaf Taylouni", "morhaf@gmail.com", "Password!q53");
            Account a3 = new Account(3, "Wiesinger Christian", "Wiesinger@gmail.com", "Password!423525");
            Account a4 = new Account(3, "Glaser Niklas", "Niklas@gmail.com", "Passwords!4124143525");

            At.AddAccount(a);
            At.AddAccount(a2);
            At.AddAccount(a3);

            At.ExportAccounts("data.txt");

            //Console.WriteLine(At.ShowAccounts());
            //Console.WriteLine("_----------------------");
            //At.ExtractAccounts("data.txt");
            //Console.WriteLine(At.ShowExtractedAccounts("\n"));
            //Console.WriteLine("_----------------------");
            //At.RemoveAccount(At.FindAccount(2));

            //Console.WriteLine(At.ShowAccounts());
            //Console.WriteLine("------------------------");
            //At.ModifyAccount(At.FindAccount(3),null,null,"asw3Adda!124");
            //Console.WriteLine(At.ShowAccounts());

            Console.WriteLine(At.ShowAccounts());
            Console.WriteLine("-------------------------------------");
            At.ExtractAccounts("data.txt");
            Console.WriteLine(At.ShowExtractedAccounts());
            Console.WriteLine("--------------------unterschied----------------");
            At.AddAccount(a4);
            Console.WriteLine(At.ShowAccounts());
            Console.WriteLine("-----------------------------------------");
            At.AddExtractedAccounts();
            Console.WriteLine(At.ShowAccounts());
        }
    }
}