using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class BombColission : MonoBehaviour
{
     // Asigna el botón en el inspector

    
    public void GoToMenu()
    {
        // Cargar la escena del menú
        SceneManager.LoadScene("mainMenu");
        Debug.Log("Reiniciando la escena del juego.");
        
        
    }
    public void RestartGame()
    {
        // Reiniciar la escena actual
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Debug.Log("Reiniciando la escena del juego.");
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            player.GetComponent<PlayerControllerX>()?.ResetPlayerPhysics();
        }
    }
    
    
}
