using Characters;
using UnityEngine;
using Utils;

public class CharactersFactory
{
    public RigidbodyCharacter CreateRigidbodyCharacter(
        RigidbodyCharacter prefab,
        Vector3 SpawnPosition)
    {
        RigidbodyCharacter instance = Object.Instantiate(prefab, SpawnPosition, Quaternion.identity, null);

        instance.Initialize();

        return instance;
    }
}
