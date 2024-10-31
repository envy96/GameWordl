
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
        string _command = Console.ReadLine();
        while (_command != "6")
        {
            
            switch (_command)
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
            _command = Console.ReadLine();
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
        Game game = new Game(USER);
        string _command = Console.ReadLine();
        while (_command != "6")
        {
            
            switch (_command)
            {
                case "1":
                    User.auth(FILEPATH, out USER);
                    break;
                case "2":
                    User.registration(FILEPATH);
                    break;
                case "3":
                    Game.GameWordle(FILEPATH);
                    break;
                case "4":
                    Game.BestRes(FILEPATH);
                    break;
                case "5":
                    Game.LastGame(FILEPATH);
                    break;
                default:
                    Console.WriteLine("Введите корректную команду");
                    break;
            }
            Console.Write(">");
            _command = Console.ReadLine();
        }
    }
    

    
    
}