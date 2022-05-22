using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private UIHeart heartPrefab;

    private List<UIHeart> heartsList = new List<UIHeart>();

    private void OnEnable()
    {
        player.HealthChanged += OnHealthChanged;
    }

    private void OnDisable()
    {
        player.HealthChanged -= OnHealthChanged;
    }

    private void OnHealthChanged(int value)
    {
        if (value < 0)
        {
            value = 0;
        }

        if (heartsList.Count < value)
        {
            int createHealth = value - heartsList.Count;
            for (int i = 0; i < createHealth; i++)
            {
                CreateHearth();
            }
        }
        else if (heartsList.Count > value && heartsList.Count != 0)
        {
            int deleteHealth = heartsList.Count - value;
            for (int i = 0; i < deleteHealth; i++)
            {
                DestroyHeart(heartsList[heartsList.Count - 1]);
            }
        }
    }

    private void CreateHearth()
    {
        UIHeart newHeart = Instantiate(heartPrefab, transform);
        heartsList.Add(newHeart.GetComponent<UIHeart>());
        newHeart.Create();
    }

    private void DestroyHeart(UIHeart heart)
    {
        heartsList.Remove(heart);
        heart.Destroy();
    }
}
