using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Selector : MonoBehaviour
{
    public GameObject[] characterModels;
    public int currentCharacterIndex = 0;

    void Start()
    {   
        currentCharacterIndex = PlayerPrefs.GetInt("SelectedCharacter", 0);
        foreach(GameObject character in characterModels)
            character.SetActive(false);

        characterModels[currentCharacterIndex].SetActive(true);
    }
}
