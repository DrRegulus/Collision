#pragma strict

    var speed = 0.2;
    var crawling = true;
     function Start()
    {
    // init text here, more space to work than in the Inspector (but you could do that instead)
    var tc = GetComponent(GUIText);
    var creds = "Group Leader: Zsade Fleming\n";
    creds += "Artist: Zsade Fleming\n";
    creds += "Dialogue: Brenda\n";
    creds += "CheckPoint: Tyler\n";
    creds += "Enemy AI: Tyler & Ryan\n";
    creds += "Level Mechanics: Ryan\n";
    creds += "Main Menu: Ryan\n";
    creds += "Level Mechanics: Ryan\n";
    creds += "Pause Menu: Yohan Kim\n";
    creds += "Combat System: Yohan Kim\n";
    creds += "Level Design: Logan Adams\n";
    creds += "Merging Works: Logan Adams\n";
    creds += "Animation: Logan Adams\n";
    creds += "Openning Music & Credit Music: 'Word play' by Wandy Mao\n";
    creds += "Tutorial & Level1: 'Air' by Ziyed Hedfi\n";
    tc.text = creds;
    crawling = true;
    }
    function Update () {
    if (!crawling)
    return;
    transform.Translate(Vector3.up * Time.deltaTime * speed);
    if (gameObject.transform.position.y > 5)
    {
    crawling = false;
    }
    if(Input.GetKey(KeyCode.Escape)){
    guiText.transform.position = new Vector3(0.29f, 0.01f, 0f);
    crawling = false;
    Application.LoadLevel("mainMenu");
    }
}