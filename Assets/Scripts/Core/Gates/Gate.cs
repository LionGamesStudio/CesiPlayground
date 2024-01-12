using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Core.Gates
{
    [RequireComponent(typeof(Collider))]
    public class Gate : MonoBehaviour
    {
        [SerializeField]
        private string _name;

        [SerializeField]
        private Gate _exit;

        [SerializeField]
        private bool _isLocked;

        [SerializeField]
        private List<string> _tags;

        public void Awake()
        {
            _isLocked = false;
        }

        public virtual void Start()
        {
            Gate gate = GatesManager.Instance.GetGate(_name);
            string tempName = _name;
            int i = 0;
            while(GatesManager.Instance.GetGate(_name) != null)
            {
                i++;
                tempName = _name + i;
            }

            _name = tempName;

            GatesManager.Instance.AddGate(this);
        }

        public virtual void OnTriggerEnter(Collider other)
        {
            if (_tags.Contains(other.tag))
            {
                PassThrough(other.gameObject);
            }
        }

        public virtual void OnDestroy()
        {
            GatesManager.Instance.RemoveGate(this);
        }

        public void Lock()
        {
            _isLocked = true;
        }

        public void Unlock()
        {
            _isLocked = false;
        }

        public bool IsLocked()
        {
            return _isLocked;
        }

        public Gate GetExit()
        {
            return _exit;
        }

        public string Name { get => _name; }

        public void ChangeDestination(Gate newExit)
        {
            _exit = newExit;
        }

        public void PassThrough(GameObject entity)
        {
            if(_exit == null) return;
            if (!_isLocked)
            {
                entity.transform.position = _exit.transform.position;
            }
        }
    }
}
