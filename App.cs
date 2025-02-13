using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.IO;

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
                Console.WriteLine("5. Spara ner informationen till en textfil");
                Console.WriteLine("0. Avsluta programmet \n\nSkriv en siffra för att göra ett menyval:\n");
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
                if (menuselection < 0 || menuselection > 5 || parse == false) 
                {
                    Console.WriteLine("Du måste välja ett alternativ mellan 0-4. Vänligen försök igen.\n");
                    parse = false;
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
                case 5:
                    SaveToFile();
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
            Console.WriteLine("Välj ett konto genom att skriva siffra 1-10.");
            while (loopparse == false)
            {
                loopparse = int.TryParse(Console.ReadLine(), out accountselection);
                if (accountselection < 0 || accountselection > 10 || loopparse == false)
                {
                    Console.WriteLine("Du måste välja ett alternativ mellan 1-10. Vänligen försök igen.");
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
            ReturnToMenu();
        }
        //*******************************************************************************************************************************

        public void Withdrawl()
        {
            double sum;
            bool loopparse = false;            
            Console.Clear();
            int account = AccountSelection();            
            Console.Clear();
            //Kollar om kontot är tomt eller har pengar. True = tomt konto
            bool emptyaccount = bank.IsAccEmpty(account);
            //Skicka tillbaka användaren till huvudmenyn om kontot är tomt, och man ej kan ta ut pengar öht
            if (emptyaccount)
            {
                Console.WriteLine("Det saknas pengar på kontot.");
            }
            //Annars får användaren gå vidare och ta ut pengar
            else
            {
                while (loopparse == false)
                {
                    Console.WriteLine("Välj summa du vill ta ut:");
                    loopparse = double.TryParse(Console.ReadLine(), out sum);
                    bool available = bank.CheckBalance(account, sum);
                    //Lägg till if-sats om saldot är noll. 
                    if (sum <= 0 || loopparse == false)
                    {
                        //Meddela saldo
                        Console.WriteLine("Ogiltig summa för uttag. Vänligen prova igen.\n");
                    }
                    else if (available == false)
                    {
                        Console.WriteLine("Du har inte tillräckligt med pengar på kontot för uttaget. Vänligen försök igen med en annan summa.\n");
                    }
                    else
                    {
                        bank.TakeOutMoney(account, sum);
                        loopparse = true;
                        Console.WriteLine($"Saldot på konto {bank.accounts[account].Number} är nu: {bank.accounts[account].Balance} kronor.\n\n");
                    }
                }
            }
            ReturnToMenu();
        }
        //*******************************************************************************************************************************

        public void PrintOneAccount()
        {
            Console.Clear();
            int account = AccountSelection();
            Console.WriteLine($"Saldot på konto {bank.accounts[account].Number} är: {bank.accounts[account].Balance} kronor.\n\n");
            ReturnToMenu();
        }
        //*******************************************************************************************************************************

        public void PrintAll()
        {
            Console.Clear();
            int i = 1;
            foreach (Account a in bank.accounts) 
            {
                Console.WriteLine($"{i}. Saldot på konto {a.Number} är: {a.Balance} kronor.");
                i++;
            }
            Console.WriteLine("\n\n");
            ReturnToMenu();
        }
        //*******************************************************************************************************************************
        public void SaveToFile() 
        {
            Console.Clear();
            Console.WriteLine("Sparar ner listan över alla bankkonton...");
            string accountinfo = JsonSerializer.Serialize(bank.accounts);
            File.WriteAllText("bankaccounts.json", accountinfo);
            Console.WriteLine("Klart.");
            ReturnToMenu();
        }
        //*******************************************************************************************************************************
        public void ReturnToMenu() 
        {
            Console.WriteLine("Tryck enter för att återgå till menyn.");
            Console.ReadKey();
        }
    }
}
