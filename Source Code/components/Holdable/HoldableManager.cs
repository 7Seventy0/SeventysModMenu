using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

    public  class HoldableManager : MonoBehaviour
    {
    public GameObject activeHoldable;
    TextMeshPro holdabletext;
    public static HoldableManager instance;
        void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        int selectedHoldable;

        void Start()
        {
            UpdateHoldable();
            StartCoroutine(LateStart());
        }
        IEnumerator LateStart()
        {
            yield return new WaitForSeconds(2f);
        UpdateHoldable();
        NextHoldable();
        yield return new WaitForSeconds(0.1f);
        PreviousHoldable();

    }
        public void NextHoldable()
        {
            selectedHoldable++;
            if (selectedHoldable >= HoldableDataBase.instance.HoldableCount())
            {
                selectedHoldable = 0;
            }

            UpdateHoldable();
        }
    public void SelectHoldable(bool righthand)
    {
        if(activeHoldable != null)
        {
           Destroy(activeHoldable);
        }
       

        GameObject Holdable = Instantiate(this.displayedHoldable.gameObject);
        if(Holdable.GetComponent<Karambit>() != null)
        {
            Destroy(Holdable.GetComponent<Karambit>());
        }
        if (Holdable.GetComponent<Bayonet>() != null)
        {
            Destroy(Holdable.GetComponent<Bayonet>());
        }
        activeHoldable = Holdable;
        Holdable _holdable = activeHoldable.GetComponent<Holdable>();
        Transform rightHand = GameObject.Find("palm.01.R").transform;
        Transform leftHand = GameObject.Find("palm.01.L").transform;
        if (righthand)
        {
            _holdable.ApplyCosmetic(rightHand);
        }
        else
        {
            _holdable.ApplyCosmetic(leftHand);
        }


    }

        public void PreviousHoldable()
        {
            selectedHoldable--;
            if (selectedHoldable < 0)
            {
                selectedHoldable = HoldableDataBase.instance.HoldableCount() - 1;
            }

            UpdateHoldable();
        }
        public Holdable displayedHoldable;
        void UpdateHoldable()
        {

            if (displayedHoldable == null)
            {
                displayedHoldable = HoldableDataBase.instance.GetHoldable(selectedHoldable).GetComponent<Holdable>();
            }
            else
            {
                displayedHoldable.gameObject.SetActive(false);
                displayedHoldable = HoldableDataBase.instance.GetHoldable(selectedHoldable).GetComponent<Holdable>();
                displayedHoldable.gameObject. SetActive(true);
            }
            if(holdabletext == null)
        {
            holdabletext = GameObject.Find("ModmenuHoldableDescriptor").GetComponent<TextMeshPro>();
        }
            holdabletext.text = displayedHoldable.holdableName + "\n<#FFFFFF> By " + displayedHoldable.holdableAuthor + "<#FFFFFF><size=10>\n"+displayedHoldable.holdableDescription;
        }

    }

