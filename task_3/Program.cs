using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Diagnostics;

namespace task_3
{
    public class SHMAC
    {
        
        public static string Encode(string input, byte[] key)
        {
            Random rnd = new Random();
            //int kkk = rnd.Next();

            HMACSHA1 myhmacsha1 = new HMACSHA1(key);
            byte[] byteArray = Encoding.ASCII.GetBytes(input);
            MemoryStream stream = new MemoryStream(byteArray);
            var aboba = myhmacsha1.ComputeHash(stream).Aggregate("", (s, e) => s + String.Format("{0:x2}", e), s => s);
            return aboba;
        }
    }
    public class HELP
    {

    }

    public static class TableParser
    {
        public static string ToStringTable<T>(
          this IEnumerable<T> values,
          string[] columnHeaders,
          params Func<T, object>[] valueSelectors)
        {
            return ToStringTable(values.ToArray(), columnHeaders, valueSelectors);
        }

        public static string ToStringTable<T>(
          this T[] values,
          string[] columnHeaders,
          params Func<T, object>[] valueSelectors)
        {
            Debug.Assert(columnHeaders.Length == valueSelectors.Length);

            var arrValues = new string[values.Length + 1, valueSelectors.Length];

            // Fill headers
            for (int colIndex = 0; colIndex < arrValues.GetLength(1); colIndex++)
            {
                arrValues[0, colIndex] = columnHeaders[colIndex];
            }

            // Fill table rows
            for (int rowIndex = 1; rowIndex < arrValues.GetLength(0); rowIndex++)
            {
                for (int colIndex = 0; colIndex < arrValues.GetLength(1); colIndex++)
                {
                    arrValues[rowIndex, colIndex] = valueSelectors[colIndex]
                      .Invoke(values[rowIndex - 1]).ToString();
                }
            }

            return ToStringTable(arrValues);
        }

        public static string ToStringTable(this string[,] arrValues)                                 /////
        {
            int[] maxColumnsWidth = GetMaxColumnsWidth(arrValues);
            var headerSpliter = new string('-', maxColumnsWidth.Sum(i => i + 3) - 1);

            var sb = new StringBuilder();
            for (int rowIndex = 0; rowIndex < arrValues.GetLength(0); rowIndex++)
            {
                for (int colIndex = 0; colIndex < arrValues.GetLength(1); colIndex++)
                {
                    // Print cell
                    string cell = arrValues[rowIndex, colIndex];
                    cell = cell.PadRight(maxColumnsWidth[colIndex]);
                    sb.Append(" | ");
                    sb.Append(cell);
                }

                // Print end of line
                sb.Append(" | ");
                sb.AppendLine();

                // Print splitter         
             
                    sb.AppendFormat(" |{0}| ", headerSpliter);
                    sb.AppendLine();
                
            }

            return sb.ToString();
        }

        private static int[] GetMaxColumnsWidth(string[,] arrValues)
        {
            var maxColumnsWidth = new int[arrValues.GetLength(1)];
            for (int colIndex = 0; colIndex < arrValues.GetLength(1); colIndex++)
            {
                for (int rowIndex = 0; rowIndex < arrValues.GetLength(0); rowIndex++)
                {
                    int newLength = arrValues[rowIndex, colIndex].Length;
                    int oldLength = maxColumnsWidth[colIndex];

                    if (newLength > oldLength)
                    {
                        maxColumnsWidth[colIndex] = newLength;
                    }
                }
            }

            return maxColumnsWidth;
        }
    }

    public class RULES
    {

    }

    public class GAME{

        public int userChoice;
        public int computerChoice;
        public int pomelo()
        {
                //game logic here
                Random r = new Random();
                int computerChoice = r.Next(1,3);

                if (computerChoice == 1)
                {
                    if (userChoice == 1)
                    {
                        Console.WriteLine("The computer chose rock");
                        Console.WriteLine("It is a tie ");
                    }
                    else if (userChoice == 2)
                    {
                        Console.WriteLine("The computer chose paper");
                        Console.WriteLine("It is a tie ");

                    }
                    else if (userChoice == 3)
                    {
                        Console.WriteLine("The computer chose scissors");
                        Console.WriteLine("It is a tie ");
                    }

                }

                else if (computerChoice == 2)
                {
                    if (userChoice == 1)
                    {
                        Console.WriteLine("The computer chose paper");
                        Console.WriteLine("Sorry you lose,paper beat rock");

                    }
                    else if (userChoice == 2)
                    {
                        Console.WriteLine("The computer chose scissors");
                        Console.WriteLine("Sorry you lose,scissors beat paper ");

                    }
                    else if (userChoice == 3)
                    {
                        Console.WriteLine("The computer chose rock");
                        Console.WriteLine("Sorry you lose,rock beats scissors");
                    }
                    else
                    {
                        Console.WriteLine("You must choose rock,paper or scissors!");
                    }
                }
                else if (computerChoice == 3)
                {
                    if (userChoice == 1)
                    {
                        Console.WriteLine("The computer chose scissors");
                        Console.WriteLine("You win,rock beats scissors");

                    }
                    else if (userChoice == 2)
                    {
                        Console.WriteLine("The computer chose rock");
                        Console.WriteLine("You win,paper beats rock");

                    }
                    else if (userChoice == 3)
                    {
                        Console.WriteLine("The computer chose paper");
                        Console.WriteLine("You win,scissors beat paper");

                    }
                    else
                    {
                        Console.WriteLine("You must choose rock,paper or scissors!");

                    }

                }


            
            return computerChoice;
        }
    }

    public class Program
    {

        static void Main(string[] args)
        {
            int message;
            string hmac1;
            string[] subs;
            if (args.Length >= 3)
            {
                subs = args;
            }
            else
            {
                string s = Console.ReadLine();
                subs = s.Split(' ');
            }
            GAME game = new GAME();
            SHMAC hmac = new SHMAC();
           // do {

                foreach (var sub in subs)
                {
                    string input = $"{sub}";
                    Console.WriteLine("\nYour choice: " + input+"\n");
                    if (input == "help")
                    {
                        Console.WriteLine("  ------------------------------------");
                        IEnumerable<Tuple<string, string, string, string>> help =
                               new[]
                               {
                              Tuple.Create("rock", "draw", "win", "lose"),
                              Tuple.Create("paper", "lose", "draw","win"),
                              Tuple.Create("scissors", "win", "lose","draw"),

                               };

                        Console.WriteLine(help.ToStringTable(
                          new[] { "pc" + @"\" + "user", "rock", "paper", "scissors" },
                          a => a.Item1, a => a.Item2, a => a.Item3, a => a.Item4));

                    }
                    else
                    {
                        try
                        {
                            int input1 = Convert.ToInt32(input);
                        byte[] inp = Encoding.UTF8.GetBytes(sub);
                        var key = SHMAC.Encode(input1.ToString(), inp);
                        Console.WriteLine("key: " + key);
                            switch (input1)
                            {
                                case 1:
                                    Console.WriteLine("\nYour choice: rock\n");

                                    game.userChoice = 1;
                                    game.pomelo();
                                     message = game.computerChoice;
                                     hmac1 = SHMAC.Encode(Convert.ToString(message), Encoding.UTF8.GetBytes(key));
                                    Console.WriteLine("HMAC: " + hmac1 + "\n");
                                break;
                                case 2:
                                    Console.WriteLine("\nYour choice: paper\n");
                                    game.userChoice = 2;
                                    game.pomelo();
                                message = game.computerChoice;
                                hmac1 = SHMAC.Encode(Convert.ToString(message), Encoding.UTF8.GetBytes(key));
                                Console.WriteLine("HMAC: " + hmac1 + "\n");
                                break;
                                case 3:
                                    Console.WriteLine("\nYour choice: scissors\n");
                                    game.userChoice = 3;
                                    game.pomelo();
                                message = game.computerChoice;
                                hmac1 = SHMAC.Encode(Convert.ToString(message), Encoding.UTF8.GetBytes(key));
                                Console.WriteLine("HMAC: " + hmac1 + "\n");
                                break;

                                case 0:
                                    Console.WriteLine("Exit");
                                    return;
                                default:
                                    Console.WriteLine("unknown command");
                                Console.WriteLine("Введите ваш ход:");
                                Console.WriteLine("1 - rock");
                                Console.WriteLine("2 - paper");
                                Console.WriteLine("3 - scissors");
                                Console.WriteLine("help");
                                Console.WriteLine("0 - exit");

                                break;
                            }

                        }
                        catch
                        {
                            Console.WriteLine("unknown command");
                            Console.WriteLine("Введите ваш ход:");
                            Console.WriteLine("1 - rock");
                            Console.WriteLine("2 - paper");
                            Console.WriteLine("3 - scissors");
                            Console.WriteLine("help");
                            Console.WriteLine("0 - exit");

                            break;
                        }
                    }

                }
            //} while ("0" == "0");
        }
    }
}

