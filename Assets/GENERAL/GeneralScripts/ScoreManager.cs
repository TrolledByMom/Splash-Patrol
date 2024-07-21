using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ScoreManager : MonoBehaviour
{

    public static ScoreManager Instance;

    public static int Score = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }else { Destroy(gameObject); }
    }
 

    public void AddScore(int amount)
    {
        Score += amount;
        Debug.Log(Score);   
        // fazer score aparecer no UI
    }
}