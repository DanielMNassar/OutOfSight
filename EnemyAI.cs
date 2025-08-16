using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    private NavMeshAgent agent;

    public Transform[] patrolPoints;
    public float detectionRange = 10f;
    public float stopChaseDistance = 15f;
    public float catchDistance = 1.5f;

    private int currentPatrolIndex = 0;
    private Transform player;
    private bool chasing = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (agent == null)
        {
            Debug.LogError("❌ EnemyAI: No NavMeshAgent found on this GameObject.");
            enabled = false;
            return;
        }

        // Find player by tag
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj == null)
        {
            Debug.LogError("❌ EnemyAI: Player not found! Make sure they're tagged 'Player'.");
            enabled = false;
            return;
        }

        player = playerObj.transform;

        GeneratePatrolPoints(4, 6f);

        // Check patrol points are valid before using them
        if (patrolPoints == null || patrolPoints.Length == 0 || patrolPoints[0] == null)
        {
            Debug.LogError("❌ EnemyAI: Patrol points were not properly generated.");
            enabled = false;
            return;
        }

        agent.SetDestination(patrolPoints[0].position);
    }

    void Update()
    {
        if (player == null || agent == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (!chasing && distanceToPlayer <= detectionRange)
        {
            chasing = true;
        }
        else if (chasing && distanceToPlayer > stopChaseDistance)
        {
            chasing = false;
            if (patrolPoints.Length > 0 && patrolPoints[currentPatrolIndex] != null)
                agent.SetDestination(patrolPoints[currentPatrolIndex].position);
        }

        if (chasing)
        {
            agent.SetDestination(player.position);
            if (distanceToPlayer <= catchDistance)
                CatchPlayer();
        }
        else
        {
            Patrol();
        }
    }

    void Patrol()
    {
        if (patrolPoints == null || patrolPoints.Length == 0) return;

        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
            if (patrolPoints[currentPatrolIndex] != null)
                agent.SetDestination(patrolPoints[currentPatrolIndex].position);
        }
    }

    void CatchPlayer()
    {
        Debug.Log("🎯 Enemy caught the player!");
        Time.timeScale = 0f;
    }

    void GeneratePatrolPoints(int count, float radius)
    {
        patrolPoints = new Transform[count];

        for (int i = 0; i < count; i++)
        {
            Vector3 offset = Random.insideUnitSphere * radius;
            offset.y = 0;
            Vector3 pointPos = transform.position + offset;

            GameObject point = new GameObject("PatrolPoint_" + i);
            point.transform.position = pointPos;
            patrolPoints[i] = point.transform;
        }

        Debug.Log($"✅ Generated {count} patrol points.");
    }

    
    private void OnDrawGizmosSelected()
    {
        if (patrolPoints == null) return;

        Gizmos.color = Color.yellow;
        foreach (var point in patrolPoints)
        {
            if (point != null)
                Gizmos.DrawSphere(point.position, 0.3f);
        }
    }
}
