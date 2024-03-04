using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCharacterPositionForSeeThroughShader : MonoBehaviour
{
    void Update()
    {
        Shader.SetGlobalVector("CharacterPosition", transform.position);
    }
}
