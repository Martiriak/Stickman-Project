using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stickman.pigeonShooter
{
    /// <summary>
    /// Script attached to empty gameobject. 
    /// When area of screen touched, turns gun and spawns bullet.
    /// </summary>
    public class PointAndShoot : MonoBehaviour
    {
        public GameObject gun;
        public GameObject bulletPrefab;
        public GameObject bulletStart;
        public float bulletSpeed = 20.0f;

        private ObjectPooler objectPooler;
        private Vector3 touchedPoint;
        private Camera myCamera;

        void Start()
        {
            objectPooler = ObjectPooler.Instance;
            myCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                RotateAndShootGun();
            }
        }

        void RotateAndShootGun()
        {
            // Rotate gun
            touchedPoint = myCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z));
            Vector3 difference = touchedPoint - gun.transform.position;
            float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            gun.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);

            // Shoot Gun
            float distance = difference.magnitude;
            Vector2 direction = difference / distance;
            direction.Normalize();
            FireBullet(direction, rotationZ);
        }

        void FireBullet(Vector2 direction, float rotationZ)
        {
            GameObject b = objectPooler.SpawnFromPool("Bullet", bulletStart.transform.position, Quaternion.Euler(0.0f, 0.0f, rotationZ));
            b.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
        }
    }
}
