using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarfieldController : MonoBehaviour
{
    [SerializeField]
    MeshRenderer meshRenderer;
    [SerializeField]
    float starfieldSpeed;

    Vector2 offset;

    void Update()
    {
        offset = meshRenderer.material.mainTextureOffset;
        offset.x += starfieldSpeed * Time.deltaTime;
        meshRenderer.material.mainTextureOffset = offset;
    }
}
