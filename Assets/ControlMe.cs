using UnityEngine;
using TMPro;
using BestHTTP;

public class ControlMe : MonoBehaviour {

    string myPath;
    string json;
    public TMP_Text NewInfo;

    [System.Serializable]
    public class ProductInfo {
        
        public string name;
    }

    [System.Serializable]
    public class RootObject {
        
        public ProductInfo location;
    }

    public void talkToMe(string myResult) {

        myPath = "https://api.dialogflow.com/v1/query?v=20150910&contexts=shop&lang=en&query=" + myResult + "&sessionId=12345&timezone=America/Los_Angeles";
       
        HTTPRequest request = new HTTPRequest(new System.Uri(myPath), HTTPMethods.Get, OnRequestFinished);
        request.AddHeader("Authorization", "Bearer bbdc984f52c54f4c914a218a8381baec");
        request.Send();
    }

    void Start() {
        
        NewInfo.text = "Start";

        //HTTPRequest request = new HTTPRequest(new System.Uri(myPath), HTTPMethods.Get, OnRequestFinished);
        //request.AddHeader("Authorization", "Bearer bbdc984f52c54f4c914a218a8381baec");
        //request.Send();
    }

    void OnRequestFinished(HTTPRequest request, HTTPResponse response) {

        //Debug.Log(response.DataAsText);
        NewInfo.text = "";
        NewInfo.text = response.DataAsText;
    }

}