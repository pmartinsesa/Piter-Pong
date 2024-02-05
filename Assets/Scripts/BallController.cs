using System;
using UnityEngine;

namespace Assets.Scripts
{
    public class BallController : MonoBehaviour
    {
        [Header("Ball Properties")]
        public float SpeedUp = 1.1f;

        private Rigidbody2D Ball;
        private float MaxSpeed = 10f;

        public void ResetBall()
        {
            gameObject.SetActive(true);
            transform.position = Vector3.zero;
            if (Ball == null) Ball = GetComponent<Rigidbody2D>();

            Ball.velocity = GetStartingBallDirection();
        }

        public void Disable()
        {
            gameObject.SetActive(false);
        }

        public Vector2 GetStartingBallDirection()
        {
            var rightDirection = UnityEngine.Random.Range(0f, 1f) > 0.5;
            return new Vector2(rightDirection ? 6f : -6f, UnityEngine.Random.Range(-6f, 6f));
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            var velocity = Ball.velocity;
            Ball.velocity = Vector2.zero;

            if (collision.gameObject.tag == "Wall")
            {
                WallColisionHandler(velocity);
            }
            else if (collision.gameObject.tag == "Player")
            {
                var relativePlayerCollision = transform.position.y - collision.transform.position.y;
                PlayerColisionHandler(relativePlayerCollision, velocity);
            }
            else if (collision.gameObject.tag == "Goal")
            {
                GoalColisionHandler();
            }
        }

        private void WallColisionHandler(Vector2 velocity)
        {
            velocity.y = -velocity.y;
            Ball.velocity = velocity;
        }

        private void GoalColisionHandler()
        {
            gameObject.SetActive(false);
            ResetBall();
        }

        private void PlayerColisionHandler(float relativePlayerCollision, Vector2 velocity)
        {
            var absoluteVelocity = GetNewAbsoluteVelocity(
                new Vector2(Math.Abs(velocity.x), Math.Abs(velocity.y)),
                relativePlayerCollision
            );

            Ball.velocity = GetVelocityDirection(absoluteVelocity, velocity, relativePlayerCollision);
        }

        private Vector2 GetNewAbsoluteVelocity(Vector2 actualAbsoluteVelocity, float relativePlayerCollision)
        {
            var newVelocity = Vector2.zero;
            var verticalSpeedUp = 1 + Math.Abs(relativePlayerCollision);

            newVelocity.x = actualAbsoluteVelocity.x * SpeedUp < MaxSpeed ?
               actualAbsoluteVelocity.x * SpeedUp : MaxSpeed;

            newVelocity.y = actualAbsoluteVelocity.y * verticalSpeedUp <= MaxSpeed ?
                actualAbsoluteVelocity.y * verticalSpeedUp : MaxSpeed;

            return newVelocity;
        }

        private Vector2 GetVelocityDirection(Vector2 absoluteVelocity, Vector2 velocity, float relativePlayerCollision)
        {
            var hasReverseYAxis = (relativePlayerCollision > 0 && absoluteVelocity.y < 0) || (relativePlayerCollision < 0 && absoluteVelocity.y > 0);
            var hasReverseXAxis = (absoluteVelocity.x < 0 && velocity.x < 0) || (absoluteVelocity.x > 0 && velocity.x > 0);

            if (hasReverseYAxis)
                absoluteVelocity.y = -absoluteVelocity.y;
            if (hasReverseXAxis)
                absoluteVelocity.x = -absoluteVelocity.x;

            return absoluteVelocity;
        }
    }
}
