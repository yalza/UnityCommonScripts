using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    public Camera cam;
    public Transform followTarget;

    // Starting position for the parallax game object
    private Vector2 _startingPosition;

    // Start Z value of the parallax game object
    private float _startingZ;

    // Distance that the came ra has moved from the starting position of the parallax object
    private Vector2 _camMoveSinceStart => (Vector2)cam.transform.position - _startingPosition;

    private float _zDistanceFromTarget => transform.position.z - followTarget.position.z;

    // If object is in front of target , use near clip plane . if behind target , user far clip plane
    private float _clippingPlane => (cam.transform.position.z + (_zDistanceFromTarget > 0 ? cam.farClipPlane : cam.nearClipPlane));

    // The futher the object from the player , use faster the ParallaxEffect object will move . Drag it's Z value closer to the target to make it move slower
    private float _parallaxFactor => Mathf.Abs(_zDistanceFromTarget) / _clippingPlane;



    private void Start()
    {
        _startingPosition = transform.position;
        _startingZ = transform.position.z;  
    }

    private void Update()
    {
        // When the target moves , move the parallax object the same distance times a multiplier
        Vector2 newPosition = _startingPosition + _camMoveSinceStart * _parallaxFactor;

        // The X/Y position changes based on target travel speed times the parallax factor , but z stay consistent  
        transform.position = new Vector3(newPosition.x, newPosition.y, _startingZ);
    }
}
