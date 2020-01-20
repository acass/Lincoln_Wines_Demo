using UnityEngine;
using TMPro;
using BestHTTP;

public class parseMyJson : MonoBehaviour {
    
    public TMP_Text mTitleText, secondText, NewInfo;
    string myPath;
    public mVideoPlayer mmVideoPlayer;
  
    void Start() {

        NewInfo.text = "Start";

        //talkToMe("What food would you pair this wine with");

        //mmVideoPlayer.PlayThisVideo("intVideo");
    }

    public void talkToMe(string myResult) {

        myPath = "https://api.dialogflow.com/v1/query?v=##########&contexts=shop&lang=en&query=" + myResult + "&sessionId=######&timezone=America/Los_Angeles";

        HTTPRequest request = new HTTPRequest(new System.Uri(myPath), HTTPMethods.Get, OnRequestFinished);
        request.AddHeader("Authorization", "Bearer ########################");
        request.Send();
    }

    [System.Serializable]
    public class mFulfillment {
        
        public string speech;
        public string BaseParam;
    }

    [System.Serializable]
    public class ProductInfo {

        public mFulfillment fulfillment;
        public string source;
        public mFulfillment parameters;
    }

    [System.Serializable]
    public class RootObject {
        
        public ProductInfo output;
        public ProductInfo result;
    }

    void OnRequestFinished(HTTPRequest request, HTTPResponse response) {

        NewInfo.text = "";
        NewInfo.text = response.DataAsText;

        RootObject m = JsonUtility.FromJson<RootObject>(response.DataAsText);

        string mContextString = m.result.source;
        secondText.text = m.result.parameters.BaseParam;

        string OutputResult = m.result.parameters.BaseParam;

        mmVideoPlayer.PlayThisVideo(OutputResult);

        mTitleText.text = m.result.fulfillment.speech;

    }
 }