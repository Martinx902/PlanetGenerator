using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public delegate void CoinCollected();

    public static event CoinCollected onCoinCollected;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            //Audio
            AudioManager.instance.PlayClip(SoundsFX.SFX_Coins);

            onCoinCollected.Invoke();

            Destroy(gameObject);
        }
    }
}