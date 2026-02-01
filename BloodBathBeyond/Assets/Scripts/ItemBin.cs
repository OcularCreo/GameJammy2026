using Unity.VisualScripting;
using UnityEngine;

public class ItemBin : MonoBehaviour
{

    [SerializeField] private Sprite[] binSprites;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Items itemType;

    public int ItemCount = 10;

    public void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();    //get the instance's sprite renderer
        spriteRenderer.sprite = binSprites[(int)itemType];  //assign the correct sprite based on what the instance's type is set to
    }

    //call when hand picks up item from bin
    public Items RemoveItem() 
    {
        if (ItemCount > 0)      //if the bin is not already empty
        {
            if (ItemCount == 0) //if the click emptied the bin, turn its sprite to empty
            {
                spriteRenderer.sprite = binSprites[5];
            }
        }
        return (itemType);
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    RemoveItem();
        //}
    }
}
