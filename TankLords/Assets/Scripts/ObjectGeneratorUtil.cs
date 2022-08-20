using UnityEngine;

public class ObjectGeneratorUtil : MonoBehaviour
{
    #region Variables

    [SerializeField] private Color gizmosColour;
    [SerializeField] private float radius;
    [SerializeField] private GameObject objPrefab;
    
    #endregion

    #region Functions

    private void OnDrawGizmos()
    {
        Gizmos.color = gizmosColour;
        Gizmos.DrawWireSphere(transform.position , radius);
    }

    //This is between private and public as only inherited classes can access unlike private where no class can assess and public where every class can access
    protected virtual GameObject GetObject()
    {
        return Instantiate(objPrefab);
    }

    protected Vector2 GetRandomPosition()
    {
        return Random.insideUnitCircle * radius + (Vector2)transform.position;
    }

    public void CreateObject()
    {
        GameObject impactObject = GetObject();
        impactObject.transform.position = transform.position;
        impactObject.transform.rotation = transform.rotation;
    }
    
    #endregion
}
