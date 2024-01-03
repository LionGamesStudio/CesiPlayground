using System;
using UnityEngine;

public class TargetController : MonoBehaviour
{
    [SerializeField] private GameObject _effect;
    [SerializeField] private int _point;
    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Bullet")) return;
        GameManager.Instance.UpgradeScore(_point);
        Instantiate(_effect, transform.position, transform.rotation);

        if (GetComponent<OnDestroyPlaySound>() != null)
            GetComponent<OnDestroyPlaySound>().OnBeforeDestroy();
        else
            Destroy(gameObject);

    }

}
