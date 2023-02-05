using UnityEngine;

public class CursorHandler : MonoBehaviour
{
    public Texture2D cursorOpen;
    public Texture2D cursorClose;
    public Texture2D cursorPointer;
    // Start is called before the first frame update
    private void Start()
    {
        SetCursorOpen();
    }

    public void SetCursorOpen()
    {
        Cursor.SetCursor(cursorOpen, new Vector2(cursorOpen.width / 2, cursorOpen.height / 2), CursorMode.ForceSoftware);
    }

    public void SetCursorClosed()
    {
        Cursor.SetCursor(cursorClose, new Vector2(cursorClose.width / 2, cursorClose.height / 2), CursorMode.ForceSoftware);
    }

    public void SetCursorPointer()
    {
        Cursor.SetCursor(null, new Vector2(0, 0), CursorMode.Auto);
    }
}
