using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public LightController Player1Light;

    public LightController Player2Light;

    public MirrorReception MirrorLight;

    private void Awake()
    {
        Instance = this;
    }

    public void MirrorPlayer1Light(Vector3 reflectionposition, float angle, bool isPositive)
    {
        MirrorLight.ReflectLightPlayer1(reflectionposition, angle, isPositive);
    }

}
