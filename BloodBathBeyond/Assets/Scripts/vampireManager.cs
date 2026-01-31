using UnityEngine;

public class vampireManager : MonoBehaviour
{
    [SerializeField] private GameObject vampirePrefab;
    [SerializeField] private float spawnRate = 5f;
    private float spawnTimer;

    public float vampireMoveSpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnTimer = 0;
        vampireMoveSpeed = 0.75f;
    }

    // Update is called once per frame
    void Update()
    {

        spawnTimer -= Time.deltaTime;

        if (spawnTimer <= 0){
            Instantiate(vampirePrefab, transform.position, Quaternion.identity);
            spawnTimer = spawnRate + Random.Range(0, 3f);
        } 
    }
}
