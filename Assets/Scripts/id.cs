using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Database;
using Firebase.Auth;

public class FirebaseDataController : MonoBehaviour
{
    [SerializeField]
    private InputField userInputField;

    [SerializeField]
    private Button saveButton;
    [SerializeField]
    private Text messageText;

    private DatabaseReference databaseReference;
    private FirebaseAuth auth;

    private string userId;

    private void Start()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            FirebaseApp app = FirebaseApp.DefaultInstance;
            databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
            auth = FirebaseAuth.DefaultInstance;
            saveButton.onClick.AddListener(StoreUserIdInDatabase);
        });
    }

    public void StoreUserIdInDatabase()
    {
        // Get the current user's ID
        FirebaseUser user = auth.CurrentUser;
        if (user != null)
        {
            userId = user.UserId;
        }
        SetMessage("ID function me nahi ja rai");

        // Save the user's ID in the database
        var reference = databaseReference.Child("users").Child(userId).Child("userID");
        reference.SetValueAsync(userId).ContinueWith(task =>
        {
            if (task.IsCompleted && !task.IsFaulted)
            {
                // The ID was successfully stored
                SetMessage("ID stored successfully!");
            }
            else
            {
                // There was an error
                SetMessage("Failed to store ID.");
            }
        });
    }

    private void SetMessage(string message)
    {
        messageText.text = message;
    }
}
