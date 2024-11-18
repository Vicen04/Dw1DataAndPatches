//makes the damage caused have an insane range, not applied to finisher or VS mode


CalculateMovementDamage(int AttackerPtr,int *DefenderPtr,int MoveID)
{
//code ignored
  rand = ReturnRandom(181); //Originally 21
  Damage = (TypeWeaknessSum * (MoveDamage + (Atk/Def * MoveDamage) / 500)) / 30) * (rand + 10)) / 100);

//code ignored
}


Disassembly:

//Battle code example, there is another similar in the tournament code

        8005c054 b4 00 04 24     _li        AttackerPtr,0xb5

        8005c084 0a 00 42 20     addi       v0,v0,0xa
