using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using Random = System.Random;

namespace Assets.Scripts.Process.GunClub.Pistol
{
    public class FireBulletOnActivate : MonoBehaviour
    {
        public GameObject bullet;

        public Transform spawnPoint;

        public float FireSpeed = 10;
        public float FireDelay = 0.5f;

        public GameObject ShootEffect;

        //Better to set in private and declare outside the start for global access in class
        private XRGrabInteractable _grabbable;
        private float _nextFireTime;


        // Start is called before the first frame update
        void Start()
        {
            _grabbable = GetComponent<XRGrabInteractable>();
            _grabbable.activated.AddListener(FireBullet);
        }

        public void FireBullet(ActivateEventArgs arg)
        {
            if (_nextFireTime > Time.time) return;
            _nextFireTime = Time.time + FireDelay;
            Vector3 posAnim = new Vector3(spawnPoint.position.x, spawnPoint.position.y + 0.05f, spawnPoint.position.z);
            GameObject spawnedBullet = Instantiate(bullet, spawnPoint.position, spawnPoint.rotation);
            GameObject anime = Instantiate(ShootEffect, posAnim, spawnPoint.rotation);


            spawnedBullet.tag = "Bullet";
            spawnedBullet.GetComponent<Rigidbody>().velocity = spawnPoint.forward * FireSpeed;
            spawnedBullet.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;

            // Play the sound of the gun
            if (GetComponent<AudioSource>() != null)
                GetComponent<AudioSource>().Play();

            Destroy(anime, 5);
            Destroy(spawnedBullet, 5);
        }
    }
}