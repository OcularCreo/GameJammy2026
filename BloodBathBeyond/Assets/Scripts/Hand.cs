using System.Drawing;
using UnityEngine;
using UnityEngine.Rendering;

public class Hand : MonoBehaviour
{
    private Vector3 mousePosition;
    [SerializeField] Texture2D handOpen;
    [SerializeField] Texture2D handClosed;
    [SerializeField] Texture2D handCucumber;

    private Vector2 handOpenOffset;
    private Vector2 handClosedOffset;
    private Vector2 handCucumberOffset;

    [SerializeField] GameObject bin;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // find image height and width, for cursor to be center of the image
        handOpenOffset = new Vector2(handOpen.width / 2, handOpen.height / 2);
        handClosedOffset = new Vector2(handClosed.width / 2, handClosed.height / 2);
        handCucumberOffset = new Vector2(handCucumber.width / 2, handCucumber.height / 2);

        // set cursor to image of the open hand
        Cursor.SetCursor(handOpen, handOpenOffset, CursorMode.ForceSoftware);

    }

    // Update is called once per frame
    void Update()
    {
        mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);


        if (Input.GetMouseButtonDown(0)) // left click
        {
            Cursor.SetCursor(handClosed, handClosedOffset, CursorMode.ForceSoftware); // change cursor to hand closed

            if (bin.GetComponent<Collider2D>().OverlapPoint(mousePosition))
            {
                Cursor.SetCursor(handCucumber, handCucumberOffset, CursorMode.ForceSoftware);
            }
        }
        else if(Input.GetMouseButtonUp(0)) // release left mouse button
        {
            Cursor.SetCursor(handOpen, handOpenOffset, CursorMode.ForceSoftware); // cursor to hand open
        }

            
    }
}
