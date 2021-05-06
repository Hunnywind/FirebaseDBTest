using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Firestore;
using Firebase.Extensions;

public class InteriorData
{
    public int id;
    public float price;
    public string priceUnit;
    public string purchaseType;
    public int unlockStage;

}

public class FirestoreTest : MonoBehaviour
{
    void Start()
    {
        FirebaseFirestore db = FirebaseFirestore.DefaultInstance;

        CollectionReference usersRef = db.Collection("InteriorDataCollection");
        usersRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            QuerySnapshot snapshot = task.Result;
            foreach (DocumentSnapshot document in snapshot.Documents)
            {
                Debug.Log(String.Format("User: {0}", document.Id));
                Dictionary<string, object> documentDictionary = document.ToDictionary();

                InteriorData interiorData = new InteriorData();

                interiorData.id = int.Parse(documentDictionary["id"].ToString());

                string[] prices = documentDictionary["price"].ToString().Split('/');

                interiorData.price = float.Parse(prices[0]);
                interiorData.priceUnit = prices[1];

                interiorData.purchaseType = documentDictionary["purchaseType"].ToString();
                interiorData.unlockStage = int.Parse(documentDictionary["unlockStage"].ToString());
            }

            Debug.Log("Read all data from the users collection.");
        });

    }

}
