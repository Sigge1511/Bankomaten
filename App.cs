using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankomatenNY
{
    public class App
    {

        private Bank bank;

        public App(Bank bank)
        {
            this.bank = bank;
        }

        //*******************************METODER FÖR MENY************************************************************************************************

        public void PrintMenu() 
        {
            int option;            
            do
            {
                Console.Clear();
                Console.WriteLine("MENY:");
                Console.WriteLine("\n1. Gör en insättning på ett konto");
                Console.WriteLine("2. Gör ett uttag från ett konto");
                Console.WriteLine("3. Skriv ut saldo på ett konto");
                Console.WriteLine("4. Skriv ut kontonummer och saldo på samtliga konton");
                Console.WriteLine("0. Avsluta programmet \n\nSkriv en siffra för att göra ett menyval");
                option = MenuSelection();
                MenuHandler(option);              
            }while (option != 0);   
        }
        //*******************************************************************************************************************************
        public int MenuSelection() 
        {
            int menuselection =0;
            bool parse = false;
            while (parse == false)
            {
                parse = int.TryParse(Console.ReadLine(), out menuselection);
                if ((menuselection < 0 || menuselection > 4) || parse == false) 
                {
                    Console.WriteLine("Du måste välja ett alternativ mellan 0-4. Vänligen försök igen.\n");
                }
                else {parse = true;}
            }
            return menuselection;
        }
        //*******************************************************************************************************************************

        public void MenuHandler(int menuselection) 
        {
            switch (menuselection)
            {
                case 1:
                    Deposit();
                    break;

                case 2:
                    Withdrawl();
                    break;

                case 3:
                    PrintOneAccount();
                    break;

                case 4:
                    PrintAll();
                    break; 
                case 0:    
                    break;
            }
        }
        //*********************************METOD FÖR KONTOVAL**********************************************************************************************

        public int AccountSelection() 
        {
            int accountselection= 0;
            bool loopparse = false;
            //Får välja 1-10 för att det är pedagogiskt för användaren            
            while (loopparse == false)
            {
                Console.WriteLine("Välj ett konto genom att skriva siffra 1-10.");
                loopparse = int.TryParse(Console.ReadLine(), out accountselection);
                if (accountselection < 0 || accountselection > 10 || loopparse == false)
                {
                    Console.WriteLine("Du måste välja ett alternativ mellan 1-10. Vänligen försök igen.\n");
                    loopparse = false;
                }
                else { loopparse = true; }
            }
            //Minskar värdet med 1 eftersom arrayen har index 0-9
            accountselection--;
            return accountselection;
        }
        //***********************METODER FÖR MENYVALEN********************************************************************************************************

        public void Deposit()
        {
            Console.Clear();
            int account = AccountSelection();
            double sum;
            Console.Clear();
            bool loopparse = false;            
            while (loopparse == false) 
            {
                Console.WriteLine("Välj summa du vill sätta in:");
                loopparse = double.TryParse(Console.ReadLine(), out sum);
                if (sum <= 0)
                {
                    Console.WriteLine("Ogiltig summa för insättning. Vänligen prova igen.\n");
                    loopparse = false;
                }
                else 
                {
                    bank.AddMoney(account, sum);
                    loopparse = true;
                    Console.WriteLine($"Saldot på konto {bank.accounts[account].Number} är nu: {bank.accounts[account].Balance} kronor.\n\n");
                }                
            }
            Console.WriteLine("Tryck enter för att återgå till menyn.");
            Console.ReadLine();
        }
        //*******************************************************************************************************************************

        public void Withdrawl()
        {
            Console.Clear();
            int account = AccountSelection();
            double sum;
            Console.Clear();
            bool loopparse = false;
            while (loopparse == false)
            {
                Console.WriteLine("Välj summa du vill ta ut:");
                loopparse = double.TryParse(Console.ReadLine(), out sum);
                bool available = bank.CheckBalance(account, sum);
                if (sum <= 0 || loopparse==false)
                {
                    Console.WriteLine("Ogiltig summa för uttag. Vänligen prova igen.\n");
                    loopparse = false;
                }
                else if (available == false) 
                {
                    Console.WriteLine("Du har inte tillräckligt med pengar på kontot för uttaget. Vänligen försök igen med en annan summa.\n");
                    loopparse = false;
                }
                else
                {
                    bank.TakeOutMoney(account, sum);
                    loopparse = true;
                    Console.WriteLine($"Saldot på konto {bank.accounts[account].Number} är nu: {bank.accounts[account].Balance} kronor.\n\n");
                }                
            }
            Console.WriteLine("Tryck enter för att återgå till menyn.");
            Console.ReadLine();
        }
        //*******************************************************************************************************************************

        public void PrintOneAccount()
        {
            Console.Clear();
            int account = AccountSelection();
            Console.WriteLine($"Saldot på konto {bank.accounts[account].Number} är: {bank.accounts[account].Balance} kronor.\n\n");
            Console.WriteLine("Tryck enter för att återgå till menyn.");
            Console.ReadLine();
        }
        //*******************************************************************************************************************************

        public void PrintAll()
        {
            Console.Clear();
            foreach (Account a in bank.accounts) 
            {
                Console.WriteLine($"Saldot på konto {a.Number} är: {a.Balance} kronor.");
            }
            Console.WriteLine("\n\n");
            Console.WriteLine("Tryck enter för att återgå till menyn.");
            Console.ReadLine();
        }
    }
}
