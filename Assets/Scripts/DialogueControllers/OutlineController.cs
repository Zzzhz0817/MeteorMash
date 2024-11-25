using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineController : MonoBehaviour
{
    [SerializeField] private GameObject gradyMeteor;
    [SerializeField] private GameObject junoMeteor;
    [SerializeField] private GameObject singhMeteor;
    [SerializeField] private DialogueManager dialogueManager;

    // Start is called before the first frame update
    void Start()
    {
        gradyMeteor.GetComponent<Outline>().enabled = false;
        junoMeteor.GetComponent<Outline>().enabled = false;
        singhMeteor.GetComponent<Outline>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (dialogueManager.GetVariable("task_received").ToString() == "true")
        {
            ActivateAllOutlines();
        }
        if (dialogueManager.GetVariable("laser_acquired").ToString() == "true")
        {
            gradyMeteor.GetComponent<Outline>().enabled = false;
        }
        if (dialogueManager.GetVariable("sealant_acquired").ToString() == "true")
        {
            junoMeteor.GetComponent<Outline>().enabled = false;
        }
        if (dialogueManager.GetVariable("id_card_acquired").ToString() == "true")
        {
            singhMeteor.GetComponent<Outline>().enabled = false;
        }
    }

    public void ActivateAllOutlines()
    {
        gradyMeteor.GetComponent<Outline>().enabled = true;
        junoMeteor.GetComponent<Outline>().enabled = true;
        singhMeteor.GetComponent<Outline>().enabled = true;
    }
}
