using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IObserver
{
    public void OnNotify(); //add notify action

    private void onEnable(Subject subject)
    {
        //adds itself to subject
        subject.AddObserver(this);
    }

}

public abstract class Subject : MonoBehaviour
{
    private List<IObserver> _observers = new List<IObserver> ();

    public void AddObserver(IObserver observer)
    {
        _observers.Add(observer); //add an observer
    }
    public void RemoveObserver(IObserver observer)
    {
        _observers.Remove(observer); //remove an observer
    }
    protected void NotifyObservers()
    {
        _observers.ForEach((_observer) =>
        {
            _observer.OnNotify(); //notify every observer
        });
    }
}
