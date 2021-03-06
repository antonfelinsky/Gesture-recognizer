using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Gesture : MonoBehaviour
{
    [SerializeField] private GameObject gestureDrawing;
    public static GameObject GuiText;
    GestureTemplates m_Templates;
    [SerializeField] public Text GUIText = null;

    ArrayList pointArr;
    static int mouseDown;
   

    // runs when game starts - main function
    void Start ()
    {
        m_Templates = new GestureTemplates();
	    pointArr = new ArrayList();
    	
	    //gestureDrawing = GameObject.Find("gesture");
        
        GUIText.text = GUIText.text + " Templates loaded: " + GestureTemplates.Templates.Count;
    }


    IEnumerator worldToScreenCoordinates ()
    {
	    // fix world coordinate to the viewport coordinate
	    Vector3 screenSpace = Camera.main.WorldToScreenPoint(gestureDrawing.transform.position);
    	
	    while (Input.GetMouseButton(0))
	    {
		    Vector3 curScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z);
		    Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenSpace); 
		    gestureDrawing.transform.position = curPosition;
		    yield return 0;
	    }
    }
    //public static void PrintValues(ArrayList pointArr)
    //{
    //    foreach (Vector2 obj in pointArr) { print(obj);}
            
    //    print("");
    //}
        void Update()
    {
	    if (Input.GetMouseButtonDown(0))
        {
		    mouseDown = 1;
	    }
    	
	    if (mouseDown == 1)
        {
		    Vector2 p = new Vector2(Input.mousePosition.x , Input.mousePosition.y);
		    pointArr.Add(p);
		    StartCoroutine(worldToScreenCoordinates());
            //PrintValues(pointArr);
	    }


	    if (Input.GetMouseButtonUp(0))
        {
		    if (Input.GetKey (KeyCode.LeftControl))
            {
			    // if CTRL is held down, the script will record a gesture. 
			    mouseDown = 0;
			    GestureRecognizer.recordTemplate(pointArr);
    		
		    }
            else
            {
			    mouseDown = 0;

			    // start recognizing! 
			    GestureRecognizer.startRecognizer(pointArr);

			    pointArr.Clear();
    		
		    }
    		
	    }
    	
    } 

    void OnGUI ()
    {
	    if(GestureRecognizer.recordDone == 1)
        { 
		    GUI.Window (0, new Rect (350, 220, 300, 100), DoMyWindow, "Save the template?");
	    }
    }

    void DoMyWindow (int windowID)
    {
        GestureRecognizer.stringToEdit = GUILayout.TextField(GestureRecognizer.stringToEdit);

        if (GUI.Button (new Rect (100,50,50,20), "Save"))
        {
            ArrayList temp = new ArrayList();
            ArrayList a = (ArrayList)GestureTemplates.Templates[GestureTemplates.Templates.Count - 1];

            for (int i = 0; i < GestureRecognizer.newTemplateArr.Count; i++)
                temp.Add(GestureRecognizer.newTemplateArr[i]);

            GestureTemplates.Templates.Add(temp);
            GestureTemplates.TemplateNames.Add(GestureRecognizer.stringToEdit);
            GestureRecognizer.recordDone = 0;
            GestureRecognizer.newTemplateArr.Clear();

            GuiText.GetComponent<GUIText>().text = "TEMPLATE: " + GestureRecognizer.stringToEdit + "\n STATUS: SAVED";
	    }

	    if (GUI.Button (new Rect (160,50,50,20), "Cancel")) 
        {
            GestureRecognizer.recordDone = 0;
	       GuiText.GetComponent<GUIText>().text = "";
	    }
    }
}
