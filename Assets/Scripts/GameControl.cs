using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour 
{
    [System.NonSerialized]
    public static int EnvironmentLayer;

    [System.NonSerialized]
    public static int SleighLayer;

    [System.NonSerialized]
    public static int DebrisLayer;

    public static GameControl instance;

    public SantaComponent Santa;

	public bool gameOver = false;
	public float scrollSpeed = -1.5f;

	void Awake()
	{
		//If we don't currently have a game control...
		if (instance == null)
			//...set this one to be it...
			instance = this;
		//...otherwise...
		else if(instance != this)
			//...destroy this one because it is a duplicate.
			Destroy (gameObject);

        this.Santa = Object.FindObjectOfType<SantaComponent>();

        EnvironmentLayer = LayerMask.NameToLayer("Environment");
        SleighLayer = LayerMask.NameToLayer("Sleigh");
        DebrisLayer = LayerMask.NameToLayer("Debris");
    }

	public void Update()
	{
		if (gameOver && Input.GetMouseButtonDown(0)) 
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
	}

	public void SantaDied()
	{
		//Set the game to be over.
		gameOver = true;
	}
}
