//The hardmore patch changes how the code for the move boost works 
    
int MoveAdd(void)
{
  Movedamage = TechDamage;  

  if (DigimonID < 66) 
    {
    if (MoveBoostID == CurrentTechID) 
       MoveDamage = MoveDamage + MoveBoost; //Playable Digimon
    
  }
  else
      MoveDamage = MoveDamage + 100; //NPC

return MoveDamage;
}

Disassembly:

                              MoveAdd()

        800a2a8c f0 ff bd 27     addiu      sp,sp,-0x10
        800a2a90 21 18 73 00     addu       v1,v1,s3
        800a2a94 00 00 66 84     lh         a2,0x0(v1) //MoveDamage
        800a2a98 00 00 00 00     nop
        800a2a9c 00 00 43 8e     lw         v1,0x0(s2) //DigimonID
        800a2aa0 00 00 00 00     nop
        800a2aa4 42 00 61 28     slti       at,v1,0x42
        800a2aa8 04 00 20 14     bne        at,zero,0x800a2ab8 //LAB_800a2ab8
        800a2aac 00 00 00 00     nop
        800a2ab0 0e 00 00 10     b          0x800a2aec //LAB_800a2aec
        800a2ab4 64 00 c6 24     addiu      a2,a2,0x64
                             LAB_800a2ab8                                    
        800a2ab8 c0 08 03 00     sll        at,v1,0x3
        800a2abc 22 08 23 00     sub        at,at,v1
        800a2ac0 80 18 01 00     sll        v1,at,0x2
        800a2ac4 12 80 01 3c     lui        at,0x8012
        800a2ac8 c7 25 21 24     addiu      at,at,0x25c7
        800a2acc 21 08 23 00     addu       at,at,v1
        800a2ad0 00 00 23 80     lb         v1,0x0(at) //TempMovementBoostID
        800a2ad4 00 00 00 00     nop
        800a2ad8 04 00 03 16     bne        s0,v1,0x800a2aec // LAB_800a2aec
        800a2adc 00 00 00 00     _nop
        800a2ae0 03 00 23 84     lh         v1,0x3(at) //TempMovementBoostValue
        800a2ae4 00 00 00 00     nop
        800a2ae8 20 30 c3 00     add        a2,a2,v1
                             LAB_800a2aec                                    
        800a2aec 08 00 e0 03     jr         ra
        800a2af0 10 00 bd 27     _addiu     sp,sp,0x10
