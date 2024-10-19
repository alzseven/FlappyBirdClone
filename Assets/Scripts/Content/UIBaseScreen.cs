using UnityEngine;

namespace Content
{
    public abstract class UIBaseScreen : MonoBehaviour
    {
        public abstract T Get<T>(string nameToFind) where T : UnityEngine.Object;

        public abstract void EnableObject<T>(string nameToFind) where T : UnityEngine.Object;
        
        public abstract void ClearAll();
    }
}