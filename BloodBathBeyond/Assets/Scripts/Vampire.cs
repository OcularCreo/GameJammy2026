using Mono.Cecil.Cil;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Vampire : MonoBehaviour
{
    [SerializeField] private Sprite[] unTreatedVampireSprites;
    [SerializeField] private Sprite[] TreatedVampireSprites;
    [SerializeField] private Sprite[] attackVampireSprites;

    private SpriteRenderer spriteRenderer;
    private Transform vtransform;

    private Items missingItem;
    [SerializeField] private float speed;

    private bool treated;
    private vampireManager vampireManager;
    [SerializeField] private float amp;
    [SerializeField] private float freq;

    private Vector2 startPos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        missingItem = (Items)Random.Range(0, 5);                                //randomly choose what item is missing
        spriteRenderer = GetComponent<SpriteRenderer>();                        //get the instance's sprite renderer
        vtransform = GetComponent<Transform>();                                  //get instance transform

        vampireManager = FindAnyObjectByType<vampireManager>();

        //ensure that the sprite renderer was found and set the sprite to the corresponding missing item
        if(spriteRenderer != null )
        {
            //spriteRenderer.sprite = unTreatedVampireSprites[(int)missingItem];
        }

        treated = false;
        startPos = transform.position;

        amp = Random.Range(0.25f, 0.6f);
        freq = Random.Range(0.25f, 0.75f);
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
}
