using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneTransition : MonoBehaviour
{
    public Image transitionPanel;  // El panel que se desvanecerá
    public float transitionTime = 1f;  // Tiempo de transición (en segundos)

    private void Start()
    {
        // Asegúrate de que el panel esté completamente transparente al inicio
        if (transitionPanel != null)
        {
            transitionPanel.color = new Color(0, 0, 0, 0);  // Totalmente transparente
        }
    }

    public void FadeIn()
    {
        // Inicia la transición de desvanecimiento a pantalla negra
        StartCoroutine(FadeToBlack());
    }

    public void FadeOut()
    {
        // Inicia la transición de desvanecimiento desde la pantalla negra
        StartCoroutine(FadeFromBlack());
    }

    // Función de desvanecimiento hacia negro
    private IEnumerator FadeToBlack()
    {
        float elapsedTime = 0f;

        while (elapsedTime < transitionTime)
        {
            float alpha = Mathf.Lerp(0, 1, elapsedTime / transitionTime);  // Calcula el valor alfa (opacidad)
            transitionPanel.color = new Color(0, 0, 0, alpha);  // Aplica el valor alfa al color del panel
            elapsedTime += Time.deltaTime;  // Incrementa el tiempo transcurrido
            yield return null;  // Espera hasta el siguiente frame
        }

        transitionPanel.color = new Color(0, 0, 0, 1);  // Asegúrate de que sea completamente negro
    }

    // Función de desvanecimiento desde negro
    private IEnumerator FadeFromBlack()
    {
        float elapsedTime = 0f;

        while (elapsedTime < transitionTime)
        {
            float alpha = Mathf.Lerp(1, 0, elapsedTime / transitionTime);  // Calcula el valor alfa (opacidad)
            transitionPanel.color = new Color(0, 0, 0, alpha);  // Aplica el valor alfa al color del panel
            elapsedTime += Time.deltaTime;  // Incrementa el tiempo transcurrido
            yield return null;  // Espera hasta el siguiente frame
        }

        transitionPanel.color = new Color(0, 0, 0, 0);  // Asegúrate de que sea completamente transparente
    }
}
