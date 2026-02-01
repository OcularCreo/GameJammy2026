using Mono.Cecil.Cil;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
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

    [SerializeField] private Items[] missingItems;
    private List<Items> list = null;

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
        
        spriteRenderer = GetComponent<SpriteRenderer>();            //get the instance's sprite renderer
        vampireManager = FindAnyObjectByType<vampireManager>();
        treated = false;

        missingItems = new Items[Random.Range(1, 6)];

        list = System.Enum.GetValues(typeof(Items))
                                  .Cast<Items>()
                                  .ToList();

        for(int i = 0; i < missingItems.Length; i++)
        {
            int index = Random.Range(0, list.Count);
            
            missingItems[i] = list[index];
            list.Remove(list[index]);
        }

        Debug.Log(missingItems);

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
