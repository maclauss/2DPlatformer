using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour {

    public AudioClip openingSound;

    private bool opened = false;
    private string[] objects = new string[] { "Ranged Weapon", "Melee Weapon", "Magical Weapon" };
    private SpriteRenderer spriteRenderer = null;
    private AudioSource source;
    private float volume = 1f;

    public void Awake() {
        spriteRenderer = (SpriteRenderer) GetComponent(typeof(SpriteRenderer));
        source = GetComponent<AudioSource>();
    }

    public string Open(){
        if (!opened){
            source.PlayOneShot(openingSound, 1f);
            string item = randomObject();
            Debug.Log("Found a [" + item + "]");
            spriteRenderer.sprite = (Sprite) Resources.LoadAll("chest_eggs", typeof(Sprite))[1];
            opened = true;
            return item;
        }
        return "";
    }

    string randomObject() {
        return objects[new System.Random().Next(0, objects.Length)];
    }

}
