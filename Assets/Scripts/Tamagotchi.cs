using System;
using UnityEngine;

public class Tamagotchi : MonoBehaviour {

    public Outside outside;

    public int Hunger { get; private set; }
    public  int Comfort { get; private set; }
    public int Boredom { get; private set; }
    public int Age { get; private set; }
    public int Weight { get; private set; }
    public int Discipline { get; private set; }
    public int Tiredness { get; private set; }
    public bool IsSleeping { get; private set; }
    public int Temperature { get; private set; }    

    public void Start()
    {

    }

    public void Update()
    {

    }

    private void AffectHunger(int affectHunger)
    {
        this.Hunger -= affectHunger;
    }

    private void AffectTiredness(int affectTiredness)
    {
        this.Tiredness -= affectTiredness;
    }

    private void AffectBoredom(int affectBoredom)
    {
        this.Boredom -= affectBoredom;
    }

    private void GettingOlder()
    {
        this.Age += 1;
    }

    private void TemperatureComfort(int outsideTemp)
    {
        var tempDiff = this.Temperature - outsideTemp;
        this.Comfort -= tempDiff;
    }
}
