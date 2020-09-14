using System;
using UnityEngine;
using Extensions;

[Serializable]
[CreateAssetMenu(menuName = "Custom Assets/Save File Manager")]
public class SaveFileManager : ScriptableObjectSingleton<SaveFileManager>
{
    [SerializeField]
    public SaveFile[] SaveFiles;
    [SerializeField]
    public int CurrentSaveFile;
}
