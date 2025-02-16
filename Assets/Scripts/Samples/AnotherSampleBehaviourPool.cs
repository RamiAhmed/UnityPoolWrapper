using PoolWrapper;
using UnityEngine;

public class AnotherSampleBehaviourPool : MonoBehaviour
{
    [SerializeField] private MonoBehaviour _prefab;

    private MonoBehaviourPool _pool;

    private void Awake()
    {
        _pool = new(transform, _prefab);
    }

    private void OnDestroy()
    {
        _pool?.Dispose();
        _pool = null;
    }

    private void OnGUI()
    {
        using var areaScope = new GUILayout.AreaScope(new Rect(600, 10, 300, 400));
        GUILayout.Label("Simpler Behaviour Pool");

        if (GUILayout.Button("Get"))
            _pool.Get(Random.insideUnitCircle * 100f);

        if (GUILayout.Button("Release"))
        {
            // Find first enabled child
            for (var i = transform.childCount - 1; i >= 0; i--)
            {
                var child = transform.GetChild(i);
                if (child.gameObject.activeInHierarchy)
                {
                    _pool.Release(child.GetComponent<MonoBehaviour>());
                    break;
                }
            }
        }
    }
}