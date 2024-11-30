using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // Asegúrate de incluir esto para usar TextMesh Pro

public class menuController : MonoBehaviour
{
    public TextMeshProUGUI selectedCharacterText; // Referencia al texto que mostrará el personaje seleccionado
    public CharacterSelector characterSelector; // Referencia al script CharacterSelector

    public void SelectCharacter(int index)
    {
        GameManager.Instance.selectedCharacterIndex = index;
        Debug.Log("Personaje seleccionado: " + index);
        
        // Actualiza el texto para mostrar el personaje seleccionado
        UpdateSelectedCharacterText(index);
        
        // Cambia el modelo en el CharacterSelector
        characterSelector.SetCharacterModel(index);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Challenge 3");
    }

    private void UpdateSelectedCharacterText(int index)
    {
        // Verifica el índice y actualiza el texto en consecuencia
    if (index == 0)
    {
        selectedCharacterText.text = "Cluck.\n Ligero como pluma";
    }
    else if (index == 1)
    {
        selectedCharacterText.text = "Azulito.\n Mas monedas y mas peso";
    }
    else if (index == 2)
    {
        selectedCharacterText.text = "Sombra. \n ¿Una vida mas?";
    }
    else
    {
        selectedCharacterText.text = "Personaje no válido.";
    }
    }
}
