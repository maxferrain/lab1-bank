using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
//using System.Timers;

namespace Bank
{
    internal class Program
    {
        private static Context context = new Context();

        public static void Transfer(double amountSum, int idFrom, int idTo)
        {
            double totalMoney = (from acc in context.Accounts
                                 where acc.id == idFrom
                                 select acc.totalSum).Sum();

            if (amountSum > totalMoney)
            {
                Console.WriteLine("Not Enought Money"); //exception here
            } else
            {
                Console.WriteLine(totalMoney);

                int idUserFrom = (from acc in context.Accounts
                                     where acc.id == idFrom
                                     select acc.User.id).Sum();

                Console.WriteLine(idUserFrom);

                int idUserTo = (from acc in context.Accounts
                                  where acc.id == idTo
                                  select acc.User.id).Sum();

                Console.WriteLine(idUserTo);

                if (idUserFrom == idUserTo)
                {
                    Console.WriteLine("Its Your Account, Dude! No comission!");
                    context.Transactions.Add(new Transaction
                    {
                        amount = amountSum,
                        accountIdFrom = idFrom,
                        accountIdTo = idTo
                    });

                    var accFrom = context.Accounts.Single(b => b.id == idFrom);
                    accFrom.totalSum = totalMoney - amountSum;

                    var accTo = context.Accounts.Single(b => b.id == idTo);
                    accTo.totalSum += amountSum;

                } else
                {
                    Console.WriteLine("Comission is 1%!");
                    double comission = amountSum * 0.01;
                    double amountTotal = amountSum - comission;

                    context.Transactions.Add(new Transaction
                    {
                        amount = amountTotal,
                        accountIdFrom = idFrom,
                        accountIdTo = idTo,
                    });

                    var accFrom = context.Accounts.Single(b => b.id == idFrom);
                    accFrom.totalSum -= (amountSum + comission);

                    var accTo = context.Accounts.Single(b => b.id == idTo);
                    accTo.totalSum += amountSum;
                }
                context.SaveChanges();
            }
        }

        class AutoInfo
        {
            public int accIdFrom;
            public int accIdTo;
            public double amount;
        }

        static void Main(string[] args)
        {

            context.Database.EnsureCreated();

            //Transfer(500, 1, 3);


            AutoInfo info = new AutoInfo();

            info.amount = 11;


            // устанавливаем метод обратного вызова
            TimerCallback tm = new TimerCallback(AutoPayment);
            // создаем таймер

            //1 Months = 2629800000 Milliseconds
            Timer timer = new Timer(tm, info.amount, 0, 5000);

            Console.ReadLine();


            context.SaveChanges();
            context.Dispose();
        }

        public static void AutoPayment(object amount)
        {
            var idFrom = 1;
            var idTo = 4;

            Console.WriteLine("Auto Payment turned on");

            context.Transactions.Add(new Transaction
            {
                amount = (double)amount,
                accountIdFrom = idFrom,
                accountIdTo = idTo
            });

            var accFrom = context.Accounts.Single(b => b.id == idFrom);
            accFrom.totalSum -= (double)amount;

            var accTo = context.Accounts.Single(b => b.id == idTo);
            accTo.totalSum += (double)amount;

            context.SaveChanges();

        }
    }
}
