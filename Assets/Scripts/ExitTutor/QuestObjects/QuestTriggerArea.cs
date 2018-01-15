using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestTriggerArea : MonoBehaviour, QuestObject {

    public Fire ChainQuestObject;
    public bool First = false;
    public bool TravelToNextArea = false;

    private TAMKVR.ExitPathManager _exitPathManager;

    void Start()
    {
        _exitPathManager = FindObjectOfType<TAMKVR.ExitPathManager>();

        if(!First)
            this.gameObject.SetActive(false);
    }

    public void Activate()
    {
        this.gameObject.SetActive(true);
    }

    public void Cleared()
    {
        if(TravelToNextArea)
        {
            _exitPathManager.NextArea();
            return;
        }

        if (this.ChainQuestObject != null)
        {
            this._exitPathManager.HideNavigation(false);
            this.ChainQuestObject.Activate();
        }
        else
        {
            _exitPathManager.SetDestination();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Cleared();
            this.gameObject.SetActive(false);
        }
    }
}
