using UnityEngine;
using JoystickPack;
using DigitalRubyShared;
using DG.Tweening;



namespace ACG{
public class PlayerController : MonoBehaviour
{
    public enum InputType
    {
        Joystick,
        Gesture
    }

    public InputType inputType;
    [SerializeField] VariableJoystick variableJoystick;
    [SerializeField] BoxCollider2D groundCollider;
    [Range(0.5f,6.0f)] public float speed = 1.0f;
    Tween move;
    private void Start() {
        if(inputType == InputType.Gesture){
            GestureManager.Instance.RegisterGestureDelegate(GestureManager.GestureRecognizerType.SingleFingerSingleTap, MoveByFigerTap);
            variableJoystick.gameObject.SetActive(false);
        }
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
        if(bounds.Contains(new Vector2(target.x,target.y)))
            transform.Translate(direction * speed * Time.deltaTime);
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