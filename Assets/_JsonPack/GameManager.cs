using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    [SerializeField] private TMP_Text balanceText;
    [SerializeField] private TMP_InputField addCoinsInput;
    [SerializeField] private TMP_InputField removeCoinsInput;

    [SerializeField] private string baseUrl = "https://yaahabibi.com/api/test/";

    private void Start()
    {
        GetBalance();
    }

    // Function to get user balance
    public void GetBalance()
    {
        StartCoroutine(GetBalanceCoroutine());
    }

    // Coroutine to handle API call for getting balance
    private IEnumerator GetBalanceCoroutine()
    {
        using (WWW www = new WWW(baseUrl + "get-balance"))
        {
            yield return www;

            if (string.IsNullOrEmpty(www.error))
            {
                UserBalance userBalance = JsonUtility.FromJson<UserBalance>(www.text);
                Debug.Log("Raw JSON Response: " + www.text);
                UpdateBalanceText(userBalance.data.balance);
            }
            else
            {
                Debug.LogError("Error getting balance: " + www.error);
            }
        }
    }
    
    public void AddCoins(int amount)
    {
        StartCoroutine(AddCoinsCoroutine(amount));
    }
    
    public void AddCoins()
    {
        int balanceToAdd = int.Parse(addCoinsInput.text);
        StartCoroutine(AddCoinsCoroutine(balanceToAdd));
    }

    // Coroutine to handle API call for adding coins
    private IEnumerator AddCoinsCoroutine(int balance)
    {
        AddRemoveCoinsRequest request = new AddRemoveCoinsRequest(balance);
        string jsonData = JsonUtility.ToJson(request);

        using (WWW www = new WWW(baseUrl + "add-balance", System.Text.Encoding.UTF8.GetBytes(jsonData), new Dictionary<string, string> { { "Content-Type", "application/json" } }))
        {
            yield return www;

            if (string.IsNullOrEmpty(www.error))
            {
                Debug.Log(www.text);
                GetBalance(); // Refresh balance after adding coins
            }
            else
            {
                Debug.LogError("Error adding coins: " + www.error);
            }
        }
    }

    // Function to remove coins
    public void RemoveCoins(int amount)
    {
        StartCoroutine(RemoveCoinsCoroutine(amount));
    }
    
    public void RemoveCoins()
    {
        int balanceToRemove = int.Parse(removeCoinsInput.text);
        Debug.Log(balanceToRemove);
        StartCoroutine(RemoveCoinsCoroutine(balanceToRemove));
    }

    // Coroutine to handle API call for removing coins
    private IEnumerator RemoveCoinsCoroutine(int balance)
    {
        AddRemoveCoinsRequest request = new AddRemoveCoinsRequest(balance);
        string jsonData = JsonUtility.ToJson(request);

        Debug.Log("RemoveCoins Request: " + jsonData);

        using (WWW www = new WWW(baseUrl + "remove-balance", System.Text.Encoding.UTF8.GetBytes(jsonData), new Dictionary<string, string> { { "Content-Type", "application/json" } }))
        {
            yield return www;

            if (string.IsNullOrEmpty(www.error))
            {
                Debug.Log("RemoveCoins Response: " + www.text);
                GetBalance(); // Refresh balance after removing coins
            }
            else
            {
                Debug.LogError("Error removing coins: " + www.error);
            }
        }
    }

    // Function to update the balance text in the UI
    private void UpdateBalanceText(int balance)
    {
        balanceText.text = "Balance: " + balance.ToString();
    }
}

// Data class to represent the JSON response from the get-balance API
[System.Serializable]
public class UserBalance
{
    public string message;
    public BalanceData data;

    [System.Serializable]
    public class BalanceData
    {
        public int id;
        public int balance;
    }
}

// Data class to represent the JSON request for add-balance and remove-balance APIs
[System.Serializable]
public class AddRemoveCoinsRequest
{
    public int balance;

    public AddRemoveCoinsRequest(int balance)
    {
        this.balance = balance;
    }
}
