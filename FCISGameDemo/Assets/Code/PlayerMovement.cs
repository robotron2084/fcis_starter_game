using System;
using System.Collections;
using System.Collections.Generic;
using Code;
using com.enemyhideout.fsm;
using UnityEditorInternal;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rigidBody;
    [SerializeField] private float walkForce = 1.0f;
    [SerializeField] private float jumpForce = 40.0f;
    [SerializeField] private float jumpFudge = 0.1f;

    [SerializeField] private Transform visualRoot;

    private bool _onGround = true;

    private Vector2 _forceThisFrame;

    public enum PlayerStates
    {
        OnGround,
        Jumping
    }

    private EnemyFsm<PlayerStates> _fsm;
    void Start()
    {
        _fsm = new EnemyFsm<PlayerStates>(this);
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    void OnGround_Enter()
    {
        _onGround = true;
    }

    void OnGround_Update()
    {
        bool hasMoved = false;
        _forceThisFrame = Vector2.zero;
        hasMoved = UnitCore.MoveIfKeyPressed(KeyCode.RightArrow, _rigidBody, new Vector2(walkForce, 0.0f), hasMoved, ref _forceThisFrame);
        UnitCore.MoveIfKeyPressed(KeyCode.LeftArrow, _rigidBody, new Vector2(-walkForce, 0.0f), hasMoved, ref _forceThisFrame);
        UnitCore.OrientUnitBasedOnXVelocity(visualRoot, _rigidBody);
        UnitCore.ChangeStateIfKeyPressed(KeyCode.Space, _fsm, PlayerStates.Jumping);
    }

    void Jumping_Enter()
    {
        _onGround = false;
        _rigidBody.AddForce(new Vector2(0.0f, jumpForce));
    }

    void Jumping_Update()
    {
        bool hasMoved = false;
        _forceThisFrame = Vector2.zero;
        hasMoved = UnitCore.MoveIfKeyPressed(KeyCode.RightArrow, _rigidBody, new Vector2(walkForce * jumpFudge, 0.0f), hasMoved, ref _forceThisFrame);
        UnitCore.MoveIfKeyPressed(KeyCode.LeftArrow, _rigidBody, new Vector2(-walkForce * jumpFudge, 0.0f), hasMoved, ref _forceThisFrame);
        UnitCore.OrientUnitBasedOnXVelocity(visualRoot, _rigidBody);
        UnitCore.ChangeStateIfTrue(_onGround, _fsm, PlayerStates.OnGround);
    }


    void FixedUpdate()
    {
        _rigidBody.AddForce(_forceThisFrame);
    }

    /// <summary>
    /// Called by the feet collider when a collision occurs.
    /// </summary>
    public void OnFeetCollide(Collider2D col, Collision2D collision2D)
    {
        // TODO: Ensure that feet are on the ground, not hitting the 'side' of something.
        //Debug.Log($"{col} hit {collision2D.collider}");
        _onGround = true;
    }
}
