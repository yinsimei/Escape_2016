using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Characters.FirstPerson
{
    [Serializable]
    public class MouseLook
    {
        public enum MouseLookMode
        {
            MouseCenter,
            MouseEdge
        }

        public MouseLookMode mouseLookMode;

        // Mouse center
        public bool lockCursor = true;
        public GameObject fakeCursor;

        // Mouse Edge
        public float movingEdgeBoudary = 50f;

        // variables for all modes
        public float XSensitivity = 2f;
        public float YSensitivity = 2f;
        public bool clampVerticalRotation = true;
        public float MinimumX = -90F;
        public float MaximumX = 90F;
        public bool smooth;
        public float smoothTime = 5f;

        private Quaternion m_CharacterTargetRot;
        private Quaternion m_CameraTargetRot;
        private bool m_cursorIsLocked = true;

        public void Init(Transform character, Transform camera)
        {
            m_CharacterTargetRot = character.localRotation;
            m_CameraTargetRot = camera.localRotation;
        }

        public void LookRotation(Transform character, Transform camera)
        {
            float xRot = 0f;
            float yRot = 0f;
            switch (mouseLookMode)
            {
                case MouseLookMode.MouseCenter:
                    {
                        Cursor.visible = false;
                        if (fakeCursor != null) fakeCursor.SetActive(true);
                        yRot = CrossPlatformInputManager.GetAxis("Mouse X") * XSensitivity;
                        xRot = CrossPlatformInputManager.GetAxis("Mouse Y") * YSensitivity;
                    }
                    break;

                case MouseLookMode.MouseEdge:
                    {
                        if (lockCursor) SetCursorLock(false);
                        if (fakeCursor != null) fakeCursor.SetActive(false);

                        float yAxis = CrossPlatformInputManager.GetAxis("Mouse X") * XSensitivity;
                        float xAxis = CrossPlatformInputManager.GetAxis("Mouse Y") * YSensitivity;

                        if (Input.mousePosition.x > Screen.width - movingEdgeBoudary)
                            yRot = yAxis > 0f ? yAxis : 0f;

                        if (Input.mousePosition.x < 0 + movingEdgeBoudary)
                            yRot = yAxis < 0f ? yAxis : 0f;

                        if (Input.mousePosition.y > Screen.height + movingEdgeBoudary)
                            xRot = xAxis > 0f ? xAxis : 0f;

                        if (Input.mousePosition.y < 0 + movingEdgeBoudary)
                            xRot = xAxis < 0f ? xAxis : 0f;
                    }
                    break;

                default:
                    break;
            }

            m_CharacterTargetRot *= Quaternion.Euler (0f, yRot, 0f);
            m_CameraTargetRot *= Quaternion.Euler (-xRot, 0f, 0f);

            if(clampVerticalRotation)
                m_CameraTargetRot = ClampRotationAroundXAxis (m_CameraTargetRot);

            if(smooth)
            {
                character.localRotation = Quaternion.Slerp (character.localRotation, m_CharacterTargetRot,
                    smoothTime * Time.deltaTime);
                camera.localRotation = Quaternion.Slerp (camera.localRotation, m_CameraTargetRot,
                    smoothTime * Time.deltaTime);
            }
            else
            {
                character.localRotation = m_CharacterTargetRot;
                camera.localRotation = m_CameraTargetRot;
            }

            UpdateCursorLock();
        }

        public void SetCursorLock(bool value)
        {
            lockCursor = value;
            if(!lockCursor)
            {//we force unlock the cursor if the user disable the cursor locking helper
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }

        public void UpdateCursorLock()
        {
            //if the user set "lockCursor" we check & properly lock the cursos
            if (lockCursor)
                InternalLockUpdate();
        }

        private void InternalLockUpdate()
        {
            if(Input.GetKeyUp(KeyCode.Escape))
            {
                m_cursorIsLocked = false;
            }
            else if(Input.GetMouseButtonUp(0))
            {
                m_cursorIsLocked = true;
            }

            if (m_cursorIsLocked)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            else if (!m_cursorIsLocked)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }

        Quaternion ClampRotationAroundXAxis(Quaternion q)
        {
            q.x /= q.w;
            q.y /= q.w;
            q.z /= q.w;
            q.w = 1.0f;

            float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan (q.x);

            angleX = Mathf.Clamp (angleX, MinimumX, MaximumX);

            q.x = Mathf.Tan (0.5f * Mathf.Deg2Rad * angleX);

            return q;
        }

    }
}