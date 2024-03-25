using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CursorLock : MonoBehaviour
{
    private void Start()
    {
        LockCursor();
    }

    [MenuItem("Utilities/Cursor/Cursor Lock %#&c")]
    private static void LockCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
