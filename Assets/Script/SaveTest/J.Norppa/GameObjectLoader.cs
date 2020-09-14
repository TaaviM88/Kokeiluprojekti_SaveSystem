using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectLoader : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        LoadGameObjects();
    }

    public void LoadGameObjects()
    {
        var objects = Resources.FindObjectsOfTypeAll<CollectableObject>();
        var saveFile = SaveFileManager.Instance.SaveFiles[SaveFileManager.Instance.CurrentSaveFile];
        var sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;

        foreach (var gameObj in objects)
        {
            if(gameObj is CollectableObject identifiable)
            {
                var instanceId = identifiable.Id.ToString();
                if (saveFile.GameObjectsStates.ContainsKey(sceneName) && saveFile.GameObjectsStates[sceneName].ContainsKey(instanceId))
                {
                    gameObj.gameObject.SetActive(saveFile.GameObjectsStates[sceneName][instanceId]);

                }
            }
           
        }
    }

    public void SaveGameObjects()
    {
        var objects = Resources.FindObjectsOfTypeAll<CollectableObject>();
        SaveFile saveFile = SaveFileManager.Instance.SaveFiles[SaveFileManager.Instance.CurrentSaveFile];
        var sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;

        if(!saveFile.GameObjectsStates.ContainsKey(sceneName))
        {
            saveFile.GameObjectsStates.Add(sceneName, new GameObjectStateDictionary());
        }

        foreach (var gameObj in objects)
        {
            if(gameObj is CollectableObject identifiable)
            {
                var instanceId = identifiable.Id.ToString();
                if (!saveFile.GameObjectsStates[sceneName].ContainsKey(instanceId))
                {
                    saveFile.GameObjectsStates[sceneName].Add(instanceId, false);
                }
                saveFile.GameObjectsStates[sceneName][instanceId] = gameObj.gameObject.activeInHierarchy;

            }
           
        }
    }
}
