using UnityEngine;

public class GunSystem : MonoBehaviour {
    [Header("射击参数")]
    public int damage = 10;
    public float range = 100f;
    public float fireRate = 15f;
    public float impactForce = 30f;

    [Header("引用")]
    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    public LineRenderer laserLine;

    private float nextTimeToFire = 0f;

    void Update() {
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire) {
            nextTimeToFire = Time.time + 1f/fireRate;
            Shoot();
        }
    }

    void Shoot() {
        muzzleFlash.Play(); // 枪口火焰
        
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range)) {
            // 伤害计算
            EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
            if (target != null) {
                target.TakeDamage(damage);
            }

            // 物理反馈
            if (hit.rigidbody != null) {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }

            // 弹道效果
            StartCoroutine(ShowShotEffect(hit.point));
            
            // 命中特效
            GameObject impact = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impact, 2f);
        }
    }

    IEnumerator ShowShotEffect(Vector3 hitPoint) {
        laserLine.SetPosition(0, transform.position);
        laserLine.SetPosition(1, hitPoint);
        laserLine.enabled = true;
        
        yield return new WaitForSeconds(0.02f);
        laserLine.enabled = false;
    }
}