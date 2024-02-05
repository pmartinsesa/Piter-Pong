using Assets.Scripts.Menu;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class GameManager : MonoBehaviour
    {
        [Header("Controllers")]
        public PlayerController Player1;
        public PlayerController Player2;
        public BallController Ball;
        public GoalController LeftGoal;
        public GoalController RightGoal;

        [Header("Texts")]
        public GameObject WinnerText;


        void Awake()
        {
            InicializeGame();
        }

        public void InicializeGame()
        {
            WinnerText.SetActive(false);

            Player1.ResetPlayer(
                new Vector3(-8f, 0f, 0f),
                SaveController.Instance.colorPlayer1,
                SaveController.Instance.IsPlayer1Computer
            );
            Player2.ResetPlayer(new Vector3(8f, 0f, 0f),
                SaveController.Instance.colorPlayer2,
                SaveController.Instance.IsPlayer2Computer
            );
            Ball.ResetBall();
            LeftGoal.ResetScoreboard();
            RightGoal.ResetScoreboard();
        }

        public void EndGame(string winner)
        {
            Player1.Disable();
            Player2.Disable();
            Ball.Disable();

            var nameOfWinner = winner == "Player 1" ? SaveController.Instance.namePlayer1 : SaveController.Instance.namePlayer2;
            StartCoroutine(SetWinner(nameOfWinner));
        }

        IEnumerator SetWinner(string nameOfWinner)
        {
            WinnerText.GetComponent<TMP_Text>().SetText($"{nameOfWinner} Venceu!");
            WinnerText.SetActive(true);

            yield return new WaitForSeconds(3);
            SceneManager.LoadScene("Main Menu");
            SaveController.Instance.ResetSingleton();
        }
    }
}
