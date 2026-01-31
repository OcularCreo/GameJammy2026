using UnityEngine;
using UnityEngine.Rendering;

public class Hand : MonoBehaviour
{
    private Vector3 mousePosition;
    [SerializeField] Texture2D handOpen;
    [SerializeField] Texture2D handClosed;

    private Vector2 handOpenOffset;
    private Vector2 handClosedOffset; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // find image height and width, for cursor to be center of the image
        handOpenOffset = new Vector2(handOpen.width / 2, handOpen.height / 2);
        handClosedOffset = new Vector2(handClosed.width / 2, handClosed.height / 2);

        // set cursor to image of the open hand
        Cursor.SetCursor(handOpen, handOpenOffset, CursorMode.ForceSoftware);

    }

    // Update is called once per frame
    void Update()
    {
        //mousePosition = Input.mousePosition;
        //mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        //transform.position = new Vector3(mousePosition.x, mousePosition.y, 0f);


        if (Input.GetMouseButtonDown(0)) // left click
        {
            Cursor.SetCursor(handClosed, handClosedOffset, CursorMode.ForceSoftware); // change cursor to hand closed
        }
        else if(Input.GetMouseButtonUp(0)) // release left mouse button
        {
            Cursor.SetCursor(handOpen, handOpenOffset, CursorMode.ForceSoftware); // cursor to hand open
        }

            
    }
}
