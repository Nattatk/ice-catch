using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class AIObjects
{
    public string groupName { get { return m_aiGroupName; } }
    public GameObject objectPrefab { get { return m_prefab; } }
    public int maxAI { get { return m_maxAI; } }
    public int spawnRate { get { return m_spawnRate; } }
    public int spawnAmount { get { return m_maxSpawnAmount; } }
    public bool randomizeStats { get { return m_randomizeStats; } }
    public bool enableSpawner { get { return m_enableSpawner; } }


    [Header("AI Group Stats")]
    [SerializeField]
    private string m_aiGroupName;
    [SerializeField]
    private GameObject m_prefab;
    [SerializeField]
    [Range(0f, 40f)]
    private int m_maxAI;
    [SerializeField]
    [Range(0f, 20f)]
    private int m_spawnRate;
    [SerializeField]
    [Range(0f, 10f)]
    private int m_maxSpawnAmount;
    [SerializeField]
    private bool m_randomizeStats;

    [SerializeField]
    private bool m_enableSpawner;
    


    public AIObjects(string Name, GameObject Prefab, int MaxAI, int SpawnRate, int SpawnAmount, bool RandomizeStats)
    {
        m_aiGroupName = Name;
        m_prefab = Prefab;
        m_maxAI = MaxAI;
        m_spawnRate = SpawnRate;
        m_maxSpawnAmount = SpawnAmount;
        m_randomizeStats = RandomizeStats;
    }

    public void SetValues(int MaxAI, int SpawnRate, int SpawnAmount)
    {
        m_maxAI = MaxAI;
        m_spawnRate = SpawnRate;
        m_maxSpawnAmount = SpawnAmount;
    }
}

public class FishSpawner : MonoBehaviour
{

    public List<Transform> Waypoints = new List<Transform>();

    public float spawnTimer {  get { return m_SpawnTimer; } }
    public Vector3 spawnArea { get { return m_SpawnArea; } }

    [Header("Global Stats")]
    [Range(0f, 600f)]
    [SerializeField]
    private float m_SpawnTimer;
    [SerializeField]
    private Vector3 m_SpawnArea = new Vector3(20f, 10f, 20f);
    [SerializeField]
    private Color m_SpawnColor = new Color(1.000f, 0.000f, 0.000f, 0.300f);

    //making an array from the AI class
    [Header("AI Group Settings")]
    public AIObjects[] AIObject = new AIObjects[5];

    

    // Start is called before the first frame update
    void Start()
    {
        GetWaypoints();
        RandomizeGroups();
        CreateAIGroups();
        InvokeRepeating("SpawnNPC", 0.5f, spawnTimer);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnNPC()
    {
        for (int i = 0; i < AIObject.Count(); i++)
        {
            if (AIObject[i].enableSpawner && AIObject[i].objectPrefab != null)
            {
                GameObject tempGroup = GameObject.Find(AIObject[i].groupName);
                if(tempGroup.GetComponentInChildren<Transform>().childCount < AIObject[i].maxAI)
                {
                    for (int y = 0; y < Random.Range(0,AIObject[i].spawnAmount); y++)
                    {
                        Quaternion randomRotation = Quaternion.Euler(Random.Range(-20, 20), Random.Range(0, 360), 0);
                        GameObject tempSpawn;
                        tempSpawn = Instantiate(AIObject[i].objectPrefab, RandomPosition(), randomRotation);
                        tempSpawn.transform.parent = tempGroup.transform;
                        tempSpawn.AddComponent<AIMove>();
                    }
                }
            }
        }
    }

    //method for spawning in a random spot within spawn area
    public Vector3 RandomPosition()
    {
        Vector3 randomPosition = new Vector3(
            Random.Range(-spawnArea.x, spawnArea.x),
            Random.Range(-spawnArea.y, spawnArea.y),
            Random.Range(-spawnArea.z, spawnArea.z));
        randomPosition = transform.TransformPoint(randomPosition * .5f);
        return randomPosition;
    }

    //pick random waypoint
    public Vector3 RandomWaypoint()
    {
        int randomWP = Random.Range(0, (Waypoints.Count - 1));
        Vector3 randomWaypoint = Waypoints[randomWP].transform.position;
        return randomWaypoint;
    }

    void RandomizeGroups()
    {
        for (int i = 0; i < AIObject.Count(); i++)
        {
            if (AIObject[i].randomizeStats)
            {
                
                //AIObject[i] = new AIObjects(AIObject[i].groupName, AIObject[i].objectPrefab, Random.Range(1, 30), Random.Range(1, 20), Random.Range(1, 10), AIObject[i].randomizeStats);
                AIObject[i].SetValues(Random.Range(1, 30), Random.Range(1, 20), Random.Range(1, 10));
            }
        }
    }

    void CreateAIGroups()
    {
        for (int i = 0; i < AIObject.Count(); i++)
        {
            //Empty Game Object to store AI
            GameObject AIGroupSpawn;

            //new game object
            AIGroupSpawn = new GameObject(AIObject[i].groupName);
            AIGroupSpawn.transform.parent = gameObject.transform;
        }
    }

    void GetWaypoints()
    {
        Transform[] wpList = transform.GetComponentsInChildren<Transform>();
        for (int i = 0; i < wpList.Length; i++)
        {
            if (wpList[i].tag == "WayPoint")
            {
                Waypoints.Add(wpList[i]);
            }
            
        }
    }
    //because I can't seee the spawn area 
    void OnDrawGizmosSelected()
    {
        Gizmos.color = m_SpawnColor;
        Gizmos.DrawCube(transform.position, spawnArea);
    }
}
