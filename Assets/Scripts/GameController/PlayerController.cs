using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JoystickPack;
using DigitalRubyShared;


namespace ACG{

public class PlayerController : MonoBehaviour
{
    public enum InputType
    {
        Joystick,
        Gesture
    }
    [SerializeField] InputType inputType;
    [SerializeField] VariableJoystick variableJoystick;
    [Range(0.5f,6.0f)] 
    public float speed = 1.0f;
    private void Start() {
        if(inputType == InputType.Gesture)
            GestureManager.Instance.RegisterGestureDelegate(GestureManager.GestureRecognizerType.SingleFingerSingleTap, MoveByFigerTap);
    }

    // Start is called before the first frame update
    public void FixedUpdate()
    {
        if(inputType == InputType.Joystick)
            MoveByJoystick();
    }
    public void MoveByJoystick()
    {
        Vector3 direction = Vector3.forward * variableJoystick.Vertical + Vector3.right * variableJoystick.Horizontal;
        transform.Translate(direction * speed * Time.fixedDeltaTime);
    }

    public void MoveByFigerTap(GestureRecognizer gesture)
    {
        if (gesture.State == GestureRecognizerState.Ended)
        {
            Debug.Log($"Tapped at {gesture.FocusX}, { gesture.FocusY}");
        }
    }
}

}