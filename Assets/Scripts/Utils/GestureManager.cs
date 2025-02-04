using System.Collections;
using System.Collections.Generic;
using DigitalRubyShared;


namespace ACG{
public class GestureManager:Singleton<GestureManager>
{
    public enum GestureRecognizerType
    {
        SingleFingerSingleTap,
        SingleFingerDoubleTap,
        LongPress,
        Debug
    }

    private const string TAG = "GestureManager";
    private readonly Dictionary<GestureRecognizerType, GestureRecognizer> _gestureRecognizers = 
        new Dictionary<GestureRecognizerType, GestureRecognizer>();


    public void Awake()
    {
        CreateSingleFingerSingleTapGesture();
        CreateLongPressGesture();
    }

    public void RegisterGestureDelegate(GestureRecognizerType gestureRecognizerType, GestureRecognizerStateUpdatedDelegate gestureDelegate)
    {
       if(_gestureRecognizers.TryGetValue(gestureRecognizerType, out var gestureRecognizer))
       {
           gestureRecognizer.StateUpdated += gestureDelegate;
       }
    }
    public void UnregisterGestureDelegate(GestureRecognizerType gestureRecognizerType, GestureRecognizerStateUpdatedDelegate gestureDelegate)
    {
        if(_gestureRecognizers.TryGetValue(gestureRecognizerType, out var gestureRecognizer))
        {
            gestureRecognizer.StateUpdated -= gestureDelegate;
        }
    }
    private void CreateSingleFingerSingleTapGesture()
    {
        var singleFingerSingleTapGesture = new TapGestureRecognizer();
        FingersScript.Instance?.AddGesture(singleFingerSingleTapGesture);
        _gestureRecognizers.Add(GestureRecognizerType.SingleFingerSingleTap, singleFingerSingleTapGesture);
    }

    void CreateLongPressGesture()
    {
        var longPressGesture = new LongPressGestureRecognizer();
        longPressGesture.MaximumNumberOfTouchesToTrack = 1;
        FingersScript.Instance?.AddGesture(longPressGesture);
        _gestureRecognizers.Add(GestureRecognizerType.LongPress, longPressGesture);
    }

   
}

}