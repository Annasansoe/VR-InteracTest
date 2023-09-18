using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit;

/// <summary>
/// Provides the ability to reset specified objects if they fall below a certain position - designated by this transform's height.
/// </summary>
public class ObjectResetPlaneForSceneThree : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Which objects to reset if falling out of range.")]
    List<Transform> m_ObjectsToReset = new List<Transform>();

    [SerializeField]
    [Tooltip("How often to check if objects should be reset.")]
    float m_CheckDuration = 2f;

    readonly List<Pose> m_OriginalPositions = new List<Pose>();

    float m_CheckTimer;

    [SerializeField] private TMP_Text wrongBin;

    [SerializeField] private float _time = 3f;

    public static int objectFellSceneThree = 0;

    [Header("The object fell sound")]
    public AudioSource audioSource;
    public AudioClip soundClip;
    /// <summary>
    /// See <see cref="MonoBehaviour"/>.
    /// </summary>
    protected void Start()
    {
        wrongBin.enabled = false;
        foreach (var currentTransform in m_ObjectsToReset)
        {
            if (currentTransform != null)
            {
                m_OriginalPositions.Add(new Pose(currentTransform.position, currentTransform.rotation));
            }
            else
            {
                Debug.LogWarning("Objects To Reset contained a null element. Update the reference or delete the array element of the missing object.", this);
                m_OriginalPositions.Add(new Pose());
            }
        }
    }

    /// <summary>
    /// See <see cref="MonoBehaviour"/>.
    /// </summary>
    protected void Update()
    {
        m_CheckTimer -= Time.deltaTime;

        if (m_CheckTimer > 0)
            return;

        m_CheckTimer = m_CheckDuration;

        var resetPlane = transform.position.y;

        for (var transformIndex = 0; transformIndex < m_ObjectsToReset.Count; transformIndex++)
        {
            var currentTransform = m_ObjectsToReset[transformIndex];
            if (currentTransform == null)
                continue;

            if (currentTransform.position.y < resetPlane)
            {
                currentTransform.SetPositionAndRotation(m_OriginalPositions[transformIndex].position, m_OriginalPositions[transformIndex].rotation);

                var rigidBody = currentTransform.GetComponentInChildren<Rigidbody>();
                if (rigidBody != null)
                {
                    rigidBody.velocity = Vector3.zero;
                    rigidBody.angularVelocity = Vector3.zero;
                    StartCoroutine(ShowMessage());
                    PlaySound();
                }
            }
        }

    }

    private IEnumerator ShowMessage()
    {
        wrongBin.enabled = true;
        objectFellSceneThree++;
        yield return new WaitForSeconds(_time);

        wrongBin.enabled = false;
    }

    void PlaySound()
    {
        if (audioSource != null && soundClip != null)
        {
            audioSource.PlayOneShot(soundClip);
        }
    }
}


