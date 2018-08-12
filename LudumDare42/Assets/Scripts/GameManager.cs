using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using FMODUnity;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour {

    
    public static int width = 10;
    public static int height = 5;
    public static Transform[,] grid;
    bool[,] gridBool;

    public int score;
    public float health = 100f;
    public int level = 1;
    public float speedIncreaseValue;

    public Vector2 gridBoxSize;

    public LayerMask mask;
    public LayerMask visualBoxMask;

    public TextMeshProUGUI scoreText, levelText, finalScoreNum;

    public Image healthBarImage;
    float startHealthBarSize, healthBarPerc;
    Vector3 healthBarSize;

    public GameObject smokePrefab;
    public GameObject gameOverScreen, gameOverRestartButton;



    [FMODUnity.EventRef]
    public string objectDestroySound;



   public  int scoreTempNum, scoreOffset;

    ObjectSpawner oS;
    MusicManager mM;
    EventSystem eS;
    HighScoreManager hSM;


    private void Awake()
    {
        oS = FindObjectOfType<ObjectSpawner>();
        mM = FindObjectOfType<MusicManager>();
        eS = FindObjectOfType<EventSystem>();
        hSM = GetComponent<HighScoreManager>();
    }



    // Use this for initialization
    void Start () {
        Time.timeScale = 1;
        healthBarSize = healthBarImage.transform.localScale;


        grid = new Transform[width, height];
        gridBool = new bool[width, height];
        ResetAllGridBools();

        mM.ChangeMusicLevel(level);
    }


    void ResetAllGridBools() {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {

                //Debug.Log(grid[x,y]);
                gridBool[x, y] = false;

            }
        }

    }
	
	// Update is called once per frame
	void Update () {
        for (int y = 0; y < height; y++) {
            if (CheckRow(y)) {
                DestroyRow(y);
                ResetAllGridBools();
            }

        }

        UpdateUI();
        UpdateLevelAndSpeed();

	}

    void UpdateUI() {
        scoreText.text = score.ToString("F0");
        levelText.text = level.ToString();

        healthBarPerc = health / 100;
        healthBarSize = new Vector3(healthBarPerc, 1, 1);
        healthBarImage.transform.localScale = healthBarSize;


    }

    void UpdateLevelAndSpeed() {
        if (score >= scoreTempNum + scoreOffset) {
            level++;
            mM.ChangeMusicLevel(level);
            scoreTempNum = score;
            if (level == 7) {
                scoreOffset  += 200;
            }

            oS.spawnSpeed -= speedIncreaseValue;
            if (oS.spawnSpeed < 2f) {
                oS.spawnSpeed = 2f;
            }
            Debug.Log("New Speed is " + oS.spawnSpeed + " with level at " + level);
        }
        

    }


    private void OnDrawGizmos()
    {
        
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {

                
               if(Physics2D.OverlapBox(new Vector3(x, y, 0), gridBoxSize, 0, mask)) {
                    Gizmos.color = Color.red;
                    Gizmos.DrawWireCube(new Vector3(x, y, 0), gridBoxSize);
                } else {
                    Gizmos.color = Color.grey;
                    Gizmos.DrawWireCube(new Vector3(x, y, 0), gridBoxSize);
                }
            }
        }
    }

    void DestroyRow(int y) {
        //Debug.Log(y + " Row is Full");
        for (int x = 0; x < width; x++)
        {
            grid[x, y] = Physics2D.OverlapBox(new Vector3(x, y, 0), gridBoxSize, 0, mask).gameObject.transform;
            if (grid[x, y] != null)
            {
                score += 10;
                if (gridBool[x, y] == false)
                {
                    
                    gridBool[x, y] = true;
                    //Debug.Log("Destroy " + grid[x, y].gameObject.name + " at position " + grid[x, y].position);
                    Destroy(grid[x, y].gameObject, 0.01f);
                    GameObject newSmoke = Instantiate(smokePrefab, grid[x, y].position, Quaternion.identity);
                    Destroy(newSmoke, 1.5f);
                    FMODUnity.RuntimeManager.PlayOneShot(objectDestroySound);
                }


            }
        }



    }


   


    bool CheckRow(int y) {
            for (int x = 0; x < width; x++) {
            if (!Physics2D.OverlapBox(new Vector3(x, y, 0), gridBoxSize, 0, mask))
            {
                return false;
            }

        }
            return true;

    }

    public void GameOver() {
        Debug.Log("GameOver!");
        finalScoreNum.text = score.ToString();
        gameOverScreen.SetActive(true);
        eS.SetSelectedGameObject(gameOverRestartButton);
        Time.timeScale = 0;
        hSM.CheckNewHighScore(score);
    }

    public void ChangeMusicLevelNum(int levelNum) {
        mM.ChangeMusicLevel(levelNum);

    }
}
