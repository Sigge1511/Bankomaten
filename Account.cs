using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankomatenNY
{
    public class Account
    {
        public int Number { get; set; }
        public double Balance { get; set; }


        public Account(int number, double balance)
        {
            Number = number;
            Balance = balance;
        }



    }
}
