using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    Transform player;

    private float minDis = 0.01f;
    [SerializeField]
    private float followingDistance = 2f;
    [SerializeField]
    private float speed = 50f;

    private IEnumerator myCoroutine;

    private void Awake()
    {
        player = FindObjectOfType<Player>().transform;
    }
    private void Update()
    {
        if (Mathf.Abs(transform.position.x - player.transform.position.x) > followingDistance ||
            Mathf.Abs(transform.position.y - player.transform.position.y) > followingDistance/2)
        {
            if (myCoroutine == null)
            {
                myCoroutine = Follow();
                StartCoroutine(myCoroutine);
            }
            //Debug.Log(myCoroutine);
        }
    }

    private IEnumerator Follow()
    {
        while (Mathf.Abs(transform.position.x - player.transform.position.x) > minDis ||
            Mathf.Abs(transform.position.y - player.transform.position.y) > minDis)
        {
            transform.position = Vector3.Lerp(transform.position,
                new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z), Time.deltaTime * speed);
                yield return null;
        }
        myCoroutine = null;
        yield return null;
    }
}
