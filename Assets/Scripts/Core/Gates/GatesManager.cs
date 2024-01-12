using System.Collections.Generic;

namespace Assets.Scripts.Core.Gates
{
    public class GatesManager : Singleton<GatesManager>
    {
        private List<Gate> _gates = new List<Gate>();

        public void AddGate(Gate gate)
        {
            _gates.Add(gate);
        }

        public void RemoveGate(Gate gate)
        {
            _gates.Remove(gate);
        }

        public Gate GetGate(int index)
        {
            return _gates[index];
        }

        public Gate GetGate(string name)
        {
            return _gates.Find(gate => gate.Name == name);
        }

        public List<Gate> GetGates(List<string> names)
        {
            return _gates.FindAll(gate => names.Contains(gate.Name));
        }

        public List<Gate> GetGates()
        {
            return _gates;
        }
    }
}
