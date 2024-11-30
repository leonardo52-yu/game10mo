using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
     public static GameManager Instance;
    public int selectedCharacterIndex = 0; // Índice del modelo seleccionado

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Hace que persista entre escenas
        }
        else
        {
            Destroy(gameObject); // Evita duplicados
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
