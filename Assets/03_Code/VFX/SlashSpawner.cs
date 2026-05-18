using System;
using System.Collections;
using _03_Code;
using _03_Code.Items.Weapons;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class SlashSpawner : MonoBehaviour
{
    public static SlashSpawner Instance;

    private void Awake()
    {
        Instance = this;
    }
    
    [Header("Components")]
    [SerializeField] private Transform slashPoint;
    [SerializeField] private SlashEffect slashPrefab;
    [SerializeField] private Sword sword;
    
    [Header("Settings")]
    [SerializeField] private float baseScale = 2.3f;

    [Header("Color")]
    [SerializeField] private Color slashColor = Color.cyan;

    [Header("SlashStyle")]
    [SerializeField] private SlashStyle style = SlashStyle.Single;

    //테스트하느라 enum으로 만들었음
    public enum SlashStyle
    {
        Single,
        Combo,
        Fury,
        Cross
    }

    public void Attack(SlashStyle slashStyle)
    {
        StartCoroutine(SpawnSlashRoutine(slashStyle));
    }

    private IEnumerator SpawnSlashRoutine(SlashStyle slashStyle)
    {
        float baseAngle = GetAngle();

        switch (slashStyle)
        {
            //한번 때리는거
            case SlashStyle.Single:
                SpawnSlash(baseAngle, 1f);
                break;

            //기억이 안남
            case SlashStyle.Combo:
                for (int i = 0; i < 3; i++)
                {
                    SpawnSlash(baseAngle + Random.Range(-25f, 25f), Random.Range(0.85f, 1.15f));
                    yield return new WaitForSeconds(0.08f);
                }
                break;

            //마구 난도질
            case SlashStyle.Fury:
                for (int i = 0; i < 5; i++)
                {
                    SpawnSlash(Random.Range(0f, 360f), Random.Range(0.65f, 1.1f));
                    yield return new WaitForSeconds(0.055f);
                }
                break;

            //2번 빡빡 때리는거
            case SlashStyle.Cross:
                SpawnSlash(baseAngle, 1.15f);
                yield return new WaitForSeconds(0.07f);
                SpawnSlash(baseAngle + 90f, 1.15f);
                break;
        }
    }

    private void SpawnSlash(float angle, float scaleMultiplier)
    {
        Vector3 pos = slashPoint ? slashPoint.position : transform.position;

        SlashEffect slash = Instantiate(slashPrefab, pos, Quaternion.identity);
        slash.Play(slashColor, angle, baseScale * scaleMultiplier);
    }

    
    //Let's Hard Coding
    private float GetAngle()
    {
        if (sword.CanUse)
        {
            if (GameManager.Instance.playerControl.RotationRight)
            {
                return -10f;
            }
            else
            {
                return 100f;
            }
        }
        else
        {
            if (GameManager.Instance.playerControl.RotationRight)
            {
                return -70f;
            }
            else
            {
                return -180f;
            }
        }
    }
}