using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace mygame
{
    public class EnemyView : MonoBehaviour
    {
        public AI ai;
        // Start is called before the first frame update
        void Start()
        {

        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                ai.target = other.gameObject;
            }
        }
        // Update is called once per frame
        void Update()
        {

        }
    }
}
