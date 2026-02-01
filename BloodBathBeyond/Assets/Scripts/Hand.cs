using System.Drawing;
using UnityEngine;
using UnityEngine.Rendering;

public class Hand : MonoBehaviour
{
    private Vector3 mousePosition;
    [SerializeField] Texture2D handOpen;
    [SerializeField] Texture2D handClosed;
    [SerializeField] Texture2D handCucumber;
    [SerializeField] Texture2D handOrange;

    private Vector2 handOpenOffset;
    private Vector2 handClosedOffset;
    private Vector2 handCucumberOffset;
    private Vector2 handOrangeOffset;

    //[SerializeField] GameObject bin;
    [SerializeField] GameObject[] bins;
    //private int binQuantity;
    private Items itemType;
    private bool itemHeld = false;
    //[SerializeField] private ItemBin itemBin;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // find image height and width, for cursor to be center of the image
        handOpenOffset = new Vector2(handOpen.width / 2, handOpen.height / 2);
        handClosedOffset = new Vector2(handClosed.width / 2, handClosed.height / 2);
        handCucumberOffset = new Vector2(handCucumber.width / 2, handCucumber.height / 2);
        handOrangeOffset = new Vector2(handOrange.width / 2, handOrange.height / 2);

        // set cursor to image of the open hand
        Cursor.SetCursor(handOpen, handOpenOffset, CursorMode.ForceSoftware);

        // store all bins in the bins array
        bins = GameObject.FindGameObjectsWithTag("Bin");
        //binQuantity = bins.Length;

    }

    // Update is called once per frame
    void Update()
    {
        mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);


        if (Input.GetMouseButtonDown(0)) // left click
        {
            Cursor.SetCursor(handClosed, handClosedOffset, CursorMode.ForceSoftware); // change cursor to hand closed

            foreach (GameObject bin in bins) // go through all bins
            {
                
                if (bin.GetComponent<Collider2D>().OverlapPoint(mousePosition)) // if mouse overlaps with a bin
                {
                    if (bin.GetComponent<ItemBin>().ItemCount > 0) // if the bin isn't empty
                    {
                        itemType = bin.GetComponent<ItemBin>().RemoveItem(); // remove one item from bin and get the bin type
                        //Debug.Log(bin.GetComponent<ItemBin>().ItemCount);
                        itemHeld = true;
                        // set the cursor image to holding the correct item
                        switch (itemType)
                        {
                            case Items.orange:
                                Cursor.SetCursor(handOrange, handOrangeOffset, CursorMode.ForceSoftware);
                                break;
                            case Items.cucumber:
                                Cursor.SetCursor(handCucumber, handCucumberOffset, CursorMode.ForceSoftware);
                                break;
                        }
                    }
                    
                }
            }

        }
        else if(Input.GetMouseButtonUp(0)) // release left mouse button
        {
            Cursor.SetCursor(handOpen, handOpenOffset, CursorMode.ForceSoftware); // cursor to hand open
            if (itemHeld)
            {
                itemHeld = false;
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                Debug.Log(hit.collider.gameObject.name);
                if (hit.collider.gameObject.name == "vampire(Clone)")
                {
                    hit.collider.gameObject.GetComponent<Vampire>().RecieveItem(itemType);
                    Debug.Log("Item dropped on vampire!");
                }
            }
        }

            
    }

}
