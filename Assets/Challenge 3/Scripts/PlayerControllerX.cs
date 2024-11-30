using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControllerX : MonoBehaviour
{
    public bool gameOver;

    public float floatForce;
    private float gravityModifier = 1.5f;
    public Rigidbody playerRb;
    public CharacterSelector characterSelector;

    public ParticleSystem explosionParticle;
    public ParticleSystem fireworksParticle;

    private AudioSource playerAudio;
    public AudioClip moneySound;
    public AudioClip explodeSound;
    public AudioClip bounceSound;
    
    public bool isHighEnough;

    public GameObject buttonToMenu;
    public GameObject buttonRestart;

    private int moneyCount = 0;

    public Text moneyText;

    private List<int> topScores = new List<int>();  //lista de puntuaciones
    public Text topScoresText;
    public Text newPuntuation; 

    //aqui comienza lo del nombre

    public GameObject namePromptPanel; // Panel que contiene el InputField y el botón
    public InputField playerNameInput;
    private int currentScoreIndex; 

    public GameObject scorePanel; 

    //aqui comienza lo del fondo
    public SpriteRenderer backgroundRenderer; // Asigna el SpriteRenderer del fondo en el Inspector
    public Sprite newBackgroundSprite; 
    public Sprite newBackgroundSprite2;

    public SceneTransition transitionScript;

    //aqui comienza las transiciones de musica

    public AudioSource mainCameraAudioSource; // Arrastra el AudioSource de la cámara en el Inspector
    public AudioClip[] musicClips;            // Lista de canciones para alternar
    private int currentClipIndex = 0; 
    public AudioSource audioSource1; // Primer AudioSource
    public AudioSource audioSource2; // Segundo AudioSource
    private AudioSource currentAudioSource;
    public float fadeDuration = 2f;  

    //para acelerar la velocidad del juego
    public float baseSpeed = 7f; // Velocidad base para MoveLeftX
    public float speedIncrement = 3f;


    // Start is called before the first frame update
    void Start()
    {
        
        Physics.gravity = new Vector3(0, -14.70f, 0);;
        
        playerAudio = GetComponent<AudioSource>();

        playerRb = GetComponent<Rigidbody>();
        characterSelector = GetComponent<CharacterSelector>();
        // Apply a small upward force at the start of the game
        playerRb.AddForce(Vector3.up * 5, ForceMode.Impulse);

        buttonToMenu.SetActive(false);
        buttonRestart.SetActive(false);
        namePromptPanel.SetActive(false);
        scorePanel.SetActive(false);


        // Actualiza el texto del contador al iniciar
        UpdateMoneyText();
         if (audioSource1 == null || audioSource2 == null)
        {
            Debug.LogError("AudioSources no asignados.");
            return;
        }

        currentAudioSource = audioSource1;

        // Inicia la primera canción si es necesario
        if (musicClips.Length > 0)
        {
            mainCameraAudioSource.clip = musicClips[currentClipIndex];
            mainCameraAudioSource.Play();
        }

        if (playerRb == null)
        {
            playerRb = GetComponent<Rigidbody>();
        }

        if (playerRb == null)
        {
            Debug.LogError("Rigidbody no asignado o no encontrado en el jugador.");
        }
        else
        {
            Debug.Log("Rigidbody correctamente asignado.");
        }
    }

    // Update is called once per frame
    void Update()
    {
    

    // Mientras se presiona Space y el juego no ha terminado, aplicar fuerza hacia arriba
    if (Input.GetKey(KeyCode.Space) && !gameOver)
    {
        playerRb.AddForce(Vector3.up * floatForce);
    }
    

    // Limitar la velocidad máxima del personaje
    playerRb.velocity = new Vector3(playerRb.velocity.x, Mathf.Clamp(playerRb.velocity.y, -10f, 10f), playerRb.velocity.z);
    }
    void FixedUpdate()
    {
        // Aplicar gravedad personalizada
        if (characterSelector != null && characterSelector.customGravity != 1.0f)
        {
            playerRb.AddForce(Physics.gravity * (characterSelector.customGravity - 1.0f), ForceMode.Acceleration);
        }
        if (characterSelector != null && characterSelector.customGravityblue != 1.5f)
        {
            playerRb.AddForce(Physics.gravity * (characterSelector.customGravityblue - 1.0f), ForceMode.Acceleration);
        }
    }
    public void CollectMoney()
    {
        // Incrementar puntos con bonificación
        int pointsToAdd = characterSelector.bonusPoints;
        moneyCount += pointsToAdd;

        Debug.Log("Monedas recolectadas: " + moneyCount);
        
        // Actualizar UI u otra lógica relacionada con el puntaje
    }

    private void OnCollisionEnter(Collision other)
    {
        // if player collides with bomb, explode and set gameOver to true
        if (other.gameObject.CompareTag("Bomb"))
        {
           if (characterSelector.LifesShadow > 0)
            {
                // Reducir una vida y no terminar el juego
                characterSelector.LifesShadow--;
                Debug.Log("Perdiste una vida. Vidas restantes: " +characterSelector.LifesShadow);
            }
            else
            {
                // Sin vidas restantes, el juego termina
                explosionParticle.Play();
                playerAudio.PlayOneShot(explodeSound, 1.0f);
                gameOver = true;
                Debug.Log("Game Over!");
                Destroy(other.gameObject);
                buttonToMenu.SetActive(true);
                buttonRestart.SetActive(true);
                scorePanel.SetActive(true);

                // Al final del juego, guarda la puntuación y actualiza las mejores puntuaciones
                SaveScore(moneyCount);
            }
        } 

        // if player collides with money, fireworks
        else if (other.gameObject.CompareTag("Money"))
        {
            fireworksParticle.Play();
            playerAudio.PlayOneShot(moneySound, 1.0f);
            moneyCount+=5; 
            Destroy(other.gameObject);
            UpdateMusic();
            CollectMoney();
            UpdateMoneyText();
            UpdateSpeed();
            playerRb.freezeRotation = true;
            if (moneyCount == 100)
            {
                StartTransitionToNewBackground(newBackgroundSprite);
            }
            if (moneyCount == 104)
            {
                StartTransitionToNewBackground(newBackgroundSprite);
            }
            if (moneyCount == 200)
            {
                StartTransitionToNewBackground(newBackgroundSprite2);
            }

        }else if (other.gameObject.CompareTag("Ground"))
        {
            playerRb.AddForce(Vector3.up*10f, ForceMode.Impulse);
            playerAudio.PlayOneShot(bounceSound, 1.5f);
            
        }

    }
    private void UpdateSpeed()
    {
        // Aumenta la velocidad al alcanzar ciertos hitos
        if (moneyCount == 100 )
        {
            baseSpeed += speedIncrement;
            Debug.Log("Velocidad aumentada a: " + baseSpeed);
        }
        if (moneyCount == 104 )
        {
            baseSpeed += speedIncrement;
            Debug.Log("Velocidad aumentada a: " + baseSpeed);
        }
        if (moneyCount == 200 )
        {
            baseSpeed += speedIncrement+speedIncrement;
            Debug.Log("Velocidad aumentada a: " + baseSpeed);
        }
    }
    void UpdateMusic()
    {
        // Cambia de canción según el número de monedas recolectadas
        if (moneyCount >= 100 && moneyCount<=104)
        {
            ChangeMusicWithCrossfade(1);
        }
        
        if (moneyCount == 200)
        {
            ChangeMusicWithCrossfade(2);
        }
    }

     void ChangeMusicWithCrossfade(int clipIndex)
    {
        if (clipIndex >= 0 && clipIndex < musicClips.Length)
        {
            // Cambiar la canción con crossfade
            StartCoroutine(CrossfadeToNewTrack(musicClips[clipIndex]));
            currentClipIndex = clipIndex;
        }
        else
        {
            Debug.LogWarning("Índice de canción no válido.");
        }
    }
    private IEnumerator CrossfadeToNewTrack(AudioClip newClip)
    {
        // Identifica el AudioSource de destino
        AudioSource newAudioSource = (currentAudioSource == audioSource1) ? audioSource2 : audioSource1;
        newAudioSource.clip = newClip;
        newAudioSource.volume = 0f; // Inicia con volumen bajo
        newAudioSource.Play();

        // Realiza el crossfade
        float timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float t = timer / fadeDuration;

            // Suaviza la transición
            currentAudioSource.volume = Mathf.Lerp(1f, 0f, t);
            newAudioSource.volume = Mathf.Lerp(0f, 1f, t);

            yield return null;
        }

        // Finaliza la transición
        currentAudioSource.Stop();
        currentAudioSource = newAudioSource; // Actualiza el AudioSource activo
    }

    //aqui comienza lo del fondo



    private void StartTransitionToNewBackground(Sprite newSprite)
    {
        // Iniciar la transición
        transitionScript.FadeIn();

        // Cambiar el fondo después de un pequeño delay para dar el efecto de transición
        StartCoroutine(ChangeBackgroundWithDelay(newSprite));

        // Iniciar la transición de desvanecimiento desde negro después de 3 segundos (para que se vea bien)
        StartCoroutine(WaitAndFadeOut(3f));
    }
    private IEnumerator ChangeBackgroundWithDelay(Sprite newSprite)
    {
        yield return new WaitForSeconds(transitionScript.transitionTime);

        // Cambiar el sprite del fondo
        if (backgroundRenderer != null && newSprite != null)
        {
            backgroundRenderer.sprite = newSprite;
        }
    }

    // Corutina para esperar y hacer el fade-out después de 3 segundos
    private IEnumerator WaitAndFadeOut(float delay)
    {
        yield return new WaitForSeconds(delay);  // Esperar el tiempo de transición

        // Desvanecer el panel hacia fuera
        transitionScript.FadeOut();
    }
    void ChangeBackground()
    {
        Debug.Log("Cambiando fondo...");
        if (backgroundRenderer != null && newBackgroundSprite != null)
        {
            backgroundRenderer.sprite = newBackgroundSprite; // Cambia el sprite del fondo
        }
    }

    void Awake()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    public void ResetPlayerPhysics()
     {
        if (playerRb != null)
        {
            // Detener cualquier movimiento o velocidad residual
            playerRb.velocity = Vector3.zero;
            playerRb.angularVelocity = Vector3.zero;

            // Opcional: Reubica al jugador si es necesario
            playerRb.position = new Vector3(0, 5, 0);

            // Asegurarte de que la gravedad esté activada
            playerRb.useGravity = true;

            Debug.Log("Físicas del jugador reiniciadas.");
        }
    }
    void UpdateMoneyText()
    {
        if (moneyText != null)
        {
            moneyText.text = "Monedas obtenidas: " + moneyCount.ToString(); // Muestra el contador en el UI
        }
    }

   public void SaveScore(int score)
    {
        DisplayTopScores();
            if (score == 0)
        {
            return;
        }

        // Crear una nueva entrada de puntaje con un nombre temporal
        ScoreManager.ScoreEntry newEntry = new ScoreManager.ScoreEntry(score, "Temporal");

        // Agregar la nueva entrada y ordenar
        ScoreManager.Instance.topScores.Add(newEntry);
        ScoreManager.Instance.topScores.Sort((a, b) => b.score.CompareTo(a.score));

        // Mantener solo las 5 mejores puntuaciones
        if (ScoreManager.Instance.topScores.Count > 5)
        {
            ScoreManager.Instance.topScores.RemoveAt(ScoreManager.Instance.topScores.Count - 1);
        }

        // Mostrar el panel para ingresar el nombre si el puntaje está en el Top 5
        currentScoreIndex = ScoreManager.Instance.topScores.IndexOf(newEntry);
        if (currentScoreIndex != -1)
        {
            // Mostrar el mensaje de felicitación
            Text congratulationsText = namePromptPanel.transform.Find("CongratulationsText").GetComponent<Text>();
            congratulationsText.text = "¡¡¡Felicidades!!!\n Nueva puntuación";
            congratulationsText.gameObject.SetActive(true);

            // Mostrar el mensaje para ingresar el nombre
            Text enterNameText = namePromptPanel.transform.Find("EnterNameText").GetComponent<Text>();
            enterNameText.text = "Ingresa tu nombre";
            enterNameText.gameObject.SetActive(true);
            namePromptPanel.SetActive(true);
        }

        // Mostrar los puntajes actualizados
        DisplayTopScores();
    }

    public void ConfirmName()
    {
        // Validar si el panel está activo y el nombre es válido
        if (namePromptPanel.activeSelf && !string.IsNullOrEmpty(playerNameInput.text))
        {
            // Asignar el nombre ingresado al puntaje correspondiente
            ScoreManager.Instance.topScores[currentScoreIndex].name = playerNameInput.text;

            // Limpiar el InputField y ocultar el panel
            playerNameInput.text = "";
            namePromptPanel.SetActive(false);

            // Actualizar la visualización de las puntuaciones
            DisplayTopScores();
        }
        else
        {
            Debug.Log("Por favor, ingresa un nombre válido.");
        }
    }

    void DisplayTopScores()
    {
        string scores = "Top 5 puntuaciones\n\n";
        int index = 1;
        foreach (ScoreManager.ScoreEntry entry in ScoreManager.Instance.topScores)
        {
             scores += $"No. {index}: {entry.name}: {entry.score}\n";
            index++; // Incrementa el índice
        }

        if (topScoresText != null)
        {
            topScoresText.text = scores; // Mostrar en el UI
        }

        Debug.Log(scores); // Mostrar en la consola
    }
}
//No. {i+1}:
