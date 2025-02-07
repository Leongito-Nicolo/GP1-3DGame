using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;           // instance of game manager
    [SerializeField] private NavMeshAgent navMeshAgent;         // instance of nav mesh
    [SerializeField] private float maxEnemyHealth;              // max health of enemies
    public float currentEnemyHealth;                            // current health of enemies
    public float range = 10.0f;                                 // range of samples
    public float maxDistanceToChase;                            // max distance necessary to start chasing

    [SerializeField] private int enemyPoints;                   // enemy points gained when killed

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GetComponent<GameManager>();
        currentEnemyHealth = maxEnemyHealth;        // set the current health to max when created

        if (gameManager == null)
        {
            gameManager = FindFirstObjectByType<GameManager>();
        }
    }

    // Update is called once per frame
    void Update()
    {

        // check enemies health
        if (currentEnemyHealth <= 0)
        {
            gameManager.UpdateScore(enemyPoints);      // gain points
            Destroy(gameObject);    // destroy game object if killed
            return;
        }

        // if the distance between player and enemy is less than 15 and the destination to the player
        if(Vector3.Distance(transform.position, FindFirstObjectByType<PlayerController>().transform.position) < maxDistanceToChase)
        {
            navMeshAgent.SetDestination(FindFirstObjectByType<PlayerController>().transform.position);
            navMeshAgent.stoppingDistance = 2;      // set the distance to 2 to not push player
        }
        else   // else wander in map
        {
            Wander();
            navMeshAgent.stoppingDistance = 0;
        }

    }

    // function to choose a random point to reach
    public void Wander()
    {
        if ((navMeshAgent.remainingDistance <= 0.5f && !navMeshAgent.pathPending))
        {
            Vector3 point;
            // if it finds a random point set destination to it
            if (RandomPoint(transform.position, range, out point))
            {
                navMeshAgent.SetDestination(point);
            }
        }
    }

    // function to sample random point in map
    public bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        for (int i = 0; i < 30; i++)
        {
            Vector3 randomPoint = center + Random.insideUnitSphere * range;
            NavMeshHit hit;
            // sample positions around AI
            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
            {
                result = hit.position;
                return true;
            }
        }
        result = Vector3.zero;
        return false;
    }
}
