using UnityEngine;

public class PlayerVisual : MonoBehaviour
{
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Vector2 lastDirection;
    
    private const string IS_RUNNING_ANIM_PARAM = "IsRunning";
    private const string XInput_ANIM_PARAM = "XInput";
    private const string YInput_ANIM_PARAM = "YInput";

    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        UpdateMovementAnimation();
        AdjustPlayerFacingDirection();
    }

    private void UpdateMovementAnimation()
    {
        Vector2 movementInput = GameInput.Instance.GetInputVector();
        animator.SetBool(IS_RUNNING_ANIM_PARAM, Player.Instance.IsRunning());

        if (movementInput != Vector2.zero)
        {
            lastDirection = movementInput;
            
            animator.SetBool(IS_RUNNING_ANIM_PARAM, true);
            animator.SetFloat(XInput_ANIM_PARAM, movementInput.x);
            animator.SetFloat(YInput_ANIM_PARAM, movementInput.y);
        }
        else
        {
            animator.SetBool(IS_RUNNING_ANIM_PARAM, false);
            animator.SetFloat(XInput_ANIM_PARAM, lastDirection.x);
            animator.SetFloat(YInput_ANIM_PARAM, lastDirection.y);
        }
    }

    private void AdjustPlayerFacingDirection()
    {
        Vector3 mousePos = GameInput.Instance.GetMousePosition();
        Vector3 playerPosition = Player.Instance.GetPlayerScreenPosition();

        if (mousePos.x < playerPosition.x)
        {
            spriteRenderer.flipX = true;
        }
        else spriteRenderer.flipX = false;
    }
    
}
