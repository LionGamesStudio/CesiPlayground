using System;
using UnityEngine;

public class TargetController : MonoBehaviour
{
    [SerializeField] private GameObject _effect;
    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Bullet")) return;
        GameManager.Instance.UpgradeScore();
        Instantiate(_effect, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
