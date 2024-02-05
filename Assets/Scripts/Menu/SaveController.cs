using UnityEngine;

namespace Assets.Scripts.Menu
{
    public class SaveController : MonoBehaviour
    {
        [Header("Play mode")]
        public bool IsPlayer1Computer = false;
        public bool IsPlayer2Computer = false;

        [Header("Players colors")]
        public Color colorPlayer1 = Color.white;
        public Color colorPlayer2 = Color.white;

        [Header("Players name")]
        public string namePlayer1 = "Player1";
        public string namePlayer2 = "Player2";

        private static SaveController _instance;

        public static SaveController Instance
        {
            get
            {
                if (_instance != null)
                    return _instance;

                _instance = FindObjectOfType<SaveController>();

                if (_instance != null)
                    return _instance;

                var singleton = new GameObject(typeof(SaveController).Name);
                _instance = singleton.AddComponent<SaveController>();

                return _instance;
            }
        }


        void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
                return;
            }

            DontDestroyOnLoad(gameObject);
        }

        public void ResetSingleton()
        {
            IsPlayer1Computer = false;
            IsPlayer2Computer = false;

            colorPlayer1 = Color.white;
            colorPlayer2 = Color.white;

            namePlayer1 = "Player1";
            namePlayer2 = "Player2";
        }
    }
}