using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace mygame
{
    public class Buff0Effect : MonoBehaviour
    {
        // Start is called before the first frame update
        public Damage damage;
        void Start()
        {
            Destroy(this.gameObject, 1.2f);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "enemy")
            {
                collision.gameObject.GetComponent<EnemyManager>().OnDamageTaken(damage);
            }
        }
        // Update is called once per frame
        void Update()
        {

        }
    }
}