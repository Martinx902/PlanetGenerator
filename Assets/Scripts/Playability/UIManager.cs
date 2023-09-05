using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI playerCoinsText;
    public TextMeshProUGUI totalCoins;

    private int playerCoins = 0;

    public void SetUpTotalCoins(int coins)
    {
        totalCoins.text = coins.ToString();
    }

    private void AddCoin()
    {
        playerCoins++;
        playerCoinsText.text = playerCoins.ToString();
    }

    private void OnEnable()
    {
        Coin.onCoinCollected += AddCoin;
    }

    private void OnDisable()
    {
        Coin.onCoinCollected -= AddCoin;
    }
}