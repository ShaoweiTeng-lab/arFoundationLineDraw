using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LinePointer : MonoBehaviour
{
    public Camera m_ArCamera;
    public Image pointer;
    [Header("lineRender預置物")]
    public GameObject LinePrefeb;
    [Header("當前畫的lineRender")]
    [SerializeField]
     LineRenderer m_lineRender;
    [SerializeField]
    List<Vector3> pointPos=new List<Vector3>();
    public Button ReturnBtn;
    public Button ChangeColorBtn;
    public Color m_Color;
    // Start is called before the first frame update
    void Start()
    {
        ReturnBtn.onClick.AddListener(() => onCLickReturn());
        ChangeColorBtn.onClick.AddListener(() => changeColor());
        
       
        ChangeColorBtn.GetComponent<Image>().color = new Color32((byte)(m_Color.r*100), (byte)(m_Color.g*100), (byte)(m_Color.b*100), 255);//記得轉byte, 乘100是因為Color 的rgb顯示為0.點多要乘回來才可轉Color32
    }

    // Update is called once per frame
    void Update()
    {
        PointerUpdate();
    }
    void PointerUpdate() {
         
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            #region 創立第一點
            pointPos.Clear();
            Vector3 newPos = m_ArCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0.5f));
            GameObject linerender = Instantiate(LinePrefeb, newPos, Quaternion.identity);
            m_lineRender = linerender.GetComponent<LineRenderer>();
            pointPos.Add(linerender.transform.position);//要兩次因為 line_Render.SetPosition 是兩個座標 所以先放入兩個相同的不然 下一行outofRange
            pointPos.Add(linerender.transform.position);
            m_lineRender.SetPosition(0, pointPos[0]);
            m_lineRender.SetPosition(1, pointPos[1]);
            #endregion

        }
        if (Input.GetKey(KeyCode.Mouse0)) {
            pointer.transform.position = Input.mousePosition;
            m_lineRender.material.color = m_Color;
            Vector3 newpos = m_ArCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0.5f)); //距離上一點的距離
            if ((Vector3.Distance(newpos, pointPos[pointPos.Count - 1]) > .01f))           
                UpdateLine(newpos);
        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
            pointPos.Clear();//防止全部color被改變所以移除
    }
    void UpdateLine(Vector3 newFingerPos)
    {
        pointPos.Add(newFingerPos);
        m_lineRender.positionCount++;
        m_lineRender.SetPosition(m_lineRender.positionCount - 1, newFingerPos);
         

    }
    void onCLickReturn() {
        SceneManager.LoadScene("LineRender");

    }
    void changeColor() {
        float r=Random.Range(0,256);
        float g = Random.Range(0, 256);
        float b = Random.Range(0, 256);
        m_Color = new Color32((byte)r, (byte)g, (byte)b, 255);//Color32轉 Color 不用除100
        ChangeColorBtn.GetComponent<Image>().color = new Color32((byte)r, (byte)g, (byte)b, 255);

    }
}
