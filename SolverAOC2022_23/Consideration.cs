using System;

namespace SolverAOC2022_23
{
  public class Consideration
  {

    public EDirection Direction { get; set; }
    public Field TargetField { get; set; }
    public Elf Elf { get; set; }

    public Consideration(Field targetField, Elf elf, EDirection direction)
    {
      TargetField = targetField;
      Elf = elf;
      Direction = direction;
    }

    internal void Execute(Map m)
    {
      m.ElvesByPosition.Remove(Elf.Position);
      Elf.Move(TargetField, Direction);
      m.ElvesByPosition.Add(TargetField, Elf);
    }
  }
}