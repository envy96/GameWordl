<h1 align="center">Hi there, I'm Danila
<img src="https://github.com/blackcater/blackcater/raw/main/images/Hi.gif" height="32"/></h1>
<h3 align="center">Computer science student, IT news writer from Russia 🇷🇺</h3>

This is my Game Wordle on C#


Метод генерации числа
static string generateNumber(){
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
Сама менюха: <br />
![alt text](image.png)