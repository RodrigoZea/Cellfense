using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class ResourceManager : MonoBehaviour
{
    //Cuanta "sangre" tiene el jugador, estos son los puntos para poder crear estructuras, entrenar infanteria, etc..
    public int bloodPts;
    //Cuenta actual de unidades
    public int currCount;
    public Text points;
    public Text currWave;
    public Image wavebar;

    public Text bcellsText;
    public Text dcellsText;

    public List<Vector3> positionList = new List<Vector3>();
    public GameObject enemies;
    public GameObject gameOverPanel;
    public bool gameOver = false;

    //Arbol con enemigos y celulas del jugador
    public KdTree<NavMeshAgent> playerSpawns = new KdTree<NavMeshAgent>();
    public KdTree<NavMeshAgent> enemySpawns = new KdTree<NavMeshAgent>();
    public int psIndex = 0;
    public int esIndex = 0;

    //lista con cada cosa por construir, para ver si algo falta de ser construido.
    //public List<GameObject> path = new List<GameObject>();

    public Vector3 currPosition;
    public int currentTime = 0;
    public bool worked = false;
    public bool attacking = false;
    float multiplier = 0.02f;
    int waveNo = 1;
    public int roundLimit = 5;

    //Singleton
    public static ResourceManager Instance { get; private set; }

    private void Awake()
    {
        positionList.Add(new Vector3(0, 0, 0));
        currWave.enabled = false;
        wavebar.fillAmount = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        //Solo podemos tener un ResourceManager
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        addPts(50);
    }

    // Update is called once per frame
    void Update()
    {
        //currentTime += 3;
        wavebar.fillAmount += multiplier * Time.deltaTime;
        if (wavebar.fillAmount >= 1) {
            nextWave();
        }

        if (worked == true)
        {
            bcellsText.text = "working";
        }
        else {
            bcellsText.text = "idle";
        }
    }

    public void gameLost() {
        gameOverPanel.SetActive(true);
    }

    void changeText(int number, Text waveText) {
        waveText.text = "wave " + number.ToString();
        //currWave.enabled = false;
    }

    void nextWave() {
        attacking = true;
        currWave.enabled = true;
        wavebar.fillAmount = 0;


        StartCoroutine(spawnEnemies());
        //spawnEnemySpawner();
        roundLimit += 2;
        changeText(waveNo, currWave);
        waveNo += 1;
        addPts(5);
    }



    IEnumerator spawnEnemies() {
        for (int i = 0; i < roundLimit; i++)
        {
            Vector3 randomPosList = positionList[Random.Range(0, positionList.Count)];

            GameObject[] allPlayers = GameObject.FindGameObjectsWithTag("Player");
            List<Vector3> playerPositionList = new List<Vector3>();

            for (int j = 0; j < allPlayers.Length; j++)
            {
                playerPositionList.Add(allPlayers[j].transform.position);
            }

            Vector3 randomPos = generatePosition(playerPositionList, randomPosList);
            Instantiate(enemies, randomPos, Quaternion.identity);
            yield return new WaitForSeconds(3f);
        }

    }

    Vector3 generatePosition(List<Vector3> posList, Vector3 randomPosList) {
        bool canSpawn = false;
        Vector3 randomPos = new Vector3(0, 0, 0);

        while (!canSpawn) {
            randomPos = new Vector3(Random.Range(randomPosList.x - 5, randomPosList.x + 5), 7, Random.Range(randomPosList.z - 5, randomPosList.z + 5));
            foreach (Vector3 pos in posList){
                if (Vector3.Distance(randomPos, pos) > 4.5f) {
                    canSpawn = true;
                    break;
                }

            }
        }

        return randomPos;
        
    }

    //private IEnumerator spawnEnemies() {
    //    for (int i = 0; i < roundLimit; i++) {
    //        Vector3 randomPosList = positionList[Random.Range(0, positionList.Count)];

    //        Vector3 randomPos = new Vector3(Random.Range(-randomPosList.x, randomPosList.x), 1, Random.Range(-randomPosList.z, randomPosList.z));

    //        Instantiate(enemy, randomPos, Quaternion.identity);
    //        yield return new WaitForSeconds(1f);
    //    }    
    //}

    public void addPts(int bPts) {
        bloodPts += bPts;
        points.text = bloodPts.ToString();
    }

    public void updatePos(Vector3 newPosition) {
        worked = true;
        currPosition = newPosition;
    }

    public void deductPoints(int bPts) {
        bloodPts -= bPts;
        points.text = bloodPts.ToString();
    }
}
