using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;


//NPC的需求值，值越低，越需要满足
[ExecuteInEditMode]
[Serializable]
public class NPCDemandValue
{
    //饥饿值
    public float  hungerLevel;
    public float HungerLevel
    {
        get
        {
            return hungerLevel;
        }
        set
        {
            hungerLevel = value;
        }
    }
    public AnimationCurve hungerCurve = AnimationCurve.EaseInOut(100, 100, 0, 0);

    //疲劳值
    public float tirednessLevel;
    public float TirednessLevel
    {
        get
        {
            return tirednessLevel;
        }
        set
        {
            tirednessLevel = value;
        }
    }

    public AnimationCurve tirednessCurve = AnimationCurve.EaseInOut(100, 100, 0, 0);

    //娱乐值
    public float funLevel;
    public float FunLevel
    {
        get
        {
            return funLevel;
        }
        set
        {
            funLevel = value;
        }
    }
    public AnimationCurve funCurve = AnimationCurve.EaseInOut(100, 100, 0, 0);


    public NPCDemandValue(int hungerLevel=50, int tirednessLevel = 100, int funLevel = 50)
    {
        this.hungerLevel = hungerLevel;
        this.tirednessLevel = tirednessLevel;
        this.funLevel = funLevel;
    }

    public void DailyCost()
    {
        hungerLevel = Mathf.Clamp(hungerLevel- 0.01f, 0, 100);
        tirednessLevel = Mathf.Clamp(tirednessLevel - 0.003f, 0, 100);
        funLevel = Mathf.Clamp(funLevel - 0.006f, 0, 100);
    }
    //获得饥饿值的权重
    public float GetWeightHunger()
    {
        return hungerCurve.Evaluate(hungerLevel);
    }

    //获得疲劳值的权重
    public float GetWeightTiredness()
    {
        return tirednessCurve.Evaluate(tirednessLevel);
    }

    //获得娱乐值的权重
    public float GetWeightFun()
    {
        return funCurve.Evaluate(funLevel);
    }

    //计算当前最想干什么
    public string WhattToDO()
    {
        SortedList<string, float> sortedList = new SortedList<string, float>();
        sortedList["吃"] = GetWeightHunger();
        sortedList["睡"] = GetWeightTiredness();
        sortedList["玩"] = GetWeightFun();
        //从大到小排序权重
        var temp = sortedList.OrderByDescending(x=>x.Value);

        //foreach (KeyValuePair<string, float> action in temp)
        //{
        //    Debug.Log("action: " + action.Key + ", value: " + action.Value);
        //}
        return temp.First().Key;
    }

    //回复值
    public void Recover(string action)
    {
        if (action == "吃")
        {
            hungerLevel = Mathf.Clamp(hungerLevel+0.1f,0,100);

        }
        else if (action == "睡")
        {
            tirednessLevel  = Mathf.Clamp(tirednessLevel + 0.05f, 0, 100);
        }
        else if (action == "玩")
        {
            funLevel  = Mathf.Clamp(funLevel + 0.1f, 0, 100);
        }
    }
}


public enum ActionType
{
    Daily,
    Realtime
}






[RequireComponent(typeof(Rigidbody2D))]
public class NPC : MonoBehaviour
{
    [SerializeField]
    public NPCDemandValue npcDemandValue = new NPCDemandValue(10, 10, 10);

    public TMP_Text emojiText;
    private Rigidbody2D rb;
    private Dictionary<GameObject, string> objFeelDic = new Dictionary<GameObject, string>();

    public string action;

    public GameObject targetObj;


    //检测周期的float变量
    private float actionCoolTime = 3;
    private float actionTimer = 0;
    public bool canAction = true;

    private float dailyActionCoolTime = 10;
    public float dailyActionTimer = 20;
    public bool canDailyAction = true;



    public ActionType actoinType = ActionType.Daily;


    public float distance = 3.0f;
    public GameObject player;


    //指的是当前正在做的日常行为
    public string nowDailyAction = "";

    [SerializeField]
    private float moveSpeed = 5f;

    private void UpdateAction()
    { 
        actionTimer += Time.deltaTime;
        if (actionTimer >= actionCoolTime)
        {
            actionTimer = actionCoolTime+1;
            canAction = true;
        }
        else
        {
            canAction = false;
        }
    }

    private void UpdateDailyAction()
    { 
        dailyActionTimer += Time.deltaTime;
        if (dailyActionTimer >= dailyActionCoolTime)
        {
            dailyActionTimer = dailyActionCoolTime+1;
            canDailyAction = true;
        }
        else
        {
            canDailyAction = false;
        }
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        //初始化的时候，加入玩家
        usefulDic.Add(player, "玩,睡");
    }
    private void Update()
    {
        UpdateAction();
        UpdateDailyAction();
        npcDemandValue.DailyCost();
        

        DoDailyAction();

        if (actoinType == ActionType.Daily)
        {
            if (targetObj == null)
            {
                return;
            }
            //靠近对方
            var dis = Vector2.Distance(transform.position, targetObj.transform.position);
            if (dis < distance)
            {
                switch (nowDailyAction)
                {
                    case "吃":
                        emojiText.text = "(^～^)嚼！";
                        Debug.Log("NPC正在吃东西");
                        break;
                    case "睡":
                        emojiText.text = "zZZ~";
                        Debug.Log("NPC正在睡觉");
                        break;
                    case "玩":
                        emojiText.text = "嘿嘿，来陪我玩耍吧";
                        Debug.Log("NPC正在玩耍");
                        break;
                }
                npcDemandValue.Recover(nowDailyAction);
                return;
            }
            else
            {
                emojiText.text = "";
                Vector2 dir = new Vector2(0, 0);
                if (targetObj != null)
                {
                    dir = (targetObj.transform.position - transform.position).normalized;
                }

                transform.Translate(dir * Time.deltaTime * 5.0f);
            }
        }
        else if (actoinType == ActionType.Realtime)
        {
            actoinType = ActionType.Daily;
            targetObj = null;
        }
    }

    public void DoAction()
    {
    
        GameObject obj;
        obj = player;
    

        if (action == "远离")
        {
            //isChase = false;
            //对方在右边
            Escape(obj);

        }
        else if (action == "靠近")
        {
            //对方在右边
            if (transform.position.x <= obj.transform.position.x)
            {

                rb.AddForce(Vector2.right * speed);
            }
            //在左边
            else if (transform.position.x > obj.transform.position.x)
            {
                rb.AddForce(Vector2.left * speed);

            }
            rb.AddForce(Vector2.left * speed);
        }
        else if (action == "保持不动")
        {
            //isChase = false;
        }
        else if (action == "高兴")
        {
            StartCoroutine(Jump());
        }
        else if (action == "左右看")
        {
            StartCoroutine(Wander());
        }
        else if (action == "追逐")
        {
            targetObj = obj;
            //isChase = true;
            //StartCoroutine(Chase());

        }
    }

    //当NPC遇到一个即时事件，会执行一个action
    public void DoAction(GameObject gameObject)
    {
        if (canAction == false)
        {
            actionTimer = 0;
            return;

        }
        //处理情绪
        objFeelDic.TryGetValue(gameObject, out string feel);
        if (feel == "高兴")
        {
            StartCoroutine(ShowEmoji("😊"));
            StartCoroutine(Jump());
        }
        else if (feel == "恐惧")
        {
            StartCoroutine(ShowEmoji("☹️"));
            //actoinType = ActionType.Realtime;
            Escape(gameObject);
            //actoinType = ActionType.Daily;
        }
        else //"没有感觉"
        {
            StartCoroutine(ShowEmoji("😒"));
            StartCoroutine(Wander());
        }
    }


    //当NPC第一次遇到一个物体的时候，会记录NPC对这个物体的感觉
    public void ProcessFirstFeel(string feel,GameObject obj)
    {
        //print(feel);
        if (feel.Contains("高兴"))
        {
            objFeelDic.Add(obj, "高兴");
        }
        else if (feel.Contains("恐惧"))
        {
            objFeelDic.Add(obj, "恐惧");
        }
        else if (feel.Contains("没有感觉"))
        {

            objFeelDic.Add(obj, "没有感觉");
        }
        DoAction(obj);
    }

    Dictionary<GameObject,string> usefulDic= new Dictionary<GameObject, string>();

    //当NPC第一次遇到一个物体的时候，会记录这个物体的用处
    public void ProcessFirstUseful( GameObject obj, string useful)
    {

        //如果这个玩意让自己害怕就不会去用
        objFeelDic.TryGetValue(obj, out string feel);
        if (feel == "恐惧")
        {
            return;
        }
       

        print(useful);
        if (useful.Contains("睡"))
        {
            usefulDic.Add(obj, "睡");
        }
        else if (useful.Contains("吃"))
        {
            usefulDic.Add(obj, "吃");
        }
        else if (useful.Contains("玩"))
        {
            usefulDic.Add(obj, "玩");
        }
    }


    IEnumerator ShowEmoji(string emoji)
    {
        emojiText.text = emoji;

        yield return new WaitForSeconds(3);

        emojiText.text = "";
    }


    public void DoDailyAction()
    {

        if (canDailyAction == false)
        {
            
            return;
        }
        
        dailyActionTimer = 0;
        string whatToDo = npcDemandValue.WhattToDO();
        Debug.Log($"NPC决定要: {whatToDo}");
        
        List<GameObject> playObjects = usefulDic.Where(pair => pair.Value.Contains(whatToDo)).Select(pair => pair.Key).ToList();
        if (playObjects.Count == 0)
        {
            Debug.Log("NPC找不到可以满足需求的物体");
            return;
        }

        nowDailyAction = whatToDo;
        int index = UnityEngine.Random.Range(0, playObjects.Count);
        targetObj = playObjects[index];
        Debug.Log($"NPC找到了目标物体: {targetObj.name}");
    }





    //情绪的表达
    IEnumerator Jump()
    {
        Vector3 startPos = transform.position;
        float jumpHeight = 2f;
        float jumpDuration = 0.5f;
        float elapsed = 0f;
        
        while (elapsed < jumpDuration)
        {
            elapsed += Time.deltaTime;
            float height = Mathf.Sin((elapsed / jumpDuration) * Mathf.PI) * jumpHeight;
            transform.position = startPos + Vector3.up * height;
            yield return null;
        }
        transform.position = startPos;
        
        yield return new WaitForSeconds(0.5f);
    }
    IEnumerator Wander()
    {
        transform.localEulerAngles = new Vector3(0, 180, 0);
        yield return new WaitForSeconds(0.5f);
        transform.localEulerAngles = new Vector3(0, 0, 0);
        yield return new WaitForSeconds(0.5f);
        transform.localEulerAngles = new Vector3(0, 180, 0);
        yield return new WaitForSeconds(0.5f);
        transform.localEulerAngles = new Vector3(0, 0, 0);
        yield return new WaitForSeconds(0.5f);

    }


    float speed = 500.0f;


    //逃走,逃离
    public void Escape(GameObject target)
    {
        Vector3 direction;
        if (transform.position.x <= target.transform.position.x)
        {
            direction = Vector3.left;
        }
        else
        {
            direction = Vector3.right;
        }
        transform.Translate(direction * moveSpeed * Time.deltaTime * 2);
    }



}
