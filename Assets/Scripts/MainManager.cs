using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;

    // text that hold the high score
    public Text HighScoreText;

    public GameObject GameOverText;
    
    private bool m_Started = false;
    private int m_Points;
    
    private bool m_GameOver = false;

    
    // Start is called before the first frame update
    void Start()
    {
        //we display the high score
        LoadHighScore();

        //we display the player name
        ScoreText.text = NameScript.playerName + " " + $"Score : {m_Points}";

        //we display the high score

        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        //ScoreText.text = $"Score : {m_Points}";
        ScoreText.text = NameScript.playerName + " " +  $"Score : {m_Points}";
        //ScoreText.text = $"Score : {m_Points}";
    }

    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);

        Debug.Log("save high score");
        saveHighScore();
    }

    [System.Serializable]
    public class PlayerData
    {
        public string playerName;
        public int playerScore;
    }


    public void saveHighScore()
    {
        //we check the current high score

        // we check if the current high score is less we exit
        if (m_Points < GetCurrentHighScore())
        {
            return;
        }


        PlayerData data = new PlayerData();
        data.playerName = NameScript.playerName;
        data.playerScore = m_Points;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadHighScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            PlayerData data = JsonUtility.FromJson<PlayerData>(json);

            //TeamColor = data.TeamColor;
            //Debug.Log("High score" + data.playerScore + " " + data.playerName);
            HighScoreText.text = data.playerName + " " + data.playerScore;

        }
    }

    //this function get the current saved high score
    public int GetCurrentHighScore()
    {
        
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            PlayerData data = JsonUtility.FromJson<PlayerData>(json);

            //TeamColor = data.TeamColor;
            //Debug.Log("High score" + data.playerScore + " " + data.playerName);
            return data.playerScore;

        }
        else
            return 0;

    }




}
