using UnityEngine;

public class CursorLock : MonoBehaviour
{
    public static CursorLock instance { get; private set; }
    [SerializeField] bool isLocked;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        if (isLocked) { LockCursor(); }
        else { UnlockCursor(); }
    }


    private static void LockCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private static void UnlockCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
