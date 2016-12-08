using UnityEngine;
using System;

public class Tamagotchi : MonoBehaviour {

    private SpriteRenderer sprite;
    private Animator anim;
    private readonly DateTime _start = DateTime.Now;
    private readonly float changeFactor = 1f;
    private readonly float threshold = 30f;    

    public float Hunger { get; private set; }    
    public float Boredom { get; private set; }
    public float Weight { get; private set; }    
    public float Tiredness { get; private set; }    
    public bool IsSleeping { get; private set; }
    public bool IsDead { get; private set; }

    public int Age
    {
        get
        {
            var timeSpent = DateTime.Now - this._start;
            return timeSpent.Minutes;
        }
    }   

    public float BoredomMultiplier
    {
        get
        {
            var value = this.threshold / this.Boredom;

            if (value < 1f) return 1f;            

            if (value > 8f) return 8f;
            
            return value;
        }
    }

    // Use this for initialization
    void Start ()
    {
        this.Hunger = 31f;        
        this.Boredom = 32f;        
        this.Weight = 10f;        
        this.Tiredness = 50f;
        this.IsSleeping = false;
        this.IsDead = false;        
        this.sprite = gameObject.GetComponent<SpriteRenderer>();
        this.anim = gameObject.GetComponent<Animator>();

        InvokeRepeating("living", 0, 1f);        
	}
	
	// Update is called once per frame
	void Update ()
    {        
        this.controlColor();
        this.isItDead();

        Debug.Log("Hunger " + this.Hunger);
        Debug.Log("Boredom " + this.Boredom);

        this.anim.SetFloat("BoredomMultiplier", this.BoredomMultiplier);        
	}

    public void FeedPet()
    {
        this.affectHunger(-20f);
    }

    public void PlayPet()
    {
        this.affectBoredom(-20f);
    }

    private void living()
    {
        this.affectHunger(this.changeFactor);

        this.affectTiredness(this.changeFactor);

        this.affectBoredom(this.changeFactor);
    }

    private void affectHunger(float affectHunger)
    {
        this.Hunger -= affectHunger;
    }

    private void affectTiredness(float affectTiredness)
    {
        this.Tiredness -= affectTiredness;
    }

    private void affectBoredom(float affectBoredom)
    {
        this.Boredom -= affectBoredom;
    }    

    private void controlColor()
    {
        if(this.Hunger < this.threshold)
        {         
            var color = this.sprite.color;
            color.g = (this.Hunger / this.threshold);
            color.b = (this.Hunger / this.threshold);
            this.sprite.color = color;
        }
        else
        {
            this.sprite.color = Color.white;
        }        
    }

    private void controlMotion()
    {                
        this.sprite.flipX = !this.sprite.flipX;        
    }

    private void isItDead()
    {
        if (this.Hunger <= 0) this.IsDead = true;
    }
}
