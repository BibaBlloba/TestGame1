using UnityEngine;
using System.Collections.Generic;
using System.Collections;

[SelectionBase]
public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }
    
    private Rigidbody2D _rb;
    private float minMovingSpeed = 0.1f;
    private bool isRunning = false;
    [SerializeField] private float movingSpeed;
    Vector2 inputVector;

    private void Awake()
    {
        Instance = this;
        
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        GameInput.Instance.OnPlayerAttack += PlayerOnPlayerAttack;
    }

    private void PlayerOnPlayerAttack(object sender, System.EventArgs e)
    {
        ActiveWeapon.Instance.GetActiveWeapon().Attack();
    }
    
    private void Update()
    {
        inputVector = GameInput.Instance.GetInputVector();
    }
    
    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        // inputVector = inputVector.normalized;
        // Debug.DrawRay(transform.position, inputVector, Color.red);
        // Debug.Log(inputVector);
        
        _rb.MovePosition(_rb.position + inputVector * (Time.fixedDeltaTime * movingSpeed));
        if (Mathf.Abs(inputVector.x) > minMovingSpeed || Mathf.Abs(inputVector.x) > minMovingSpeed)
        {
            isRunning = true;
        }
        else isRunning = false;
    }

    public bool IsRunning()
    {
        return isRunning;
    }

    public Vector2 GetPlayerScreenPosition()
    {
        Vector3 playerScreenPosition = Camera.main.WorldToScreenPoint(transform.position);
        return playerScreenPosition;
    }
}