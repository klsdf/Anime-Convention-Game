namespace ACG
{
    public delegate void Callback();
    public delegate void Callback<T>(T arg1);
    public delegate void Callback<T, U>(T arg1, U arg2);
    public delegate void Callback<T, U, V>(T arg1, U arg2, V arg3);
    public delegate void Callback<T, U, V, X>(T arg1, U arg2, V arg3, X arg4);
    public delegate void Callback<T, U, V, X, Y>(T arg1, U arg2, V arg3, X arg4, Y arg5);
    public delegate void Callback<T, U, V, X, Y, Z>(T arg1, U arg2, V arg3, X arg4, Y arg5, Z arg6);
    public delegate void Callback<T, U, V, X, Y, Z, A>(T arg1, U arg2, V arg3, X arg4, Y arg5, Z arg6, A arg7);
    public delegate void Callback<T, U, V, X, Y, Z, A, B>(T arg1, U arg2, V arg3, X arg4, Y arg5, Z arg6, A arg7, B arg8);
    public delegate void Callback<T, U, V, X, Y, Z, A, B, C>(T arg1, U arg2, V arg3, X arg4, Y arg5, Z arg6, A arg7, B arg8, C arg9);
    public delegate void Callback<T, U, V, X, Y, Z, A, B, C, D>(T arg1, U arg2, V arg3, X arg4, Y arg5, Z arg6, A arg7, B arg8, C arg9, D arg10);
    public delegate void Callback<T, U, V, X, Y, Z, A, B, C, D, E>(T arg1, U arg2, V arg3, X arg4, Y arg5, Z arg6, A arg7, B arg8, C arg9, D arg10, E arg11);
    public delegate void Callback<T, U, V, X, Y, Z, A, B, C, D, E, F>(T arg1, U arg2, V arg3, X arg4, Y arg5, Z arg6, A arg7, B arg8, C arg9, D arg10, E arg11, F arg12);
    public delegate void Callback<T, U, V, X, Y, Z, A, B, C, D, E, F, G>(T arg1, U arg2, V arg3, X arg4, Y arg5, Z arg6, A arg7, B arg8, C arg9, D arg10, E arg11, F arg12, G arg13);
}