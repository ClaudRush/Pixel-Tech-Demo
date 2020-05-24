using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatStarter2 : MonoBehaviour
{
    void Start()
    {
        Cat2 cat = new Cat2("Murzik", 3, 9, 1, 2);//Это называется - создать экзкмпляр
                                                  //Можно оставить параметры пустыми, тогда будет использоваться конструкор *Cat2()*
        cat.Meow();

        int result = Tools.Sum(3, 5);//пример работы статического метода
        Debug.Log(result);
    }
}
public class Cat2
{
    public string name;
    public int age;
    public float height;
    public float longe;
    public int Age => age;//Только метод get

    public float Wight { get; set; }//Методы get и set

    public Cat2(string name, int age, float wight, float height, float longe)//Используется по дефолту
    {
        this.name = name;
        this.height = height;
        this.longe = longe;
        this.age = age;
        Wight = wight;
    }

    public Cat2()
    {

    }
    public void Meow()
    {
        Debug.Log("Имя: " + name + ",возраст: " + Age + ",вес: " + Wight + ",рост: " + height + ",длинна: " + longe);
    }
}
public static class Tools
{
    public static int Sum(int x, int y)//static позволяет обращаться к методу через его класс
    {
        return x + y;
    }
}
