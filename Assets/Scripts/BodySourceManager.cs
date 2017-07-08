using UnityEngine;
using System.Collections;
using Windows.Kinect;

public class BodySourceManager : MonoBehaviour 
{
    private KinectSensor m_sensor;
    private BodyFrameReader m_reader;
    private Body[] m_data = null;
    
    public Body[] GetData()
    {
        return m_data;
    }
    
    void Start () 
    {
        m_sensor = KinectSensor.GetDefault();

        if (m_sensor != null)
        {
            m_reader = m_sensor.BodyFrameSource.OpenReader();
            
            if (!m_sensor.IsOpen)
            {
                m_sensor.Open();
            }
        }   
    }
    
    void Update () 
    {
        if (m_reader != null)
        {
            var frame = m_reader.AcquireLatestFrame();
            if (frame != null)
            {
                if (m_data == null)
                {
                    m_data = new Body[m_sensor.BodyFrameSource.BodyCount];
                }
                
                frame.GetAndRefreshBodyData(m_data);
                
                frame.Dispose();
                frame = null;
            }
        }    
    }
    
    void OnApplicationQuit()
    {
        if (m_reader != null)
        {
            m_reader.Dispose();
            m_reader = null;
        }
        
        if (m_sensor != null)
        {
            if (m_sensor.IsOpen)
            {
                m_sensor.Close();
            }
            
            m_sensor = null;
        }
    }
}
