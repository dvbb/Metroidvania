using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    private GameObject camera;

    [SerializeField] private float parallaxEffect = .9f;

    private float xPosition;

    // Start is called before the first frame update
    void Start()
    {
        camera = GameObject.Find("Main Camera");
        xPosition = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToMove = camera.transform.position.x * parallaxEffect;
        transform.position = new Vector3(xPosition + distanceToMove, transform.position.y, 0);
    }
}
