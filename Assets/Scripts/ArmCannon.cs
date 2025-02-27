﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ArmCannon : MonoBehaviour
{

    private Vector3 cannonLocalPos;

    public Transform cannonModel;
    [Space]
    public ParticleSystem cannonParticleShooter;
    public ParticleSystem missleShooter;
    public ParticleSystem chargingParticle;
    public ParticleSystem chargedParticle;
    public ParticleSystem lineParticles;
    public ParticleSystem chargedCannonParticle;
    public ParticleSystem chargedEmission;
    public ParticleSystem muzzleFlash;

    public bool activateCharge;
    public bool charging;
    public bool charged;
    public float holdTime = 1;
    public float chargeTime = .5f;

    private float holdTimer;
    private float chargeTimer;

    private float limitBugTime = 0.1F;
    private float limitBugTimer;

    [Space]

    public float punchStrenght = .2f;
    public int punchVibrato = 5;
    public float punchDuration = .3f;
    [Range(0, 1)]
    public float punchElasticity = .5f;

    [Space]
    [ColorUsageAttribute(true, true)]
    public Color normalEmissionColor;
    [ColorUsageAttribute(true, true)]
    public Color finalEmissionColor;

    public AudioClip shootAudio;
    public AudioClip missleAudio;
    public AudioClip chargeAudio;
    public AudioClip chargeShotAudio;
    private AudioSource audioSource;

    private MissleAmmo missleAmmo;


    void Start()
    {
        cannonLocalPos = cannonModel.localPosition;
        if(audioSource == null){
            audioSource = GetComponent<AudioSource>();
            Debug.Log(audioSource);
        }
        if(shootAudio == null){
            shootAudio = Resources.Load<AudioClip>("firebig_1");

        }
        if(missleAudio == null){
            missleAudio = Resources.Load<AudioClip>("Audio/ice04.wav");
        }
        if(missleAmmo == null){
            missleAmmo = GetComponent<MissleAmmo>();
            Debug.Log(missleAmmo);
        }

    }

    void Update()
    {

        //SHOOT
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();

            holdTimer = Time.time;
            activateCharge = true;
            
            audioSource.PlayOneShot(shootAudio, 0.1F);
        } 
        else if (Input.GetMouseButtonDown(1))
        {
            //muzzleFlash.Play();
            if(missleAmmo.ammo > 0) {
                cannonModel.DOComplete();
                cannonModel.DOPunchPosition(new Vector3(0, 0, -2*punchStrenght), 2*punchDuration, punchVibrato, punchElasticity);
                audioSource.PlayOneShot(missleAudio, 0.01F);
                missleShooter.Play();
                missleAmmo.ammo -= 1;
            } else {
                cannonModel.DOComplete();
                cannonModel.DOPunchPosition(new Vector3(0, 0, -0.1f*punchStrenght), 0.1f*punchDuration, punchVibrato, punchElasticity);
                audioSource.PlayOneShot(missleAudio, 0.01F);
            }
            

        }

        //RELEASE
        if (Input.GetMouseButtonUp(0))
        {
            activateCharge = false;


            if (charging)
            {
                audioSource.PlayOneShot(chargeShotAudio, 0.1F);
                chargedCannonParticle.Play();
                charging = false;
                charged = false;
                chargedParticle.transform.DOScale(0, .05f).OnComplete(()=>chargedParticle.Clear());
                lineParticles.Stop();


                Sequence s = DOTween.Sequence();
                s.Append(cannonModel.DOPunchPosition(new Vector3(0, 0, -punchStrenght), punchDuration, punchVibrato, punchElasticity));
                s.Join(cannonModel.GetComponentInChildren<Renderer>().material.DOColor(normalEmissionColor, "_EmissionColor", punchDuration));
                s.Join(cannonModel.DOLocalMove(cannonLocalPos, punchDuration).SetDelay(punchDuration));
            }
        }

        //HOLD CHARGE
        if (activateCharge && !charging)
        {
            if (Time.time - holdTimer > holdTime)
            {
                charging = true;
                chargingParticle.Play();
                lineParticles.Play();
                chargeTimer = Time.time;
                audioSource.PlayOneShot(chargeAudio, 0.1F);

                cannonModel.DOLocalMoveZ(cannonLocalPos.z - .22f, chargeTime);
                cannonModel.GetComponentInChildren<Renderer>().material.DOColor(finalEmissionColor, "_EmissionColor", chargeTime);
            }
        }

        //CHARGING
        if (charging && !charged)
        {
            if (Time.time - chargeTimer > chargeTime &&  Input.GetMouseButton(0))
            {
                charged = true;
                chargedParticle.Play();

                chargedParticle.transform.localScale = Vector3.zero;
                chargedParticle.transform.DOScale(1, .4f).SetEase(Ease.OutBack);
                chargedEmission.Play();
            }
        } 
        // if(!charged && !charging && !activateCharge && chargedParticle.isPlaying){
        //     if(limitBugTimer > limitBugTime) {
        //         Debug.Log("Stopped BS");
        //         chargedParticle.Stop();
        //         limitBugTime = 0;
        //         } else {
        //             limitBugTimer += Time.deltaTime;
        //         }
                
        // }
    }


    void Shoot()
    {
        muzzleFlash.Play();

        cannonModel.DOComplete();
        cannonModel.DOPunchPosition(new Vector3(0, 0, -punchStrenght), punchDuration, punchVibrato, punchElasticity);

        cannonParticleShooter.Play();
        
    }

}
