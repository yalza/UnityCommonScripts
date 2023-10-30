using UnityEngine;

namespace DATA.Scripts.Player
{
    public class MouseRotation : MonoBehaviour
    {
        [SerializeField] private Vector2 rotationRange = new Vector2(70, 70);   // Góc xoay tối đa của camera
        [SerializeField] private float rotationSpeed = 10;                          // Tốc độ xoay của camera
        [SerializeField] private float dampingTime = 0.2f;                          // Thời gian để camera xoay về vị trí ban đầu
        
        Vector3 _targetAngles;                                                      // Góc xoay của camera
        Vector3 _followAngles;                                                      // Góc xoay của player
        Vector3 _followVelocity;                                                    // Tốc độ xoay của player
        
        Quaternion _originalRotation;                                               // Góc xoay ban đầu của camera
        
        
        private void Start()
        {
            _originalRotation = transform.localRotation;
        }

        private void Update()
        {
            transform.localRotation = _originalRotation; // Reset camera về góc xoay ban đầu

            
            var inputH = Input.GetAxis("Mouse X");
            var inputV = Input.GetAxis("Mouse Y");

            // Đảm bảo góc xoay không vượt quá 180 độ
            if (_targetAngles.y > 180)
            {
                _targetAngles.y -= 360;
                _followAngles.y -= 360;
            }  
            if (_targetAngles.y < -180)
            {
                _targetAngles.y += 360;
                _followAngles.y += 360;
            }
            if (_targetAngles.x > 180)
            {
                _targetAngles.x -= 360;
                _followAngles.x -= 360;
            }
            if (_targetAngles.x < -180)
            {
                _targetAngles.x += 360;
                _followAngles.x += 360;
            }

            // Tính toán góc xoay mới
            _targetAngles.y += inputH * rotationSpeed;
            _targetAngles.x += inputV * rotationSpeed;
            
            // Giới hạn góc xoay
            _targetAngles.y = Mathf.Clamp(_targetAngles.y, -rotationRange.y * 0.5f, rotationRange.y * 0.5f); 
            _targetAngles.x = Mathf.Clamp(_targetAngles.x, -rotationRange.x * 0.5f, rotationRange.x * 0.5f);
            
            // Tính toán góc xoay của player
            _followAngles = Vector3.SmoothDamp(_followAngles,_targetAngles,ref _followVelocity,dampingTime); 
            
            // Xoay camera theo góc xoay của player
            transform.localRotation = _originalRotation * Quaternion.Euler(-_followAngles.x, _followAngles.y, 0);

        }
    }
}
