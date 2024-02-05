using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Menu
{
    public class ColorSelectionButton : MonoBehaviour
    {
        public Button button;
        public Image paddleMenuImage;
        public bool isPlayer1Color = false;

        public void OnButtonClick()
        {
            paddleMenuImage.color = button.colors.normalColor;

            if (isPlayer1Color)
                SaveController.Instance.colorPlayer1 = button.colors.normalColor;
            else
                SaveController.Instance.colorPlayer2 = button.colors.normalColor;

        }
    }
}
