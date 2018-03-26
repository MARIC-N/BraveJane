using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public int Value = 5;
    public AudioClip CoinPickSFX;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            AudioSource.PlayClipAtPoint(CoinPickSFX,transform.position);
            Destroy(gameObject);
            GameManager.Instance.UpdateScore(1);
        }
    }

}
