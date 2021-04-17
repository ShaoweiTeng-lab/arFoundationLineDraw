using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
public class LinePointer : MonoBehaviour
{
    public Camera m_ArCamera;
    public Image pointer;
    public GameObject LinePrefeb;
    public LineRenderer m_lineRender;
    public List<Vector3> pointPos=new List<Vector3>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PointerUpdate();
    }
    void PointerUpdate() {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            pointPos.Clear();
            Vector3 newPos = m_ArCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0.5f));
            GameObject linerender = Instantiate(LinePrefeb, newPos, Quaternion.identity);
            m_lineRender = linerender.GetComponent<LineRenderer>();
            pointPos.Add(linerender.transform.position);
            pointPos.Add(linerender.transform.position);
            m_lineRender.SetPosition(0, pointPos[0]);
            m_lineRender.SetPosition(1, pointPos[1]);


        }
        if (Input.GetKey(KeyCode.Mouse0)) {
            pointer.transform.position = Input.mousePosition;
            Vector3 newpos = m_ArCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0.5f));
            if ((Vector3.Distance(newpos, pointPos[pointPos.Count - 1]) > .01f))           
                UpdateLine(newpos);
        }

    }
    void UpdateLine(Vector3 newFingerPos)
    {
        pointPos.Add(newFingerPos);
        m_lineRender.positionCount++;
        m_lineRender.SetPosition(m_lineRender.positionCount - 1, newFingerPos);
         

    }
}
