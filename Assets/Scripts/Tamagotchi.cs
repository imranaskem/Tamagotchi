using UnityEngine;
using System;

public class Tamagotchi : MonoBehaviour {

    private SpriteRenderer sprite;
    private Animator anim;
    private Transform form;
    private GameController controller;
    private readonly DateTime _start = DateTime.Now;    
    private readonly float threshold = 50f;    

    public float Hunger { get; private set; }    
    public float Boredom { get; private set; }
    public float Weight { get; private set; }        

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

    public float HungerMultiplier
    {
        get
        {
            return this.Hunger / this.threshold;
        }
    }

    public float WeightMultiplier
    {
        get
        {
            var value = this.Weight / this.threshold;

            if (value < 0.5f) return 0.5f;

            if (value > 2.5f) return 2.5f;

            return value;
        }
    }

    // Use this for initialization
    void Start ()
    {                          
        this.sprite = gameObject.GetComponent<SpriteRenderer>();
        this.anim = gameObject.GetComponent<Animator>();
        this.form = gameObject.GetComponent<Transform>();
        this.controller = GameObject.FindObjectOfType<GameController>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(!this.controller.IsDead)
        {
            this.runDisplays();
            this.living();
            this.winConditions();
        }

        Debug.Log("Hunger " + this.Hunger);
        Debug.Log("Boredom " + this.Boredom);
        Debug.Log("Weight " + this.Weight);             
	}

    public void FeedPet()
    {
        this.affectHunger(-20f);
        this.affectWeight(-20f);
        this.affectBoredom(10f);
    }

    public void PlayPet()
    {
        this.affectBoredom(-20f);
        this.affectWeight(10f);
        this.affectHunger(10f);
    }

    public void GameReset()
    {
        this.Hunger = 55f;
        this.Boredom = 55f;
        this.Weight = 55f;        
    }   

    private void living()
    {
        this.affectHunger(Time.deltaTime);

        this.affectWeight(-Time.deltaTime);

        this.affectBoredom(Time.deltaTime);
    }

    private void affectHunger(float affectHunger)
    {
        this.Hunger -= affectHunger;
    }

    private void affectWeight(float affectWeight)
    {
        this.Weight -= affectWeight;
    }

    private void affectBoredom(float affectBoredom)
    {
        this.Boredom -= affectBoredom;
    }  
    
    private void runDisplays()
    {
        this.hungerDisplay();
        this.weightDisplay();
        this.boredomDisplay();
    }  

    private void hungerDisplay()
    {
        if (this.Hunger < this.threshold)
        {         
            var color = this.sprite.color;
            color.g = (this.HungerMultiplier);
            color.b = (this.HungerMultiplier);
            this.sprite.color = color;
        }
        else
        {
            this.sprite.color = Color.white;
        }        
    }    

    private void weightDisplay()
    {
        var newScale = new Vector3(this.WeightMultiplier, this.WeightMultiplier, 1f);
        this.form.localScale = newScale;
    }

    private void boredomDisplay()
    {
        this.anim.SetFloat("BoredomMultiplier", this.BoredomMultiplier);
    }

    private void winConditions()
    {
        this.isItDead();
        this.didItWin();
    }

    private void isItDead()
    {
        if (this.Hunger <= 0)
        {            
            this.controller.GameOver(0, "It died of hunger!");
        }

        if (this.Hunger >= 100)
        {         
            this.controller.GameOver(0, "It died of too much food!");
        }

        if (this.Weight <= 0)
        {         
            this.controller.GameOver(0, "It wasted away!");
        }

        if (this.Weight >= 100)
        {         
            this.controller.GameOver(0, "It got too fat!");
        }

        if (this.Boredom <= 0)
        {         
            this.controller.GameOver(0, "It was bored too death!");
        }

        if (this.Boredom >= 100)
        {         
            this.controller.GameOver(0, "It got too excited and exploded!");
        }   
    }

    private void didItWin()
    {
        if (this.Age == 6)
        {            
            this.controller.GameOver(1, "You got to 6!");
        }
    }
}
