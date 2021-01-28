using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class ObjectPoolManager : SingletonBase<ObjectPoolManager>
{


    [SerializeField] private GameObject[] Prefabs;
    private List<GameObject> ObjPool;
    private Projector temp_p;
    private const  string indicator = "Indicator";

    private const string _text = "Text";
    private void Awake()
    {
        ObjPool = new List<GameObject>();
    }
   
    public void CreateObject(string name, int cnt = 5, Transform parent = null)
    {
        GameObject obj = null;
        for (int i = 0; i < Prefabs.Length; i++)
        {
            if (Prefabs[i].name.Equals(name))
            {
                obj = Prefabs[i];
                break;
            }
        }
        if (obj == null)
        {
            Debug.Log(name + " can't be found");
            return;
        }
        CreateObject(obj, name, cnt, parent);
    }
    public void CreateObject(GameObject newObj, string name, int cnt, Transform parent = null)
    {
        for (int i = 0; i < cnt; i++)
        {
            GameObject obj = GameObject.Instantiate(newObj);
            obj.transform.name = name;
            obj.SetActive(false);
            obj.transform.SetParent(parent);
            ObjPool.Add(obj);
        }
    }
    public GameObject GetObject(string name)
    {
        if (ObjPool.Count == 0) return null;
        for (int i = 0; i < ObjPool.Count; i++)
        {
            if (i == ObjPool.Count - 1)
            {
                CreateObject(name, 1);
                return ObjPool[i + 1];
            }
            if (!ObjPool[i].name.Equals(name)) continue;
            if (!ObjPool[i].activeSelf)return ObjPool[i];
        }
        return null;
    }
    
    public void CallObject(string name, Transform transform, bool deact = false, float time = 0f)
    {
        GameObject target = GetObject(name);
        if (target != null)
        {
            target.transform.position = transform.position;
            target.transform.rotation = transform.rotation;
            target.SetActive(true);
            if (deact)
            {
                StartCoroutine(Deactivate(target, time));
            }
        }
    }
    public void CallObject(string name, Vector3 position, Quaternion rotation, bool deact = false, float time = 0f)
    {
        GameObject target = GetObject(name);
        if (target != null)
        {
            target.transform.position = position;
            target.transform.rotation = rotation;
            target.SetActive(true);
            if (deact)
            {
                StartCoroutine(Deactivate(target, time));
            }
        }
    }
    public void CallBulletTypeObj(string name, Transform transform, float damage,bool deact = false, float time = 0f)
    {
        GameObject target = GetObject(name);
        if (target != null)
        {
            target.GetComponent<EffectTrigger>().DaamageScalar = damage;
            target.transform.position = transform.position;
            target.transform.rotation = transform.rotation;
            target.SetActive(true);
            if (deact)
            {
                StartCoroutine(Deactivate(target, time));
            }
        }
    }
    public void CallBulletTypeObj(string name, Vector3 position, Quaternion rotation, float damage, bool deact = false, float time = 0f)
    {
        GameObject target = GetObject(name);
        if (target != null)
        {
            target.GetComponent<EffectTrigger>().DaamageScalar = damage;
            target.transform.position = position;
            target.transform.rotation = rotation;
            target.SetActive(true);
            if (deact)
            {
                StartCoroutine(Deactivate(target, time));
            }
        }
    }
    public void CallText(string text, Vector3 position)
    {

        GameObject target = GetObject(_text);
        if (target != null)
        {
            target.transform.position = position+Vector3.up;
            target.GetComponent<TextMeshPro>().text = text;
            target.SetActive(true);
        }
    }
    public void CallIndicator(Vector3 position, Quaternion quaternion, float size = 1f, float ratio = 1f,float far=5f,bool ortho = true )
    {

        GameObject target = GetObject(indicator);
        if (target != null)
        {
            temp_p = target.GetComponent<Projector>();
            target.transform.position = position + Vector3.up;
            target.transform.rotation = quaternion;
            temp_p.orthographicSize = size;
            temp_p.aspectRatio = ratio;
            temp_p.orthographic = ortho;
            temp_p.farClipPlane = far;
            target.SetActive(true);

            StartCoroutine(Deactivate(target, 2f));
        }
    }
    public void DeletePool()
    {
        for (int i = ObjPool.Count - 1; i >= 0; i++)
        {
            Destroy(ObjPool[i]);
        }
        ObjPool = null;
    }


    //이펙트만 띄우는 오브젝트에만 한함
    private IEnumerator Deactivate(GameObject obj, float time)
    {
        float t = 0f;
        while (t < time)
        {
            t += Time.deltaTime * Time.timeScale;
            yield return null;
        }
        obj.SetActive(false);
    }
}

