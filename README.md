<h1 align="center" height="128" backgroundcolor="ebbebe"><img src="https://i.pinimg.com/originals/12/ce/65/12ce65bc6c2b201d68c29822ecbd186c.gif" height="32"/>
Hi there, I'm Danila
<img src="https://i.pinimg.com/originals/12/ce/65/12ce65bc6c2b201d68c29822ecbd186c.gif" height="32"/></h1>
<h3 align="center">Sudent DVGUPS</h3>

This is my Game Wordle on C#


Метод генерации числа
```
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
```
Сама менюха: <br />
![alt text](image.png)