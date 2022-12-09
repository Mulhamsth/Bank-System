namespace Bank_System
{
    internal class Program
    {
        static void Main(string[] args)
        {
            AccountsDB At = new AccountsDB();


            Account a = new Account("Mulham Taylouni", "mulham@gmail.com", "Password!1234");
            Account a2 = new Account("Morhaf Taylouni", "morhaf@gmail.com", "Password!q53");
            Account a3 = new Account("Wiesinger Christian", "Wiesinger@gmail.com", "Password!423525");
            Account a4 = new Account("Glaser Niklas", "Niklas@gmail.com", "Passwords!4124143525");

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
            Register.NewAccount(At, "test", "test@gmail.com", "Password!32525");
            Console.WriteLine("-------------------------------------");
            Console.WriteLine(At.ShowAccounts());
            Console.WriteLine("------------------------");
            Console.WriteLine(Login.IsValid(At, "test@gmail.com", "Password!32525"));


        }
    }
}