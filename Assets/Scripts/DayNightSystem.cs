using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;


public class DayNightSystem : MonoBehaviour
{
    private static DayNightSystem instance;
    public static DayNightSystem Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<DayNightSystem>();
            }
            return instance;
        }
    }


    public enum TimeType 
    {
        Dawn,
        Day,
        Noon,
        Dusk,
        Night,
        Midnight

    }

    private TimeType[] timeTypes = new TimeType[] { TimeType.Dawn, TimeType.Day, TimeType.Noon, TimeType.Dusk, TimeType.Night, TimeType.Midnight };
    private int timeTypeIndex = 0;

    private TimeType lastTimeType;
    public TimeType timeType;


    public Light2D globalLight;
    public Light2D playerLight;

    private float duration = 2.0f;

    private bool isChanging = false;

    public Light2D[] lights;



    public float coolDown = 60.0f;
    private float timer = 0;
    //让所有灯的亮度变为0

    //public GPTDialog gPTDialog;

    public void intensityOfAllLights(float value)
    {
        foreach (var light in lights)
        {
            light.intensity = value;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        timeType = TimeType.Day;
        lastTimeType = timeType;


        //changeTo(TimeType.Dawn);
    }

    // Update is called once per frame
    void Update()
    {

        //当按下F键的适合，切换一个timeType
        if (Input.GetKeyDown(KeyCode.F)  && isChanging ==false)
        {
            if (timeTypeIndex >= timeTypes.Length)
            {
                timeTypeIndex = 0;
                
            }
            timeType = timeTypes[timeTypeIndex];
            StartCoroutine(changeTo(timeTypes[timeTypeIndex]));
            timeTypeIndex++;
        }

        timer += Time.deltaTime;
        if (timer > coolDown && isChanging == false)
        {
            timer = 0;


            if (timeTypeIndex >= timeTypes.Length)
            {
                timeTypeIndex = 0;

            }
            timeType = timeTypes[timeTypeIndex];
            StartCoroutine(changeTo(timeTypes[timeTypeIndex]));
            timeTypeIndex++;

        }
    }

    IEnumerator changeTo(TimeType timeType)
    {
        isChanging = true;
        Color globalLightColor = Color.white;
        float globalIntensity = 0.8f;


        Color initGlobalLightColor = globalLight.color;
        float initGlobalIntensity = globalLight.intensity;


        Color playerLightColor = Color.white;
        float playerIntensity = 1.0f;
        float playerLightOuterRadius = 5.0f;


        Color initPlayerLightColor = playerLight.color;
        float initPlayerIntensity = playerLight.intensity;
        float initPlayerLightOuterRadius = playerLight.pointLightOuterRadius;


        switch (timeType)
        {
            case TimeType.Dawn:
                globalLightColor = new Color(255 / 255.0f, 236 / 255.0f, 165 / 255.0f);
                globalIntensity = 0.5f;

                playerLightColor = new Color(87 / 255.0f, 87 / 255.0f, 87 / 255.0f);
                playerIntensity = 1.0f;
                playerLightOuterRadius = 6.0f;
                intensityOfAllLights(0.7f);



                break;
            case TimeType.Day:
                globalLightColor = new Color(255 / 255.0f, 255 / 255.0f, 255 / 255.0f);
                globalIntensity = 1.1f;

                playerLightColor = new Color(255 / 255.0f, 255 / 255.0f, 255 / 255.0f);
                playerIntensity = 0f;
                playerLightOuterRadius = 6.0f;
                intensityOfAllLights(0.5f);


                break;
            case TimeType.Noon:
                globalLightColor = new Color(255 / 255.0f, 246 / 255.0f, 213 / 255.0f);
                globalIntensity = 1.3f;

                playerLightColor = new Color(255 / 255.0f, 255 / 255.0f, 255 / 255.0f);
                playerIntensity = 1.0f;
                playerLightOuterRadius = 6.0f;
                intensityOfAllLights(0f);
                break;
            case TimeType.Dusk:
                globalLightColor = new Color(255 / 255.0f, 109 / 255.0f, 217 / 255.0f);
                globalIntensity = 0.8f;

                playerLightColor = new Color(255 / 255.0f, 255 / 255.0f, 255 / 255.0f);
                playerIntensity = 0.5f;
                playerLightOuterRadius = 6.0f;
                intensityOfAllLights(0.5f);
;
                break;
            case TimeType.Night:
                globalLightColor = new Color(42 / 255.0f, 116 / 255.0f, 197 / 255.0f);
                globalIntensity = 0.5f;

                playerLightColor = new Color(248 / 255.0f, 241 / 255.0f, 188 / 255.0f);
                playerIntensity = 1.5f;
                playerLightOuterRadius = 3f;
                intensityOfAllLights(1f);
                break;
            case TimeType.Midnight:
                globalLightColor = new Color(5 / 255.0f, 23 / 255.0f, 48 / 255.0f);
                globalIntensity = 0.3f;

                playerLightColor = new Color(224 / 255.0f, 224 / 255.0f, 224 / 255.0f);
                playerIntensity = 1.5f;
                playerLightOuterRadius = 3f;
                intensityOfAllLights(1.5f);

                break;
            default:
                break;

              
        }

        //print("渐变前");
        float timer = 0;

        while (timer < duration)
        {
            //print("渐变中");

            playerLight.pointLightOuterRadius = Mathf.Lerp(initPlayerLightOuterRadius, playerLightOuterRadius, timer / duration);
            playerLight.intensity = Mathf.Lerp(initPlayerIntensity, playerIntensity, timer / duration);
            playerLight.color = Color.Lerp(initPlayerLightColor, playerLightColor, timer / duration);
            globalLight.intensity = Mathf.Lerp(initGlobalIntensity, globalIntensity, timer / duration);
            globalLight.color = Color.Lerp(initGlobalLightColor, globalLightColor, timer / duration);

            timer += Time.deltaTime;
            yield return null;
        }
        //print("渐变结束");
        playerLight.pointLightOuterRadius = playerLightOuterRadius;
        playerLight.intensity = playerIntensity;
        playerLight.color = playerLightColor;

        isChanging = false;


    }





   

}
