using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankomatenNY
{
    public class Account
    {
        //Properties
        public int Number { get; set; }
        public double Balance { get; set; }
        //********************************************CONSTRUCTOR***********************************************
        public Account(int number, double balance)
        {
            Number = number;
            Balance = balance;
        }
    }
}
