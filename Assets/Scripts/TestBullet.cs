using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using UnityEngine;
namespace mygame
{
    public class TestBullet : MonoBehaviour
    {
        public Damage damage;
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Enemy")
            {
                EnemyManager enenmyManager = other.gameObject.GetComponent<EnemyManager>();
                enenmyManager.OnDamageTaken(damage);
                Destroy(gameObject);
            }
        }
        // Start is called before the first frame update
        void Start()
        {
            Destroy(gameObject, 5);
        }

        // Update is called once per frame
        void Update()
        {

        }
        public void fire(Vector3 vector3,float velocity)
        {
            Rigidbody rigidbody = GetComponent<Rigidbody>();
            rigidbody.velocity = vector3.normalized*velocity;
        }
    }
}