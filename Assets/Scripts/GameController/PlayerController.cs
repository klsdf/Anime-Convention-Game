using UnityEngine;
using JoystickPack;
using DigitalRubyShared;
using DG.Tweening;

namespace ACG{
public class PlayerController : MonoBehaviour
{
    private bool isMoving = false;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private Tween move;


    public enum InputType
    {
        Joystick,
        Gesture
    }

    public InputType inputType;
    [SerializeField] Collider2D groundCollider;
    [SerializeField] VariableJoystick variableJoystick;
    [Range(0.5f,6.0f)] public float speed = 1.0f;

    private void Start() {
        if(inputType == InputType.Gesture){
            GestureManager.Instance.RegisterGestureDelegate(GestureManager.GestureRecognizerType.SingleFingerSingleTap, MoveByFigerTap);
            variableJoystick.gameObject.SetActive(false);
        }
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        OnMoveAnime();
    }
        // Start is called before the first frame update
    public void FixedUpdate()
    {
        if(inputType == InputType.Joystick)
            MoveByJoystick();
    }
    public void MoveByJoystick()
    {
        Vector3 direction = Vector3.up * variableJoystick.Vertical + Vector3.right * variableJoystick.Horizontal;
        var bounds = groundCollider.bounds;
        var target = transform.position + direction * speed * Time.deltaTime;
        isMoving = direction.magnitude > 0.1f;
        if(bounds.Contains(new Vector2(target.x,target.y)))
        {
            transform.Translate(direction * speed * Time.deltaTime);
            spriteRenderer.flipX = direction.x < 0;
        }
    }

    public void OnMoveAnime()
    {
        animator.SetBool("isRun",isMoving);
    }

    public void MoveByFigerTap(GestureRecognizer gesture)
    {
        if (gesture.State == GestureRecognizerState.Ended)
        {
            Debug.Log($"Tap at {gesture.FocusX},{gesture.FocusY}");
            RaycastHit2D hit = Physics2D.GetRayIntersection(
                Camera.main.ScreenPointToRay(new Vector3(gesture.FocusX, gesture.FocusY,0)),Mathf.Infinity,layerMask:1<<LayerMask.NameToLayer("Ground"));
            if(hit)
            {
                var duration = Vector3.Distance(transform.position, hit.point) / speed;
                move = DOTween.To(() => transform.position, x => transform.position = x, (Vector3)hit.point,duration).SetEase(Ease.Linear);
                move.Restart();
            }
        }
    }
}

}