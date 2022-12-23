using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SolverAOC2022_21
{
  internal class Node
  {

    public string Name { get; set; }

    public long Value { get; set; }

    public ENodeType Type { get; set; }

    public Node LeftChild { get; set; }
    public Node RightChild { get; set; }
    public Node Parent { get; set; }

    public bool IsUnknown { get; set; } = false;

    public Node(string name)
    {
      Name = name;
    }

    public void AddChilds(Node left, Node right, ENodeType type)
    {
      Type = type;
      LeftChild = left;
      RightChild = right;

      LeftChild.Parent = this;
      RightChild.Parent = this;
    }

    public bool IsValueType()
    {
      return Type == ENodeType.Value;
    }

    

    public long Solve()
    {
      if (Type == ENodeType.Value)
      {
        return Value;
      }
 
      long valLeft = Value = LeftChild.Solve();
      long valRight = Value = RightChild.Solve();

      switch (Type)
      {
        case ENodeType.Plus:
          Value = valLeft + valRight;
          break;
        case ENodeType.Minus:
          Value = valLeft - valRight;
          break;
        case ENodeType.Multiply:
          Value = valLeft * valRight;
          break;
        case ENodeType.Divide:
          Value = valLeft / valRight;
          break;
        case ENodeType.Equal:
          Value = valLeft - valRight;
          break;
        case ENodeType.Value:
          throw new Exception($"Incorrect type {Type}");
      }

      return Value;
    }

    internal static ENodeType GetNodeType(string operation)
    {
      switch(operation)
      {
        case "+":
          return ENodeType.Plus;
        case "-":
          return ENodeType.Minus;
        case "*":
          return ENodeType.Multiply;
        case "/":
          return ENodeType.Divide;
        case "=":
          return ENodeType.Equal;
        default:
          throw new Exception($"Unknown operation {operation}");
      }
    }

    internal void SetUnknownPath()
    {

      Node tmp = this;
      this.IsUnknown = true;
      tmp.Parent?.SetUnknownPath();
    }

    internal void SolveTopDown(long targetValue)
    {
      if(LeftChild == null && RightChild == null)
      {
        Value = targetValue;
        return;
      }

      if(LeftChild.IsUnknown && RightChild.IsUnknown)
      {
        throw new Exception($"Unknown childrens {this.Name}");
      }

      IsUnknown = false;

      Node knownNode = LeftChild.IsUnknown?  RightChild : LeftChild;
      Node unknownNode = LeftChild.IsUnknown ? LeftChild : RightChild;
      long knownValue = knownNode.Value;
      

      switch (Type)
      {
        case ENodeType.Plus:
          unknownNode.SolveTopDown(targetValue - knownValue);
          break;
        case ENodeType.Minus:
          if(unknownNode == RightChild)
          {
            unknownNode.SolveTopDown(knownValue - targetValue);
          } 
          else
          {
            unknownNode.SolveTopDown(targetValue + knownValue);
          }
          break;
        case ENodeType.Multiply:
          unknownNode.SolveTopDown(targetValue / knownValue);
          break;
        case ENodeType.Divide:
          if (unknownNode == RightChild)
          {
            unknownNode.SolveTopDown(knownValue / targetValue);
          } 
          else
          {
            unknownNode.SolveTopDown(targetValue * knownValue);
          }
          break;
        case ENodeType.Value:
          throw new Exception("Invalid solve TopDown");
        case ENodeType.Equal:
          unknownNode.SolveTopDown(knownValue);
          break;
      }
      
    }
  }

  public enum ENodeType
  {
    Plus,
    Minus,
    Multiply,
    Divide,
    Value,
    Equal
  }
}
