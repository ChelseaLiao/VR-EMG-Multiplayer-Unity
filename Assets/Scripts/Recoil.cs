using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recoil : MonoBehaviour
{
    public FighterBase fighter;

    private float recoil = 0.0f;
    private float maxRecoil_x = -20f;
    private float maxRecoil_y = 20f;
    public float recoilSpeed = 2f;
    public float damageRecoilMultiplier = 0.2f;

    protected void Start()
    {
        fighter.OnAfterTakingDamage.AddListener((float damage) =>
        {
            StartRecoil(damage);
        });
    }

    public void StartRecoil(float pRecoil) => StartRecoil(pRecoil, maxRecoil_x, recoilSpeed);

    private bool _recoiling = false;

    public void StartRecoil(float recoilParam, float maxRecoil_xParam, float recoilSpeedParam)
    {
        // in seconds
        recoil = recoilParam;
        maxRecoil_x = maxRecoil_xParam;
        recoilSpeed = recoilSpeedParam;
        maxRecoil_y = Random.Range(-maxRecoil_xParam, maxRecoil_xParam);
        _targetRecoilPosition = transform.position + (-transform.forward * recoilParam * damageRecoilMultiplier);
        _targetRecoilPosition.y = transform.position.y;
        _recoiling = true;
    }

    private Vector3 _targetRecoilPosition;

    void Recoiling()
    {
        if (_recoiling && Vector3.Distance(transform.position, _targetRecoilPosition) >= 0.1f)
        {
            //Quaternion maxRecoil = Quaternion.Euler(maxRecoil_x, maxRecoil_y, 0f);
            // Dampen towards the target rotation
            //transform.position = -transform.forward * Time.deltaTime * recoilSpeed;
            //Quaternion.Slerp(transform.localRotation, maxRecoil, Time.deltaTime * recoilSpeed);

            float step = recoilSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, _targetRecoilPosition, step);

            recoil -= Time.deltaTime;
        }
        else
        {
            _recoiling = false;
        }
        //else
        //{
        //    recoil = 0f;
        //    // Dampen towards the target rotation
        //    transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.identity, Time.deltaTime * recoilSpeed / 2);
        //}
    }

    // Update is called once per frame
    void Update()
    {
        Recoiling();
    }
}
