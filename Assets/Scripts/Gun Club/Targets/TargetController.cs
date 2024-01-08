using Assets.Scripts.All.Game;
using System;
using UnityEngine;

public class TargetController : MonoBehaviour
{
    [SerializeField] private GameObject _effect;
    [SerializeField] private int _point;

    private Game _game;


    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Bullet")) return;
        _game.UpgradeScore(_point);
        Instantiate(_effect, transform.position, transform.rotation);

        if (GetComponent<OnDestroyPlaySound>() != null)
            GetComponent<OnDestroyPlaySound>().OnBeforeDestroy();
        else
            Destroy(gameObject);

    }

    public void SetGame(Game game)
    {
        _game = game;
    }

}
