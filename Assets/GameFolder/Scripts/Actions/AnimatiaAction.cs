using UnityEngine;
using UnityEngine.Events;

public class AnimatiaAction : MonoBehaviour
{
    [SerializeField] private UnityEvent ActionEvent;

    public void Action()
    {
        ActionEvent.Invoke();
    }
}
