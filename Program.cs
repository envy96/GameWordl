
using static System.Runtime.InteropServices.JavaScript.JSType;

class Program
{
    static string LOGIN, PASSWORD;
    static void Main(string[] args)
    {
        
        string filePath = "История.txt";
        Console.WriteLine("Игра с угадыванием числа\n" +
            "Выберите пункт меню:\n" +
            "1)Авторизация\n" +
            "2)Регистрация\n" +
            "3)Игра\n" +
            "4)Вывести лучший результат\n" +
            "5)Последний матч\n" +
            "6)Выход");
        Console.Write(">");
        string command = Console.ReadLine();
        while (command != "4")
        {
            
            switch (command)
            {
                case "1":
                    auth(filePath);
                    break;
                case "2":
                    registration(filePath);
                    break;
                case "3":
                    GameWordle();
                    break;
                case "4":
                    BestRes(filePath);
                    break;
                case "5":
                    LastGame(filePath);
                    break;
                default:
                    Console.WriteLine("Введите корректную команду");
                    break;
            }
            Console.Write(">");
            command = Console.ReadLine();
        }
        
        
    }
    static void registration(string filePath){
        
        string[] users, user, userLogPass;
        string log, pass;
        bool existUser = false;
        int lineCount = 0;
        if (File.Exists(filePath))
            {
                lineCount = File.ReadAllLines(filePath).Length;
                users = File.ReadAllLines(filePath);
                Console.Write("Логин:");
                log = Console.ReadLine();
                Console.Write("Пароль:");
                pass = Console.ReadLine();
                for(int i = 0; i < lineCount; i++){
                    user = users[i].Split('~');
                    userLogPass = user[0].Split('|');
                    if(log == userLogPass[0]){
                        existUser = true;
                    }
                }
                if(existUser){
                    Console.WriteLine("Пользователь с таким логином существует!");
                }else{
                    string text = $"\n{log}|{pass}~";
                    File.AppendAllText(filePath, text);
                    Console.WriteLine("Пользователь добавллен");
                }
                
            }
    }
    static void auth(string filePath){
        string[] users, user, userLogPass;
        string log, pass;
        bool existUser = false;
        int lineCount = 0;
        
        if(File.Exists(filePath)){
                lineCount = File.ReadAllLines(filePath).Length;
                users = File.ReadAllLines(filePath);
                Console.Write("Логин:");
                log = Console.ReadLine();
                Console.Write("Пароль:");
                pass = Console.ReadLine();
                for(int i = 0; i < lineCount; i++){
                    user = users[i].Split('~');
                    userLogPass = user[0].Split('|');
                    if(log == userLogPass[0]){
                        existUser = true;
                    }
                }
                if(existUser){
                    Console.WriteLine("Пользователь Найден");
                    LOGIN = log;
                    PASSWORD = pass;
                }else{
                    Console.WriteLine("Пользователя с таким логином не существует");
                }
        }
    }
    static void LastGame(string filePath)
    {
        try
        {
            string text;
            string[] history;
            int lineCount = 0;
            if (File.Exists(filePath))
            {
                lineCount = File.ReadAllLines(filePath).Length;
                history = File.ReadAllLines(filePath);
                string[] game = history[lineCount-1].Split(':');
                text = $"Последняя игра - угадывали число {game[2]}, число попыток угадать число - {game[1]}\n";
                Console.WriteLine(text);
            }

        }
        catch (Exception ex)
        {
            Console.Write($"Ошибка при записи в файл: {ex.Message}");
        }
    }
    static void BestRes(string filePath)
    {
        try
        {
            int lineCount = 0;
            string[] history;
            int minHod = 100000;
            string text = "";
            if (File.Exists(filePath))
            {

                lineCount = File.ReadAllLines(filePath).Length;
                history = File.ReadAllLines(filePath);

                for (int i = 0; i < lineCount; i++)
                {
                    string[] game = history[i].Split(':');
                    minHod = Math.Min(minHod, Convert.ToInt32(game[1]));
                }
                for (int i = 0; i < lineCount; i++)
                {
                    string[] game = history[i].Split(':');
                    if (Convert.ToInt32(game[1]) == minHod)
                    {
                        text += $"Игра {game[0]}: Кол-во ходов-{game[1]}, Загадонное число {game[2]}\n";
                    }
                }
                Console.Write(text);

            }
            
            
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при записи в файл: {ex.Message}");
        }
    }
    /// <summary>
    /// My method
    /// </summary>
    /// <param name="number">param 1</param>
    /// <param name="filePath">param 2</param>
    /// <param name="botNumber">param 3</param>
    static void WriteToFile(int number, string filePath, string botNumber)
    {
        try
        {
            string[] history, user, userLogPass;
            int lineCount = 0;
            if (File.Exists(filePath))
            {
                lineCount = File.ReadAllLines(filePath).Length;
                history = File.ReadAllLines(filePath);
                for(int i = 0; i < lineCount; i++){
                    user = history[i].Split("~");
                    userLogPass = user[0].Split("|");
                    if(LOGIN == userLogPass[0]){
                        
                    }
                }
                int Lastgame = Convert.ToInt32(history[lineCount-1].Split(':')[0]);
                string text = $"{Lastgame+1}:{number}:{botNumber}\n";
                File.AppendAllText(filePath, text);
            }
            
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при записи в файл: {ex.Message}");
        }
    }
    static void GameWordle()
    {
        int cnt = 0;
        int a;
        string botNumber = generateNumber();
        string number = "";
        Console.WriteLine(botNumber);
        while (true)
        {
            while(true){
                Console.Write('>');
                number = Console.ReadLine();
                if(number.Length == 4 && int.TryParse(number,out a)){
                    break;
                }else{
                    Console.WriteLine("Введено неправильное число");
                }
                
            }
             
            cnt++;
            if (Game(number, botNumber))
            {
                Console.WriteLine("Поздравляю, вы угадали число!");
                WriteToFile(cnt, "История.txt", botNumber.ToString());
                Console.WriteLine("Результат записан!");
                break;
            }
        }
        static string generateNumber()
        {
            string num ="";
            string number = "";
            Random rnd = new Random();
            while(number.Length < 4){
                num = Convert.ToString(rnd.Next(0,10));
                if(!number.Contains(num)){
                    number += num;
                }
            }
            
            
            return number;
        }

        static bool Game(string userNumber, string botNumber)
        {
            int polSovp = 0;
            int chastSovp = 0;
            char[] userChisla = userNumber.ToArray();
            char[] botChisla = botNumber.ToArray();


            for (int i = 0; i < botChisla.Length; i++)
            {

                if (botChisla[i] == userChisla[i])
                {

                    polSovp++;
                }
                else if (botChisla.Contains(userChisla[i]))
                {
                    chastSovp++;
                }

            }
            Console.WriteLine($"Чисел совпало по позициям - {polSovp}\n" +
                $"ЧИсла совпали, но не на своей позиции {chastSovp}");
            if (polSovp == 4)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}