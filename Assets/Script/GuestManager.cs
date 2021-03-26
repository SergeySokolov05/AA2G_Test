using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuestManager : MonoBehaviour
{
    [SerializeField] private Transform startPositionGuest;
    [SerializeField] private Transform[] arrayPositionGuestToConveyor;


    private List<Guest> _listGust;
    
    private void Start()
    {
        _listGust = new List<Guest>();

        var tempGuest = GameManager.instance.PrefabsGame.Guest;
        
        for (var i = 0; i < arrayPositionGuestToConveyor.Length; i++)
        {
            var guest = Instantiate(tempGuest, startPositionGuest.position, Quaternion.identity);
            _listGust.Add(guest);
        }
        
        StartMoveGuest();
    }

    public void StartMoveGuest()
    {
        for (var i = 0; i < arrayPositionGuestToConveyor.Length; i++)
        {
            _listGust[i].MoveTarget(arrayPositionGuestToConveyor[i]);
        }
    }
}
