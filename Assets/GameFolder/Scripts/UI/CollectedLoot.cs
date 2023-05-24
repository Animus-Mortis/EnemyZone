using TMPro;
using UnityEngine;

namespace Game.UI
{
    public class CollectedLoot : MonoBehaviour, IViewer
    {
        [SerializeField] private TextMeshProUGUI valueText;
        [SerializeField] private int price;
        [SerializeField] private ScoreViewer scoreViewer;

        public int count;

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
            valueText.text = count.ToString("0");
        }

        public void Spend()
        {
            scoreViewer.TakeValue(count * price);
            count = 0;
            UpdateText();
        }
    }
}