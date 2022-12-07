using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CallBackManager : MonoBehaviour
{
    public static CallBackManager Instance;

    #region Singleton

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    #endregion
    
    public UnityEvent onPopupClosed;
    public UnityEvent onPopupOpened;
    
    public UnityEvent onCoinCollected;
}
