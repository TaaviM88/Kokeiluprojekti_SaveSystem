using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CollectableObject : MonoBehaviour, ICollectable
{

    public bool isCollected = false;
    [SerializeField]
    public string Id = Guid.NewGuid().ToString();

    private void Awake()
    {
        Id = gameObject.name;
        if (string.IsNullOrEmpty(Id))
        {
            //Id = Guid.NewGuid().ToString();
            Id = gameObject.name;
            Debug.Log("test");
        }
        
    }

    public bool isColleceted()
    {
        return isCollected;
    }


}
