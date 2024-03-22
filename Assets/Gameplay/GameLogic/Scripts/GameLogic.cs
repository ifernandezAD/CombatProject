using UnityEngine;

[DefaultExecutionOrder(-100)]
public class GameLogic : MonoBehaviour
{
    public static GameLogic instance { get; private set; }

    public Transform playerTransfrom { get; private set; }


    private void Awake()
    {
        instance = this;

        playerTransfrom = GameObject.FindWithTag("Player").transform;
    }


}
