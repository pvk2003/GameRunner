using UnityEngine;

public class coin : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameObject effect = ObjectPool.instance.GetPooledObject();

            if (effect != null)
            {
                effect.transform.position = transform.position;
                effect.transform.rotation = effect.transform.rotation;
                effect.SetActive(true);
            }

            PlayerPrefs.SetInt("TotalCoins", PlayerPrefs.GetInt("TotalCoins", 0) + 1);
            FindObjectOfType<AudioManager>().PlaySound("PickUpCoin");
            PlayerManager.score += 2;
            gameObject.SetActive(false);
        }
    }
}
