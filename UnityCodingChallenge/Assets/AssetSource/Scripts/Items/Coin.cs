using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Pickup
{

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Player")
            return;

        SetValue(_value);
        gameObject.SetActive(false);
        AudioManager.PlayClip(_pickupSfx);
    }

    protected override void SetValue(int amount)
    {
        PlayerData.ChangePlayerMoney(amount);
    }
}
