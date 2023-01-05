using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Vuforia;

public class ARManager : MonoBehaviour
{
    /*[SerializeField]
    GameObject[] m_TargetsInScene;

    List<ImageTargetBehaviour> m_ITBinScene;*/
    [SerializeField]
    GameObject[] m_PlaceableObjects;

    [SerializeField]
    HTLogger logger;
    // Start is called before the first frame update
    void Start()
    {
        /*foreach(GameObject target in m_TargetsInScene)
        {
            m_ITBinScene.Add(target.GetComponent<ImageTargetBehaviour>());
        }*/
    }

    /*public void ResetTracking()
    {
        foreach (ImageTargetBehaviour itb in m_ITBinScene)
        {
            if (itb.TargetStatus.Status == Status.TRACKED)
            {

            }
        }
    }*/
    public void SaveScene()
    {
        List<SaveLoadSystem.PosRotScale> PRSWine = new List<SaveLoadSystem.PosRotScale>();
        List<SaveLoadSystem.PosRotScale> PRSFlower = new List<SaveLoadSystem.PosRotScale>();
        logger.AddMsg($"Saving to {Application.persistentDataPath}...");
        Debug.Log(Application.persistentDataPath);
        for (int i=0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            string childName = child.name;
            childName = childName.Substring(0, childName.IndexOf(" "));
            logger.AddMsg(childName);
            if(childName == "Flower")
            {
                PRSFlower.Add(new SaveLoadSystem.PosRotScale(
                    child.position.x, child.position.y, child.position.z,
                    child.rotation.eulerAngles.x, child.rotation.eulerAngles.y, child.rotation.eulerAngles.z,
                    child.lossyScale.x, child.lossyScale.y, child.lossyScale.z));
            }
            else if(childName == "Wine")
            {
                PRSWine.Add(new SaveLoadSystem.PosRotScale(
                    child.position.x, child.position.y, child.position.z,
                    child.rotation.eulerAngles.x, child.rotation.eulerAngles.y, child.rotation.eulerAngles.z,
                    child.lossyScale.x, child.lossyScale.y, child.lossyScale.z));
            }
        }
        logger.AddMsg($"Wine count: {PRSWine.Count} | Flower count: {PRSFlower.Count}");
        SaveLoadSystem.SaveData data = new SaveLoadSystem.SaveData();
        data.m_CountOfEachObject = new int[]{ PRSWine.Count, PRSFlower.Count};
        data.m_ListOfPosRotScale = new List<SaveLoadSystem.PosRotScale>();
        foreach(SaveLoadSystem.PosRotScale prs in PRSWine)
        {
            data.m_ListOfPosRotScale.Add(prs);
        }
        foreach (SaveLoadSystem.PosRotScale prs in PRSFlower)
        {
            data.m_ListOfPosRotScale.Add(prs);
        }
        SaveLoadSystem.Save("Scene1", data);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
