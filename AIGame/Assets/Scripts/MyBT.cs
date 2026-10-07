using UnityEngine;
using static Unity.VisualScripting.Metadata;

public class MyBT : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public class Node
{
    public enum result
    {
        SUCCESS,
        FAILURE
    }

    public virtual result process()
    {
        return result.FAILURE;
    }
}

public class sequenceNode : Node
{
    public Node[] children;
    public void processChildren()
    {
        for (int i = 0; i < children.Length; i++)
        {
            //should probably return result and exit if fail
            children[i].process();
        }
    }
}

public class MoveNode : Node
{
    public override result process()
    {
        //use navmesh to move to a position
        return result.SUCCESS;
    }
}
