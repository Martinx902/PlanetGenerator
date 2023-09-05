using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int totalCoins;
    private int playerCoins = 0;

    private void CoinCounter()
    {
        playerCoins++;

        if (playerCoins >= totalCoins)
            ResetScene();
    }

    public void SetTotalCoins(int coins)
    {
        totalCoins = coins;
    }

    private void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnEnable()
    {
        CollisionDetector.endGameDelegate += ResetScene;
        Coin.onCoinCollected += CoinCounter;
    }

    private void OnDisable()
    {
        CollisionDetector.endGameDelegate -= ResetScene;
        Coin.onCoinCollected -= CoinCounter;
    }
}