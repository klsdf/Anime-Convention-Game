using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// 泛型实现，无类型拆装箱开销
/// </summary>
namespace ACG
{

    public class BroadcastException : Exception
    {
        public BroadcastException(string msg)
            : base(msg)
        {
        }
    }

    public class ListenerException : Exception
    {
        public ListenerException(string msg)
            : base(msg)
        {
        }
    }

    public interface MInterface
    {

    }
    public class PDelegate
    {
        public MInterface obj;
        public Delegate delegate_listener;
    }
    public class PObjDelegate
    {
        private const string TAG = "MInterface";

        static public BroadcastException CreateBroadcastSignatureException(int eventType)
        {
            return new BroadcastException(string.Format("Broadcasting message \"{0}\" but listeners have a different signature than the broadcaster.", eventType));
        }
        //public Delegate func = null;
        List<PDelegate> lst_obj = new List<PDelegate>();
        public bool IsEmpty() { return lst_obj.Count == 0; }
        PDelegate findDelegate(MInterface obj)
        {
            PDelegate find = lst_obj.Find(delegate (PDelegate temp)
            {
                return temp.obj == obj;
            });
            return find;
        }
        PDelegate addObj(MInterface obj)
        {
            PDelegate find = findDelegate(obj);
            if (find == null)
            {
                find = new PDelegate
                {
                    obj = obj,
                    delegate_listener = null,
                };

                lst_obj.Add(find);
            }
            return find;
        }
        void delObj(PDelegate find)
        {
            if (find == null) { return; }

            if (find.delegate_listener == null)
            {
                lst_obj.Remove(find);
            }
        }

        public void AddObjDelegate(MInterface obj, Callback handler)
        {
            PDelegate x = addObj(obj);
            x.delegate_listener = (Callback)x.delegate_listener + handler;
        }
        public void AddObjDelegate<T>(MInterface obj, Callback<T> handler)
        {
            PDelegate x = addObj(obj);
            x.delegate_listener = (Callback<T>)x.delegate_listener + handler;
        }
        public void AddObjDelegate<T, U>(MInterface obj, Callback<T, U> handler)
        {
            PDelegate x = addObj(obj);
            x.delegate_listener = (Callback<T, U>)x.delegate_listener + handler;
        }
        public void AddObjDelegate<T, U, V>(MInterface obj, Callback<T, U, V> handler)
        {
            PDelegate x = addObj(obj);
            x.delegate_listener = (Callback<T, U, V>)x.delegate_listener + handler;
        }
        public void AddObjDelegate<T, U, V, X>(MInterface obj, Callback<T, U, V, X> handler)
        {
            PDelegate x = addObj(obj);
            x.delegate_listener = (Callback<T, U, V, X>)x.delegate_listener + handler;
        }
        public void AddObjDelegate<T, U, V, X, Z>(MInterface obj, Callback<T, U, V, X, Z> handler)
        {
            PDelegate x = addObj(obj);
            x.delegate_listener = (Callback<T, U, V, X, Z>)x.delegate_listener + handler;
        }
        public void AddObjDelegate<T, U, V, X, Z, A>(MInterface obj, Callback<T, U, V, X, Z, A> handler)
        {
            PDelegate x = addObj(obj);
            x.delegate_listener = (Callback<T, U, V, X, Z, A>)x.delegate_listener + handler;
        }
        public void AddObjDelegate<T, U, V, X, Z, A, B>(MInterface obj, Callback<T, U, V, X, Z, A, B> handler)
        {
            PDelegate x = addObj(obj);
            x.delegate_listener = (Callback<T, U, V, X, Z, A, B>)x.delegate_listener + handler;
        }
        public void AddObjDelegate<T, U, V, X, Z, A, B, C>(MInterface obj, Callback<T, U, V, X, Z, A, B, C> handler)
        {
            PDelegate x = addObj(obj);
            x.delegate_listener = (Callback<T, U, V, X, Z, A, B, C>)x.delegate_listener + handler;
        }
        public void AddObjDelegate<T, U, V, X, Z, A, B, C, D>(MInterface obj, Callback<T, U, V, X, Z, A, B, C, D> handler)
        {
            PDelegate x = addObj(obj);
            x.delegate_listener = (Callback<T, U, V, X, Z, A, B, C, D>)x.delegate_listener + handler;
        }
        public void AddObjDelegate<T, U, V, X, Z, A, B, C, D, E>(MInterface obj, Callback<T, U, V, X, Z, A, B, C, D, E> handler)
        {
            PDelegate x = addObj(obj);
            x.delegate_listener = (Callback<T, U, V, X, Z, A, B, C, D, E>)x.delegate_listener + handler;
        }
        public void AddObjDelegate<T, U, V, X, Y, A, B, C, D, E, F>(MInterface obj, Callback<T, U, V, X, Y, A, B, C, D, E, F> handler)
        {
            PDelegate x = addObj(obj);
            x.delegate_listener = (Callback<T, U, V, X, Y, A, B, C, D, E, F>)x.delegate_listener + handler;
        }
        public void AddObjDelegate<T, U, V, X, Y, A, B, C, D, E, F, G, H>(MInterface obj, Callback<T, U, V, X, Y, A, B, C, D, E, F, G, H> handler)
        {
            PDelegate x = addObj(obj);
            x.delegate_listener = (Callback<T, U, V, X, Y, A, B, C, D, E, F, G, H>)x.delegate_listener + handler;
        }


        public void RemoveObjDelegate(MInterface obj)
        {
            PDelegate find = findDelegate(obj);
            if (find != null)
            {
                find.delegate_listener = null;
                delObj(find);
            }
        }
        public void RemoveObjDelegate(MInterface obj, Callback handler)
        {
            PDelegate find = findDelegate(obj);
            if (find != null)
            {
                find.delegate_listener = (Callback)find.delegate_listener - handler;
                delObj(find);
            }
        }
        public void RemoveObjDelegate<T>(MInterface obj, Callback<T> handler)
        {
            PDelegate find = findDelegate(obj);
            if (find != null)
            {
                find.delegate_listener = (Callback<T>)find.delegate_listener - handler;
                delObj(find);
            }
        }
        public void RemoveObjDelegate<T, U>(MInterface obj, Callback<T, U> handler)
        {
            PDelegate find = findDelegate(obj);
            if (find != null)
            {
                find.delegate_listener = (Callback<T, U>)find.delegate_listener - handler;
                delObj(find);
            }
        }
        public void RemoveObjDelegate<T, U, V>(MInterface obj, Callback<T, U, V> handler)
        {
            PDelegate find = findDelegate(obj);
            if (find != null)
            {
                find.delegate_listener = (Callback<T, U, V>)find.delegate_listener - handler;
                delObj(find);
            }
        }
        public void RemoveObjDelegate<T, U, V, X>(MInterface obj, Callback<T, U, V, X> handler)
        {
            PDelegate find = findDelegate(obj);
            if (find != null)
            {
                find.delegate_listener = (Callback<T, U, V, X>)find.delegate_listener - handler;
                delObj(find);
            }
        }
        public void RemoveObjDelegate<T, U, V, X, Z>(MInterface obj, Callback<T, U, V, X, Z> handler)
        {
            PDelegate find = findDelegate(obj);
            if (find != null)
            {
                find.delegate_listener = (Callback<T, U, V, X, Z>)find.delegate_listener - handler;
                delObj(find);
            }
        }
        public void RemoveObjDelegate<T, U, V, X, Z, A>(MInterface obj, Callback<T, U, V, X, Z, A> handler)
        {
            PDelegate find = findDelegate(obj);
            if (find != null)
            {
                find.delegate_listener = (Callback<T, U, V, X, Z, A>)find.delegate_listener - handler;
                delObj(find);
            }
        }
        public void RemoveObjDelegate<T, U, V, X, Z, A, B>(MInterface obj, Callback<T, U, V, X, Z, A, B> handler)
        {
            PDelegate find = findDelegate(obj);
            if (find != null)
            {
                find.delegate_listener = (Callback<T, U, V, X, Z, A, B>)find.delegate_listener - handler;
                delObj(find);
            }
        }
        public void RemoveObjDelegate<T, U, V, X, Z, A, B, C>(MInterface obj, Callback<T, U, V, X, Z, A, B, C> handler)
        {
            PDelegate find = findDelegate(obj);
            if (find != null)
            {
                find.delegate_listener = (Callback<T, U, V, X, Z, A, B, C>)find.delegate_listener - handler;
                delObj(find);
            }
        }
        public void RemoveObjDelegate<T, U, V, X, Z, A, B, C, D>(MInterface obj, Callback<T, U, V, X, Z, A, B, C, D> handler)
        {
            PDelegate find = findDelegate(obj);
            if (find != null)
            {
                find.delegate_listener = (Callback<T, U, V, X, Z, A, B, C, D>)find.delegate_listener - handler;
                delObj(find);
            }
        }
        public void RemoveObjDelegate<T, U, V, X, Z, A, B, C, D, E>(MInterface obj, Callback<T, U, V, X, Z, A, B, C, D, E> handler)
        {
            PDelegate find = findDelegate(obj);
            if (find != null)
            {
                find.delegate_listener = (Callback<T, U, V, X, Z, A, B, C, D, E>)find.delegate_listener - handler;
                delObj(find);
            }
        }
        public void RemoveObjDelegate<T, U, V, X, Y, A, B, C, D, E, F>(MInterface obj, Callback<T, U, V, X, Y, A, B, C, D, E, F> handler)
        {
            PDelegate find = findDelegate(obj);
            if (find != null)
            {
                find.delegate_listener = (Callback<T, U, V, X, Y, A, B, C, D, E, F>)find.delegate_listener - handler;
                delObj(find);
            }
        }
        public void RemoveObjDelegate<T, U, V, X, Y, A, B, C, D, E, F, G, H>(MInterface obj, Callback<T, U, V, X, Y, A, B, C, D, E, F, G, H> handler)
        {
            PDelegate find = findDelegate(obj);
            if (find != null)
            {
                find.delegate_listener = (Callback<T, U, V, X, Y, A, B, C, D, E, F, G, H>)find.delegate_listener - handler;
                delObj(find);
            }
        }

        public void Exec(MInterface sender, int eventType)
        {
            PDelegate[] tmp = lst_obj.ToArray();
            foreach (var obj in tmp)
            {
                if (null == obj)
                    continue;
                if (obj.delegate_listener is Callback callback)
                {
                    try
                    {
                        callback();
                    } catch(Exception e)
                    {
                        LogUtil.e(TAG, e.ToString());
                    }
                    
                }
                else
                {
                    throw CreateBroadcastSignatureException(eventType);
                }
            }
        }
        public void Exec<T>(MInterface sender, int eventType, T arg1)
        {
            PDelegate[] tmp = lst_obj.ToArray();
            foreach (var obj in tmp)
            {
                if (null == obj)
                    continue;
                if (obj.delegate_listener is Callback<T> callback)
                {
                    callback(arg1);
                }
                else
                {
                    throw CreateBroadcastSignatureException(eventType);
                }
            }
        }
        public void Exec<T, U>(MInterface sender, int eventType, T arg1, U arg2)
        {
            PDelegate[] tmp = lst_obj.ToArray();
            foreach (var obj in tmp)
            {
                if (null == obj)
                    continue;
                if (obj.delegate_listener is Callback<T, U> callback)
                {
                    callback(arg1, arg2);
                }
                else
                {
                    throw CreateBroadcastSignatureException(eventType);
                }
            }
        }
        public void Exec<T, U, V>(MInterface sender, int eventType, T arg1, U arg2, V arg3)
        {
            PDelegate[] tmp = lst_obj.ToArray();
            foreach (var obj in tmp)
            {
                if (null == obj)
                    continue;
                if (obj.delegate_listener is Callback<T, U, V> callback)
                {
                    callback(arg1, arg2, arg3);
                }
                else
                {
                    throw CreateBroadcastSignatureException(eventType);
                }
            }
        }
        public void Exec<T, U, V, X>(MInterface sender, int eventType, T arg1, U arg2, V arg3, X arg4)
        {
            PDelegate[] tmp = lst_obj.ToArray();
            foreach (var obj in tmp)
            {
                if (null == obj)
                    continue;
                if (obj.delegate_listener is Callback<T, U, V, X> callback)
                {
                    callback(arg1, arg2, arg3, arg4);
                }
                else
                {
                    throw CreateBroadcastSignatureException(eventType);
                }
            }
        }
        public void Exec<T, U, V, X, Z>(MInterface sender, int eventType, T arg1, U arg2, V arg3, X arg4, Z arg5)
        {
            PDelegate[] tmp = lst_obj.ToArray();
            foreach (var obj in tmp)
            {
                if (null == obj)
                    continue;
                if (obj.delegate_listener is Callback<T, U, V, X, Z> callback)
                {
                    callback(arg1, arg2, arg3, arg4, arg5);
                }
                else
                {
                    throw CreateBroadcastSignatureException(eventType);
                }
            }
        }
        public void Exec<T, U, V, X, Z, A>(MInterface sender, int eventType, T arg1, U arg2, V arg3, X arg4, Z arg5, A arg6)
        {
            PDelegate[] tmp = lst_obj.ToArray();
            foreach (var obj in tmp)
            {
                if (null == obj)
                    continue;
                if (obj.delegate_listener is Callback<T, U, V, X, Z, A> callback)
                {
                    callback(arg1, arg2, arg3, arg4, arg5, arg6);
                }
                else
                {
                    throw CreateBroadcastSignatureException(eventType);
                }
            }
        }
        public void Exec<T, U, V, X, Z, A, B>(MInterface sender, int eventType, T arg1, U arg2, V arg3, X arg4, Z arg5, A arg6, B arg7)
        {
            PDelegate[] tmp = lst_obj.ToArray();
            foreach (var obj in tmp)
            {
                if (null == obj)
                    continue;
                if (obj.delegate_listener is Callback<T, U, V, X, Z, A, B> callback)
                {
                    callback(arg1, arg2, arg3, arg4, arg5, arg6, arg7);
                }
                else
                {
                    throw CreateBroadcastSignatureException(eventType);
                }
            }
        }
        public void Exec<T, U, V, X, Z, A, B, C>(MInterface sender, int eventType, T arg1, U arg2, V arg3, X arg4, Z arg5, A arg6, B arg7, C arg8)
        {
            PDelegate[] tmp = lst_obj.ToArray();
            foreach (var obj in tmp)
            {
                if (null == obj)
                    continue;
                if (obj.delegate_listener is Callback<T, U, V, X, Z, A, B, C> callback)
                {
                    callback(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
                }
                else
                {
                    throw CreateBroadcastSignatureException(eventType);
                }
            }
        }
        public void Exec<T, U, V, X, Z, A, B, C, D>(MInterface sender, int eventType, T arg1, U arg2, V arg3, X arg4, Z arg5, A arg6, B arg7, C arg8, D arg9)
        {
            PDelegate[] tmp = lst_obj.ToArray();
            foreach (var obj in tmp)
            {
                if (null == obj)
                    continue;
                if (obj.delegate_listener is Callback<T, U, V, X, Z, A, B, C, D> callback)
                {
                    callback(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
                }
                else
                {
                    throw CreateBroadcastSignatureException(eventType);
                }
            }
        }
        public void Exec<T, U, V, X, Z, A, B, C, D, E>(MInterface sender, int eventType, T arg1, U arg2, V arg3, X arg4, Z arg5, A arg6, B arg7, C arg8, D arg9, E arg10)
        {
            PDelegate[] tmp = lst_obj.ToArray();
            foreach (var obj in tmp)
            {
                if (null == obj)
                    continue;
                if (obj.delegate_listener is Callback<T, U, V, X, Z, A, B, C, D, E> callback)
                {
                    callback(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);
                }
                else
                {
                    throw CreateBroadcastSignatureException(eventType);
                }
            }
        }
        public void Exec<T, U, V, X, Z, A, B, C, D, E, F>(MInterface sender, int eventType, T arg1, U arg2, V arg3, X arg4, Z arg5, A arg6, B arg7, C arg8, D arg9, E arg10, F arg11)
        {
            PDelegate[] tmp = lst_obj.ToArray();
            foreach (var obj in tmp)
            {
                if (null == obj)
                    continue;
                if (obj.delegate_listener is Callback<T, U, V, X, Z, A, B, C, D, E, F> callback)
                {
                    callback(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11);
                }
                else
                {
                    throw CreateBroadcastSignatureException(eventType);
                }
            }
        }
        public void Exec<T, U, V, X, Z, A, B, C, D, E, F, G, H>(MInterface sender, int eventType, T arg1, U arg2, V arg3, X arg4, Z arg5, A arg6, B arg7, C arg8, D arg9, E arg10, F arg11, G arg12, H arg13)
        {
            PDelegate[] tmp = lst_obj.ToArray();
            foreach (var obj in tmp)
            {
                if (null == obj)
                    continue;
                if (obj.delegate_listener is Callback<T, U, V, X, Z, A, B, C, D, E, F, G, H> callback)
                {
                    callback(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13);
                }
                else
                {
                    throw CreateBroadcastSignatureException(eventType);
                }
            }
        }

    }

    public class EventCenter
    {
        public static Dictionary<int, PObjDelegate> mEventTable = new Dictionary<int, PObjDelegate>();
        static public void OnBroadcasting(int eventType)
        {
            // #if REQUIRE_LISTENER
            // if (!EventCenter.mEventTable.ContainsKey(eventType))
            // {
            // }
            // #endif
        }
        static PObjDelegate queryDelegate(int type)
        {
            PObjDelegate d = null;
            if (!EventCenter.mEventTable.TryGetValue(type, out d))
            {
                d = new PObjDelegate();
                EventCenter.mEventTable[type] = d;
            }
            return d;
        }
        static PObjDelegate getDelegate(int type)
        {
            PObjDelegate d = null;
            if (!EventCenter.mEventTable.TryGetValue(type, out d))
            {
                return null;
            }
            return d;
        }

        public static void Broadcast(MInterface obj, int eventType)
        {
            EventCenter.OnBroadcasting(eventType);

            PObjDelegate d = getDelegate((int)eventType);
            if (d != null)
            {
                d.Exec(obj, eventType);
            }
        }
        public static void Broadcast<T>(MInterface obj, int eventType, T arg1)
        {
            OnBroadcasting(eventType);

            PObjDelegate d = getDelegate((int)eventType);
            if (d != null)
            {
                d.Exec(obj, eventType, arg1);
            }
        }
        public static void Broadcast<T, U>(MInterface obj, int eventType, T arg1, U arg2)
        {
            OnBroadcasting(eventType);

            PObjDelegate d = getDelegate((int)eventType);
            if (d != null)
            {
                d.Exec<T, U>(obj, eventType, arg1, arg2);
            }
        }
        public static void Broadcast<T, U, V>(MInterface obj, int eventType, T arg1, U arg2, V arg3)
        {
            OnBroadcasting(eventType);

            PObjDelegate d = getDelegate((int)eventType);
            if (d != null)
            {
                d.Exec(obj, eventType, arg1, arg2, arg3);
            }
        }
        public static void Broadcast<T, U, V, X>(MInterface obj, int eventType, T arg1, U arg2, V arg3, X arg4)
        {
            OnBroadcasting(eventType);

            PObjDelegate d = getDelegate((int)eventType);
            if (d != null)
            {
                d.Exec(obj, eventType, arg1, arg2, arg3, arg4);
            }
        }
        public static void Broadcast<T, U, V, X, Z>(MInterface obj, int eventType, T arg1, U arg2, V arg3, X arg4, Z arg5)
        {
            OnBroadcasting(eventType);

            PObjDelegate d = getDelegate((int)eventType);
            if (d != null)
            {
                d.Exec(obj, eventType, arg1, arg2, arg3, arg4, arg5);
            }
        }
        public static void Broadcast<T, U, V, X, Z, A>(MInterface obj, int eventType, T arg1, U arg2, V arg3, X arg4, Z arg5, A arg6)
        {
            OnBroadcasting(eventType);
            PObjDelegate d = getDelegate((int)eventType);
            if (d != null)
            {
                d.Exec(obj, eventType, arg1, arg2, arg3, arg4, arg5, arg6);
            }
        }
        public static void Broadcast<T, U, V, X, Z, A, B>(MInterface obj, int eventType, T arg1, U arg2, V arg3, X arg4, Z arg5, A arg6, B arg7)
        {
            OnBroadcasting(eventType);

            PObjDelegate d = getDelegate((int)eventType);
            if (d != null)
            {
                d.Exec(obj, eventType, arg1, arg2, arg3, arg4, arg5, arg6, arg7);
            }
        }
        public static void Broadcast<T, U, V, X, Z, A, B, C>(MInterface obj, int eventType, T arg1, U arg2, V arg3, X arg4, Z arg5, A arg6, B arg7, C arg8)
        {
            OnBroadcasting(eventType);
            PObjDelegate d = getDelegate((int)eventType);
            if (d != null)
            {
                d.Exec(obj, eventType, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
            }
        }
        public static void Broadcast<T, U, V, X, Y, A, B, C, D>(MInterface obj, int eventType, T arg1, U arg2, V arg3, X arg4, Y arg5, A arg6, B arg7, C arg8, D arg9)
        {
            OnBroadcasting(eventType);

            PObjDelegate d = getDelegate((int)eventType);
            if (d != null)
            {
                d.Exec(obj, eventType, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
            }
        }
        public static void Broadcast<T, U, V, X, Y, A, B, C, D, E>(MInterface obj, int eventType, T arg1, U arg2, V arg3, X arg4, Y arg5, A arg6, B arg7, C arg8, D arg9, E arg10)
        {
            OnBroadcasting(eventType);

            PObjDelegate d = getDelegate((int)eventType);
            if (d != null)
            {
                d.Exec(obj, eventType, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);
            }
        }
        public static void Broadcast<T, U, V, X, Z, A, B, C, D, E, F>(MInterface obj, int eventType, T arg1, U arg2, V arg3, X arg4, Z arg5, A arg6, B arg7, C arg8, D arg9, E arg10, F arg11)
        {
            OnBroadcasting(eventType);

            PObjDelegate d = getDelegate((int)eventType);
            if (d != null)
            {
                d.Exec(obj, eventType, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11);
            }
        }
        public static void Broadcast<T, U, V, X, Y, A, B, C, D, E, F, G, H>(MInterface obj, int eventType, T arg1, U arg2, V arg3, X arg4, Y arg5, A arg6, B arg7, C arg8, D arg9, E arg10, F arg11, G arg12, H arg13)
        {
            OnBroadcasting(eventType);

            PObjDelegate d = getDelegate((int)eventType);
            if (d != null)
            {
                d.Exec(obj, eventType, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13);
            }
        }

        public static void RegistListener(MInterface obj, int eventType, Callback handler)
        {
            int type = (int)eventType;
            PObjDelegate d = queryDelegate(type);
            if (d != null)
            {
                d.AddObjDelegate(obj, handler);
            }
        }
        public static void RegistListener<T>(MInterface obj, int eventType, Callback<T> handler)
        {
            int type = (int)eventType;
            PObjDelegate d = queryDelegate(type);
            if (d != null)
            {
                d.AddObjDelegate(obj, handler);
            }
        }
        public static void RegistListener<T, U>(MInterface obj, int eventType, Callback<T, U> handler)
        {
            int type = (int)eventType;
            PObjDelegate d = queryDelegate(type);
            if (d != null)
            {
                d.AddObjDelegate(obj, handler);
            }
        }
        public static void RegistListener<T, U, V>(MInterface obj, int eventType, Callback<T, U, V> handler)
        {
            int type = (int)eventType;
            PObjDelegate d = queryDelegate(type);
            if (d != null)
            {
                d.AddObjDelegate(obj, handler);
            }
        }
        public static void RegistListener<T, U, V, X>(MInterface obj, int eventType, Callback<T, U, V, X> handler)
        {
            int type = (int)eventType;
            PObjDelegate d = queryDelegate(type);
            if (d != null)
            {
                d.AddObjDelegate(obj, handler);
            }
        }
        public static void RegistListener<T, U, V, X, Z>(MInterface obj, int eventType, Callback<T, U, V, X, Z> handler)
        {
            int type = (int)eventType;
            PObjDelegate d = queryDelegate(type);
            if (d != null)
            {
                d.AddObjDelegate(obj, handler);
            }
        }
        public static void RegistListener<T, U, V, X, Z, A>(MInterface obj, int eventType, Callback<T, U, V, X, Z, A> handler)
        {
            int type = (int)eventType;
            PObjDelegate d = queryDelegate(type);
            if (d != null)
            {
                d.AddObjDelegate(obj, handler);
            }
        }
        public static void RegistListener<T, U, V, X, Z, A, B>(MInterface obj, int eventType, Callback<T, U, V, X, Z, A, B> handler)
        {
            int type = (int)eventType;
            PObjDelegate d = queryDelegate(type);
            if (d != null)
            {
                d.AddObjDelegate(obj, handler);
            }
        }
        public static void RegistListener<T, U, V, X, Z, A, B, C>(MInterface obj, int eventType, Callback<T, U, V, X, Z, A, B, C> handler)
        {
            int type = (int)eventType;
            PObjDelegate d = queryDelegate(type);
            if (d != null)
            {
                d.AddObjDelegate(obj, handler);
            }
        }
        public static void RegistListener<T, U, V, X, Z, A, B, C, D>(MInterface obj, int eventType, Callback<T, U, V, X, Z, A, B, C, D> handler)
        {
            int type = (int)eventType;
            PObjDelegate d = queryDelegate(type);
            if (d != null)
            {
                d.AddObjDelegate(obj, handler);
            }
        }
        public static void RegistListener<T, U, V, X, Z, A, B, C, D, E>(MInterface obj, int eventType, Callback<T, U, V, X, Z, A, B, C, D, E> handler)
        {
            int type = (int)eventType;
            PObjDelegate d = queryDelegate(type);
            if (d != null)
            {
                d.AddObjDelegate(obj, handler);
            }
        }
        public static void RegistListener<T, U, V, X, Y, A, B, C, D, E, F>(MInterface obj, int eventType, Callback<T, U, V, X, Y, A, B, C, D, E, F> handler)
        {
            int type = (int)eventType;
            PObjDelegate d = queryDelegate(type);
            if (d != null)
            {
                d.AddObjDelegate(obj, handler);
            }
        }
        public static void RegistListener<T, U, V, X, Y, A, B, C, D, E, F, G, H>(MInterface obj, int eventType, Callback<T, U, V, X, Y, A, B, C, D, E, F, G, H> handler)
        {
            int type = (int)eventType;
            PObjDelegate d = queryDelegate(type);
            if (d != null)
            {
                d.AddObjDelegate(obj, handler);
            }
        }

        static void OnListenerRemoved(PObjDelegate d, int type)
        {
            if (d.IsEmpty())
            {
                EventCenter.mEventTable.Remove(type);
            }
        }
        public static void UnRegistListener(MInterface obj)
        {
            List<int> keyColl = mEventTable.Keys.ToList();
            foreach (var type in keyColl)
            {
                UnRegistListener(obj, (int)type);
            }
        }
        public static void UnRegistListener(MInterface obj, int eventType)
        {
            int type = (int)eventType;
            PObjDelegate d = queryDelegate(type);
            if (d != null)
            {
                d.RemoveObjDelegate(obj);
                OnListenerRemoved(d, type);
            }
        }
        public static void UnRegistListener(MInterface obj, int eventType, Callback handler)
        {
            int type = (int)eventType;
            PObjDelegate d = queryDelegate(type);
            if (d != null)
            {
                d.RemoveObjDelegate(obj, handler);
                OnListenerRemoved(d, type);
            }
        }
        public static void UnRegistListener<T>(MInterface obj, int eventType, Callback<T> handler)
        {
            int type = (int)eventType;
            PObjDelegate d = queryDelegate(type);
            if (d != null)
            {
                d.RemoveObjDelegate(obj, handler);
                OnListenerRemoved(d, type);
            }
        }
        public static void UnRegistListener<T, U>(MInterface obj, int eventType, Callback<T, U> handler)
        {
            int type = (int)eventType;
            PObjDelegate d = queryDelegate(type);
            if (d != null)
            {
                d.RemoveObjDelegate(obj, handler);
                OnListenerRemoved(d, type);
            }

        }
        public static void UnRegistListener<T, U, V>(MInterface obj, int eventType, Callback<T, U, V> handler)
        {
            int type = (int)eventType;
            PObjDelegate d = queryDelegate(type);
            if (d != null)
            {
                d.RemoveObjDelegate(obj, handler);
                OnListenerRemoved(d, type);
            }
        }
        public static void UnRegistListener<T, U, V, X>(MInterface obj, int eventType, Callback<T, U, V, X> handler)
        {
            int type = (int)eventType;
            PObjDelegate d = queryDelegate(type);
            if (d != null)
            {
                d.RemoveObjDelegate(obj, handler);
                OnListenerRemoved(d, type);
            }
        }
        public static void UnRegistListener<T, U, V, X, Z>(MInterface obj, int eventType, Callback<T, U, V, X, Z> handler)
        {
            int type = (int)eventType;
            PObjDelegate d = queryDelegate(type);
            if (d != null)
            {
                d.RemoveObjDelegate(obj, handler);
                OnListenerRemoved(d, type);
            }
        }
        public static void UnRegistListener<T, U, V, W, X, A>(MInterface obj, int eventType, Callback<T, U, V, W, X, A> handler)
        {
            int type = (int)eventType;
            PObjDelegate d = queryDelegate(type);
            if (d != null)
            {
                d.RemoveObjDelegate(obj, handler);
                OnListenerRemoved(d, type);
            }
        }
        public static void UnRegistListener<T, U, V, W, X, A, B>(MInterface obj, int eventType, Callback<T, U, V, W, X, A, B> handler)
        {
            int type = (int)eventType;
            PObjDelegate d = queryDelegate(type);
            if (d != null)
            {
                d.RemoveObjDelegate(obj, handler);
                OnListenerRemoved(d, type);
            }
        }
        public static void UnRegistListener<T, U, V, W, X, A, B, C>(MInterface obj, int eventType, Callback<T, U, V, W, X, A, B, C> handler)
        {
            int type = (int)eventType;
            PObjDelegate d = queryDelegate(type);
            if (d != null)
            {
                d.RemoveObjDelegate(obj, handler);
                OnListenerRemoved(d, type);
            }
        }
        public static void UnRegistListener<T, U, V, X, Y, A, B, C, D>(MInterface obj, int eventType, Callback<T, U, V, X, Y, A, B, C, D> handler)
        {
            int type = (int)eventType;
            PObjDelegate d = queryDelegate(type);
            if (d != null)
            {
                d.RemoveObjDelegate(obj, handler);
                OnListenerRemoved(d, type);
            }
        }
        public static void UnRegistListener<T, U, V, X, Y, A, B, C, D, E>(MInterface obj, int eventType, Callback<T, U, V, X, Y, A, B, C, D, E> handler)
        {
            int type = (int)eventType;
            PObjDelegate d = queryDelegate(type);
            if (d != null)
            {
                d.RemoveObjDelegate(obj, handler);
                OnListenerRemoved(d, type);
            }
        }
        public static void UnRegistListener<T, U, V, X, Y, A, B, C, D, E, F>(MInterface obj, int eventType, Callback<T, U, V, X, Y, A, B, C, D, E, F> handler)
        {
            int type = (int)eventType;
            PObjDelegate d = queryDelegate(type);
            if (d != null)
            {
                d.RemoveObjDelegate(obj, handler);
                OnListenerRemoved(d, type);
            }
        }
        public static void UnRegistListener<T, U, V, X, Y, A, B, C, D, E, F, G, H>(MInterface obj, int eventType, Callback<T, U, V, X, Y, A, B, C, D, E, F, G, H> handler)
        {
            int type = (int)eventType;
            PObjDelegate d = queryDelegate(type);
            if (d != null)
            {
                d.RemoveObjDelegate(obj, handler);
                OnListenerRemoved(d, type);
            }
        }

    }

    /// <summary>
    /// 需要attach到gameobject，继承MObject, 否则EventObject
    /// 如果对象继承了其他类，可以将EventObject作为内部成员，在对象被销毁的时候，需要手动调用
    /// EventObject.ClearAllListener
    /// TODO: 后续可以增加Broadcast异步能力
    /// </summary>
    public class EventObject : MInterface
    {
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