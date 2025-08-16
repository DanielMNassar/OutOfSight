using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;

public class PatrolPointGenerator : MonoBehaviour
{
    public GameObject enemy;
    public int numberOfPoints = 5;
    public Vector3 areaMin;
    public Vector3 areaMax;
    public float updateInterval = 30f; // Regenerate every 30 seconds

    private List<Transform> currentPatrolPoints = new List<Transform>();

    void Start()
    {
        StartCoroutine(GeneratePatrolPointsRoutine());
    }

    IEnumerator GeneratePatrolPointsRoutine()
    {
        while (true)
        {
            GeneratePatrolPoints();
            yield return new WaitForSeconds(updateInterval);
        }
    }

    void GeneratePatrolPoints()
    {
        // Clean up old points
        foreach (var point in currentPatrolPoints)
        {
            if (point != null)
                Destroy(point.gameObject);
        }
        currentPatrolPoints.Clear();

        Transform[] patrolArray = new Transform[numberOfPoints];

        for (int i = 0; i < numberOfPoints; i++)
        {
            Vector3 randomPos = new Vector3(
                Random.Range(areaMin.x, areaMax.x),
                areaMax.y,
                Random.Range(areaMin.z, areaMax.z)
            );

            if (Physics.Raycast(randomPos, Vector3.down, out RaycastHit hit, 100f))
            {
                if (NavMesh.SamplePosition(hit.point, out NavMeshHit navHit, 2f, NavMesh.AllAreas))
                {
                    GameObject point = new GameObject($"PatrolPoint_{i}");
                    point.transform.position = navHit.position;
                    point.transform.parent = transform;
                    patrolArray[i] = point.transform;
                    currentPatrolPoints.Add(point.transform);
                }
            }
        }

        if (enemy != null)
        {
            EnemyAI ai = enemy.GetComponent<EnemyAI>();
            ai.patrolPoints = patrolArray;
        }
    }

    public List<Transform> GetCurrentPoints()
    {
        return currentPatrolPoints;
    }
}
