using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class AutoAttach : MonoBehaviour
{
    [SerializeField] private GameObject _gameObjectToAttach;
    
    private XRGrabInteractable _grabInteractable;
    // Start is called before the first frame update
    void Start()
    {
        _grabInteractable = _gameObjectToAttach.GetComponent<XRGrabInteractable>();
        _gameObjectToAttach.transform.SetParent(transform);
        _gameObjectToAttach.transform.localPosition = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
