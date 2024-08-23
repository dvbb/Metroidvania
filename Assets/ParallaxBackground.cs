using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    private GameObject camera;

    [SerializeField] private float parallaxEffect = .9f;

    private float xPosition;
    private float length;

    // Start is called before the first frame update
    void Start()
    {
        camera = GameObject.Find("Main Camera");

        length = GetComponentInChildren<SpriteRenderer>().bounds.size.x;
        xPosition = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToMove = camera.transform.position.x * parallaxEffect;
        float distanceMoved = camera.transform.position.x * (1 - parallaxEffect);
        transform.position = new Vector3(xPosition + distanceToMove, transform.position.y, 0);

        //if (distanceMoved > xPosition + length)
        //    xPosition = xPosition + length;
        //else if (distanceMoved < xPosition - length)
        //    xPosition = xPosition - length;

    }
}
