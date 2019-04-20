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
            Destroy(this.gameObject, 1.5f);
        }

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("trigger");
            Debug.Log(other.gameObject.tag);
            if (other.gameObject.tag == "Enemy")
            {
                Debug.Log("boom");
                other.gameObject.GetComponent<EnemyManager>().OnDamageTaken(damage);
            }
        }
        // Update is called once per frame
        void Update()
        {

        }
    }
}