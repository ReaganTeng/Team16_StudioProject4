using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;
using UnityEngine.SceneManagement;
using TMPro;

public static class GlobalStuffs {
    public static string username="GuestPlayer";
    public static int xp=0;
    public static int cash=0;
    public static int level=1;
    public static bool exist = false;
    public static bool isCompleted = false;
    public static int timeTaken = 0;
    
    public static string baseURL= "http://34.124.188.6/server/"; //rename this to your server path
    static string addscorebackendURL=baseURL+"AddScoreBackend.php";
    static string ReadSBJSONURL=baseURL+"ReadScoreBoardJSON.php";
    static string DeleteAllScoreURL=baseURL+"DeleteAllScores.php";
    static string UpdatePlayerStatsURL=baseURL+"UpdatePlayerStatsBackend.php";
    static string UpdateExistingPlayerStatsURL =baseURL+"UpdateExistingPlayerStatsBackend.php";
    static string DeleteUserURL=baseURL+"DeleteUserBackend.php";
    
    public static IEnumerator DoSendScore(string pname,int score){
        WWWForm form=new WWWForm();
        form.AddField("sUsername",pname);
        form.AddField("iScore",score);
        UnityWebRequest webreq=UnityWebRequest.Post(addscorebackendURL,form);
        yield return webreq.SendWebRequest();
        switch (webreq.result)
            {
                case UnityWebRequest.Result.Success:
                    Debug.Log(":\nReceived: " + webreq.downloadHandler.text);
                    SceneManager.LoadScene("WinScreen");

                    //GetScoreBoard();
                    break;
                default:
                    Debug.Log("error");
                    break;
            }
            webreq.Dispose();            
    }
    
    public static IEnumerator GetScoreBoard(TextMeshProUGUI pos,TextMeshProUGUI name, TextMeshProUGUI time, TextMeshProUGUI lastPlayed)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(ReadSBJSONURL))
        {
            yield return webRequest.SendWebRequest();

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.Success:
                    Debug.Log("Received: " + webRequest.downloadHandler.text);
                    //displayTxt.text="RawData:\n"+webRequest.downloadHandler.text+"\n";
                    Deserialize(webRequest.downloadHandler.text, pos, name, time, lastPlayed); //added
                    break;
                default:
                    Debug.Log("error"+webRequest.error);
                    break;
            }
            webRequest.Dispose();
        }
    }

     public static IEnumerator ClearScores(){
        UnityWebRequest webreq=UnityWebRequest.Get(DeleteAllScoreURL);
        yield return webreq.SendWebRequest();
        switch (webreq.result)
            {
                case UnityWebRequest.Result.Success:
                    Debug.Log(":\nReceived: " + webreq.downloadHandler.text);
                    //GetScoreBoard();
                    break;
                default:
                    Debug.Log("error");
                    break;
            }
         webreq.Dispose();
            
    }
    static void Deserialize(String RawJSON, TextMeshProUGUI pos, TextMeshProUGUI name, TextMeshProUGUI time, TextMeshProUGUI lastPlayed)
    { 
        ScoreList sb=JsonUtility.FromJson<ScoreList>(RawJSON); //convert raw json to objects
         
        name.text = "NAME";
        time.text = "TIME";
        lastPlayed.text = "PLAYED ON";
        for (int a=0;a<sb.scores.Count;a++){
            //if (a == 0)
            //{
            //    pos.color = new Color(1f, 0.7f, 0f, 255f);
            //}
            //else if (a == 1)
            //{
            //    pos.color = new Color(0.59f, 0.59f, 1f, 1f);
            //}
            //else if (a == 2)
            //{
            //    pos.color = new Color(0.55f, 0.235f, 0.55f, 1f);
            //}
            //else
            //{
            //    pos.color = new Color(1f, 1f, 1f, 1f);
            //}
            OneScore oneScore=sb.scores[a];
            pos.text += "\n" + (a + 1) + ".";
            name.text +=  "\n" + oneScore.username;
            if (oneScore.score % 60 < 10)
            {
                time.text += "\n" + oneScore.score / 60 + ":0" + oneScore.score % 60;
            }
            else
            {
                time.text += "\n" + oneScore.score / 60 + ":" + oneScore.score % 60;
            }
            lastPlayed.text += "\n" + oneScore.lastPlayed;
            //Debug.Log(oneScore.username + " : " + oneScore.score / 60 + " : " + oneScore.score % 60 + oneScore.lastPlayed);
            //ddata += (a + 1 + ".)"+oneScore.username+"|Time Taken:"+oneScore.score / 60+":"+ oneScore.score % 60 +"|Played On:" + oneScore.lastPlayed + "\n");
        }

    }
    //public static void DeserializeForStats(String RawJSON)
    //{
    //    StatSheet sb = JsonUtility.FromJson<StatSheet>(RawJSON); //convert raw json to objects
    //    if (sb.stats.Count > 0)
    //    {
    //        username = sb.stats[0].username;
    //        level = sb.stats[0].level;
    //        xp = sb.stats[0].xp;
    //        cash = sb.stats[0].cash;
    //        exist = true;
    //    }
    //}
    //public static IEnumerator UpdatePlayerStats(){
    //    WWWForm form=new WWWForm();
    //    form.AddField("username",username);
    //    form.AddField("newxp",xp);
    //    form.AddField("newlevel",level);
    //    form.AddField("newcash",cash);

    //    using(UnityWebRequest webreq=UnityWebRequest.Post(UpdatePlayerStatsURL,form)){
    //    yield return webreq.SendWebRequest();
    //    switch (webreq.result)
    //        {
    //            case UnityWebRequest.Result.Success:
    //                Debug.Log(":\nReceived: " + webreq.downloadHandler.text);
    //                //GetScoreBoard();
    //                break;
    //            default:
    //                Debug.Log("baderror");
    //                break;
    //        }
    //    }
    //    //webreq.Dispose();
            
    //}
    //public static IEnumerator UpdateExistingPlayerStats()
    //{
    //    WWWForm form = new WWWForm();
    //    form.AddField("username", username);
    //    form.AddField("newxp", xp);
    //    form.AddField("newlevel", level);
    //    form.AddField("newcash", cash);

    //    using (UnityWebRequest webreq = UnityWebRequest.Post(UpdateExistingPlayerStatsURL, form))
    //    {
    //        yield return webreq.SendWebRequest();
    //        switch (webreq.result)
    //        {
    //            case UnityWebRequest.Result.Success:
    //                Debug.Log(":\nReceived: " + webreq.downloadHandler.text);
    //                //GetScoreBoard();
    //                break;
    //            default:
    //                Debug.Log("baderror");
    //                break;
    //        }
    //    }
    //}
        public static IEnumerator DeleteCurrentUser(){
        WWWForm form=new WWWForm();
        form.AddField("sUsername", username);        

        using(UnityWebRequest webreq=UnityWebRequest.Post(DeleteUserURL,form)){
        yield return webreq.SendWebRequest();
        switch (webreq.result)
            {
                case UnityWebRequest.Result.Success:
                    Debug.Log(":\nReceived: " + webreq.downloadHandler.text);
                    //GetScoreBoard();
                    break;
                default:
                    Debug.Log("baderror");
                    break;
            }
        }
        //webreq.Dispose();
            
    }
}



//https://docs.unity3d.com/ScriptReference/Networking.UnityWebRequest.Get.html

//create classes and data structure to match the JSON structure
[Serializable]
class OneScore {
    public string username;
    public int score;
    public string lastPlayed;
}
//[Serializable]
//class OneStats
//{
//    public string username;
//    public int level;
//    public int xp;
//    public int cash;

//}
[Serializable]
class ScoreList {
    public List<OneScore> scores=new List<OneScore>();
}
//[Serializable]
//class StatSheet
//{
//    public List<OneStats> stats = new List<OneStats>();
//}



