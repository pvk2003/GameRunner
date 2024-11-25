using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class ShopManager : MonoBehaviour
{
    public GameObject[] characterModels;
    public int currentCharacterIndex = 0;
    public ShopElement[] characters;
    public Button buy;

    void Start()
    {
        foreach (ShopElement character in characters)
        {
            if (character.price == 0)
            {
                character.isLocked = true;
            }
            else
            {
                character.isLocked = PlayerPrefs.GetInt(character.name, 0) == 0 ? false : true;
            }
        }

        currentCharacterIndex = PlayerPrefs.GetInt("SelectedCharacter", 0);
        foreach (GameObject character in characterModels)
            character.SetActive(false);

        characterModels[currentCharacterIndex].SetActive(true);
    }
    void Update()
    {
        UpdateUI();
    }

    public void ChangeNext()
    {
        characterModels[currentCharacterIndex].SetActive(false);
        currentCharacterIndex++;
        if (currentCharacterIndex == characterModels.Length)
            currentCharacterIndex = 0;
        characterModels[currentCharacterIndex].SetActive(true);
        ShopElement c = characters[currentCharacterIndex];
        if (!c.isLocked)
        {
            return;
        }
        PlayerPrefs.SetInt("SelectedCharacter", currentCharacterIndex);
    }
    public void ChangePrevious()
    {
        characterModels[currentCharacterIndex].SetActive(false);
        currentCharacterIndex--;
        if (currentCharacterIndex == -1)
            currentCharacterIndex = characterModels.Length - 1;
        characterModels[currentCharacterIndex].SetActive(true);
        ShopElement c = characters[currentCharacterIndex];
        if (!c.isLocked)
        {
            return;
        }
        PlayerPrefs.SetInt("SelectedCharacter", currentCharacterIndex);
    }
    public void UnlockeCharacter()
    {
        ShopElement c = characters[currentCharacterIndex];
        PlayerPrefs.SetInt(c.name, 1);
        PlayerPrefs.SetInt("SelectedCharacter", currentCharacterIndex);
        c.isLocked = true;
        PlayerPrefs.SetInt("TotalCoins", PlayerPrefs.GetInt("TotalCoins", 0) - c.price);
    }
    private void UpdateUI()
    {
        ShopElement c = characters[currentCharacterIndex];
        if (c.isLocked)
        {
            buy.gameObject.SetActive(false);
        }
        else
        {
            buy.gameObject.SetActive(true);
            buy.GetComponentInChildren<TextMeshProUGUI>().text = "Buy - " + c.price;
            if (c.price < PlayerPrefs.GetInt("TotalCoins", 0))
            {
                buy.interactable = true;
            }
            else
            {
                buy.interactable = false;
            }
        }
    }
}
