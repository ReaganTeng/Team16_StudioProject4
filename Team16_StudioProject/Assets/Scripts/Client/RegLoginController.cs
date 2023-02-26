using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class RegLoginController : MonoBehaviour
{
    string URLLoginBackend=GlobalStuffs.baseURL+"LoginBackend.php";
    string URLRegBackend=GlobalStuffs.baseURL+"RegisterBackend.php";
    string URLReadPlayerStats=GlobalStuffs.baseURL+"ReadPlayerStatsJSON.php";
    public TextMeshProUGUI displayTxt; //must add using UnityEngine.UI


    public TMP_InputField  if_regusername,if_regpassword,if_regemail; //to link to the inpufields
    public TMP_InputField  if_loginusername,if_loginpassword; //to link to the inpufields
    private GameObject Login;
    private GameObject Register;

    void Awake()
    {
        Login = GameObject.FindWithTag("Login");
        Register = GameObject.FindWithTag("Register");
        Register.SetActive(false);
    }
    public void OnButtonLogin(){ //to be invoked by button click
        StartCoroutine(DoLogin());
    }
    IEnumerator DoLogin(){
        WWWForm form=new WWWForm();
        form.AddField("sUsername",if_loginusername.text);
        form.AddField("sPassword",if_loginpassword.text);
        UnityWebRequest webreq=UnityWebRequest.Post(URLLoginBackend,form);
        yield return webreq.SendWebRequest();
        switch (webreq.result)
            {
                case UnityWebRequest.Result.Success:
                displayTxt.text = webreq.downloadHandler.text;
                displayTxt.color = new Color(255, 255, 255, 1);
                if ((webreq.downloadHandler.text).Substring(0,13)=="Login Success"){
                    Debug.Log("Load new Scene");
                        GlobalStuffs.username=if_loginusername.text;
                    //GlobalStuffs.xp=0;
                    //GlobalStuffs.cash=0;
                    //StartCoroutine(GetPlayerStats(if_loginusername.text));
                    yield return new WaitForSeconds(3);
                    ClearLoginInput();
                    SceneManager.LoadScene("MainMenu");
                    }
                displayTxt.color = new Color(255, 0, 0, 1);

                break;
                default:
                    displayTxt.text="server error";
                    break;
            }
        webreq.Dispose();
    }
    public void OnButtonRegister(){ //to be invoked by button click
        StartCoroutine(DoRegister());
    }
    IEnumerator DoRegister(){
        WWWForm form=new WWWForm();
        form.AddField("sUsername",if_regusername.text);
        form.AddField("sPassword",if_regpassword.text);
        form.AddField("sEmail",if_regemail.text);
        UnityWebRequest webreq=UnityWebRequest.Post(URLRegBackend,form);
        yield return webreq.SendWebRequest();
        switch (webreq.result)
            {
                case UnityWebRequest.Result.Success:
                displayTxt.text = webreq.downloadHandler.text;
                displayTxt.color = new Color(1, 1, 1, 1);

                if (webreq.downloadHandler.text == "Registered")
                {
                    RegisterToLoginScreen();
                    break;
                }
                displayTxt.color = new Color(1, 0, 0, 1);

                break;
                default:
                    displayTxt.text="server error";
                    break;
            }
        webreq.Dispose();
    }
    
    IEnumerator GetPlayerStats(string playername)
    {
        WWWForm form=new WWWForm();
        form.AddField("username",playername);
        UnityWebRequest webRequest = UnityWebRequest.Post(URLReadPlayerStats,form);
        
        // Request and wait for the desired page.
        yield return webRequest.SendWebRequest();

        switch (webRequest.result)
        {
                case UnityWebRequest.Result.Success:
                Debug.Log("Received: " + webRequest.downloadHandler.text);
                displayTxt.text="RawData:\n"+webRequest.downloadHandler.text+"\n";
                //displayTxt.text=Deserialize(webRequest.downloadHandler.text); //added
                //GlobalStuffs.DeserializeForStats(webRequest.downloadHandler.text);
               
                
               
                
                //Debug.Log("Username:" + ps.username + "\nLevel:" + ps.level + "\nXP:" + ps.xp + "\nCash:" + ps.cash);

                break;
              default:
                    displayTxt.text="server error";
                    break;
        }
        webRequest.Dispose();
    }
    public void GuestLogin(){

        Login.SetActive(false);
        Register.SetActive(true);
        //SceneManager.LoadScene("MainMenu");
    }
    public void RegisterToLoginScreen()
    {
        Login.SetActive(true);
        Register.SetActive(false);
        ClearInput();
    }
    public void OnDeleteUser()
    {
        StartCoroutine(GlobalStuffs.DeleteCurrentUser());
        //BacktoMain();
    }
    public void ClearInput()
    {
        if_regusername.Select();
        if_regusername.text = "";
        if_regpassword.Select();
        if_regpassword.text = "";
        if_regemail.Select();
        if_regemail.text = "";
    }
    public void ClearLoginInput()
    {
        if_loginusername.Select();
        if_loginusername.text = "";
        if_loginpassword.Select();
        if_loginpassword.text = "";
        displayTxt.text = "";
    }

}
