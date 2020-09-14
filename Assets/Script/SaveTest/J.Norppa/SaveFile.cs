using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu(menuName = "Custom Assets/Save File")]
public class SaveFile : ScriptableObject
{
    [SerializeField]
    public LevelDictionary GameObjectsStates;

    [SerializeField]
    public string CharacterName = "Link";

}
