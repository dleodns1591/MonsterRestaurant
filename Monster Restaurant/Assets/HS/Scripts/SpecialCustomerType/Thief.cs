using HS_Tree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EThiefSpeechCase
{
    NormalOrder,


}

public enum EThiefSelectCase
{
    Okey,
    ReOrder,
    What,
    Wait,
    Show,
    Sorry
}

public class Thief : MonoBehaviour, I_CustomerType
{
    public void SpecialType(TreeNode<string> SelectSpeechTree)
    {
        TreeNode<string> node = SelectSpeechTree.Children[(int)EeventCustomerType.Thief];

        node.AddChild(new TreeNode<string>("선택 대사"));
        node.AddChild(new TreeNode<string>("강도의 대사"));
        
        node.Children[0].AddChild(new TreeNode<string>("자랐을 시"));
        node.Children[0].AddChild(new TreeNode<string>("안 자랐을 시"));
        
        node.Children[0].Children[0].AddChild(new TreeNode<string>("알겠습니다"));
        node.Children[0].Children[0].AddChild(new TreeNode<string>("네?"));
        node.Children[0].Children[1].AddChild(new TreeNode<string>("네?"));
        node.Children[0].Children[1].AddChild(new TreeNode<string>("잠..잠시만요.. "));
        node.Children[0].Children[0].Children[1].AddChild(new TreeNode<string>("식인 식물을 보여준다."));
        node.Children[0].Children[0].Children[1].AddChild(new TreeNode<string>("죄송합니다."));
        
        node.Children[1].Children[0].AddChild(new TreeNode<string>("자랐을 시"));
        node.Children[1].Children[0].AddChild(new TreeNode<string>("안 자랐을 시"));

        node.Children[1].Children[0].Children[0].AddChild(new TreeNode<string>("알겠습니다"));
        node.Children[1].Children[0].Children[1].AddChild(new TreeNode<string>("네?"));
        node.Children[1].Children[0].Children[1].AddChild(new TreeNode<string>("잠.. 잠시만요.."));
    }
}
