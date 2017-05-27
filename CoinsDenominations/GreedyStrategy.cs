using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoinsDenominations
{
    class GreedyStrategy
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Write("User Input of Amount : ");
                string number = Console.ReadLine();//Take the Console input
                int realAmount;
                bool parsed = Int32.TryParse(number, out realAmount);//Parse th input to double
                if (!parsed)//Check whether the output cannot parse to the double
                {
                    Console.WriteLine("\nWARNING : Could not parse '{0}' to an Integer\n", number);
                    if (number.Equals("exit") || number.Equals("EXIT")) {
                        Environment.Exit(0);//Exit from the system
                    }
                }
                else
                {
                    int[] denominations = { 7, 1, 2, 5, 10, 25, 50, 100 };//1 st => size of the list
                    int[] coins = new int[denominations.Length];//Store the # of coins that each denominator
                                 
                    Console.WriteLine("\n.....................Result.........................\n");
                    Console.WriteLine("\nDynamic changing values : \n");
                    //Calculate the minimum # of units in dynamic
                    dynamicChanges(realAmount, coins, denominations);
                    //Show the final output of dynamic algorithm
                    showdynamicResult(denominations, coins);
                    Console.WriteLine("\nGreedy changing values : \n");
                    //Calculate the minimum # of units in greedy
                    greedyChanges(realAmount, coins, denominations);
                    //Show the final output of greedy algorithm
                    showgreedyResult(denominations, coins);                    
                    Console.WriteLine("\n....................................................\n");
                }
            }
        }

        //Calculate the minimum number of units for given amount (Greedy Strategy)
        private static void greedyChanges(int amount, int[] coin, int[] denomination) {
            int counter;
            Array.Sort(denomination, 1, denomination.Length-1);
            for (int i = 0; i < coin.Length; i++) {
                coin[i] = 0;
            }
            for (counter = coin.Length - 1; counter >= 0 & amount > 0; counter--) {
                coin[counter] = amount / denomination[counter];
                amount -= coin[counter] * denomination[counter];
            }
        }

        //Display the final output on the Console (Greedy Results)
        private static void showgreedyResult(int[] denominations, int[] coins)
        {
            for (int i = 1; i < coins.Length; i++)
            {
                if (coins[i] > 0)
                    Console.WriteLine("Number of " + denominations[i] + "s : " + coins[i]);
            }
        }
        
        //Calculate the minimum number of units for given amount (Dynamic Strategy)
        public static void dynamicChanges(int amount, int[] coin, int[] denomination)
        {
	        int n = denomination.Length-1;
	        int [][] table = new int[n+1][]; //Tabular format (Table for dynamic approach)
            for (int counter = 0; counter < table.Length; counter++)
                table[counter] = new int[amount + 1];
	        int j, k;
	        //base case for tabular format
	        for ( k = 0; k <= amount; k++ )
                table[0][k] = 0;
            //Tabular Format generating
	        for ( j = 1; j <= n; j++ )
	        { 
		        coin[j] = 0; // zeroes in the coins-used vector
                table[j][0] = 0; // column 0 holds zeroes.
		        for ( k = 1; k <= amount; k++ )
			        if ( j == 1 )
				        if ( k < denomination[j] )
                            table[j][k] = Int32.MaxValue / 2; // restrict the overflow
				        else
                            table[j][k] = 1 + table[j][k - denomination[j]];
			        else
				        if ( k < denomination[j] )
                            table[j][k] = table[j - 1][k];
                        else//Take the min value
                            table[j][k] = Math.Min(table[j - 1][k], 1 + table[j][k - denomination[j]]);
	        } 
	        
	        // Start out walking the table
	        j = n; k = amount;
	        //Get the coins used from the table
	        while ( k > 0 && j > 0 )
                if (table[j][k] == table[j - 1][k]) // denomination not used
			        j = j - 1;
		        else // denomination was used
		        { ++coin[j]; k = k - denomination[j]; }
	    } 

        //Display the final output on the Console (Dynamic Results)
        public static void showdynamicResult(int[] denominations, int[] coins) {
            for (int i = 1; i < coins.Length; i++) {
                if(coins[i]>0)
                    Console.WriteLine("Number of " + denominations[i] + "s : " + coins[i]);
            }
        }
    }
}
