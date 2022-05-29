using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBar : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private UIBarElement ammoPrefab;

    private List<UIBarElement> ammoList = new List<UIBarElement>();

    private void OnEnable()
    {
        player.AmmoChanged += OnAmmoChanged;
    }

    private void OnDisable()
    {
        player.AmmoChanged -= OnAmmoChanged;
    }

    private void OnAmmoChanged(int value)
    {
        if (value < 0)
        {
            value = 0;
        }

        if (ammoList.Count < value)
        {
            int createHealth = value - ammoList.Count;
            for (int i = 0; i < createHealth; i++)
            {
                CreateAmmo();
            }
        }
        else if (ammoList.Count > value && ammoList.Count != 0)
        {
            int deleteHealth = ammoList.Count - value;
            for (int i = 0; i < deleteHealth; i++)
            {
                DestroyAmmo(ammoList[ammoList.Count - 1]);
            }
        }
    }

    private void CreateAmmo()
    {
        UIBarElement newAmmo = Instantiate(ammoPrefab, transform);
        ammoList.Add(newAmmo.GetComponent<UIBarElement>());
        newAmmo.Create();
    }

    private void DestroyAmmo(UIBarElement ammo)
    {
        ammoList.Remove(ammo);
        ammo.Destroy();
    }
}
