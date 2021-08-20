#if UNITY_EDITOR
using UnityEngine;

public class ValidateLevel : MonoBehaviour
{
    private Level m_level;

    [InspectorButton("OnValidateLevel")]
    public bool m_validate;

    private void OnValidateLevel()
    {
        if (m_level == null)
            m_level = GetComponent<Level>();

        m_level.Validate();
    }
}
#endif
