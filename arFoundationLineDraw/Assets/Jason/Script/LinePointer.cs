using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LinePointer : MonoBehaviour
{
    public Camera m_ArCamera;
    public Image pointer;
    [Header("lineRender�w�m��")]
    public GameObject LinePrefeb;
    [Header("��e�e��lineRender")]
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
        
       
        ChangeColorBtn.GetComponent<Image>().color = new Color32((byte)(m_Color.r*100), (byte)(m_Color.g*100), (byte)(m_Color.b*100), 255);//�O�o��byte, ��100�O�]��Color ��rgb��ܬ�0.�I�h�n���^�Ӥ~�i��Color32
    }

    // Update is called once per frame
    void Update()
    {
        PointerUpdate();
    }
    void PointerUpdate() {
         
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            #region �Х߲Ĥ@�I
            pointPos.Clear();
            Vector3 newPos = m_ArCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0.5f));
            GameObject linerender = Instantiate(LinePrefeb, newPos, Quaternion.identity);
            m_lineRender = linerender.GetComponent<LineRenderer>();
            pointPos.Add(linerender.transform.position);//�n�⦸�]�� line_Render.SetPosition �O��Ӯy�� �ҥH����J��ӬۦP�����M �U�@��outofRange
            pointPos.Add(linerender.transform.position);
            m_lineRender.SetPosition(0, pointPos[0]);
            m_lineRender.SetPosition(1, pointPos[1]);
            #endregion

        }
        if (Input.GetKey(KeyCode.Mouse0)) {
            pointer.transform.position = Input.mousePosition;
            m_lineRender.material.color = m_Color;
            Vector3 newpos = m_ArCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0.5f)); //�Z���W�@�I���Z��
            if ((Vector3.Distance(newpos, pointPos[pointPos.Count - 1]) > .01f))           
                UpdateLine(newpos);
        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
            pointPos.Clear();//�������color�Q���ܩҥH����
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
        m_Color = new Color32((byte)r, (byte)g, (byte)b, 255);//Color32�� Color ���ΰ�100
        ChangeColorBtn.GetComponent<Image>().color = new Color32((byte)r, (byte)g, (byte)b, 255);

    }
}
