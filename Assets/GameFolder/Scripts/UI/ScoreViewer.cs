using TMPro;
using UnityEngine;

namespace Game.UI
{
    public class ScoreViewer : MonoBehaviour, IViewer
    {
        [SerializeField] private TextMeshProUGUI valueText;

        private int count;

        private void Awake()
        {
            UpdateText();
        }

        public void TakeValue(int value)
        {
            count += value;
            UpdateText();
        }

        public void UpdateText()
        {
            valueText.text = count.ToString();
        }
    }
}