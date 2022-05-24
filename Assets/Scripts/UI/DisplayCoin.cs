using UnityEngine;
using TMPro;

public class DisplayCoin : MonoBehaviour
{
    [SerializeField] private ScoreKeeper scoreKeeper;
    [SerializeField] private TMP_Text coinCountText;

    private void OnEnable()
    {
        scoreKeeper.CoinChanged += OnCoinChanged;
    }

    private void OnDisable()
    {
        scoreKeeper.CoinChanged -= OnCoinChanged;
    }

    private void Start()
    {
        coinCountText.text = "" + 0;
    }

    private void OnCoinChanged(int coinCount)
    {
        coinCountText.text = coinCount.ToString();
    }
}
