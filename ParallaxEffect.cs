using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    public Camera cam;
    public Transform followTarget;

    // Starting position for this parallax game object
    Vector2 startingPosition;

    // Starting Z value of the parallax game object
    float startingZ;

    // Distance that the camera has movedd from the starting position of the parallax object
    Vector2 camMoveSinceStart =>(Vector2)cam.transform.position - startingPosition;

    float zDistanceFromTarget => transform.position.z - followTarget.transform.position.z;

    float clippingPlane => (cam.transform.position.z + (zDistanceFromTarget > 0 ? cam.farClipPlane : cam.nearClipPlane));

    float parallaxFactor => Mathf.Abs(zDistanceFromTarget) / clippingPlane;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        // when the target moves, move the parallax object the same distance times a multiplier
        Vector2 newPostion = startingPosition + camMoveSinceStart * parallaxFactor;

        // The X/Y position changes based on target travel speed times the parallax factor, but Z stays consistent
        transform.position = new Vector3(newPostion.x, newPostion.y, startingZ);

    }
}
