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
	public float ScrollSpeed = -1.5f;

	void Awake()
	{
		if (GameControl.instance == null)
        {
            GameControl.instance = this;
        }
		else if (GameControl.instance != this)
        {
            Object.Destroy(this.gameObject);
        }

        this.Santa = Object.FindObjectOfType<SantaComponent>();

        GameControl.EnvironmentLayer = LayerMask.NameToLayer("Environment");
        GameControl.SleighLayer = LayerMask.NameToLayer("Sleigh");
        GameControl.DebrisLayer = LayerMask.NameToLayer("Debris");
    }

	public void Update()
	{
		if (this.gameOver && Input.GetMouseButtonDown(0)) 
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
	}

	public void SantaDied()
	{
		//Set the game to be over.
		this.gameOver = true;
	}
}
