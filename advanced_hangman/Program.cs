using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;

namespace advanced_hangman
{
    class Program
    {

        public static string read;
        public static string titleName;
        private static void header()
        {
            int width = Console.WindowWidth;
            string gameName = "Hangman";
            for (int i = 0; i < width; i++)
            {
                Console.Write("_");
            }

            Console.WriteLine();
            int center1 = 0;
            foreach (char a in gameName)
            {
                center1++;
            }
            int center2 = center1 / 2;
            Console.SetCursorPosition((width / 2 - center2), 1);
            Console.Write(gameName);
            Console.WriteLine();

            center1 = 0;
            foreach (char a in titleName)
            {
                center1++;
            }
            center2 = center1 / 2;
            Console.SetCursorPosition((width / 2 - center2), 2);
            Console.Write(titleName);
            Console.WriteLine();
            for (int i = 0; i < width; i++)
            {
                Console.Write("-");
            }

        }
        static void Main(string[] args)
        {
            string[] words = { "random", "cable", "bun", "fridge", "bonkers", "crazy", "hat", "bird", "decoration", "trash", "candle", "snow", "gloves", "christ" };
            string thought = (words[new Random().Next(0, words.Length)]);
            char[] letters = new char[thought.Length];
            bool[] letterDisplay = new bool[thought.Length];
            for (int i = 0; i < thought.Length; i++)
            {
                letterDisplay[i] = false;
                letters[i] = thought[i];
            }
            bool exit = false;
            string misses = "";
            int lives = thought.Length + 2;
            bool menu = false;
            string name;

            do
            {
                Console.Clear();
                ConsoleKeyInfo menuChoice;
                titleName = "Menu";
                header();
                Console.WriteLine("[1] Play\n[2] High Scores\n[Esc] Exit");
                menuChoice = Console.ReadKey();
                if (menuChoice.Key == ConsoleKey.D2)
                {
                    bool hs = false;
                    do
                    {
                        Console.Clear();
                        titleName = "High Scores";
                        header();
                        try
                        {
                            hs = true;
                            using (StreamReader readtext = new StreamReader("highscores.txt"))
                            {
                                Console.Clear();
                                header();
                                string read = readtext.ReadToEnd();
                                Console.WriteLine("NAME\tSCORE");
                                for (int i = 0; i < 13; i++)
                                {
                                    Console.Write("-");
                                }
                                Console.WriteLine(read);
                            }
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("No High Scores yet!");
                        }
                        Console.ReadKey();
                        menu = true;
                        hs = false;
                        
                    } while (hs == true);
                }
                else if (menuChoice.Key == ConsoleKey.Escape)
                {
                    menu = false;
                }

                else if (menuChoice.Key == ConsoleKey.D1)
                {
                    
                    Console.Clear();
                    titleName = "Play Game";
                    do
                    {
                        
                        ConsoleKeyInfo guess;
                        header();
                        Console.Write("Good luck!\n ");
                        for (int x = 0; x < thought.Length; x++)
                        {
                            if (letterDisplay[x] == true)
                            {
                                Console.Write(" " + thought[x]);
                            }
                            else
                            {
                                Console.Write(" _");
                            }
                        }
                        Console.WriteLine("\nYour incorrect guesses so far: {0}", misses);
                        Console.WriteLine("You have {0} tries left", lives);

                        Console.WriteLine("\nGuess a letter! ");
                        guess = Console.ReadKey();
                        char guessed = guess.KeyChar;                        
                       
                        if (letters.Contains(guessed))
                        {


                           for (int x = 0; x < letters.Length; x++)
                            {
                                if (guessed == letters[x])
                                {
                                    letterDisplay[x] = true;
                                    Console.Clear();
                                }
                            }
                            
                        }
                        else if (misses.Contains(guessed))
                        {
                            Console.WriteLine("\nYou already tried that letter!");
                            Thread.Sleep(750);
                            Console.Clear();
                        }                       
                        else
                        {
                            lives--;
                            string miss = guessed.ToString();
                            misses += miss + " ";
                            Console.WriteLine("\nSorry, {0} not in the word!", miss);
                            Thread.Sleep(750);
                            Console.Clear();

                        }

                        exit = true;
                        foreach (bool a in letterDisplay)
                        {
                            if (a == false)
                            {
                                exit = false;
                            } 
                                                      
                        }
                        



                    } while (lives > 0 && exit == false);
                    
                    
                    int score = (1000 - (misses.Length*30));

                    if (lives != 0)
                    {

                        if (misses.Length == 0)
                        {
                            score += 500;
                        }
                        titleName = "You won!";
                        header();
                        Console.WriteLine("{0}\nGood job!", thought);
                        Thread.Sleep(1000);
                        Console.WriteLine("Your socre was {0} points", score);
                        Thread.Sleep(500);
                        Console.WriteLine("Please enter your name for the scoreboard!");
                        
                    }
                    else
                    {
                        score = 0;
                        foreach (bool b in letterDisplay)
                        {
                            if (b == true)
                            {
                                score += 50;
                            }
                        }
                        titleName = "Game Over";
                        header();
                        Console.WriteLine("Out of lives! Game over!");
                        Thread.Sleep(500);
                        Console.WriteLine("Try again", thought);
                        Thread.Sleep(500);
                        Console.WriteLine("Your socre was {0} points", score);
                        Thread.Sleep(500);
                        Console.WriteLine("Please enter your name for the scoreboard!");

                    }
                    name = Console.ReadLine();

                    try
                    {
                        using (StreamReader readtext = new StreamReader("highscores.txt"))
                        {
                            read = readtext.ReadToEnd();
                        }
                    }
                    catch (Exception)
                    {
                    }
                    using (StreamWriter writetext = new StreamWriter("highscores.txt"))
                    {
                        writetext.WriteLine("\n" + name + "\t" + score + "\n" +read);                        
                    }

                    menu = false;
                   
                    
                }
                else
                {
                    Console.WriteLine("\nPlease only navigate with the [1], [2] and [Esc] keys!");
                    Thread.Sleep(1000);
                    Console.Clear();
                    menu = true;
                }
                    

                
            } while (menu == true);




        }
    }
}
