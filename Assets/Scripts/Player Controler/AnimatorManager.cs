using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorManager : MonoBehaviour
{
    Animator animator;
    int vertical;
    int horizontal;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        vertical = Animator.StringToHash("Vert");
        horizontal = Animator.StringToHash("Horz");

    }

    public void UpdateAnimValues(float horizontalInput, float verticalInput)
    {
        //Animation snapping
        float snappedHorizontal;
        float snappedVertical;
        #region Snapping Horizontal
        if (horizontalInput > 0 && horizontalInput < 0.55f)
        {
            snappedHorizontal = 0.5f;
        }
        else if (horizontalInput > 0.55f)
        {
            snappedHorizontal = 1;
        }
        else if (horizontalInput < 0 && horizontalInput > -0.55f)
        {
            snappedHorizontal = -0.5f;
        }
        else if (horizontalInput < -0.55f)
        {
            snappedHorizontal = -1;
        }
        else
        {
            snappedHorizontal = 0;
        }
        #endregion
        #region Snapping Vertical
        if (verticalInput > 0 && verticalInput < 0.55f)
        {
            snappedVertical = 0.5f;
        }
        else if (verticalInput > 0.55f)
        {
            snappedVertical = 1;
        }
        else if (verticalInput < 0 && verticalInput > -0.55f)
        {
            snappedVertical = -0.5f;
        }
        else if (verticalInput < -0.55f)
        {
            snappedVertical = -1;
        }
        else
        {
            snappedVertical = 0;
        }
        #endregion
        animator.SetFloat(horizontal, snappedHorizontal, 0.1f, Time.deltaTime);
        animator.SetFloat(vertical, snappedVertical, 0.1f, Time.deltaTime);
    }
}
