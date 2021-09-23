using System;
namespace bullsandcows
{
    class BullsAndCows
    {
        private int MysteryNumber;
        struct PairOfCowsAndBulls
        {
            public int Cows, Bulls;
            public PairOfCowsAndBulls(int cows,int bulls)
            {
                Cows = cows;
                Bulls = bulls;
            }
        }
        public BullsAndCows()
        {
            Console.WriteLine("Привет, привет, давай играть в быки-коровы!");
            MysteryNumber = GenerateNumber();
        }
        private  int Find(byte[] arr, int n)
        {
            for (int i = 0; i < arr.Length; i++) if (arr[i] == n) return i;
            return -1;
        }
        private  byte[] Erase(byte[] arr,int n)
        {
            int s = Find(arr, n);
            if (s == -1) return arr;
            var arr1 = new byte[arr.Length - 1];
            for (int i = 0; i < arr1.Length; i++)
            {
                if (i < s) arr1[i] = arr[i];
                else arr1[i] = arr[i + 1];
            }
            return arr1;
        }
        private int GenerateNumber()
        {
            var rnd = new Random();
            var value = 0;
            var arr = new byte[] {0, 1, 2, 3, 4, 5, 6, 7, 8, 9};
            for (int i = 3; i >= 0; i--)
            {
                int t;
                t = rnd.Next(arr.Length);
                if (i == 3) t = rnd.Next(1, arr.Length);
                value += arr[t] * (Convert.ToInt32(Math.Pow(10, i)));
                arr = Erase(arr, arr[t]);
            }
            Console.WriteLine(value);
            return value;
        }
        private PairOfCowsAndBulls CountCowsAndBulls(int n)
        {
            var strMysteryNumber = MysteryNumber.ToString();
            var strn = n.ToString();
            int cows = 0, bulls = 0;
             for (int i = 3; i >= 0; i--)
             {
                 if (strMysteryNumber[i] == strn[i]) bulls++;
                 else foreach(var el in strn) if (el == strMysteryNumber[i]) { cows++; break; }
             }
             
             return new PairOfCowsAndBulls(cows, bulls);
        }
        private void PrintRules()
        {
            Console.WriteLine("Компьютер задумывает четыре различные цифры из 0,1,2,...9. Игрок делает ходы, чтобы узнать эти цифры и их порядок."+
                              "\nКаждый ход состоит из четырёх цифр, 0 не может стоять на первом месте."+ 
                              "\nВ ответ компьютер показывает число отгаданных цифр, стоящих на своих местах (число быков) и число отгаданных цифр, стоящих не на своих местах (число коров)."
                              );
        }
        public int PlayGame()
        {
            PrintRules();
            bool isVictory = false;
            do
            {
                int value;
                bool isCorrectNumber = false;
                Console.WriteLine("Введите e (eng), чтобы завершить работу программы (В любом предложенном поле ввода)");
                do
                {
                    Console.Write("Введите число: ");
                    bool isNumber = false;
                    string StrValue = Console.ReadLine();
                    if (StrValue.Length == 1 && StrValue[0] == 'e') return 0;
                    isNumber = Int32.TryParse(StrValue, out value);
                    if (!(isNumber) || value.ToString().Length != 4)
                    {
                        Console.Write(
                            "Введенная вами строка не может быть конвертирована в число либо длина этого числа не соотвествует " +
                            "4 (4-х значное число). Попробуйте ввести его снова или введите e (eng), чтобы завершить работу программы)"+
                            "\nВведите число: "
                        );
                        string t = Console.ReadLine();
                        if (t.Length == 1 && t[0] == 'e') return 0;
                        bool isNumber2 = Int32.TryParse(t, out value);
                        if(isNumber2==false)Console.WriteLine("Вы снова ввели неверную по формату строку, попробуйте снова!");
                        if (t.Length == 4 && (isNumber2)) isNumber = true;
                    }
                    else isCorrectNumber = true;
                    if (isNumber && value.ToString().Length == 4) isCorrectNumber = true;
                } while (!(isCorrectNumber));

                var countCowsAndBulls = CountCowsAndBulls(value);
                Console.WriteLine($"Кол-во Быков:{countCowsAndBulls.Bulls} Коров:{countCowsAndBulls.Cows}");
                if (countCowsAndBulls.Bulls == 4) {
                    isVictory = true;
                }
            } while (!(isVictory));
            Console.WriteLine($"Вы победили! Число: {MysteryNumber}");
            Console.WriteLine("Хотите ли вы продолжить игру?(y-да или любой другой символ, если нет)");
            string wantToContinue = Console.ReadLine();
            if (wantToContinue.Length == 1 && wantToContinue[0] == 'y'){ 
                RestartGame();
                return 1;
            }
            else { Console.WriteLine("Спасибо за игру!"); return 1; }
        }
        private void RestartGame()
        {
            System.Console.Clear();
            MysteryNumber = GenerateNumber();
            PlayGame();
        }
    }
    class Program
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            var bac = new BullsAndCows();
            bac.PlayGame();
        }
    }
}