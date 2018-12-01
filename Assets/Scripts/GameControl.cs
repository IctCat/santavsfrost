using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using XInputDotNetPure;

public class GameControl : MonoBehaviour 
{
    [System.NonSerialized]
    public static int GroundLayer;

    [System.NonSerialized]
    public static int EnvironmentLayer;

    [System.NonSerialized]
    public static int SleighLayer;

    [System.NonSerialized]
    public static int DebrisLayer;

    [System.NonSerialized]
    public static int IgnoreObstaclesLayer;

    public static int Player1Score;
    public static int Player2Score;

    public static bool Player1Santa;

    public Text Player1ScoreText;
    public Text Player2ScoreText;

    public static GameControl instance;

    [System.NonSerialized]
    public SantaComponent Santa;

    [System.NonSerialized]
    public FrostComponent Frost;

	public bool gameOver = false;
	public float ScrollSpeed = -1.5f;

	void Awake()
	{
        if (GameControl.instance == null)
        {
            GameControl.instance = this;
            Object.DontDestroyOnLoad(this.gameObject);
        }
		else if (GameControl.instance != this)
        {
            Object.Destroy(this.gameObject);
            return;
        }

        GameControl.GroundLayer = LayerMask.NameToLayer("Ground");
        GameControl.EnvironmentLayer = LayerMask.NameToLayer("Environment");
        GameControl.SleighLayer = LayerMask.NameToLayer("Sleigh");
        GameControl.DebrisLayer = LayerMask.NameToLayer("Debris");
        GameControl.IgnoreObstaclesLayer = LayerMask.NameToLayer("Ignore Obstacles");

        GameControl.Player1Santa = true;

        this.InitializeRound();
    }

	public void Update()
	{
		if (this.gameOver && Input.GetMouseButtonDown(0)) 
		{
            this.SwitchPlayers();
            this.gameOver = false;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            this.InitializeRound();
        }
	}

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Object.Destroy(collision.gameObject);
    }

    public void InitializeRound()
    {
        this.Santa = Object.FindObjectOfType<SantaComponent>();
        this.Frost = Object.FindObjectOfType<FrostComponent>();
        this.UpdateScoreTexts();

        Debug.Log(this.Santa);
    }

    public void AddSantaScore()
    {
        if (GameControl.Player1Santa)
        {
            GameControl.Player1Score++;
        }
        else
        {
            GameControl.Player2Score++;
        }

        this.UpdateScoreTexts();
    }

    public void SantaDied()
	{
		//Set the game to be over.
		this.gameOver = true;
	}

    public void SwitchPlayers()
    {
        GameControl.Player1Santa = !GameControl.Player1Santa;
        this.UpdateScoreTexts();
    }

    public void UpdateScoreTexts()
    {
        if (GameControl.Player1Santa)
        {
            this.Player1ScoreText.color = Color.red;
            this.Player2ScoreText.color = Color.blue;
        }
        else
        {
            this.Player1ScoreText.color = Color.blue;
            this.Player2ScoreText.color = Color.red;
        }

        this.Player1ScoreText.text = "Player 1: " + GameControl.Player1Score;
        this.Player2ScoreText.text = "Player 2: " + GameControl.Player2Score;
    }
}
