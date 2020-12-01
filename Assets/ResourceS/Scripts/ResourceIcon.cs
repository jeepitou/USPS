using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceIcon : Image
{
    [SerializeField]public Resource resource;
    public string nameasd;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        this.sprite = resource.resourceIcon;
    }
}
