using UnityEngine;
using TMPro;

public class DisplayCoin : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private TMP_Text coinCountText;

    private void OnEnable()
    {
        player.CoinChanged += OnCoinChanged;
    }

    private void OnDisable()
    {
        player.CoinChanged -= OnCoinChanged;
    }

    private void Start()
    {
        coinCountText.text = "" + 0;
        OnCoinChanged(player.CoinCount);
    }

    private void OnCoinChanged(int coinCount)
    {
        coinCountText.text = coinCount.ToString();
    }
}
