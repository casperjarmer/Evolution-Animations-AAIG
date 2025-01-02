using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class FirstAgent : Agent
{
    [SerializeField] private Transform targetTransform;
    [SerializeField] private Material winMaterial;
    [SerializeField] private Material loseMaterial;
    [SerializeField] private MeshRenderer floorMeshRenderer;
    [SerializeField] private GameObject[] legs;

    public override void OnEpisodeBegin()
    {
        SetReward(+(transform.localPosition.z/Vector3.Distance(transform.localPosition,targetTransform.localPosition)));

        transform.parent.localPosition = new Vector3(0f,0.5f,-1f);
        transform.parent.localRotation = Quaternion.Euler(0, 0, 0);
        legs[0].transform.localRotation = Quaternion.Euler(0, 0, 0);
        legs[1].transform.localRotation = Quaternion.Euler(0, 0, 0);
        legs[2].transform.localRotation = Quaternion.Euler(0, 0, 0);
        legs[3].transform.localRotation = Quaternion.Euler(0, 0, 0);
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.localPosition);
        sensor.AddObservation(targetTransform.localPosition);
    }
    public override void OnActionReceived(ActionBuffers actions)
    {
        /*float move1 = actions.ContinuousActions[0];
        float move2 = actions.ContinuousActions[1];
        float move3 = actions.ContinuousActions[2];
        float move4 = actions.ContinuousActions[3];*/
        int move11 = actions.DiscreteActions[0];
        int move12 = actions.DiscreteActions[1];
        int move13 = actions.DiscreteActions[2];
        int move14 = actions.DiscreteActions[3];

        float moveSpeed = 5f;
        //transform.localPosition += new Vector3(moveX,0,moveZ)*Time.deltaTime * moveSpeed;
        /*legs[0].transform.Rotate(move1 * Time.deltaTime * moveSpeed, 0, 0, Space.Self);
        legs[1].transform.Rotate(move2 * Time.deltaTime * moveSpeed, 0, 0, Space.Self);
        legs[2].transform.Rotate(move3 * Time.deltaTime * moveSpeed, 0, 0, Space.Self);
        legs[3].transform.Rotate(move4 * Time.deltaTime * moveSpeed, 0, 0, Space.Self);
*/
        legs[0].transform.Rotate(move11 * Time.deltaTime * moveSpeed, 0, 0, Space.Self);
        legs[1].transform.Rotate(move12 * Time.deltaTime * moveSpeed, 0, 0, Space.Self);
        legs[2].transform.Rotate(move13 * Time.deltaTime * moveSpeed, 0, 0, Space.Self);
        legs[3].transform.Rotate(move14 * Time.deltaTime * moveSpeed, 0, 0, Space.Self);


        /*legs[0].GetComponent<Rigidbody>().AddTorque(new Vector3(0, 0, move11 * Time.deltaTime).normalized * moveSpeed, ForceMode.Force);
        legs[1].GetComponent<Rigidbody>().AddTorque(new Vector3(0, 0, move12 * Time.deltaTime).normalized * moveSpeed, ForceMode.Force);
        legs[2].GetComponent<Rigidbody>().AddTorque(new Vector3(0, 0, move13 * Time.deltaTime).normalized * moveSpeed, ForceMode.Force);
        legs[3].GetComponent<Rigidbody>().AddTorque(new Vector3(0, 0, move14 * Time.deltaTime).normalized * moveSpeed, ForceMode.Force);
*/

    }


    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
        continuousActions[0] = -Input.GetAxisRaw("Vertical");
        continuousActions[1] = Input.GetAxisRaw("Horizontal");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Goal"))
        {
            SetReward(+1f);
            floorMeshRenderer.material = winMaterial;
            EndEpisode();
        }
        if (other.TryGetComponent<Wall>(out Wall wall))
        {
            SetReward(-1f);
            floorMeshRenderer.material = loseMaterial;
            EndEpisode();
        }

    }

}
