using Core;
using UnityEngine;

namespace Content
{
    public class OffsetScrolling : MonoBehaviour 
    {
        [SerializeField] private float scrollSpeed;
        [SerializeField] private float loopThreshold;
        private Renderer _renderer;
        private static readonly int MainTex = Shader.PropertyToID("_MainTex");

        private void Awake()
        {
            if (TryGetComponent(out _renderer) == false)
            {
                Debug.LogWarning($"No Renderer on {gameObject.name}");
            }
        }

        void Update ()
        {
            Vector2 currentTextureOffset = _renderer.material.GetTextureOffset(MainTex);
        
            float newXOffset = currentTextureOffset.x + scrollSpeed * GameInstance.GameDelta;
            Vector2 newOffset = new Vector2(loopThreshold == 0 ? newXOffset : newXOffset % loopThreshold,
                currentTextureOffset.y);
            _renderer.material.SetTextureOffset(MainTex, newOffset);
        }
    }
}