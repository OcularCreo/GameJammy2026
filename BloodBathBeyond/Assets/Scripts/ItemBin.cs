//using System.Collections;
//using System.Collections.Generic;
//using Unity.VisualScripting;
using UnityEngine;

//[CreateAssetMenu(menuName = "Scriptable Objects/Items")]
public class ItemBin : MonoBehaviour
{

    [SerializeField] private enum ItemType { Cucumber = 0,
                                             Orange = 1,
                                             Lemon = 2,
                                             PinkMask = 3,
                                             GreenMask = 4,
                                             Towel = 5 }

    [SerializeField] private Sprite[] itemSprites;
    [SerializeField] private SpriteRenderer spriteRenderer;

    [SerializeField] private ItemType itemType;
    private int ItemCount = 10;

    public void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();    //get the instance's sprite renderer

        //assign the correct sprite based on what the instance's type is set to
        Debug.Log((int)itemType);
        spriteRenderer.sprite = itemSprites[(int)itemType];
    }

    //call when hand picks up item from bin
    public void RemoveItem() 
    {

        if (ItemCount > 0)
        {
            ItemCount--;
            Debug.Log("Decrement");
        }

        Debug.Log(ItemCount);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            RemoveItem();
        }
    }
}
