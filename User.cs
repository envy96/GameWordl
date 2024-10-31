using System.Security.Cryptography.X509Certificates;

class User{
    public string name,password;

    public User(string name, string password){
        this.name = name;
        this.password = password;
    }

    public static void registration(string filePath){
        
        string[] _users, _user, _userLogPass;
        string _log, _pass;
        bool _existUser = false;
        int _lineCount = 0;
        if (File.Exists(filePath))
            {
                _lineCount = File.ReadAllLines(filePath).Length;
                _users = File.ReadAllLines(filePath);
                Console.Write("Логин:");
                _log = Console.ReadLine();
                Console.Write("Пароль:");
                _pass = Console.ReadLine();
                
                for(int i = 0; i < _lineCount; i++){
                    _user =_users[i].Split('~');
                    _userLogPass = _user[0].Split('|');
                    if(_log == _userLogPass[0]){
                        _existUser = true;
                    }
                }
                if(_existUser){
                    Console.WriteLine("Пользователь с таким логином существует!");
                }else{
                    string _text = $"{_log}|{_pass}~";
                    File.AppendAllText(filePath, _text);
                    Console.WriteLine("Пользователь добавллен");
                    
                }
                
            }
    }
        public static void auth(string filePath, out User user){
            string[] _users, _user, _userLogPass;
            user = null;
            string _log, _pass;
            bool _existUser = false;
            int _lineCount = 0;
            System.Console.WriteLine("Здравствуйте пользователь, это авторизация!!!");
            if(File.Exists(filePath)){
                    _lineCount = File.ReadAllLines(filePath).Length;
                    _users = File.ReadAllLines(filePath);
                    Console.Write("Логин:");
                    _log = Console.ReadLine();
                    Console.Write("Пароль:");
                    _pass = Console.ReadLine();
                    for(int i = 0; i < _lineCount; i++){
                        _user = _users[i].Split('~');
                        _userLogPass = _user[0].Split('|');
                        if(_log == _userLogPass[0]){
                            _existUser = true;
                        }
                    }
                    if(_existUser){
                        Console.WriteLine("Пользователь Найден, добро пожаловть");
                        user = new User(_log, _pass);
                    }else{
                        Console.WriteLine("Пользователя с таким логином не существует");
                    }
            }
        }
    }
