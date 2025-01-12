using UnityEngine;

namespace ACG
{
    /// <summary>
    /// 需要attach到gameobject，继承MObject, 否则EventObject
    /// 如果对象继承了其他类，可以将EventObject作为内部成员，在对象被销毁的时候，需要手动调用
    /// EventObject.ClearAllListener.
    /// TODO: 后续可以增加Broadcast异步能力
    /// </summary>
    public class MObject : MonoBehaviour, MInterface
    {
        protected virtual void OnDestroy()
        {
            ClearAllListener();
        }

        #region Event Listener
        public void Broadcast(int eventType)
        {
            EventCenter.Broadcast(this, eventType);
        }
        public void Broadcast<T>(int eventType, T arg1)
        {
            EventCenter.Broadcast(this, eventType, arg1);
        }
        public void Broadcast<T, U>(int eventType, T arg1, U arg2)
        {
            EventCenter.Broadcast(this, eventType, arg1, arg2);
        }
        public void Broadcast<T, U, V>(int eventType, T arg1, U arg2, V arg3)
        {
            EventCenter.Broadcast(this, eventType, arg1, arg2, arg3);
        }
        public void Broadcast<T, U, V, X>(int eventType, T arg1, U arg2, V arg3, X arg4)
        {
            EventCenter.Broadcast(this, eventType, arg1, arg2, arg3, arg4);
        }
        public void Broadcast<T, U, V, X, Z>(int eventType, T arg1, U arg2, V arg3, X arg4, Z arg5)
        {
            EventCenter.Broadcast(this, eventType, arg1, arg2, arg3, arg4, arg5);
        }
        public void Broadcast<T, U, V, X, Z, A>(int eventType, T arg1, U arg2, V arg3, X arg4, Z arg5, A arg6)
        {
            EventCenter.Broadcast(this, eventType, arg1, arg2, arg3, arg4, arg5, arg6);
        }
        public void Broadcast<T, U, V, X, Z, A, B>(int eventType, T arg1, U arg2, V arg3, X arg4, Z arg5, A arg6, B arg7)
        {
            EventCenter.Broadcast(this, eventType, arg1, arg2, arg3, arg4, arg5, arg6, arg7);
        }
        public void Broadcast<T, U, V, X, Z, A, B, C>(int eventType, T arg1, U arg2, V arg3, X arg4, Z arg5, A arg6, B arg7, C arg8)
        {
            EventCenter.Broadcast(this, eventType, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
        }
        public void Broadcast<T, U, V, X, Y, A, B, C, D>(int eventType, T arg1, U arg2, V arg3, X arg4, Y arg5, A arg6, B arg7, C arg8, D arg9)
        {
            EventCenter.Broadcast(this, eventType, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
        }
        public void Broadcast<T, U, V, X, Y, A, B, C, D, E>(int eventType, T arg1, U arg2, V arg3, X arg4, Y arg5, A arg6, B arg7, C arg8, D arg9, E arg10)
        {
            EventCenter.Broadcast(this, eventType, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);
        }
        public void Broadcast<T, U, V, X, Z, A, B, C, D, E, F>(int eventType, T arg1, U arg2, V arg3, X arg4, Z arg5, A arg6, B arg7, C arg8, D arg9, E arg10, F arg11)
        {
            EventCenter.Broadcast(this, eventType, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11);
        }
        public void Broadcast<T, U, V, X, Y, A, B, C, D, E, F, G, H>(int eventType, T arg1, U arg2, V arg3, X arg4, Y arg5, A arg6, B arg7, C arg8, D arg9, E arg10, F arg11, G arg12, H arg13)
        {
            EventCenter.Broadcast(this, eventType, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13);
        }

        public void RegistListener(int eventType, Callback handler)
        {
            EventCenter.RegistListener(this, eventType, handler);
        }
        public void RegistListener<T>(int eventType, Callback<T> handler)
        {
            EventCenter.RegistListener(this, eventType, handler);
        }
        public void RegistListener<T, U>(int eventType, Callback<T, U> handler)
        {
            EventCenter.RegistListener(this, eventType, handler);
        }
        public void RegistListener<T, U, V>(int eventType, Callback<T, U, V> handler)
        {
            EventCenter.RegistListener(this, eventType, handler);
        }
        public void RegistListener<T, U, V, X>(int eventType, Callback<T, U, V, X> handler)
        {
            EventCenter.RegistListener(this, eventType, handler);
        }
        public void RegistListener<T, U, V, X, Z>(int eventType, Callback<T, U, V, X, Z> handler)
        {
            EventCenter.RegistListener(this, eventType, handler);
        }
        public void RegistListener<T, U, V, X, Z, A>(int eventType, Callback<T, U, V, X, Z, A> handler)
        {
            EventCenter.RegistListener(this, eventType, handler);
        }
        public void RegistListener<T, U, V, X, Z, A, B>(int eventType, Callback<T, U, V, X, Z, A, B> handler)
        {
            EventCenter.RegistListener(this, eventType, handler);
        }
        public void RegistListener<T, U, V, X, Z, A, B, C>(int eventType, Callback<T, U, V, X, Z, A, B, C> handler)
        {
            EventCenter.RegistListener(this, eventType, handler);
        }
        public void RegistListener<T, U, V, X, Z, A, B, C, D>(int eventType, Callback<T, U, V, X, Z, A, B, C, D> handler)
        {
            EventCenter.RegistListener(this, eventType, handler);
        }
        public void RegistListener<T, U, V, X, Z, A, B, C, D, E>(int eventType, Callback<T, U, V, X, Z, A, B, C, D, E> handler)
        {
            EventCenter.RegistListener(this, eventType, handler);
        }
        public void RegistListener<T, U, V, X, Y, A, B, C, D, E, F>(int eventType, Callback<T, U, V, X, Y, A, B, C, D, E, F> handler)
        {
            EventCenter.RegistListener(this, eventType, handler);
        }
        public void RegistListener<T, U, V, X, Y, A, B, C, D, E, F, G, H>(int eventType, Callback<T, U, V, X, Y, A, B, C, D, E, F, G, H> handler)
        {
            EventCenter.RegistListener(this, eventType, handler);
        }

        public void UnRegistListener(int eventType)
        {
            EventCenter.UnRegistListener(this, eventType);
        }
        public void UnRegistListener(int eventType, Callback handler)
        {
            EventCenter.UnRegistListener(this, eventType, handler);
        }
        public void UnRegistListener<T>(int eventType, Callback<T> handler)
        {
            EventCenter.UnRegistListener(this, eventType, handler);
        }
        public void UnRegistListener<T, U>(int eventType, Callback<T, U> handler)
        {
            EventCenter.UnRegistListener(this, eventType, handler);
        }
        public void UnRegistListener<T, U, V>(int eventType, Callback<T, U, V> handler)
        {
            EventCenter.UnRegistListener(this, eventType, handler);
        }
        public void UnRegistListener<T, U, V, X>(int eventType, Callback<T, U, V, X> handler)
        {
            EventCenter.UnRegistListener(this, eventType, handler);
        }
        public void UnRegistListener<T, U, V, X, Z>(int eventType, Callback<T, U, V, X, Z> handler)
        {
            EventCenter.UnRegistListener(this, eventType, handler);
        }
        public void UnRegistListener<T, U, V, W, X, A>(int eventType, Callback<T, U, V, W, X, A> handler)
        {
            EventCenter.UnRegistListener(this, eventType, handler);
        }
        public void UnRegistListener<T, U, V, W, X, A, B>(int eventType, Callback<T, U, V, W, X, A, B> handler)
        {
            EventCenter.UnRegistListener(this, eventType, handler);
        }
        public void UnRegistListener<T, U, V, W, X, A, B, C>(int eventType, Callback<T, U, V, W, X, A, B, C> handler)
        {
            EventCenter.UnRegistListener(this, eventType, handler);
        }
        public void UnRegistListener<T, U, V, X, Y, A, B, C, D>(int eventType, Callback<T, U, V, X, Y, A, B, C, D> handler)
        {
            EventCenter.UnRegistListener(this, eventType, handler);
        }
        public void UnRegistListener<T, U, V, X, Y, A, B, C, D, E>(int eventType, Callback<T, U, V, X, Y, A, B, C, D, E> handler)
        {
            EventCenter.UnRegistListener(this, eventType, handler);
        }
        public void UnRegistListener<T, U, V, X, Y, A, B, C, D, E, F>(int eventType, Callback<T, U, V, X, Y, A, B, C, D, E, F> handler)
        {
            EventCenter.UnRegistListener(this, eventType, handler);
        }
        public void UnRegistListener<T, U, V, X, Y, A, B, C, D, E, F, G, H>(int eventType, Callback<T, U, V, X, Y, A, B, C, D, E, F, G, H> handler)
        {
            EventCenter.UnRegistListener(this, eventType, handler);
        }
        public void ClearAllListener()
        {
            EventCenter.UnRegistListener(this);
        }
        #endregion
    }
}