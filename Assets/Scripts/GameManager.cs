using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
	public static GameManager gm;
	public enum GameStates {Play, Win};
	public GameStates gameState;
	public GameObject winCanvas;
	public GameObject mainCanvas;
    public GameObject overCanvas;
	public Text lifeDisplay;
	public int total;
	public string nextLevel;
	public int balls;
	public string clip;

	int lifes;
	int bricks;
    int level;
	bool paused;
    AudioSource audioSource;

    void Start () {
		lifes = PlayerPrefsManager.GetLifes ();
        level = SceneManager.GetActiveScene().buildIndex;
        PlayerPrefsManager.SetLastLevel (level);
		if (gm == null)
			gm = this;
		bricks = 0;
		paused = false;
		lifeDisplay.text = lifes.ToString ();
		audioSource = GetComponent<AudioSource> ();
		audioSource.clip = (AudioClip) Resources.Load (clip);
        audioSource.loop = true;
		audioSource.Play ();
	}
	
	// Update is called once per frame
	void Update () {
		if (bricks == total) {
			Victory ();
		}
	}

	public void LoadNext()
	{
		PlayerPrefsManager.SetLifes (lifes);
		SceneManager.LoadScene(nextLevel);
	}

	public bool LoseLife()
	{
		if (balls == 1) {
			if (lifes > 0) {
				lifes--;
				lifeDisplay.text = lifes.ToString ();
			} else {
				PlayerPrefsManager.ResetLife ();
                overCanvas.SetActive(true);
                mainCanvas.SetActive(false);
                gameState = GameStates.Win;
			}
			return true;
		} else {
			balls--;
			return false;
		}
	}

	void Victory()
	{
		gameState = GameStates.Win;
		winCanvas.SetActive (true);
		mainCanvas.SetActive (false);
        PlayerPrefsManager.SetUnlockedLevel(level);
	}

	public void CollectBrick()
	{
		bricks++;
	}

	public void PauseButton()
	{
		if (paused) {
			Time.timeScale = 1.0f;
			paused = false;
            audioSource.UnPause();
		} else {
			Time.timeScale = 0.0f;
			paused = true;
            audioSource.Pause();
        }
    }

	public void ExitGame()
	{
		PlayerPrefsManager.ResetLife ();
		SceneManager.LoadScene ("Menu");
	}

    public void RetryButton()
    {
        SceneManager.LoadScene(level);
    }

	public void ResetPrefs()
	{
		PlayerPrefsManager.ResetLastLevel ();
	}
}
