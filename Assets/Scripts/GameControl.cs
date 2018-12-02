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

    public Text RestartText;

    public static GameControl instance;

    [System.NonSerialized]
    public SantaComponent Santa;

    [System.NonSerialized]
    public FrostComponent Frost;

	public bool gameOver = false;
	public float ScrollSpeed = -1.5f;

    private Color ColorRed;
    private Color ColorBlue;

	public void Awake()
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

        this.ColorRed = Color.red;
        this.ColorBlue = Color.blue;
        ColorUtility.TryParseHtmlString("#D60202", out this.ColorRed);
        ColorUtility.TryParseHtmlString("#184281", out this.ColorBlue);

        GameControl.GroundLayer = LayerMask.NameToLayer("Ground");
        GameControl.EnvironmentLayer = LayerMask.NameToLayer("Environment");
        GameControl.SleighLayer = LayerMask.NameToLayer("Sleigh");
        GameControl.DebrisLayer = LayerMask.NameToLayer("Debris");
        GameControl.IgnoreObstaclesLayer = LayerMask.NameToLayer("Ignore Obstacles");

        GameControl.Player1Santa = true;

        this.UpdateScoreTexts();
        this.UpdateStartText();
    }

    public void Update()
    {
        if (true || this.gameOver)
        {
            float scale = 1 + 0.1f * Mathf.Sin(4 * Time.time);
            this.RestartText.transform.localScale = new Vector3(scale, scale, scale);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Object.Destroy(collision.gameObject);
    }

    public void ReturnToMenu()
    {
        GameControl.Player1Santa = true;
        GameControl.Player1Score = 0;
        GameControl.Player2Score = 0;
        SceneManager.LoadScene(0);
    }

    public void RestartRound()
    {
        if (this.gameOver)
        {
            this.gameOver = false;
            this.SwitchPlayers();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            this.UpdateScoreTexts();
            this.RestartText.enabled = false;
            this.UpdateStartText();
        }
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
        this.RestartText.enabled = true;
    }

    public void SwitchPlayers()
    {
        GameControl.Player1Santa = !GameControl.Player1Santa;
        this.UpdateScoreTexts();
    }

    private void UpdateStartText()
    {
        string text = "Player ";
        text += GameControl.Player1Santa ? "2" : "1";
        text += "\nPress A";

        this.RestartText.text = text;
    }

    public void UpdateScoreTexts()
    {
        if (GameControl.Player1Santa)
        {

            this.Player1ScoreText.color = this.ColorRed;
            this.Player2ScoreText.color = this.ColorBlue;
        }
        else
        {
            this.Player1ScoreText.color = this.ColorBlue;
            this.Player2ScoreText.color = this.ColorRed;
        }

        this.Player1ScoreText.text = "Player 1: " + GameControl.Player1Score;
        this.Player2ScoreText.text = "Player 2: " + GameControl.Player2Score;
    }
}
