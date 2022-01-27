using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField]
    protected ItemType _type;
    [SerializeField]
    protected int _value;
    [SerializeField]
    protected AudioClip _pickupSfx;

    public enum ItemType { Money, Health };

    private void OnEnable()
    {
        float rotY = Random.Range(0.0f, 360.0f);//Not picky about rotation, just doing this so it doesn't look the same as all the other items that spawn at the same time.
        transform.localEulerAngles = new Vector3(0, rotY, 0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Player")
            return;

        AudioManager.PlayClip(_pickupSfx);
    }

    protected virtual void SetValue (int amount)
    {

    }
}
