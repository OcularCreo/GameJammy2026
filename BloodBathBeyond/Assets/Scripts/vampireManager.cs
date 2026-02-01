using UnityEngine;

public class vampireManager : MonoBehaviour
{
    [SerializeField] private GameObject vampirePrefab;
    [SerializeField] private float spawnRate = 9f;   //Delay between each spawn (the lower, the faster they spawn in)
    private float spawnTimer;
    private int spawnCount;

    public float vampireMoveSpeed = 0.75f;

    [SerializeField] private Quaternion spawnRotation = Quaternion.Euler(0f, 0f, 180f);

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnTimer = 0;
        spawnCount = 0;
    }

    // Update is called once per frame
    void Update()
    {

        spawnTimer -= Time.deltaTime;

        if (spawnTimer <= 0)
        {
            Instantiate(vampirePrefab, transform.position, spawnRotation);
            spawnCount++;

            if (spawnCount % 2 == 0 && spawnRate > 2.3)
            {
                spawnRate -= 0.55f;
                vampireMoveSpeed += 0.15f;
            }

            spawnTimer = spawnRate + Random.Range(0, 0.5f);
        }


    }
}
