using Mono.Cecil.Cil;
using System.Runtime.CompilerServices;
using UnityEditor.Rendering;
using UnityEngine;

public class Vampire : MonoBehaviour
{
    [Header("Sprites")]
    [SerializeField] private Sprite[] unTreatedVampireSprites;
    [SerializeField] private Sprite[] TreatedVampireSprites;
    [SerializeField] private Sprite[] attackVampireSprites;

    private SpriteRenderer spriteRenderer;
    private Transform vtransform;

    private Items missingItem;
    private bool treated;
    private vampireManager vampireManager;

    [Header("Bobbing Variables")]
    [SerializeField] private float bobspeed;
    [SerializeField] private float maxAmp;
    [SerializeField] private float maxFreq;
    private float amp;
    private float freq;
    private Vector2 startPos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        missingItem = (Items)Random.Range(0, 5);                                //randomly choose what item is missing
        spriteRenderer = GetComponent<SpriteRenderer>();                        //get the instance's sprite renderer
        vtransform = GetComponent<Transform>();                                 //get instance transform

        vampireManager = FindAnyObjectByType<vampireManager>();

        treated = false;
        missingItem = (Items)Random.Range(0, 5);

        //ensure that the sprite renderer was found and set the sprite to the corresponding missing item
        if (spriteRenderer != null )
        {
            //spriteRenderer.sprite = unTreatedVampireSprites[(int)missingItem];
        }

        startPos = transform.position;

        amp = Random.Range(0.1f, maxAmp);
        freq = Random.Range(0.1f, maxFreq);
    }

    // Update is called once per frame
    void Update()
    {

        transform.Translate(Vector2.right * vampireManager.vampireMoveSpeed * Time.deltaTime);

        float newY = startPos.y + Mathf.Sin(Time.time * freq) * amp;

        transform.position = new Vector2 (transform.position.x, newY);

    }


    void recieveItem(Items givenItem)
    {
        if(givenItem == missingItem)
        {
            spriteRenderer.sprite = TreatedVampireSprites[(int)givenItem];
        } else
        {
            attack();
        }
    }

    void attack()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "killzone")
        {
            Debug.Log("collided");
            Destroy(gameObject);
        }
    }
}
