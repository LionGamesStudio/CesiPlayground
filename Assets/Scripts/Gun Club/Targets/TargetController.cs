using Assets.Scripts.All.Game;
using Assets.Scripts.All.Spawn.Spawners;
using System;
using UnityEngine;

public class TargetController : MonoBehaviour
{
    [SerializeField] private GameObject _effect;
    [SerializeField] private int _point;

    private Game _game;
    private Spawner _spawner;


    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Bullet")) return;

        this.GetComponent<Collider>().enabled = false;

        _game.UpgradeScore(_point);
        Instantiate(_effect, transform.position, transform.rotation);

        if (GetComponent<OnDestroyPlaySound>() != null)
            GetComponent<OnDestroyPlaySound>().OnBeforeDestroy(() =>
            {
                _spawner.Dispawn(this.gameObject);
            });
        else
            _spawner.Dispawn(this.gameObject);

    }

    public void SetGame(Game game)
    {
        _game = game;
    }

    public void SetSpawner(Spawner spawner)
    {
        _spawner = spawner;
    }   

}
