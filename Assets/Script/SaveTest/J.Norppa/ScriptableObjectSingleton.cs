using System.Linq;
using UnityEngine;
namespace Extensions
{
    public class ScriptableObjectSingleton<T> : ScriptableObject where T : ScriptableObject
    {
        private static T instance;

        public static T Instance
        {
            get
            {
                if(!instance)
                {
                    instance = Resources.FindObjectsOfTypeAll<T>().FirstOrDefault();
                }
                return instance;
            }
        }
    }
}

