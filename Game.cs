class Game{
    static User USER;
    public Game(User _user){
        USER = _user;
    }
    /// <summary>
    /// the method of searching for a user by username
    /// </summary>
    /// <param name="_filePath">file of History game</param>
    /// <returns>string history of game</returns>
    static string findUser(string _filePath){
        string[] _users, _user, _userLogPass;
        
        
        int _lineCount = 0;
        _lineCount = File.ReadAllLines(_filePath).Length;
        _users = File.ReadAllLines(_filePath); 
        for(int i = 0; i < _lineCount; i++){
            _user = _users[i].Split('~');
            _userLogPass = _user[0].Split('|');
            if(USER.Name == _userLogPass[0]){
                return _user[1];
            }
        }  

        return "";
    }
    /// <summary>
    /// The method for displaying the latest account result
    /// </summary>
    /// <param name="_filePath">file of History game</param>
    public static void LastGame(string _filePath)
    {
        try
        {
            string _text;
            string[] _history;
            int _lineCount = 0;
            if (File.Exists(_filePath))
            {
                
                string[] _games = findUser(_filePath).Split('/');
                string[] _lastGame = _games[_games.Length - 1].Split(':');
                _text = $"Последняя игра - угадывали число {_lastGame[2]}, число попыток угадать число - {_lastGame[1]}\n";
                Console.WriteLine(_text);
            }

        }
        catch (Exception ex)
        {
            Console.Write($"Ошибка при записи в файл: {ex.Message}");
        }
    }
    /// <summary>
    /// A method for displaying the best account result
    /// </summary>
    /// <param name="_filePath">file of History game</param>
    public static void BestRes(string _filePath)
    {
        try
        {
            string[] _history, _users, _user;
            int _minHod = 100000;
            string _text = "";
            if (File.Exists(_filePath))
            {

                
                _users = File.ReadAllLines(_filePath);

                _history = findUser(_filePath).Split('/');
                for (int i = 0; i < _history.Length; i++)
                {
                    string[] _game = _history[i].Split(':');
                    _minHod = Math.Min(_minHod, Convert.ToInt32(_game[1]));
                }
                for (int i = 0; i < _history.Length; i++)
                {
                    string[] _game = _history[i].Split(':');
                    if (Convert.ToInt32(_game[1]) == _minHod)
                    {
                        _text += $"Игра {_game[0]}: Кол-во ходов-{_game[1]}, Загадонное число {_game[2]}\n";
                    }
                }
                Console.Write(_text);

            }
            
            
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при записи в файл: {ex.Message}");
        }
    }
    /// <summary>
    /// The method of writing the results of the game to a file
    /// </summary>
    /// <param name="_number">your number</param>
    /// <param name="_filePath">file of History game</param>
    /// <param name="_botNumber">Bot's number</param>
    static void WriteToFile(int _number, string _filePath, string _botNumber)
    {
        try
        {
            string[] _history, _user, _userLogPass;
            string _newText;
            int _lineCount = 0;
            if (File.Exists(_filePath))
            {
                _lineCount = File.ReadAllLines(_filePath).Length;
                _history = File.ReadAllLines(_filePath);
                for(int i = 0; i < _lineCount; i++){
                    _user = _history[i].Split("~");
                    
                    _userLogPass = _user[0].Split("|");
                    if(USER.Name == _userLogPass[0] && _user[1] != ""){
                        
                        string _oldtext = _history[i];
                        Console.WriteLine('1');
                        string[] _games = _user[1].Split('/');
                        string[] _lastGame = _games[_games.Length-1].Split(':');
                        int _lastNumGame = Convert.ToInt32(_lastGame[0]);
                        _newText = _history[i] + $"/{_lastNumGame+1}:{_number}:{_botNumber}";
                        _history[i] = _history[i].Replace(_oldtext, _newText);

                        File.WriteAllLines(_filePath, _history);
                    }
                    else if(USER.Name == _userLogPass[0] && _user[1] == ""){
                        Console.WriteLine("2"); 
                        string oldtext = _history[i];
                        int _lastNumGame = 0;
                        _newText = _history[i] + $"{_lastNumGame+1}:{_number}:{_botNumber}";
                        _history[i] = _history[i].Replace(oldtext, _newText);

                        File.WriteAllLines(_filePath, _history);
                    }
                }
            }
            
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при записи в файл: {ex.Message}");
        }
    }
    /// <summary>
    /// The method of starting a number guessing game
    /// </summary>
    /// <param name="_filepath">file of History game</param>
    public static void GameWordle(string _filepath)
    {
        int _cnt = 0;
        int _numberToInt;
        string _botNumber = generateNumber();
        string _number = "";
        Console.WriteLine(_botNumber);
        while (true)
        {
            while(true){
                Console.Write('>');
                _number = Console.ReadLine();
                if(_number.Length == 4 && int.TryParse(_number,out _numberToInt)){
                    break;
                }else{
                    Console.WriteLine("Введено неправильное число");
                }
                
            }
             
            _cnt++;
            if (Game(_number, _botNumber))
            {
                Console.WriteLine("Поздравляю, вы угадали число!");
                WriteToFile(_cnt, _filepath, _botNumber.ToString());
                Console.WriteLine("Результат записан!");
                break;
            }
        }
        
        string generateNumber()
        {
            string _num ="";
            string _number = "";
            Random _rnd = new Random();
            while(_number.Length < 4){
                _num = Convert.ToString(_rnd.Next(0,10));
                if(!_number.Contains(_num)){
                    _number += _num;
                }
            }
            
            
            return _number;
        }
        bool Game(string _userNumber, string _botNumber)
        {
            int _polSovp = 0;
            int _chastSovp = 0;
            char[] _usersChisla = _userNumber.ToArray();
            char[] _botChisla = _botNumber.ToArray();


            for (int i = 0; i < _botChisla.Length; i++)
            {

                if (_botChisla[i] == _usersChisla[i])
                {

                    _polSovp++;
                }
                else if (_botChisla.Contains(_usersChisla[i]))
                {
                    _chastSovp++;
                }

            }
            Console.WriteLine($"Чисел совпало по позициям - {_polSovp}\n" +
                $"ЧИсла совпали, но не на своей позиции {_chastSovp}");
            if (_polSovp == 4)
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