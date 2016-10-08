using UnityEngine;
using System.Collections;

namespace RoidRunner
{


    public class healthBar : MonoBehaviour
    {
        [SerializeField]private Player player;
        [SerializeField]
        private Rect healthBox;
        private float perc;
        private float size;
        private Rect newBox;
        // Use this for initialization
        void Start()
        {
            perc = player.currHealth;
            size = healthBox.width;
        }

        // Update is called once per frame
        void Update()
        {
            

        }

        void UpdateHealthBar()
        {
            float increase = player.currHealth / perc;
            float size2 = Mathf.Abs(size * increase);
            healthBox.width = size2;
            Debug.Log("Current Health = " + player.currHealth);
            Debug.Log("Current % = " + size2);
        }
    }
}