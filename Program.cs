
using System.Runtime.InteropServices;
using static System.Runtime.InteropServices.JavaScript.JSType;

class Program
{
    static User USER;
    static string FILEPATH = "История.txt";
    static void Main(string[] args)
    {
        
        System.Console.WriteLine("1)Авторизация\n" + 
        "2)Регистрация");
        Console.Write(">");
        string command = Console.ReadLine();
        while (command != "6")
        {
            
            switch (command)
            {
                case "1":
                    User.auth(FILEPATH, out USER);
                    menu();
                    break;
                case "2":
                    User.registration(FILEPATH);
                    break;
                default:
                    Console.WriteLine("Введите корректную команду");
                    break;
            }
            Console.Write(">");
            command = Console.ReadLine();
        }
    }
    static void menu(){
        
        Console.WriteLine("Игра с угадыванием числа\n" +
            "Выберите пункт меню:\n" +
            "1)Поменять пользователя\n" +
            "2)Регистрация Нового пользователя\n" +
            "3)Игра\n" +
            "4)Вывести лучший результат\n" +
            "5)Последний матч\n" +
            "6)Выход");
        Console.Write(">");
        string command = Console.ReadLine();
        while (command != "6")
        {
            
            switch (command)
            {
                case "1":
                    User.auth(FILEPATH, out USER);
                    break;
                case "2":
                    User.registration(FILEPATH);
                    break;
                case "3":
                    GameWordle(FILEPATH);
                    break;
                case "4":
                    BestRes(FILEPATH);
                    break;
                case "5":
                    LastGame(FILEPATH);
                    break;
                default:
                    Console.WriteLine("Введите корректную команду");
                    break;
            }
            Console.Write(">");
            command = Console.ReadLine();
        }
    }
    

    
    static string findUser(string filePath){
        string[] users, user, userLogPass;
        
        
        int lineCount = 0;
        lineCount = File.ReadAllLines(filePath).Length;
        users = File.ReadAllLines(filePath); 
        for(int i = 0; i < lineCount; i++){
            user = users[i].Split('~');
            userLogPass = user[0].Split('|');
            if(USER.name == userLogPass[0]){
                return user[1];
            }
        }  

        return "";
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
                
                string[] Games = findUser(filePath).Split('/');
                string[] lastGame = Games[Games.Length - 1].Split(':');
                text = $"Последняя игра - угадывали число {lastGame[2]}, число попыток угадать число - {lastGame[1]}\n";
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
            string[] history, users, user;
            int minHod = 100000;
            string text = "";
            if (File.Exists(filePath))
            {

                
                users = File.ReadAllLines(filePath);

                history = findUser(filePath).Split('/');
                for (int i = 0; i < history.Length; i++)
                {
                    string[] game = history[i].Split(':');
                    minHod = Math.Min(minHod, Convert.ToInt32(game[1]));
                }
                for (int i = 0; i < history.Length; i++)
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
            string newText;
            int lineCount = 0;
            if (File.Exists(filePath))
            {
                lineCount = File.ReadAllLines(filePath).Length;
                history = File.ReadAllLines(filePath);
                for(int i = 0; i < lineCount; i++){
                    user = history[i].Split("~");
                    
                    userLogPass = user[0].Split("|");
                    if(USER.name == userLogPass[0] && user[1] != ""){
                        
                        string oldtext = history[i];
                        Console.WriteLine('1');
                        string[] Games = user[1].Split('/');
                        string[] LastGame = Games[Games.Length-1].Split(':');
                        int lastNumGame = Convert.ToInt32(LastGame[0]);
                        newText = history[i] + $"/{lastNumGame+1}:{number}:{botNumber}";
                        history[i] = history[i].Replace(oldtext, newText);

                        File.WriteAllLines(filePath, history);
                    }
                    else if(USER.name == userLogPass[0] && user[1] == ""){
                        Console.WriteLine("2"); 
                        string oldtext = history[i];
                        int lastNumGame = 0;
                        newText = history[i] + $"{lastNumGame+1}:{number}:{botNumber}";
                        history[i] = history[i].Replace(oldtext, newText);

                        File.WriteAllLines(filePath, history);
                    }
                }
                
                
                
            }
            
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при записи в файл: {ex.Message}");
        }
    }
    static void GameWordle(string filepath)
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
                WriteToFile(cnt, filepath, botNumber.ToString());
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