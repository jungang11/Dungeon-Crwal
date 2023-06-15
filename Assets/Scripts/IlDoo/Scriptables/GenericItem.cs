using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "ItemType_", menuName = "PluggableItem/ItemType")]
public class GenericItem : ScriptableObject
{
    [SerializeField] private float InteractingRange;

    Item[] itemList;
}
