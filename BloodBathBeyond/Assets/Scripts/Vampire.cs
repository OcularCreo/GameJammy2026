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
    }

    // Update is called once per frame
    void Update()
    {
        vtransform.Translate(Vector2.right * vampireManager.vampireMoveSpeed * Time.deltaTime);
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
