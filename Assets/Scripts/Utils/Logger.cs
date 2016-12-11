using System;
using UnityEngine;

public class Logger {

    public static void d(object message)
    {
        Debug.Log(message);
    }

    public static void d(object message, UnityEngine.Object context)
    {
        Debug.Log(message, context);
    }

    public static void w(object message)
    {
        Debug.LogWarning(message);
    }

    public static void w(object message, UnityEngine.Object context)
    {
        Debug.LogWarning(message, context);
    }

    public static void e(object message)
    {
        Debug.LogError(message);
    }

    public static void e(object message, UnityEngine.Object context)
    {
        Debug.LogError(message, context);
    }

    public static void exp(Exception message)
    {
        Debug.LogException(message);
    }

    public static void exp(Exception message, UnityEngine.Object context)
    {
        Debug.LogException(message);
    }
}
