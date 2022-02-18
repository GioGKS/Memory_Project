using System;

namespace HMW_MemoryGame_Project
{
    class Program
    {
        static void Main(string[] args)
        {

            var game = new MemoryGame(4);
            int counter = 0;

            game.Start();

            while (!game.IsOver())
            {

                //Write With , Between Numbers

                Console.Write("Please guess a number, " +
                              "enter the guess in the following format: i,j - ");

                string input = Console.ReadLine();
                int i = int.Parse(input.Split(",")[0]);
                int j = int.Parse(input.Split(",")[1]);

                bool isValid = game.PlaceGuessAndValidate((i, j));
                if (!isValid) continue;
                game.RedrawBoard();

                //Write With , Between Numbers

                Console.Write("Please guess the second number, " +
                              "enter the guess in the following format: i,j - ");


                input = Console.ReadLine();
                i = int.Parse(input.Split(",")[0]);
                j = int.Parse(input.Split(",")[1]);


                if (isMatch)
                {
                    Console.WriteLine("Your guess was correct!");
                    counter++;
                    Console.WriteLine($"Good Job! You Have {counter} Points.");

                }
                else
                {
                    Console.WriteLine("Your guess was incorrect!");

                }
                Console.Clear();
            }

        }
        public static bool isMatch { get; set; }
    }



    public class MemoryGame
    {
        private int N;
        private int[,] board;
        private int guessedPairs;


        public MemoryGame(int N)
        {
            this.N = N;
            board = new int[N, N];
        }

        public (int i, int j) CurrentUserGuess { get; set; } = (-1, -1);

        public int GetNumberOfMatrix()
        {
            Console.WriteLine("Choose Number For Matrix:");
            int userNumber = int.Parse(Console.ReadLine());


            if (userNumber < 0 || userNumber % 2 == 1 || userNumber > 8)
            {

                for (int i = 0; i < int.MaxValue; i++)
                {
                    Console.WriteLine("Try One More Time:");
                    userNumber = int.Parse(Console.ReadLine());
                    if (userNumber == 2 || userNumber == 4 || userNumber == 6 || userNumber == 8)
                    {
                        break;
                    }
                }
            }
            Console.WriteLine($"Your Matrix Number Is: {userNumber}");

            return userNumber;
        }

        public void Start()
        {
            Fillboard();
            PrintBoard();
        }

        public void Fillboard()
        {
            int pairCount = N * N / 2;
            int pairNumber = 1;
            Random rand = new Random();

            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    PlacePair(pairNumber, rand);
                    pairNumber++;
                    if (pairNumber > pairCount)
                        return;
                }
            }
        }

        private void PrintBoard()
        {
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    int value = board[i, j];
                    if (guessedPairs == value || ((i, j) == CurrentUserGuess))
                    {
                        Console.Write($"{board[i, j]}\t");
                    }
                    else
                    {
                        Console.Write($"[*]\t");
                    }
                }

                Console.WriteLine();
            }
        }

        private void PlacePair(int pairNumber, Random random)
        {
            int elementsAdded = 0;

            do
            {
                int i = random.Next(N);
                int j = random.Next(N);

                if (board[i, j] == 0)
                {
                    board[i, j] = pairNumber;
                    elementsAdded++;
                }

            } while (elementsAdded < 2);
        }

        public bool PlaceGuessAndValidate((int i, int j) doubleNum)
        {

            int guess = board[doubleNum.i, doubleNum.j];
            if (guessedPairs == guess)
                return false;
            CurrentUserGuess = (doubleNum.i, doubleNum.j);
            return true;
        }

        public bool IsOver()
        {
            return guessedPairs == (N * N) / 2;
        }

        public void RedrawBoard()
        {
            PrintBoard();
        }


    }
}