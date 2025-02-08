using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankomatenNY
{
    public class Bank
    {
        public string? BankName {get; set;}
        private App app;
        public Account[] accounts = new Account[10];
        
        public void Run() 
        {
            AddAccounts();
            this.app = new App(this);
            app.PrintMenu();
            Console.Clear();
            Console.WriteLine("Tryck enter för att avsluta programmet.");
            Console.ReadLine();
        }
        
        public void AddAccounts()
        {
            accounts[0] = new Account(1001, 0);
            accounts[1] = new Account(1012, 0);
            accounts[2] = new Account(1123, 0);
            accounts[3] = new Account(1234, 0);
            accounts[4] = new Account(2345, 0);
            accounts[5] = new Account(3456, 0);
            accounts[6] = new Account(4567, 0);
            accounts[7] = new Account(5647, 0);
            accounts[8] = new Account(6789, 0);
            accounts[9] = new Account(7890, 0);
        }
        public bool CheckBalance(int account, double withdrawl) 
        { 
            bool available = false;
            double balance = accounts[account].Balance;
            if (balance >= withdrawl) {available = true;}
            return available;
        }
        public void AddMoney(int account, double sum) 
        {
            accounts[account].Balance = sum + accounts[account].Balance;
        }
        public void TakeOutMoney(int account, double sum)
        {
            accounts[account].Balance = accounts[account].Balance - sum;
        }
    }
}
