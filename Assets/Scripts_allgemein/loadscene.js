#pragma strict

function Start () {

}

function Update () {

}

//If the player collides with the object with the script and he and tag "player" the scene "scene" will load

function OnTriggerEnter (collider : Collider) {

    //tag your player "Player" 
    
        
        
    if(collider.tag == "Player"){
        //"scene" this is a name of level to load
        Application.LoadLevel("wohnzimmer");
    }
}


