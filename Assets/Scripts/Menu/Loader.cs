using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Menu
{
    public class Loader : MonoBehaviour
    {
        public TMP_InputField inputPlayer1;
        public TMP_InputField inputPlayer2;

        public void LoadMainScene(string ButtonPressed)
        {
            SetPlayersName();
            SetPlayMode(ButtonPressed);

            SceneManager.LoadScene("Main");
        }

        private void SetPlayersName()
        {
            if (inputPlayer1.text != string.Empty)
            {
                SaveController.Instance.namePlayer1 = inputPlayer1.text;
            }

            if (inputPlayer2.text != string.Empty)
            {
                SaveController.Instance.namePlayer2 = inputPlayer2.text;
            }
        }

        private void SetPlayMode(string ButtonPressed)
        {
            if (ButtonPressed == "Single")
            {
                SetSinglePlayer();
            }
            else if (ButtonPressed == "PvP")
            {
                SetMultiPlayer();
            }
            else if (ButtonPressed == "COMvCOM")
            {
                SetComputerPlay();
            }
        }

        private void SetSinglePlayer()
        {
            SaveController.Instance.IsPlayer1Computer = false;
            SaveController.Instance.IsPlayer2Computer = true;
        }

        private void SetMultiPlayer()
        {
            SaveController.Instance.IsPlayer1Computer = false;
            SaveController.Instance.IsPlayer2Computer = false;
        }

        private void SetComputerPlay()
        {
            SaveController.Instance.IsPlayer1Computer = true;
            SaveController.Instance.IsPlayer2Computer = true;
        }
    }
}


