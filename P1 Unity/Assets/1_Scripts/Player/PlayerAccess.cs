using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAccess : MonoBehaviour
{
    private PlayerAnimation playerAnimation;
    private PlayerAttackController playerAttackController;
    private PlayerLifeComponent playerLifeComponent;
    private PlayerMovementManager playerMovementManager;
    private Transform myTransform;
    private Rigidbody2D myRigidbody;
    private InputManager input;
    static private PlayerAccess _me;

    public PlayerAnimation Animation { get => playerAnimation; }
    public PlayerAttackController Attack { get => playerAttackController; }
    public PlayerLifeComponent Life { get => playerLifeComponent; }
    public PlayerMovementManager Movement { get => playerMovementManager; }
    public Transform Transform { get => myTransform; }
    public Rigidbody2D Rigidbody { get => myRigidbody; }
    public InputManager Input { get => input; }
    static public PlayerAccess Instance { get => _me; }

    void Awake() {
        _me = this;
        playerAnimation = GetComponent<PlayerAnimation>();
        playerAttackController = GetComponent<PlayerAttackController>();
        playerLifeComponent = GetComponent<PlayerLifeComponent>();
        playerMovementManager = GetComponent<PlayerMovementManager>();
        myTransform = transform;
        myRigidbody = GetComponent<Rigidbody2D>();
        input = GetComponent<InputManager>();
    }
}
