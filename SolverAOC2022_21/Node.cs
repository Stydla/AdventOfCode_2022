using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

    public bool Solved { get; set; } = false;

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
        Solved = true;
        return Value;
      }
 
      long valLeft = Value = LeftChild.Solve();
      long valRight = Value = RightChild.Solve();
      Solved = true;

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
        default:
          throw new Exception($"Unknown operation {operation}");
      }
    }
  }

  public enum ENodeType
  {
    Plus,
    Minus,
    Multiply,
    Divide,
    Value
  }
}
