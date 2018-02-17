using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
	LivesManager lm = null;
    public GameObject[] hazards;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    public GUIText scoreText;
    public GUIText restartText;
    public GUIText gameOverText;

    private bool gameOver;
    private bool restart;
    private int score;

    void Start()
    {
        gameOver = false;
        restart = false;
        gameOverText.text = "";
	    restartText.text = "";
        score = 0;
        UpdateScore();
        StartCoroutine(SpawnWaves());
						// Get LivesManager object from the scene
		GameObject gameObject = GameObject.Find ("LivesManager");
		// If LivesManager object exist
		if (gameObject != null) {
			// Get LivesManager component
			lm = gameObject.GetComponent<LivesManager> ();
					// Display configuration values in scene. (only for demo)
		} else {
			Debug.Log("ERROR: LivesManager prefab not found!");
		}
	}
    void Update()
    {
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Application.LoadLevel(Application.loadedLevel);
            }
        }
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                GameObject hazard = hazards[Random.Range(0, hazards.Length)];
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);

			if (gameOver)
			{
				restart = true;
				break;
			}
        }
    }
    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }
	public void GameOver ()
	{
		gameOverText.text = "Press 'R' for Restart";
		gameOver = true;
	}
	public void canPlay()
	{
		if (lm) {
			if (lm.canPlay ())
				gameOver = false;
			}else{
				int previousLevel = PlayerPrefs.GetInt( "previousLevel" );
				Application.LoadLevel( previousLevel );
				}
	}
}
