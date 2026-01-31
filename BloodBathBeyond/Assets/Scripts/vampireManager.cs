using UnityEngine;

public class vampireManager : MonoBehaviour
{
    [SerializeField] private GameObject vampirePrefab;
    [SerializeField] private float spawnRate = 5f;
    private float spawnTimer;
    private int spawnCount;

    public float vampireMoveSpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnTimer = 0;
        spawnCount = 0;
        vampireMoveSpeed = 0.75f;
    }

    // Update is called once per frame
    void Update()
    {

        spawnTimer -= Time.deltaTime;

        if (spawnTimer <= 0){
            Instantiate(vampirePrefab, transform.position, Quaternion.identity);
            spawnCount++;

            if(spawnCount % 5 == 0 && spawnRate > 2)
            {
                spawnRate -= 2;
                vampireMoveSpeed += 0.25f;
            }

            spawnTimer = spawnRate + Random.Range(0, 3f);
        } 

         
    }
}
