using UnityEngine;

public class Outside : MonoBehaviour {

    public double CurrentTemp { get; private set; }

    // Use this for initialization
	void Start ()
    {
        InvokeRepeating("GetWeather", 0, 60f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private void GetWeather()
    {     

        
    }
}
