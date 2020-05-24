using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dissolve : MonoBehaviour
{
    Material material;

    bool isDisolving;
    float fade = 1f;

    private void Start()
    {
        material = GetComponent<SpriteRenderer>().material;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            isDisolving = true;

        if (isDisolving)
        {
            fade -= Time.deltaTime;
            if (fade <= 0f)
            {
                fade = 0f;
                isDisolving = false;
            }
            material.SetFloat("_Fade", fade);
        }
    }
}
