using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class GuestManager : MonoBehaviour, IInitializationManager
{
    [SerializeField] private Transform startPositionGuest;
    [SerializeField] private Transform[] arrayPositionGuestToConveyor;
    
    private Dictionary<Sprite, Items> _dictionarySpriteItems;
    private List<Guest> _listGust;
    private float _timeExpectationGuest => Random.Range(10, 15);

    private void OnDestroy()
    {
        for (var i = 0; i < _listGust.Count; i++)
        {
            _listGust[i]._onUpdateGuest -= UpdateGuest;
        }
    }
    
    public void Initialization()
    {
        _listGust = new List<Guest>();
        _dictionarySpriteItems = new Dictionary<Sprite, Items>();
        
        var tempGuest = GameManager.instance.PrefabsGame.Guest;
        var spritesItems = GameManager.instance.GetSpritesItems();
        
        for (var i = 0; i < spritesItems.Count; i++)
        {
            _dictionarySpriteItems.Add(spritesItems[i].Sprite, spritesItems[i].Items);
        }
        
        for (var i = 0; i < arrayPositionGuestToConveyor.Length; i++)
        {
            var guest = Instantiate(tempGuest, startPositionGuest.position, Quaternion.identity);
            _listGust.Add(guest);
        }
        
        StartMoveGuest();
    }

    public void RestartGame()
    {
        for (var i = 0; i < _listGust.Count; i++)
        {
            _listGust[i].RestartGame();
            _listGust[i].InitializedBubble(GetRandomSetItems());
            _listGust[i].InitializedTime(_timeExpectationGuest);
        }
    }

    public void StartMoveGuest()
    {
        for (var i = 0; i < arrayPositionGuestToConveyor.Length; i++)
        {
            _listGust[i]._onUpdateGuest += UpdateGuest;
            _listGust[i].InitializedBubble(GetRandomSetItems());
            _listGust[i].InitializedTime(_timeExpectationGuest);
            _listGust[i].MoveTarget(arrayPositionGuestToConveyor[i].position);
        }
    }

    private void UpdateGuest(Guest guest)
    {
        guest.InitializedBubble(GetRandomSetItems());
        guest.InitializedTime(_timeExpectationGuest);
    }

    private Dictionary<Sprite, List<Items>> GetRandomSetItems()
    {
        var tempRandomCountItems = Random.Range(1, _dictionarySpriteItems.Count);
        Dictionary<Sprite, List<Items>> tempNewDictionary = new Dictionary<Sprite, List<Items>>();
                    
        while (tempRandomCountItems >= 0)
        {
            var tempRandomItem = _dictionarySpriteItems.ElementAt(Random.Range(0, _dictionarySpriteItems.Count));

            if (!tempNewDictionary.ContainsKey(tempRandomItem.Key))
            {
                var tempListItems = new List<Items>();
                tempListItems.Add(tempRandomItem.Value);
                tempNewDictionary.Add(tempRandomItem.Key,tempListItems);
            }
            else
            {
                tempNewDictionary[tempRandomItem.Key].Add(tempRandomItem.Value);
            }
            
            tempRandomCountItems--;
        }

        return tempNewDictionary;
    }


}
