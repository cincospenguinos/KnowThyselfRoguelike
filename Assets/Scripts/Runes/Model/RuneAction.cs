using System;
using TMPro;

public abstract class RuneAction {
  public Entity OwningEntity;
  protected int CurrentCharge;

  public RuneAction(Entity entityToModify) {
    OwningEntity = entityToModify;
    CurrentCharge = 0;
  }

  public abstract void ReceiveCharge(int amount);
  public abstract RuneAction Clone(Entity otherEntity);
  public abstract string Text();
}