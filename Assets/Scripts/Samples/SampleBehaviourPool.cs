using PoolWrapper;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public sealed class SampleBehaviourPool : MonoBehaviour
{
    [SerializeField] private SampleBehaviour _prefab;
    [SerializeField] private int _defaultCapacity = 10;
    [SerializeField] private int _maxSize = 10000;

    private int _count = 10;
    private SimpleBehaviourPool<SampleBehaviour> _pool;

    private void Awake()
    {
        _pool = new SimpleBehaviourPool<SampleBehaviour>(
            transform,
            _prefab,
            defaultCapacity: _defaultCapacity,
            maxSize: _maxSize);
    }

    private void OnDestroy()
    {
        _pool?.Dispose();
        _pool = null;
    }

    private void OnGUI()
    {
        using var areaScope = new GUILayout.AreaScope(new Rect(10, 10, 300, 400));
        GUILayout.Label("Sample Behaviour Pool");

        using (var horizontalScope = new GUILayout.HorizontalScope())
        {
            GUILayout.Label("Count");
            
            var count = GUILayout.TextField(_count.ToString());
            if (!string.IsNullOrEmpty(count))
            {
                if (!int.TryParse(count, out _count))
                    Debug.LogWarning($"{count} is not a valid number, please input a valid integer number.");
            }
        }

        if (GUILayout.Button($"Get {_count}"))
        {
            for (var i = 0; i < _count; i++)
            {
                _pool.Get(
                    Random.insideUnitCircle * 100f,
                    $"SampleBehaviour_{i}");
            }
        }

        if (GUILayout.Button($"Release {_count}"))
        {
            var target = math.min(_count, transform.childCount);
            while (target > 0)
            {
                // Find first enabled child
                for (var i = transform.childCount - 1; i >= 0; i--)
                {
                    var child = transform.GetChild(i);
                    if (child.gameObject.activeInHierarchy)
                    {
                        _pool.Release(child.GetComponent<SampleBehaviour>());
                        break;
                    }
                }
                
                target--;
            }
        }
    }
}