using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts
{
    public class GoalController : MonoBehaviour
    {
        [Header("Goal Properties")]
        public TMP_Text GoalText;

        [Header("Events")]
        public UnityEvent EndGame;

        public void ResetScoreboard()
        {
            GoalText.text = "0";
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.name == "Ball")
            {
                GoalHandler();
            }
        }

        private void GoalHandler()
        {
            var result = int.Parse(GoalText.text) + 1;
            GoalText.text = result.ToString();

            if (result >= 3)
            {
                EndGame.Invoke();
            }
        }
    }
}
