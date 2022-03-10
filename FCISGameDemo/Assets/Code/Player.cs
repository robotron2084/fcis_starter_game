using System.Collections;
using System.Collections.Generic;
using Code;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rBod;
    [SerializeField] private float force = 1.0f;

    [SerializeField]private Transform visualRoot;
    
    // Start is called before the first frame update
    void Start()
    {
        rBod = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        bool hasMoved = false;
        hasMoved = UnitCore.MoveIfKeyPressed(KeyCode.RightArrow, rBod, force, hasMoved);
        UnitCore.MoveIfKeyPressed(KeyCode.LeftArrow, rBod, -force, hasMoved);
        UnitCore.OrientUnitBasedOnXVelocity(visualRoot, rBod);
        
    }
}
