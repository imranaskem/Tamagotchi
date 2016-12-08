using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour {

    private GameObject[] PlayerControls;
    private GameObject TextToPlayer;    
    private Tamagotchi Tama;
    private string LossText = "You lost!\r\n";
    private string WinText = "You won!\r\n";

    public bool IsDead { get; private set; }
    
    // Use this for initialization
	void Start ()
    {
        this.PlayerControls = GameObject.FindGameObjectsWithTag("PlayerControls");
        this.Tama = GameObject.FindObjectOfType<Tamagotchi>();
        this.TextToPlayer = GameObject.Find("TextToPlayer");
        this.IsDead = true;   

        this.TextToPlayer.SetActive(true);        
        this.setPlayerControlVisibility(false);
        this.Tama.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (this.IsDead && Input.GetMouseButtonDown(0))
        {
            this.TextToPlayer.SetActive(false);
            this.setPlayerControlVisibility(true);
            this.Tama.gameObject.SetActive(true);
            this.Tama.GameReset();
            this.IsDead = false;
        }
	}

    public void GameOver(int condition, string message)
    {
        if (condition == 0)
        {
            this.setTextToPlayer(this.LossText + message);
            this.TextToPlayer.SetActive(true);
            this.setPlayerControlVisibility(false);
            this.Tama.gameObject.SetActive(false);
        }

        if (condition == 1)
        {
            this.setTextToPlayer(this.WinText + message);
            this.TextToPlayer.SetActive(true);
            this.setPlayerControlVisibility(false);
            this.Tama.gameObject.SetActive(false);
        }

        this.IsDead = true;
    }

    private void setPlayerControlVisibility(bool activity)
    {
        foreach (var item in this.PlayerControls)
        {
            item.SetActive(activity);
        }
    }

    private void setTextToPlayer(string message)
    {
        this.TextToPlayer.GetComponent<Text>().text = message;
    }
}
