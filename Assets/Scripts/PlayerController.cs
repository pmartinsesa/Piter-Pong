using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerController : MonoBehaviour
    {
        [Header("Player Properties")]
        public float Speed = 5;
        public bool IsComputer = false;

        private GameObject Ball;

        private delegate Vector3 PlayerMover();
        private PlayerMover PlayerMoverCallback;

        public void ResetPlayer(Vector3 initialPosition, Color color, bool isComputer)
        {
            gameObject.GetComponent<SpriteRenderer>().color = color;

            transform.position = initialPosition;
            IsComputer = isComputer;
            gameObject.SetActive(true);
        }

        public void Disable()
        {
            gameObject.SetActive(false);
        }

        void Start()
        {
            if (IsComputer)
            {
                Ball = GameObject.Find("Ball");
                PlayerMoverCallback = GetNewEnemyPosition;
            }
            else
            {
                PlayerMoverCallback = GetNewPlayerPosition;
            }
        }

        void Update()
        {
            transform.position = PlayerMoverCallback();
        }

        private Vector3 GetNewPlayerPosition()
        {
            var moveInput = Input.GetAxis(gameObject.name == "Player1" ? "Vertical" : "Vertical2");
            var newPosition = transform.position + moveInput * Speed * Time.deltaTime * Vector3.up;
            newPosition.y = Mathf.Clamp(newPosition.y, -4f, 4f);

            return newPosition;
        }

        private Vector3 GetNewEnemyPosition()
        {
            var ballVerticalPostion = Mathf.Clamp(Ball.transform.position.y, -4f, 4f);
            var targetPosition = new Vector2(transform.position.x, ballVerticalPostion);
            return Vector2.MoveTowards(transform.position, targetPosition, Speed * Time.deltaTime);
        }
    }
}
