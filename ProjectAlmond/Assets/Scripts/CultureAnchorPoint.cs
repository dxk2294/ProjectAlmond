﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class CultureAnchorPointEvent : UnityEvent<GameObject, Culture, CultureAnchorPoint> { }

public class CultureAnchorPoint : AnchorBehavior
{
    public AnchorBehaviorGroup anchorGroup;
    public CultureAnchorPointEvent onAttach;
    public CultureAnchorPointEvent onDetach;

    public CultureAnchorPointEvent onHover;
    public CultureAnchorPointEvent onUnhover;

    public Transform cameraAngle;
    public GameObject Culture { get; private set; }

    CameraController cameraController;

    public void Start()
    {
        cameraController = Camera.main.GetComponent<CameraController>();

        if (anchorGroup)
        {
            anchorGroup.RegisterAnchor(this);
        }
    }

    public override void Attach(GameObject attachedObject)
    {
        base.Attach(attachedObject);

        Culture c = attachedObject.GetComponent<Culture>();

        Culture = attachedObject;

        onAttach.Invoke(attachedObject, c, this);
    }

    public override void Detach(GameObject attachedObject)
    {
        base.Detach(attachedObject);

        if (anchorGroup)
        {
            anchorGroup.AnchorDetached(this);
        }

        Culture c = attachedObject.GetComponent<Culture>();

        Culture = null;

        onDetach.Invoke(attachedObject, c, this);
    }

    public override void HoverDidChange(GameObject hoveredObject, bool isHovering) {
        Culture c = hoveredObject.GetComponent<Culture>();
        if (isHovering) {
            onHover.Invoke(hoveredObject, c, this);
        } else {
            onUnhover.Invoke(hoveredObject, c, this);
        }
    }

        
}
