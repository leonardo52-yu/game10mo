using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeftX : MonoBehaviour
{
    private PlayerControllerX playerControllerScript;
    private float leftBound = -10;

    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerControllerX>();
    }

    // Update is called once per frame
    void Update()
    {
        // Si el juego no ha terminado, mover los objetos hacia la izquierda
        if (!playerControllerScript.gameOver)
        {
            // Obtén la velocidad actual desde PlayerControllerX
            float currentSpeed = playerControllerScript.baseSpeed;
            
            transform.Translate(Vector3.left * currentSpeed * Time.deltaTime, Space.World);
        }

        // Si el objeto sale de la pantalla y no es el fondo, destruirlo
        if (transform.position.x < leftBound && !gameObject.CompareTag("Background"))
        {
            Destroy(gameObject);
        }
    }
}