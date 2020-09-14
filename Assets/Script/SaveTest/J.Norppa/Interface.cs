using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICollectable
{
    bool isColleceted();
}

public interface IIdentifiable
{
    string Id { get; set; }
}