// AUTHOR: Qiaofang Deng
// FILENAME: Program.cs
// DATE: 4/29/2018
// REVISION HISTORY: version 1
//
// Description:
// Get input from the user then call the driver 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Driver driver = new Driver();
            int guessNumber;
            bool isGuessRight;
            driver.Welcome();
            driver.Gamestart();
          
            do
            {
                driver.AskGuessNumber();
                guessNumber = int.Parse(Console.ReadLine());
                isGuessRight = driver.CheckGuessNumber(guessNumber);
            } while (!isGuessRight);
          
            driver.Goodbye();
        }

    }
}
