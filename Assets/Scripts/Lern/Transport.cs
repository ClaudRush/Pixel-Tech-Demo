using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transport : MonoBehaviour
{
    void Start()
    {
        //Тут обращаемся к абстрактным классам

        Vehicle vehicle1 = new Car();
        Vehicle vehicle2 = new Bus();
        Vehicle vehicle3 = new Tractor();

        vehicle1.Beep();
        vehicle2.Beep();
        vehicle3.Beep();

        //Тут обращаемся к интерфесу у которого есть наследуемый класс

        /*
        IVehicle vehicle1 = new Car();
        IVehicle vehicle2 = new Bus();
        IVehicle vehicle3 = new Tractor();

        vehicle1.Sound();
        vehicle1.Sound();
        vehicle1.Sound();
        */
    }


}
public abstract class Vehicle : IVehicle
{
    protected string name;//protected делает поле видимым только в наследуемых классах
    private string seName;
    public virtual void Beep() => Debug.Log("Звук");
    public abstract void Sound();
}
public class Car : Vehicle
{
    public override void Beep()
    {
        //seName = "Gorochovskiy";//нельзя установить из-за уровня защиты(private)
        name = "Boboic";//protected name
        Debug.Log("БИ "+ name);
    }
    public override void Sound()
    {
        Debug.Log("Я автомобиль!");
    }
}
public class Bus : Vehicle
{
    public override void Beep()
    {
        Debug.Log("БУП");
    }
    public override void Sound()
    {
        Debug.Log("Я автобус!");
    }
}
public class Tractor : Vehicle
{
    public override void Beep()
    {
        Debug.Log("БАПИ");
    }

    public override void Sound()
    {
        Debug.Log("Я трактор!");
    }
}

interface IVehicle
{
    void Beep();
    void Sound();
}
