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

        node.AddChild(new TreeNode<string>("���� ���"));
        node.AddChild(new TreeNode<string>("������ ���"));
        
        node.Children[0].AddChild(new TreeNode<string>("�ڶ��� ��"));
        node.Children[0].AddChild(new TreeNode<string>("�� �ڶ��� ��"));
        
        node.Children[0].Children[0].AddChild(new TreeNode<string>("�˰ڽ��ϴ�"));
        node.Children[0].Children[0].AddChild(new TreeNode<string>("��?"));
        node.Children[0].Children[1].AddChild(new TreeNode<string>("��?"));
        node.Children[0].Children[1].AddChild(new TreeNode<string>("��..��ø���.. "));
        node.Children[0].Children[0].Children[1].AddChild(new TreeNode<string>("���� �Ĺ��� �����ش�."));
        node.Children[0].Children[0].Children[1].AddChild(new TreeNode<string>("�˼��մϴ�."));
        
        node.Children[1].Children[0].AddChild(new TreeNode<string>("�ڶ��� ��"));
        node.Children[1].Children[0].AddChild(new TreeNode<string>("�� �ڶ��� ��"));

        node.Children[1].Children[0].Children[0].AddChild(new TreeNode<string>("�˰ڽ��ϴ�"));
        node.Children[1].Children[0].Children[1].AddChild(new TreeNode<string>("��?"));
        node.Children[1].Children[0].Children[1].AddChild(new TreeNode<string>("��.. ��ø���.."));
    }
}
