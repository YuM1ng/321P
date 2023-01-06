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
    public void SaveSceneBin()
    {
        /*List<SaveLoadSystem.PosRotScale> PRSWine = new List<SaveLoadSystem.PosRotScale>();
        List<SaveLoadSystem.PosRotScale> PRSFlower = new List<SaveLoadSystem.PosRotScale>();*/
        List<float[]> PRSWine = new List<float[]>();
        List<float[]> PRSFlower = new List<float[]>();
        logger.AddMsg($"Saving to {Application.persistentDataPath}...");
        Debug.Log(Application.persistentDataPath);
        for (int i=0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            string childName = child.name;
            childName = childName.Substring(0, childName.IndexOf(" "));
            logger.AddMsg(childName);
            if (childName == "Flower")
            {
                /*PRSFlower.Add(new SaveLoadSystem.PosRotScale(
                    child.position.x, child.position.y, child.position.z,
                    child.rotation.eulerAngles.x, child.rotation.eulerAngles.y, child.rotation.eulerAngles.z,
                    child.lossyScale.x, child.lossyScale.y, child.lossyScale.z));*/
                PRSFlower.Add(new float[]{
                    child.position.x, child.position.y, child.position.z,
                    child.rotation.eulerAngles.x, child.rotation.eulerAngles.y, child.rotation.eulerAngles.z,
                    child.lossyScale.x, child.lossyScale.y, child.lossyScale.z});
            }
            else if(childName == "Wine")
            {
                /*PRSWine.Add(new SaveLoadSystem.PosRotScale(
                    child.position.x, child.position.y, child.position.z,
                    child.rotation.eulerAngles.x, child.rotation.eulerAngles.y, child.rotation.eulerAngles.z,
                    child.lossyScale.x, child.lossyScale.y, child.lossyScale.z));*/
                PRSWine.Add(new float[]{
                    child.position.x, child.position.y, child.position.z,
                    child.rotation.eulerAngles.x, child.rotation.eulerAngles.y, child.rotation.eulerAngles.z,
                    child.lossyScale.x, child.lossyScale.y, child.lossyScale.z });
            }
        }
        logger.AddMsg($"Wine count: {PRSWine.Count} | Flower count: {PRSFlower.Count}");
        SaveLoadSystem.SaveData data = new SaveLoadSystem.SaveData();
        data.m_CountOfEachObject = new int[]{ PRSWine.Count, PRSFlower.Count};
        //data.m_ListOfPosRotScale = new List<SaveLoadSystem.PosRotScale>();
        data.m_ListOfPosRotScale = new List<float[]>();
        foreach(float[] prs in PRSWine)
        {
            data.m_ListOfPosRotScale.Add(prs);
        }
        foreach (float[] prs in PRSFlower)
        {
            data.m_ListOfPosRotScale.Add(prs);
        }
        SaveLoadSystem.SaveBin("Scene1", data);
    }
    public void LoadSceneBin()
    {
        logger.AddMsg("Loading file >>>");
        SaveLoadSystem.SaveData data = SaveLoadSystem.LoadBin("Scene1");
        if (data.m_CountOfEachObject == null)
        {
            logger.AddMsg("File not loaded");
            return;
        }
        logger.AddMsg("File loaded");
        foreach (int count in data.m_CountOfEachObject)
        {
            logger.AddMsg($"Count read: {count}");
        }
        foreach (float[] prs in data.m_ListOfPosRotScale)
        {
            logger.AddMsg($"PRS read: {prs}");
        }
    }

    public void SaveScene()
    {
        List<SaveLoadSystem.PosScaRot> WinePSR = new List<SaveLoadSystem.PosScaRot>();
        List<SaveLoadSystem.PosScaRot> FlowerPSR = new List<SaveLoadSystem.PosScaRot>();
        for (int i = 0; i < transform.childCount; ++i)
        {
            Transform child = transform.GetChild(i);
            string childName = child.name;
            childName = childName.Substring(0, childName.IndexOf(" "));
            if (childName == "Flower")
            {
                FlowerPSR.Add(new SaveLoadSystem.PosScaRot(
                                            child.position,
                                            child.lossyScale,
                                            child.rotation.eulerAngles));
            }
            else if (childName == "Wine")
            {
                WinePSR.Add(new SaveLoadSystem.PosScaRot(
                                            child.position,
                                            child.lossyScale,
                                            child.rotation.eulerAngles));
            }
        }
        /*SaveLoadSystem.GOData flowerDat = new SaveLoadSystem.GOData("Flower", FlowerPSR);
        SaveLoadSystem.GOData wineDat = new SaveLoadSystem.GOData("Wine", WinePSR);
        Debug.Log($"Flower {JsonUtility.ToJson(flowerDat.m_PSR)}");
        Debug.Log($"Wine {JsonUtility.ToJson(wineDat.m_PSR)}");
        string dataJSon = JsonUtility.ToJson(flowerDat, true) 
                        + JsonUtility.ToJson(wineDat, true);*/
        string dataJSon = JsonUtility.ToJson(new SaveLoadSystem.GOData(_psrWine: WinePSR, _psrFlower: FlowerPSR));
        Debug.Log(dataJSon);
        SaveLoadSystem.SaveJSon("GoData", dataJSon);
    }
    public void LoadScene()
    {
        string rawJson = SaveLoadSystem.LoadJSon("GoData");
        if (rawJson == "Fail")
        {
            logger.AddMsg("Load Failed");
        }
        SaveLoadSystem.GOData data = JsonUtility.FromJson<SaveLoadSystem.GOData>(rawJson);
        //Debug.Log($"File loaded: {data.m_PSRFlower.Count} flowers, {data.m_PSRWine.Count} wine");
        /*
         * spawn them
         */
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
