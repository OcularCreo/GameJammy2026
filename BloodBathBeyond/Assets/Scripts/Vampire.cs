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
    [SerializeField] private GameObject[] towelSprites;
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

        //towel sprite at index 0 will be known as the sprite that includes the towel
        towelSprites[0].SetActive(true);
        towelSprites[1].SetActive(false);

        selectMissing(Random.Range(1,4));                 //Call select missing items function to choose what's missing

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
                    itemPool.Remove(Items.cucumber);
                    missingEyePiece = setupEyes(treatmentItems[(int)Items.orange], treatmentItems[(int)Items.cucumber], oranges, missingEye);
                    break;
                case Items.cucumber:
                    itemPool.Remove(Items.orange);
                    missingEyePiece = setupEyes(treatmentItems[(int)Items.cucumber], treatmentItems[(int)Items.orange], cucumbers, missingEye);
                    break;
                case Items.towel:
                    toggleTowel();
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
                treatmentItems[(int)Items.cucumber].SetActive(false);
            }
        }
    
    }

    void toggleTowel()
    {
        towelSprites[0].SetActive(!towelSprites[0].activeSelf);
        towelSprites[1].SetActive(!towelSprites[0].activeSelf);
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

        transform.Translate(Vector2.right * vampireManager.vampireMoveSpeed * Time.deltaTime, Space.World);

        float newY = startPos.y + Mathf.Sin(Time.time * freq) * amp;

        transform.position = new Vector2 (transform.position.x, newY);

    }

    //public funciton that should be called when the hand tries to give an item to a vampire instance
    public void recieveItem(Items givenItem)
    {
        //check if the given item matches any missing items
        if (missingItems.Contains(givenItem))
        {
            //if it does match remove it from the list of missing items
            missingItems.Remove(givenItem);

            //set the correct item to be active on the vampire
            switch(givenItem)
            {
                case Items.orange:
                    setActiveEyes(oranges, true);
                    break;
                case Items.cucumber:
                    setActiveEyes(cucumbers, true);
                    break;
                case Items.towel:
                    toggleTowel();
                    break;
                default:
                    treatmentItems[(int)givenItem].SetActive(true);
                    break;
            }
            
        } else
        {
            Debug.Log("Gave the INCORRECT item - I want to suck your finger");
            attack();
        }

        //if there are no longer any more missing items then set treated to true
        if(missingItems.Count == 0)
        {
            Debug.Log("Happy vampire client");
            treated = true;
        }

    }

    //helper function to toggle all eyes on or off
    void setActiveEyes(GameObject[] eyes, bool active)
    {
        foreach (GameObject eye in eyes)
        {
            eye.SetActive(active);
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
