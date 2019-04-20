using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace mygame
{
    public class Buff0Effect : MonoBehaviour
    {
        // Start is called before the first frame update
        public Damage damage;
        int i;
        void Start()
        {
            Destroy(this.gameObject, 1.5f);
            EnemyCheck();
        }
        
        public void EnemyCheck()
        {
            Collider []c = Physics.OverlapSphere(transform.position, 3);
            for(int i = 0; i < c.Length; i++)
            {
                if (c[i].gameObject.tag == "Enemy")
                {
                    c[i].gameObject.GetComponent<EnemyManager>().OnDamageTaken(damage);
                }
            }
        }
        // Update is called once per frame
        void Update()
        {

        }
    }
}