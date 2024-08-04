//These are the changes that the patches do


int HandleBattleIconsInput()
{
//code ignored  

          if (_CanRunFromBattle == 0 && MoveLeft && BattleIconValue == 1)     
            BattleIconValue = 2; //skip the run command

          if (_CanRunFromBattle != 0 && MoveRight && BattleIconValue == 1) 
            BattleIconValue = 0; //skip the run command

//code ignored
}

int HandleBattleIconsInput()
{
//code ignored

          if (MoveLeft && BattleIconValue == 1)     
            BattleIconValue = 2; //It will always skip run command ignoring the flag

          if (MoveRight && BattleIconValue == 1) 
            BattleIconValue = 0; //It will always skip run

//code ignored
}

//This one is not accurate, there's actually a lot more in between, but this is a short version
SetBagSmallBoxColour(int CurrentText)
{
//code ignored

 if(isInBattle && ItemAllowedInBattle)
    colour = 0x9; //White
 else
    colour = 0xa  //Grey

//code ignored
}


SetBagSmallBoxColour(int CurrentText)
{
//code ignored

 if(isInBattle && ItemAllowedInBattle)
    colour = 0xa  //Grey

//code ignored
}


//Same as before, this is not an accurate representation
SetFlagItemUse()
{
//code ignored
      if (IsInBattle == true) 
        {
         if (BattleItemType == 1) || (BattleItemType == 2)) 
            EnableItemUse = 5;  //The trigger to throw the item
        }
        
//code ignored      
}

SetFlagItemUse()
{
//code ignored
      if (IsInBattle == true)         
         return; //Does not trigger the item and end the function
        
        
//code ignored      
}
        

Disassembly:                    
                             HandleBattleIconsInput                                

Original:
        800f0c04 09 00 40 10     beq        v0,zero,0x800f0c2c

        800f0c54 04 00 40 10     beq        v0,zero,0x800f0c68

        800f0cb8 09 00 40 10     beq        v0,zero,0x800f0ce0



Changed:
        800f0c04 00 00 00 00     nop        

        800f0c54 00 00 00 00     nop 

        800f0cb8 00 00 00 00     nop       


                              SetBagSmallBoxColour

Original:                           
        800dab70 0e 00 41 10     beq        v0,at,0x800dabac

Changed:
        800dab70 17 00 40 14     beq        v0,at,0x800dabd8


                              SetFlagItemUse
Original:
        800dc82c 0d 00 41 10     beq        v0,at,0x800dc864

Changed:
        800dc82c 38 00 41 10     beq        v0,at,0x800dc910




//Ultra version exclusive change


MoveAdd()
{

  moveDamage = techDamage;

  if (Attacker != PartnerDigimon)
  {
     if (bossBattle)
         moveDamage = moveDamage + 250;
     else
         moveDamage = moveDamage + 150;
  }

  return moveDamage;
}



                               MoveAdd                
        800a2a8c f0 ff bd 27     addiu      sp,sp,-0x10
        800a2a90 00 00 00 00     nop
        800a2a94 21 18 73 00     addu       v1,v1,s3
        800a2a98 00 00 66 84     lh         a2,0x0(v1)
        800a2a9c 00 00 00 00     nop
        800a2aa0 13 80 01 3c     lui        at,0x8013
        800a2aa4 48 f3 21 8c     lw         at,-0xcb8(at) //PartnerPtr
        800a2aa8 00 00 00 00     nop
        800a2aac 0f 00 41 12     beq        s2,at,0x800a2aec
        800a2ab0 00 00 00 00     _nop
        800a2ab4 8c 94 83 8f     lw         v1,-0x6b74(gp)
        800a2ab8 00 00 00 00     nop
        800a2abc fa 00 63 24     addiu      v1,v1,0xfa
        800a2ac0 59 01 63 90     lbu        v1,0x159(v1) 
        800a2ac4 01 00 01 24     li         at,0x1
        800a2ac8 07 00 61 14     bne        v1,at,LAB_800a2ae8 //Check if this is a boss battle
        800a2acc 00 00 00 00     _nop
        800a2ad0 fa 00 c6 24     addiu      a2,a2,0xfa
        800a2ad4 00 00 00 00     nop
        800a2ad8 04 00 00 10     b          0x800a2aec
        800a2adc 00 00 00 00     _nop
        800a2ae0 00 00 00 00     nop
        800a2ae4 00 00 00 00     nop
                             LAB_800a2ae8                                     
        800a2ae8 96 00 c6 24     addiu      a2,a2,0x96
                             LAB_800a2aec                                    
        800a2aec 08 00 e0 03     jr         ra
        800a2af0 10 00 bd 27     _addiu     sp,sp,0x10



