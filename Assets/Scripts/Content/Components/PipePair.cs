using System;
using Core;
using UnityEngine;
using UnityEngine.Assertions;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Content.Components
{
    public class PipePair : MonoBehaviour //, IComponentSetAble
    {
        // Components
        public PipeComponent UpperPipeComponent => upperPipeComponent;
        [SerializeField] private PipeComponent upperPipeComponent;
        public PipeComponent LowerPipeComponent => lowerPipeComponent;
        [SerializeField] private PipeComponent lowerPipeComponent;
        // Values
        public float pipeScrollSpeed;
        [SerializeField] private MinMaxFloat randomOffsetLower;
        [SerializeField] private MinMaxFloat pipeGapVertical;
        [SerializeField] private MinMaxFloat pipeGapHorizontal;
        
        public event Action OnBirdPassedPipes;
        private bool _isPassedBird;

        private static Transform _lastPipe = null;

#if UNITY_EDITOR
        [ContextMenu(nameof(SetComponents))]
        public void SetComponents()
        {
            // Components
            var pipes = GetComponentsInChildren<PipeComponent>();
            if (pipes.Length > 2) Debug.Log("There's more than two pipes as child");
            else if (pipes.Length <= 1) Debug.Log($"There's missing pipe in {name}");

            var isFirstPipeUpper = pipes[0].name == "Pipe_Up";
            upperPipeComponent = isFirstPipeUpper ? pipes[0] : pipes[1];
            lowerPipeComponent = isFirstPipeUpper ? pipes[1] : pipes[0];

            // Values
            pipeScrollSpeed = 8.4375f;
            randomOffsetLower = new MinMaxFloat(min: -6.1f, max: -3.1f);
            pipeGapHorizontal = new MinMaxFloat(min: 3.5f, max: 5.5f);
            pipeGapVertical = new MinMaxFloat(min: 12f, max: 16f);
            
            EditorUtility.SetDirty(this);
        }
#endif
        private void Awake()
        {
            // Components
            Assert.IsNotNull(upperPipeComponent);
            Assert.IsNotNull(lowerPipeComponent);
            // Values
            Assert.IsFalse(pipeScrollSpeed <= 0);
            
            Assert.IsFalse(randomOffsetLower.min > randomOffsetLower.max);
            Assert.IsFalse(pipeGapHorizontal.min > pipeGapHorizontal.max);
            Assert.IsFalse(pipeGapVertical.min > pipeGapVertical.max);
        }

        private void OnEnable() => Initialize();

        private void Initialize() => _isPassedBird = false;

        //7.TODO:
        private void Update()
        {
            // Move
            transform.position += Vector3.left * (pipeScrollSpeed * GameInstance.GameDelta);
            
            // Passed Bird
            if (transform.position.x <= 0 && _isPassedBird == false)
            {
                OnBirdPassedPipes?.Invoke();
                _isPassedBird = true;
            }
            
            // Reached End
            if (transform.position.x <= GameConst.WorldXMin - transform.localScale.x / 2)
            {
                SetRandomPipePositions(this, _lastPipe);
                _isPassedBird = false;
            }
        }
        
        public Transform SetRandomPipePositions(PipePair target, Transform lastPipeTransform = null)
        {
            var randomHeightLower = UnityEngine.Random.Range(randomOffsetLower.min, randomOffsetLower.max);
            var randomGapVertical = UnityEngine.Random.Range(pipeGapVertical.min, pipeGapVertical.max);
            var randomGapHorizontal = UnityEngine.Random.Range(pipeGapHorizontal.min, pipeGapHorizontal.max);

            var targetTransform = target.transform;
            var targetPosition = targetTransform.position;
            targetPosition = new Vector3((ReferenceEquals(lastPipeTransform, null) ? GameConst.WorldXMax : lastPipeTransform.position.x) + randomGapHorizontal, targetPosition.y, targetPosition.z);
            targetTransform.position = targetPosition;

            _lastPipe = target.transform;

            target.UpperPipeComponent.transform.position = new Vector3(targetPosition.x,
                randomHeightLower + randomGapVertical, targetPosition.z);
            target.LowerPipeComponent.transform.position =
                new Vector3(targetPosition.x, randomHeightLower, targetPosition.z);

            return _lastPipe;
        }
    }
}