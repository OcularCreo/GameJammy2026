using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class Vampire : MonoBehaviour
{
    [Header("Sprites")]
    [SerializeField] private GameObject[] treatmentItems;
    [SerializeField] private GameObject[] cucumbers;
    [SerializeField] private GameObject[] oranges;
    [SerializeField] private Sprite[] attackVampireSprites;

    private SpriteRenderer spriteRenderer;

    [SerializeField] private List<Items> missingItems;

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
        vampireManager = FindAnyObjectByType<vampireManager>();     //get the vampiremanager script
        treated = false;                                            //start treated as faulse

        //missingItems = new Items[Random.Range(1, 4)];   //Randomly choose how many missing items (1-3)
        selectMissing(Random.Range(1,4));             //Call select missing items function to choose what's missing

        //ensure that the sprite renderer was found and set the sprite to the corresponding missing item
        if (spriteRenderer != null )
        {
            //spriteRenderer.sprite = unTreatedVampireSprites[(int)missingItem];
        }

        startPos = transform.position;

        amp = Random.Range(0.1f, maxAmp);
        freq = Random.Range(0.1f, maxFreq);
    }

    //helper function to choose what items are missing
    void selectMissing(int num)
    {
        //List contains pool of items to choose from - useful for choosing unique items
        List<Items> itemPool = new List<Items>() {Items.towel, Items.cucumber, Items.orange, Items.mask};

        bool missingEyePiece = false;
        int missingEye = Random.Range(0, 2);        //choose which potential eye will be missing the eye piece

        //populate each slot in the missingItems array
        for (int i = 0; i < num; i++)
        {

            //end loop if there are no more items in the pool to choose from
            if (itemPool.Count == 0)
            {
                break;
            }

            int idx = Random.Range(0, itemPool.Count);  //pick a random existing index in the item pool
            Items selectedItem = itemPool[idx];         //Save the selected item
            missingItems.Add(selectedItem);             //add the selected item to the missingItems array    

            itemPool.RemoveAt(idx);                     //the selected missing item from the pool to ensure it is not picked again

            //if an orange is picked we must remove cucumber from the pool and vice versa
            switch (selectedItem)
            {
                case Items.orange:
                    /*
                    itemPool.Remove(Items.cucumber);
                    treatmentItems[(int)Items.orange].SetActive(true);
                    oranges[missingEye].SetActive(false); 
                    treatmentItems[(int)Items.cucumber].SetActive(false); */
                    missingEyePiece = setupEyes(treatmentItems[(int)Items.orange], treatmentItems[(int)Items.cucumber], oranges, missingEye);
                    break;
                case Items.cucumber:
                    itemPool.Remove(Items.orange);
                    /*
                    treatmentItems[(int)Items.orange].SetActive(false);
                    cucumbers[missingEye].SetActive(false);
                    treatmentItems[(int)Items.cucumber].SetActive(true);*/
                    missingEyePiece = setupEyes(treatmentItems[(int)Items.cucumber], treatmentItems[(int)Items.orange], cucumbers, missingEye);
                    break;
                default:
                    treatmentItems[(int)selectedItem].SetActive(false);
                    break;
            }
        }

        //if there aren't any missing eye pieces randomly choose one to put on
        if(!missingEyePiece)
        {
            if(missingEye == 0)
            {
                treatmentItems[(int)Items.orange].SetActive(false);
            } else
            {
                treatmentItems[(int)Items.orange].SetActive(false);
            }
        }
    
    }

    bool setupEyes(GameObject toActivate, GameObject toDeactive, GameObject[] eyes, int deactiveEyeIdx)
    {
        toActivate.SetActive(true);
        toDeactive.SetActive(false);
        eyes[deactiveEyeIdx].SetActive(false);
        return true;
    }

    // Update is called once per frame
    void Update()
    {

        transform.Translate(Vector2.right * vampireManager.vampireMoveSpeed * Time.deltaTime);

        float newY = startPos.y + Mathf.Sin(Time.time * freq) * amp;

        transform.position = new Vector2 (transform.position.x, newY);

    }


    public void recieveItem(Items givenItem)
    {

        if (missingItems.Contains(givenItem))
        {
            missingItems.Remove(givenItem);

            if(givenItem == Items.orange)
            {
                oranges[0].SetActive(true);
                oranges[1].SetActive(true);
            } else if(givenItem == Items.cucumber)
            {
                cucumbers[0].SetActive(true);
                cucumbers[1].SetActive(true);
            } else
            {
                treatmentItems[(int)givenItem].SetActive(true);
            }
                
            //make item visible
            Debug.Log("Gave the CORRECT ITEM");
        } else
        {
            Debug.Log("Gave the INCORRECT item - I want to suck your finger");
            attack();
        }

        if(missingItems.Count == 0)
        {
            Debug.Log("Happy vampire client");
            treated = true;
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

            if (!treated)
            {
                //attack
            }
        }
    }
}
