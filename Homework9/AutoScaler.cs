// Requirements:
// Setup: 
// - Start with a new Unity 3D project and place a 3D GameObject (e.g., Cube) into your scene.
// Code Implementation: 
// - enables the GameObject to automatically and continuously increase and decrease in size
// Behavior Parameters:
// - Utilize a constant scaling factor per second (time.deltaTime)
// - reverse the scaling direction after a predetermined duration
// - direction of scale either 1 or -1 use alter scaleSign *= -1
// - ensure smooth transitions

using UnityEngine;

public class AutoScaler : MonoBehaviour
{
    [SerializeField] float duration = 2.0f; // how long you want object to shrink or grow
    private float timer; // timer to keep track of time
    private float scaleSpeed = 1.0f; // speed of scale change
    private int scaleSign = 1; // direction of scale change

    void Update()
    {
        // keep track of time smoothly
        timer += Time.deltaTime;

        // makes sure scale change is also smooth
        float scaleChange = scaleSpeed * scaleSign * Time.deltaTime;

        // save scale change in vector 3 and apply to GameObject
        Vector3 scaleVector = new Vector3(scaleChange, scaleChange, scaleChange);
        transform.localScale += scaleVector;

        // change direction after timer reaches desired duration
        if (timer >= duration)
        {
            // flip direction
            scaleSign *= -1;
            // reset timer
            timer = 0f;
        }
    }
}

