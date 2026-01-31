using UnityEngine;
using UnityEngine.Rendering;

public class Hand : MonoBehaviour
{
    private Vector3 mousePosition;
    [SerializeField] Texture2D cursor;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //mousePosition = Input.mousePosition;
        //mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        //transform.position = new Vector3(mousePosition.x, mousePosition.y, 0f);


        Cursor.SetCursor(cursor, Vector2.zero, CursorMode.ForceSoftware);
    }
}
