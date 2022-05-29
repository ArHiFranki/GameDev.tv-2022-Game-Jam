using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private UIBarElement heartPrefab;

    private List<UIBarElement> heartsList = new List<UIBarElement>();

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
        UIBarElement newHeart = Instantiate(heartPrefab, transform);
        heartsList.Add(newHeart.GetComponent<UIBarElement>());
        newHeart.Create();
    }

    private void DestroyHeart(UIBarElement heart)
    {
        heartsList.Remove(heart);
        heart.Destroy();
    }
}
