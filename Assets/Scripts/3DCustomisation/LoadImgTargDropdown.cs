using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LoadImgTargDropdown : MonoBehaviour
{
    [SerializeField]
    TMP_Dropdown m_ImgTargetDropdown;
    /*[SerializeField]
    RawImage m_DisplayTexture;*/

    private List<Texture2D> m_targetTextures;
    // Start is called before the first frame update
    void Start()
    {
        m_ImgTargetDropdown.ClearOptions();
        m_targetTextures= new List<Texture2D>();
        StartCoroutine(GetImgTargets());
        m_ImgTargetDropdown.onValueChanged.AddListener(delegate { OnSelectionChanged(m_ImgTargetDropdown); });

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator GetImgTargets()
    {
        UnityWebRequest www = UnityWebRequest.Get("https://lunar-byte-371808.et.r.appspot.com/api/getGreetingCards");
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            // Debug.Log("GetProduct Response: " + www.downloadHandler.text);
            var getGreetingCardResponseList = JsonUtility.FromJson<GreetingCardResponseList>(www.downloadHandler.text);
            var greetingCardsResponse = getGreetingCardResponseList.greeting_cards;
            
            for (int i = 0; i < greetingCardsResponse.Count; i++)
            {
                // set gc image
                string rawImageBytes = greetingCardsResponse[i].image.Split(',')[1];
                Texture2D imgTxtr = new Texture2D(2, 2);
                if (imgTxtr.LoadImage(Convert.FromBase64String(rawImageBytes)))
                {
                    TMP_Dropdown.OptionData newOp = new TMP_Dropdown.OptionData();
                    newOp.text = greetingCardsResponse[i].name;
                    newOp.image = Sprite.Create(imgTxtr,/*m_ImgTargetDropdown.itemImage.rectTransform.rect*/new Rect(0,0,imgTxtr.width,imgTxtr.height),Vector2.zero);
                    m_ImgTargetDropdown.options.Add(newOp);
                    m_targetTextures.Add(imgTxtr);
                }
                
                
            }
            m_ImgTargetDropdown.RefreshShownValue();
            OnSelectionChanged(m_ImgTargetDropdown);
        }
    }

    private void OnSelectionChanged(TMP_Dropdown _dropdown)
    {
        int selectionID = _dropdown.value;
        Debug.Log("Selection ID: "+ selectionID);
        //m_DisplayTexture.texture = m_targetTextures[selectionID];
    }
}
