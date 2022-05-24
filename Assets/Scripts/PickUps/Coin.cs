using UnityEngine;

public class Coin : PickUp
{
    [SerializeField] private int coinValue;

    private ScoreKeeper myScoreKeeper;

    protected override void PickUpAction(Player player)
    {
        player.PickUpCoin();
        myScoreKeeper.ModifyCoinCount(coinValue);
    }

    public void InitScoreKeeper(ScoreKeeper scoreKeeper)
    {
        myScoreKeeper = scoreKeeper;
    }
}
