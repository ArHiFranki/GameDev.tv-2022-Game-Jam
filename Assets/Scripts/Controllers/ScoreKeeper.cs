using UnityEngine;
using UnityEngine.Events;

public class ScoreKeeper : MonoBehaviour
{
    private int coinCount;

    public int CoinCount => coinCount;

    public event UnityAction<int> CoinChanged;

    public void ModifyCoinCount(int value)
    {
        coinCount += value;
        Mathf.Clamp(coinCount, 0, int.MaxValue);
        CoinChanged?.Invoke(coinCount);
    }

    public void ResetScore()
    {
        coinCount = 0;
    }
}
