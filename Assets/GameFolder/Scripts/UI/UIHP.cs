using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class UIHP : MonoBehaviour
    {
        [SerializeField] private Image HPBar;
        [SerializeField] private bool lookToCam = true;

        private void Start()
        {
            HPBar.fillAmount = 1;
        }

        public void ChangeHP(float value, float maxValue)
        {
            HPBar.fillAmount = 1 - ((maxValue - value) / maxValue);
        }

        private void LateUpdate()
        {
            if (!lookToCam) return;

            transform.LookAt(new Vector3(transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z));
            transform.Rotate(0, 180, 0);
        }

    }
}