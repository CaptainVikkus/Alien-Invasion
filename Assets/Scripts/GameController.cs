using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;


public class GameController : MonoBehaviour
{
    //Health
    [SerializeField]
    private TextMeshProUGUI healthUI;
    [SerializeField]
    private GameObject spacePort;
    private Health portHealth;

    //Wave Control
    [SerializeField]
    private TextMeshProUGUI waveUI;
    [SerializeField]
    private GameObject spawnerController;
    private Spawner spawner;
    public int currentWave = 1;
    public int maxWave = 12;
    static public int currEnemies = 0;

    // Start is called before the first frame update
    void Start()
    {
        portHealth = spacePort.GetComponent<Health>();
        Assert.IsNotNull(portHealth, "Must add Health to spaceport");
        spawner = spawnerController.GetComponent<Spawner>();
        Assert.IsNotNull(spawner, "Must add spawning to spawner");
    }

    // Update is called once per frame
    void Update()
    {
        //Update UI
        healthUI.text = "Health: " + portHealth.health;
        waveUI.text = "Wave: " + currentWave + "/" + maxWave;

        //Check Health - Lose Condition
        if (portHealth.health <= 0)
        {
            Lose();
        }
        //Check Wave - Win Condition
        if (currEnemies == 0)
        {
            currentWave++;
            spawner.maxSpawn += currentWave * 2;
            spawner.ResetSpawnCount();

            if (currentWave > maxWave)
            {
                Win();
            }
        }
    }

    private void Lose()
    {
        SceneManager.LoadScene("LevelLost");
        Debug.Log("Switching to " + "LevelLost");
    }
    private void Win()
    {
        SceneManager.LoadScene("MainMenu");
        Debug.Log("Switching to " + "MainMenu");
    }
}
