using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;
using Unity.VisualScripting;

public class TargetSpawner : MonoBehaviour
{
    [Header("Target Prefabs")]
    [SerializeField] private Target[] targetPrefabs;

    [Header("Spawn Settings")]
    [SerializeField] private Vector3 areaSize = new Vector3(12f, 3f, 4f);

    [SerializeField] private float minSpacing = 1.0f;

    [SerializeField] private float spawnInterval = 1f;

    [SerializeField] private int maxTargets = 6;

    private List<Target> _activeTargets = new List<Target>();
    private float _spawnTimer = 0f;

    private HUD hud;

    public UnityEvent<int> onScoreChange;
    public UnityEvent<string> onScoreChangeText;

    private void Start()
    {
        hud = GameObject.FindFirstObjectByType<HUD>();
    }

    private void Update()
    {
        _activeTargets.RemoveAll(target => target == null);

        _spawnTimer -= Time.deltaTime;

        if (_spawnTimer < 0f && _activeTargets.Count < maxTargets)
        {
            if (TrySpawnRandomTarget())
            {
                _spawnTimer = spawnInterval;
            }
        }
    }

    private bool TrySpawnRandomTarget()
    {
        Target prefab = targetPrefabs[Random.Range(0,targetPrefabs.Length)];

        Vector3 position = GetRandomPointInArea();
        if (IsTooCloseToOtherTargets(position)) return false;

        Target newTarget = Instantiate(prefab, position, Quaternion.identity);

        newTarget.onDown.AddListener(Announce);

        _activeTargets.Add(newTarget);
        return true;
    }

    private void OnDestroy()
    {
        foreach(Target target in _activeTargets)
        {
            if (target != null) target.onDown.RemoveListener(Announce);
        }
    }

    private Vector3 GetRandomPointInArea()
    {
        //new Vector3(1,4,5)

        Vector3 localOffset = new Vector3(
                Random.Range(-0.5f, 0.5f) * areaSize.x,
                Random.Range(-0.5f, 0.5f) * areaSize.y,
                Random.Range(-0.5f, 0.5f) * areaSize.z);

        return transform.position + transform.rotation * localOffset;
    }

    private bool IsTooCloseToOtherTargets(Vector3 position)
    {
        foreach (Target target in _activeTargets)
        {
            if (Vector3.Distance(target.transform.position, position) < minSpacing)
            {
                return true;
            }
        }
        return false;
    }

    private void Announce()
    {
        onScoreChange.Invoke(1);
        onScoreChangeText.Invoke("1");
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one);
        Gizmos.DrawWireCube(Vector3.zero, areaSize);
    }
}