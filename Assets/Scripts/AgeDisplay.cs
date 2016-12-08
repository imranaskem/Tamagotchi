using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AgeDisplay : MonoBehaviour {

    private Tamagotchi tamagotchi;
    private Text text;
    
    // Use this for initialization
	void Start ()
    {
        this.tamagotchi = GameObject.FindObjectOfType<Tamagotchi>();
        this.text = gameObject.GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        this.text.text = this.displayAge();
	}

    private string displayAge()
    {
        var returnString = "Age: " + this.tamagotchi.Age.ToString();       

        return returnString;
    }
}
