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
        Cursor.SetCursor(cursorOpen, new Vector2(cursorOpen.width * 0.4f, cursorOpen.height * 0.4f), CursorMode.ForceSoftware);
    }

    public void SetCursorClosed()
    {
        Cursor.SetCursor(cursorClose, new Vector2(cursorClose.width * 0.35f, cursorClose.height * 0.15f), CursorMode.ForceSoftware);
    }

    public void SetCursorPointer()
    {
        Cursor.SetCursor(cursorPointer, new Vector2(0,0), CursorMode.ForceSoftware);
    }
}
