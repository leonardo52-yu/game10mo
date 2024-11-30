    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelector : MonoBehaviour
{
   public GameObject[] characterModels; // Array con los prefabs de los modelos 3D
    private GameObject currentCharacter;
    public float customGravity = 1.0f;
    public float customGravityblue = 1.5f; // Gravedad predeterminada
    public int bonusPoints = 0; 
    public int LifesShadow = 0;       

    void Start()
    {
        int index = GameManager.Instance.selectedCharacterIndex;
        SetCharacterModel(index);
        ApplyCharacterAbilities(index);
    }

    public void SetCharacterModel(int index)
    {
        if (index < 0 || index >= characterModels.Length)
        {
            Debug.LogWarning("Índice fuera de rango");
            return;
        }

        currentCharacter = Instantiate(characterModels[index], transform.position, transform.rotation);
        currentCharacter.transform.SetParent(transform);
    }

    private void ApplyCharacterAbilities(int index)
    {
        // Configurar habilidades según el personaje seleccionado
        switch (index)
        {
            case 0: // Modelo 3D en la posición 0 (menor gravedad)
                customGravity = 0.5f;
                Debug.Log("entroooofdsfd");  // Menor gravedad
                break;

            case 1: // Modelo 3D en la posición 1 (puntos dobles)
                bonusPoints = 3;
                customGravityblue=2.0f;
                Debug.Log("entroooo"); // Puntos extra por cada moneda
                break;

            case 2: // Modelo 3D en la posición 2 (habilidades futuras)
                Debug.Log("Habilidades adicionales para el personaje 3");
                LifesShadow=1;
                break;

            default:
                Debug.LogWarning("Índice no tiene habilidades configuradas.");
                break;
        }
    }
}
