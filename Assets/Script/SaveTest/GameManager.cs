using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<GameObject> bullets = new List<GameObject>();
    public int shots = 0;
    public int hits = 0;
    [SerializeField]
    private Text hitsText;
    [SerializeField]
    private Text shotsText;
    [SerializeField]
    private GameObject menu;
    [SerializeField]
    private GameObject[] targets;
    private bool isPaused = false;

    // Start is called before the first frame update
    void Awake()
    {
        Pause();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }

    private void Pause()
    {
        menu.SetActive(true);
        Cursor.visible = true;
        Time.timeScale = 0;
        isPaused = true;
    }

    public void Unpause()
    {
        menu.SetActive(false);
        Cursor.visible = false;
        Time.timeScale = 1;
        isPaused = false;
    }


    public bool IsGamePause()
    {
        return isPaused;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (isPaused)
            {
                Unpause();
            }
            else
            {
                Pause();
            }
        }
    }

    private SaveTest CreateSaveGameObject()
    {
        SaveTest savestest = new SaveTest();
        int i = 0;
        foreach(GameObject targetGameObject in targets)
        {
            Target target = targetGameObject.GetComponent<Target>();
            if(target.activeRobot !=null)
            {
                savestest.livingTargetPositions.Add(target.position);
                savestest.livingTargetsTypes.Add((int)target.activeRobot.GetComponent<Robot>().type);
                i++;
            }
        }
        savestest.hits = hits;
        savestest.shots = shots;
        return savestest;
    }

    public void SaveGame()
    {
        SaveTest saveTest = CreateSaveGameObject();
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "gamesave.save");
        bf.Serialize(file, saveTest);

        hits = 0;
        shots = 0;
        shotsText.text = "Shots: " + shots;
        hitsText.text = "Hits: " + hits;

        ClearRobots();
        ClearBullets();
        Debug.Log("Game saved");
    }

    public void LoadGame()
    {
        if(File.Exists(Application.persistentDataPath + "/gamesave.save"))
        {
            ClearBullets();
            ClearRobots();
            RefreshRobots();

            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/gamesave.save", FileMode.Open);
            SaveTest saveT = (SaveTest)bf.Deserialize(file);
            file.Close();

            for (int i = 0; i < saveT.livingTargetPositions.Count; i++)
            {
                int position = saveT.livingTargetPositions[i];
                Target target = targets[position].GetComponent<Target>();
                target.ActivateRobot((RobotTypes)saveT.livingTargetsTypes[i]);
                target.GetComponent<Target>().ResetDeathTimer();
            }

            shotsText.text = "Shots: " + saveT.shots;
            hitsText.text = "Hits: " + saveT.hits;
            shots = saveT.shots;
            hits = saveT.hits;
            Debug.Log("Game Loaded");
            Unpause();

        }
        else
        {
            Debug.Log("No game saved");
        }
    }

    public void NewGame()
    {
        hits = 0;
        shots = 0;
        shotsText.text = "Shots: " + shots;
        hitsText.text = "Hits: " + hits;

        ClearRobots();
        ClearBullets();
        RefreshRobots();

        Unpause();
    }

    private void RefreshRobots()
    {
        foreach (GameObject target in targets)
        {
            target.GetComponent<Target>().RefreshTimers();
        }
    }

    private void ClearBullets()
    {
        foreach (GameObject bullet in bullets)
        {
            Destroy(bullet);
        }
    }

    private void ClearRobots()
    {
       foreach(GameObject target in targets)
        {
            target.GetComponent<Target>().DisableRobot();
        }
    }
    public void AddShot()
    {
        shots++;
        shotsText.text = "Shots: " + shots;
    }

    public void AddHit()
    {
        hits++;
        hitsText.text = "Hits: " + hits;
    }



    public void SaveAsJSON()
    {
        SaveTest saveT = CreateSaveGameObject();
        string json = JsonUtility.ToJson(saveT);
        Debug.Log("Saving as JSON:" + json);
    }
}
