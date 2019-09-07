using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int numEnemies = 2;

    public Canvas dieCanvas;

    public Text textDebugger;
    public static string textDebug;

    private Text textHealth;
    private List<GameObject> listEnemies = new List<GameObject>();
    
    void Awake()
    {
        EventManager.StartListening("PlayerDead", OnPlayerDead);

        if (dieCanvas != null)
        {
            dieCanvas.enabled = false;
        }

        initEnemies();

        textHealth = GameObject.Find("TextHealth").GetComponent<Text>();
        Cursor.lockState = CursorLockMode.Locked; // keep confined in the game window
        Cursor.visible = false;

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameState gameState = GameState.getSingleton();

        textHealth.text = "Health: " + gameState.playerHealth + 
            "\nEnemies: " + listEnemies.Count;
        if (textDebugger != null)
        {
            textDebugger.text = textDebug;
        }
    }

    void initEnemies()
    {
        GameObject setEnemies = GameObject.Find("SetEnemies");
        GameObject enemy = GameObject.Find("PumpkinhulkTemplate");

        if (listEnemies == null)
        {
            listEnemies = new List<GameObject>();
        } else
        {
            listEnemies.Clear();
        }

        for (int i = 0; i < numEnemies; i++)
        {
            Vector3 position = new Vector3(Random.Range(-28, 28), 0, Random.Range(-28, 28));
            Quaternion rotation = new Quaternion();

            GameObject newEnemy = Instantiate(enemy, position, rotation, setEnemies.transform);

            RaycastHit hitInfo;
            bool collision = Physics.Raycast(newEnemy.transform.position, Vector3.up, out hitInfo, Mathf.Infinity);
            if (collision && hitInfo.point.y > 0)
            {
                newEnemy.transform.Translate(Vector3.up * hitInfo.point.y);
            }

            listEnemies.Add(newEnemy);
        }
        
        enemy.SetActive(false);
    }

    public void destroyEnemy(GameObject enemy)
    {
        listEnemies.Remove(enemy);
        Destroy(enemy, 0);
    }

    private void OnPlayerDead()
    {
        if (dieCanvas != null)
        {
            dieCanvas.enabled = true;
        }
        Invoke("RestartScene", 5);
    }

    private void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
