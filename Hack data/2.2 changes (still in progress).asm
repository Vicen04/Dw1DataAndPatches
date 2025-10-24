//Calculate damage function, it has been changed and adapted to the new effects, it is also now shared between the battle and tournament functions


int CalculateDamage(DigimonEntity *AttackerPtr, DigimonEntity *DefenderPtr, int moveID)
{
  byte attackType;
  int attack;
  int rand;
  uint DigimonType;
  int defense;
  int attDamage;
  byte typeDamageFactor[3];
  
  attackType = AllMovementData[moveID].type;
  for (int i = 0; i < 3; i++) 
  {
    DigimonType = AllDigimonData[(DefenderPtr->DigimonEntity).DigimonType].specialities[i];
    if (DigimonType == -1) 
      typeDamageFactor[i] = 10;    
    else 
      typeDamageFactor[i] = TypeResistances[DigimonType + attackType * 7];    
  }

  attack = AttackerPtr->DigimonStats.DigimonOff;
  defense = DefenderPtr->DigimonStats.DigimonDef;

  if (moveID == 45) //attack is counter
    defense = (defense * 3) / 10;
  
  if ((moveID < 57) || (116 < moveID)) //is not a finisher
  {
    attack = attack - defense;

    if (500 < attack) 
      attack = 500;
    
    if (attack < -500) 
      attack = -500;
  
    rand = ReturnRandom(21);
    attDamage = AllMovementData[moveID].damage;
    if ((AttackerPtr == PartnerPtr) && (moveID == RaiseData[AttackerPtr->DigimonEntity.DigimonType].moveBoost)) 
      attDamage = attDamage + RaiseData[AttackerPtr->DigimonEntity.DigimonType].boostValue;
    
    attDamage = ((((typeDamageFactor[2] + typeDamageFactor[0] + typeDamageFactor[1]) * 
                   (attDamage + (attack * attDamage) / 500)) / 30) * (rand + 90)) / 100;
  }
  else 
    attDamage = (attack + AllMovementData[moveID].damage) * (typeDamageFactor[2] + typeDamageFactor[0] + typeDamageFactor[1]) / 30;
  
  if ((56 < moveID) && (moveID < 117)) 
  {
    if (AttackerPtr == PartnerPtr)     
      if (40 < FinisherFilled)                        //Finisher filled is how filled the finisher bar is (the one where you have to mash L1 + R1), the max is 80
        attDamage = (attDamage * FinisherFilled) / 40;       
    else 
    {
      rand = ReturnRandom(101);
      attDamage = (attDamage * (rand + 100)) / 100;
    }

    rand = ReturnRandom(21);
    attDamage = (attDamage * (rand + 90)) / 100;
  }

  if (AttackerPtr->NewEffects == 2) //attacker is burned
    attDamage = attDamage - (attDamage / 4);
  
  if (attDamage < 1) 
    attDamage = 1;
  
  if (DefenderPtr->NewEffects == 4) //defender is frozen
    attDamage = attDamage + (attDamage / 4);
  
  if (9999 < attDamage) 
    attDamage = 9999;
  
  return attDamage;
}
                             
                             CalculateDamage
        80095934 cc ff bd 27     addiu      sp,sp,-0x34
        80095938 24 00 bf af     sw         ra,0x24(sp)
        8009593c 1c 00 b3 af     sw         s3,0x1c(sp)
        80095940 18 00 b2 af     sw         s2,0x18(sp)
        80095944 14 00 b1 af     sw         s1,0x14(sp)
        80095948 10 00 b0 af     sw         s0,0x10(sp)
        8009594c 20 00 b4 af     sw         s4,0x20(sp)
        80095950 12 80 03 3c     lui        v1,0x8012
        80095954 21 a0 05 00     move       s4,a1
        80095958 21 90 80 00     move       s2,a0
        8009595c 21 80 c0 00     move       s0,a2
        80095960 00 21 10 00     sll        a0,s0,0x4
        80095964 45 62 63 24     addiu      v1,v1,0x6245
        80095968 21 98 80 00     move       s3,a0
        8009596c 21 18 64 00     addu       v1,v1,a0
        80095970 00 00 64 90     lbu        a0,0x0(v1)  //AllMovementData[moveID].type
        80095974 21 10 00 00     clear      v0
        80095978 c0 18 04 00     sll        v1,a0,0x3
        8009597c 1c 00 00 10     b          0x800959f0
        80095980 22 18 64 00     _sub       v1,v1,a0
                             LAB_80095984                                   
        80095984 00 00 a6 8c     lw         a2,0x0(a1)  
        80095988 ff 00 01 24     li         at,0xff
        8009598c 40 20 06 00     sll        a0,a2,0x1
        80095990 20 20 86 00     add        a0,a0,a2
        80095994 80 20 04 00     sll        a0,a0,0x2
        80095998 20 20 86 00     add        a0,a0,a2
        8009599c 80 30 04 00     sll        a2,a0,0x2
        800959a0 13 80 04 3c     lui        a0,0x8013
        800959a4 d2 ce 84 24     addiu      a0,a0,-0x312e
        800959a8 21 20 86 00     addu       a0,a0,a2
        800959ac 21 20 44 00     addu       a0,v0,a0
        800959b0 00 00 84 90     lbu        a0,0x0(a0)  ////AllDigimonData[(DefenderPtr->DigimonEntity).DigimonType].specialities[i]
        800959b4 00 00 00 00     nop
        800959b8 09 00 81 10     beq        a0,at,0x800959e0
        800959bc 21 30 80 00     _move      a2,a0
        800959c0 12 80 04 3c     lui        a0,0x8012
        800959c4 70 5f 84 24     addiu      a0,a0,0x5f70
        800959c8 21 20 83 00     addu       a0,a0,v1
        800959cc 21 20 c4 00     addu       a0,a2,a0
        800959d0 00 00 86 90     lbu        a2,0x0(a0)  //TypeResistances[DigimonType + attackType * 7]
        800959d4 21 20 a2 03     addu       a0,sp,v0
        800959d8 04 00 00 10     b          0x800959ec
        800959dc 2c 00 86 a0     _sb        a2,0x2c(a0)
                             LAB_800959e0                                   
        800959e0 0a 00 06 24     li         a2,0xa
        800959e4 21 20 a2 03     addu       a0,sp,v0
        800959e8 2c 00 86 a0     sb         a2,0x2c(a0)
                             LAB_800959ec                                    
        800959ec 01 00 42 20     addi       v0,v0,0x1
                             LAB_800959f0                                  
        800959f0 03 00 41 28     slti       at,v0,0x3
        800959f4 e3 ff 20 14     bne        at,zero,0x80095984
        800959f8 00 00 00 00     _nop
        800959fc 38 00 42 86     lh         v0,0x38(s2)  //AttackerPtr->DigimonStats.DigimonOff
        80095a00 3a 00 a4 84     lh         a0,0x3a(a1)  //DefenderPtr->DigimonStats.DigimonDef
        80095a04 2d 00 01 24     li         at,0x2d
        80095a08 0c 00 01 16     bne        s0,at,0x80095a3c
        80095a0c 00 00 00 00     _nop
        80095a10 40 18 04 00     sll        v1,a0,0x1
        80095a14 20 20 64 00     add        a0,v1,a0
        80095a18 66 66 03 3c     lui        v1,0x6666
        80095a1c 67 66 63 34     ori        v1,v1,0x6667
        80095a20 18 00 64 00     mult       v1,a0
        80095a24 10 18 00 00     mfhi       v1
        80095a28 c2 27 04 00     srl        a0,a0,0x1f
        80095a2c 83 18 03 00     sra        v1,v1,0x2
        80095a30 21 18 64 00     addu       v1,v1,a0
        80095a34 00 24 03 00     sll        a0,v1,0x10
        80095a38 03 24 04 00     sra        a0,a0,0x10
                             LAB_80095a3c                                    
        80095a3c 39 00 01 2a     slti       at,s0,0x39
        80095a40 1b 00 20 14     bne        at,zero,0x80095ab0
        80095a44 00 00 00 00     _nop
        80095a48 75 00 01 2a     slti       at,s0,0x75
        80095a4c 18 00 20 10     beq        at,zero,0x80095ab0
        80095a50 00 00 00 00     _nop
        80095a54 2d 00 a5 93     lbu        a1,0x2d(sp)
        80095a58 2c 00 a4 93     lbu        a0,0x2c(sp)
        80095a5c 2e 00 a3 93     lbu        v1,0x2e(sp)
        80095a60 20 20 85 00     add        a0,a0,a1
        80095a64 20 20 64 00     add        a0,v1,a0
        80095a68 12 80 03 3c     lui        v1,0x8012
        80095a6c 40 62 63 24     addiu      v1,v1,0x6240
        80095a70 21 18 73 00     addu       v1,v1,s3
        80095a74 00 00 63 84     lh         v1,0x0(v1)  //AllMovementData[moveID].damage
        80095a78 00 00 00 00     nop
        80095a7c 20 10 43 00     add        v0,v0,v1
        80095a80 18 00 44 00     mult       v0,a0
        80095a84 88 88 02 3c     lui        v0,0x8888
        80095a88 12 20 00 00     mflo       a0
        80095a8c 89 88 42 34     ori        v0,v0,0x8889
        80095a90 00 00 00 00     nop
        80095a94 18 00 44 00     mult       v0,a0
        80095a98 c2 1f 04 00     srl        v1,a0,0x1f
        80095a9c 10 10 00 00     mfhi       v0
        80095aa0 21 10 44 00     addu       v0,v0,a0
        80095aa4 03 11 02 00     sra        v0,v0,0x4
        80095aa8 4b 00 00 10     b          0x80095bd8
        80095aac 21 88 43 00     _addu      s1,v0,v1
                             LAB_80095ab0                                   
        80095ab0 22 88 44 00     sub        s1,v0,a0
        80095ab4 f5 01 21 2a     slti       at,s1,0x1f5
        80095ab8 02 00 20 14     bne        at,zero,0x80095ac4
        80095abc 00 00 00 00     _nop
        80095ac0 f4 01 11 24     li         s1,0x1f4
                             LAB_80095ac4                                      
        80095ac4 0c fe 21 2a     slti       at,s1,-0x1f4
        80095ac8 02 00 20 10     beq        at,zero,0x80095ad4
        80095acc 00 00 00 00     _nop
        80095ad0 0c fe 11 24     li         s1,-0x1f4
                             LAB_80095ad4                                   
        80095ad4 b5 8d 02 0c     jal        0x800a36d4  //ReturnRandom                                    
        80095ad8 15 00 04 24     _li        a0,0x15
        80095adc 12 80 03 3c     lui        v1,0x8012
        80095ae0 40 62 63 24     addiu      v1,v1,0x6240
        80095ae4 21 18 73 00     addu       v1,v1,s3
        80095ae8 00 00 66 84     lh         a2,0x0(v1)  //AllMovementData[moveID].damage
        80095aec 00 00 00 00     nop
        80095af0 13 80 01 3c     lui        at,0x8013
        80095af4 48 f3 21 8c     lw         at,-0xcb8(at)  //PartnerPtr                       
        80095af8 00 00 00 00     nop
        80095afc 10 00 41 16     bne        s2,at,0x80095b40
        80095b00 00 00 00 00     _nop
        80095b04 00 00 43 8e     lw         v1,0x0(s2)
        80095b08 00 00 00 00     nop
        80095b0c c0 08 03 00     sll        at,v1,0x3
        80095b10 22 08 23 00     sub        at,at,v1
        80095b14 80 18 01 00     sll        v1,at,0x2
        80095b18 12 80 01 3c     lui        at,0x8012
        80095b1c c7 25 21 24     addiu      at,at,0x25c7
        80095b20 21 08 23 00     addu       at,at,v1
        80095b24 00 00 23 80     lb         v1,0x0(at)  //RaiseData[AttackerPtr->DigimonEntity.DigimonType].moveBoost
        80095b28 00 00 00 00     nop
        80095b2c 04 00 03 16     bne        s0,v1,0x80095b40
        80095b30 00 00 00 00     _nop
        80095b34 03 00 23 84     lh         v1,0x3(at)  //RaiseData[AttackerPtr->DigimonEntity.DigimonType].boostValue
        80095b38 00 00 00 00     nop
        80095b3c 20 30 c3 00     add        a2,a2,v1
                             LAB_80095b40                                   
        80095b40 2d 00 a5 93     lbu        a1,0x2d(sp)
        80095b44 18 00 26 02     mult       s1,a2
        80095b48 62 10 03 3c     lui        v1,0x1062
        80095b4c 12 20 00 00     mflo       a0
        80095b50 d3 4d 63 34     ori        v1,v1,0x4dd3
        80095b54 00 00 00 00     nop
        80095b58 18 00 64 00     mult       v1,a0
        80095b5c 5a 00 42 20     addi       v0,v0,0x5a
        80095b60 10 18 00 00     mfhi       v1
        80095b64 c2 27 04 00     srl        a0,a0,0x1f
        80095b68 43 19 03 00     sra        v1,v1,0x5
        80095b6c 21 18 64 00     addu       v1,v1,a0
        80095b70 20 30 c3 00     add        a2,a2,v1
        80095b74 2c 00 a4 93     lbu        a0,0x2c(sp)
        80095b78 2e 00 a3 93     lbu        v1,0x2e(sp)
        80095b7c 20 20 85 00     add        a0,a0,a1
        80095b80 20 18 64 00     add        v1,v1,a0
        80095b84 18 00 66 00     mult       v1,a2
        80095b88 88 88 03 3c     lui        v1,0x8888
        80095b8c 12 28 00 00     mflo       a1
        80095b90 89 88 63 34     ori        v1,v1,0x8889
        80095b94 00 00 00 00     nop
        80095b98 18 00 65 00     mult       v1,a1
        80095b9c c2 27 05 00     srl        a0,a1,0x1f
        80095ba0 10 18 00 00     mfhi       v1
        80095ba4 21 18 65 00     addu       v1,v1,a1
        80095ba8 03 19 03 00     sra        v1,v1,0x4
        80095bac 21 18 64 00     addu       v1,v1,a0
        80095bb0 18 00 62 00     mult       v1,v0
        80095bb4 eb 51 02 3c     lui        v0,0x51eb
        80095bb8 12 18 00 00     mflo       v1
        80095bbc 1f 85 42 34     ori        v0,v0,0x851f
        80095bc0 00 00 00 00     nop
        80095bc4 18 00 43 00     mult       v0,v1
        80095bc8 10 10 00 00     mfhi       v0
        80095bcc c2 1f 03 00     srl        v1,v1,0x1f
        80095bd0 43 11 02 00     sra        v0,v0,0x5
        80095bd4 21 88 43 00     addu       s1,v0,v1
                             LAB_80095bd8                                  
        80095bd8 39 00 01 2a     slti       at,s0,0x39
        80095bdc 35 00 20 14     bne        at,zero,0x80095cb4
        80095be0 00 00 00 00     _nop
        80095be4 75 00 01 2a     slti       at,s0,0x75
        80095be8 32 00 20 10     beq        at,zero,0x80095cb4
        80095bec 00 00 00 00     _nop
        80095bf0 13 80 01 3c     lui        at,0x8013
        80095bf4 48 f3 22 8c     lw         v0,-0xcb8(at)  //PartnerPtr                        
        80095bf8 00 00 00 00     nop
        80095bfc 13 00 42 16     bne        s2,v0,0x80095c4c
        80095c00 00 00 00 00     _nop
        80095c04 20 92 82 8f     lw         v0,-0x6de0(gp)  //CombatHead                      
        80095c08 00 00 00 00     nop
        80095c0c 68 06 42 90     lbu        v0,0x668(v0)  //FinisherFilled
        80095c10 00 00 00 00     nop
        80095c14 29 00 41 2c     sltiu      at,v0,0x29
        80095c18 19 00 20 14     bne        at,zero,0x80095c80
        80095c1c 21 18 40 00     _move      v1,v0
        80095c20 18 00 23 02     mult       s1,v1
        80095c24 66 66 02 3c     lui        v0,0x6666
        80095c28 12 18 00 00     mflo       v1
        80095c2c 67 66 42 34     ori        v0,v0,0x6667
        80095c30 00 00 00 00     nop
        80095c34 18 00 43 00     mult       v0,v1
        80095c38 10 10 00 00     mfhi       v0
        80095c3c c2 1f 03 00     srl        v1,v1,0x1f
        80095c40 03 11 02 00     sra        v0,v0,0x4
        80095c44 0e 00 00 10     b          0x80095c80
        80095c48 21 88 43 00     _addu      s1,v0,v1
                             LAB_80095c4c                                    
        80095c4c b5 8d 02 0c     jal        0x800a36d4  //ReturnRandom                                       
        80095c50 65 00 04 24     _li        a0,0x65
        80095c54 64 00 42 20     addi       v0,v0,0x64
        80095c58 18 00 22 02     mult       s1,v0
        80095c5c eb 51 02 3c     lui        v0,0x51eb
        80095c60 12 18 00 00     mflo       v1
        80095c64 1f 85 42 34     ori        v0,v0,0x851f
        80095c68 00 00 00 00     nop
        80095c6c 18 00 43 00     mult       v0,v1
        80095c70 10 10 00 00     mfhi       v0
        80095c74 c2 1f 03 00     srl        v1,v1,0x1f
        80095c78 43 11 02 00     sra        v0,v0,0x5
        80095c7c 21 88 43 00     addu       s1,v0,v1
                             LAB_80095c80                                    
        80095c80 b5 8d 02 0c     jal        0x800a36d4  //ReturnRandom                                     
        80095c84 15 00 04 24     _li        a0,0x15
        80095c88 5a 00 42 20     addi       v0,v0,0x5a
        80095c8c 18 00 22 02     mult       s1,v0
        80095c90 eb 51 02 3c     lui        v0,0x51eb
        80095c94 12 18 00 00     mflo       v1
        80095c98 1f 85 42 34     ori        v0,v0,0x851f
        80095c9c 00 00 00 00     nop
        80095ca0 18 00 43 00     mult       v0,v1
        80095ca4 10 10 00 00     mfhi       v0
        80095ca8 c2 1f 03 00     srl        v1,v1,0x1f
        80095cac 43 11 02 00     sra        v0,v0,0x5
        80095cb0 21 88 43 00     addu       s1,v0,v1
                             LAB_80095cb4                                   
        80095cb4 57 00 42 82     lb         v0,0x57(s2)
        80095cb8 02 00 01 24     li         at,0x2
        80095cbc 03 00 41 14     bne        v0,at,0x80095ccc
        80095cc0 00 00 00 00     _nop
        80095cc4 83 18 11 00     sra        v1,s1,0x2
        80095cc8 22 88 23 02     sub        s1,s1,v1
                             LAB_80095ccc                                   
        80095ccc 02 00 20 1e     bgtz       s1,0x80095cd8
        80095cd0 00 00 00 00     _nop
        80095cd4 01 00 11 24     li         s1,0x1
                             LAB_80095cd8                                     
        80095cd8 57 00 82 82     lb         v0,0x57(s4)
        80095cdc 04 00 01 24     li         at,0x4
        80095ce0 03 00 41 14     bne        v0,at,0x80095cf0
        80095ce4 00 00 00 00     _nop
        80095ce8 83 18 11 00     sra        v1,s1,0x2
        80095cec 21 88 23 02     addu       s1,s1,v1
                             LAB_80095cf0                                   
        80095cf0 10 27 21 2a     slti       at,s1,0x2710
        80095cf4 02 00 20 14     bne        at,zero,0x80095d00
        80095cf8 00 00 00 00     _nop
        80095cfc 0f 27 11 24     li         s1,0x270f
                             LAB_80095d00                                   
        80095d00 21 10 20 02     move       v0,s1
        80095d04 24 00 bf 8f     lw         ra,0x24(sp)
        80095d08 1c 00 b3 8f     lw         s3,0x1c(sp)
        80095d0c 18 00 b2 8f     lw         s2,0x18(sp)
        80095d10 14 00 b1 8f     lw         s1,0x14(sp)
        80095d14 10 00 b0 8f     lw         s0,0x10(sp)
        80095d18 20 00 b4 8f     lw         s4,0x20(sp)
        80095d1c 08 00 e0 03     jr         ra
        80095d20 34 00 bd 27     _addiu     sp,sp,0x34


// Hardmode version
int CalculateDamage(DigimonEntity *AttackerPtr, DigimonEntity *DefenderPtr, int moveID)
{
//code ignored
    digimonID = AttackerPtr->DigimonEntity.DigimonType;
    if (digimonID < 66) 
    {
      if (moveID == RaiseData[digimonID].moveBoost) 
        attDamage = attDamage + RaiseData[digimonID].boostValue;
      
    }
    else 
      attDamage = attDamage + 100;

//code ignored

}


        80095af0 00 00 43 8e     lw         v1,0x0(s2)
        80095af4 00 00 00 00     nop
        80095af8 42 00 61 28     slti       at,v1,0x42
        80095afc 03 00 01 14     bne        zero,at,0x80095b0c
        80095b00 00 00 00 00     _nop
        80095b04 0e 00 00 10     b          0x80095b40
        80095b08 64 00 c6 24     _addiu     a2,a2,0x64
                             LAB_80095b0c                                     
        80095b0c c0 08 03 00     sll        at,v1,0x3
        80095b10 22 08 23 00     sub        at,at,v1
        80095b14 80 18 01 00     sll        v1,at,0x2
        80095b18 12 80 01 3c     lui        at,0x8012
        80095b1c c7 25 21 24     addiu      at,at,0x25c7
        80095b20 21 08 23 00     addu       at,at,v1
        80095b24 00 00 23 80     lb         v1,0x0(at)  //RaiseData[digimonID].moveBoost
        80095b28 00 00 00 00     nop
        80095b2c 04 00 03 16     bne        s0,v1,0x80095b40
        80095b30 00 00 00 00     _nop
        80095b34 03 00 23 84     lh         v1,0x3(at)  //RaiseData[digimonID].boostValue
        80095b38 00 00 00 00     nop
        80095b3c 20 30 c3 00     add        a2,a2,v1



// Hardcore version

int CalculateDamage(DigimonEntity *AttackerPtr, DigimonEntity *DefenderPtr, int moveID)
{
//code ignored
  //hardcore
    if (600 < attack) 
      attack = 600;
    
    if (attack < -600) 
      attack = -600;

// true hardcore
    if (700 < attack) 
      attack = 700;
    
    if (attack < -700) 
      attack = -700;

//same in both

    digimonID = AttackerPtr->DigimonEntity.DigimonType;
    if (digimonID < 66) 
    {
      if (moveID == RaiseData[digimonID].moveBoost) 
        attDamage = attDamage + RaiseData[digimonID].boostValue;
      
    }
//hardcore
    else 
      attDamage = attDamage + 200;

//true hardcore
    else 
      attDamage = attDamage + 300;

//code ignored

 //hardcore
    else 
    {
      rand = ReturnRandom(201);
      attDamage = (attDamage * (rand + 200)) / 100;
    }

//true hardcore
    else 
    {
      rand = ReturnRandom(201);
      attDamage = (attDamage * (rand + 300)) / 100;
    }

//code ignored
}

        //hardcore
        80095ab4 59 02 21 2a     slti       at,s1,0x259
        //true hardcore
        80095ab4 bc 02 21 2a     slti       at,s1,0x2bd
	
        //hardcore
        80095ac0 f4 01 11 24     li         s1,0x258
        //true hardcore
        80095ac0 bc 02 21 2a     li         s1,0x2bc

        //hardcore
        80095ac4 a7 fd 21 2a     slti       at,s1,-0x259
        //true hardcore
        80095ac4 43 fd 21 2a     slti       at,s1,-0x2bd
	
        //hardcore
        80095ad0 a8 fd 11 24     li         s1,-0x258
        //true hardcore
        80095ad0 44 fd 21 2a     li         s1,-0x2bc
                             


        80095af0 00 00 43 8e     lw         v1,0x0(s2)
        80095af4 00 00 00 00     nop
        80095af8 42 00 61 28     slti       at,v1,0x42
        80095afc 03 00 01 14     bne        zero,at,0x80095b0c
        80095b00 00 00 00 00     _nop
        80095b04 0e 00 00 10     b          0x80095b40
        //hardcore
        80095b08 c8 00 c6 24     _addiu     a2,a2,0xc8
        //true hardcore
        80095b08 2c 01 c6 24     _addiu     a2,a2,0x12c
                             LAB_80095b0c                                     
        80095b0c c0 08 03 00     sll        at,v1,0x3
        80095b10 22 08 23 00     sub        at,at,v1
        80095b14 80 18 01 00     sll        v1,at,0x2
        80095b18 12 80 01 3c     lui        at,0x8012
        80095b1c c7 25 21 24     addiu      at,at,0x25c7
        80095b20 21 08 23 00     addu       at,at,v1
        80095b24 00 00 23 80     lb         v1,0x0(at)  //RaiseData[digimonID].moveBoost
        80095b28 00 00 00 00     nop
        80095b2c 04 00 03 16     bne        s0,v1,0x80095b40
        80095b30 00 00 00 00     _nop
        80095b34 03 00 23 84     lh         v1,0x3(at)  //RaiseData[digimonID].boostValue
        80095b38 00 00 00 00     nop
        80095b3c 20 30 c3 00     add        a2,a2,v1

        //hardcore
        80095c50 c9 00 04 24     _li        a0,0xc9

        //hardcore
        80095c54 c8 00 42 20     addi       v0,v0,0xc8
        //true hardcore
        80095c54 2c 01 42 20     addi       v0,v0,0x12c

        

//ultra version

int CalculateDamage(DigimonEntity *AttackerPtr, DigimonEntity *DefenderPtr, int moveID)
{
//code ignored
    digimonID = AttackerPtr->DigimonEntity.DigimonType;
    if (digimonID != PartnerPtr) 
    {
      //ultra hardmode
      if (GameState[595] == 1) //is a boss battle
        attDamage = attDamage + 250;
      else 
        attDamage = attDamage + 150;

      //ultra hardcore
      if (GameState[595] == 1)
        attDamage = attDamage + 350;
      else 
        attDamage = attDamage + 250;

      //ultra true hardcore
      if (GameState[595] == 1) 
        attDamage = attDamage + 450;
      else 
        attDamage = attDamage + 350;
    }   

//code ignored
}

        80095afc 10 00 41 12     beq        s2,at,0x80095b40
        80095b00 00 00 00 00     _nop
        80095b04 8c 94 83 8f     lw         v1,-0x6b74(gp) //GameState
        80095b08 00 00 00 00     nop
        80095b0c fa 00 63 24     addiu      v1,v1,0xfa
        80095b10 59 01 63 90     lbu        v1,0x159(v1) //BossFlag
        80095b14 01 00 01 24     li         at,0x1
        80095b18 08 00 23 14     bne        at,v1,0x80095b3c
        80095b1c 00 00 00 00     _nop

//ultra hardmode
        80095b20 fa 00 c6 24     addiu      a2,a2,0xfa
//ultra hardcore
        80095b20 5e 01 c6 24     addiu      a2,a2,0x15e
//ultra true hardcore
        80095b20 c2 01 c6 24     addiu      a2,a2,0x1c2

        80095b24 06 00 00 10     b          0x80095b40
        80095b28 00 00 00 00     _nop
        80095b2c 00 00 00 00     nop
        80095b30 00 00 00 00     nop
        80095b34 00 00 00 00     nop
        80095b38 00 00 00 00     nop
                             LAB_80095b3c  
//ultra hardmode                                 
        80095b3c 96 00 c6 24     addiu      a2,a2,0x96
//ultra hardcore
        80095b3c fa 00 c6 24     addiu      a2,a2,0xfa
//ultra true hardcore
        80095b3c 5e 01 c6 24     addiu      a2,a2,0x15e




// This is a new funciton for the status effects, it works in the same way in the battles and tournaments but the addresses are different

void StatusEffectNew(DigimonEntity *AffectedPtr,FighterData *CombatPtr,int Tech4,int attPtr)
{
  byte value;
  BattleFlags BVar2;
  int iVar3;

    if ((CombatPtr->BattleFlags & 0x100) != 0) //Protection is active
    return;
  
  iVar3 = 0;
  if ((attPtr == PartnerPtr) && (attPtr = RaiseData[(AttackerPtr->DigimonEntity).DigimonType].moveBoost * 16, Tech4 == attPtr)) 
    iVar3 = 3;
  
  value = AllMovementData[iVar3 + Tech4];  //Either the normal effect chance or the boosted effect chance
  if (value == 0) 
    return;
  
  iVar3 = ReturnRandom(100);
  if (value <= iVar3) 
    return;
  
  iVar3 = 0;
  if (attPtr == Tech4) 
    iVar3 = 4;
  
  value = AllMovementData[iVar3 + Tech4]; //Either the normal effect or the boosted effect

  if (value == 4) //Flatten
  {
    if ((CombatPtr->BattleFlags & 8) == 0) //is flatten
    {
      CombatPtr->flattenTimer = -1;
      RemoveEffectSprites(AffectedPtr,&CombatPtr);
      ResetDumbCooldownTimers(CombatPtr);
    }   
  }
  else if (value == 3) //Stun
  {
    if (((ushort)CombatPtr->BattleFlags & 4) == 0) //is stunned
    {
      CombatPtr->BattleFlags = CombatPtr->BattleFlags | 4; //set the stun
      iVar3 = ReturnRandom(41);
      CombatPtr->stunTimer = iVar3 + 200;
      SetStunFX(AffectedPtr,CombatPtr);
      ResetDumbCooldownTimers(CombatPtr);
    }    
  }
  else if (value == 2) //Confusion
  {
    if (((ushort)CombatPtr->BattleFlags & 2) == 0) //is confused
    {
      CombatPtr->BattleFlags = CombatPtr->BattleFlags | 2; //set the confusion
      iVar3 = ReturnRandom(101);
      CombatPtr->confusedTimer = iVar3 + 200;
      SetConfusionFX(AffectedPtr,CombatPtr);
      ResetDumbCooldownTimers(CombatPtr);
    }    
  }
  else ((value < 8) && ((CombatPtr->BattleFlags & 1) != 0)) 
  {
    if (value == 1 || value == 5) 
    {
      if (value == 1)
      AffectedPtr->NewEffects = 1;
      else if (value == 5)
      AffectedPtr->NewEffects = 2;
      CombatPtr->BattleFlags = BVar2 | 1;
      CombatPtr->poisonTimer = 100;
    }
    else 
    {
      if (value == 6) 
      {
        AffectedPtr->NewEffects = 4;
        CombatPtr->BattleFlags = BVar2 | 1;
      }
      else if (value == 7)
      {        
        AffectedPtr->NewEffects = 8;
        CombatPtr->BattleFlags = BVar2 | 1;
      }
    }
    SetPoisonFX(AffectedPtr,&CombatPtr);
  }
  if (CombatPtr == *CombatHead) //fighter is your partner
      CombatHead.statusCount++;
  
  return;
}


                             
 //battle offset                                                       
                             StatusEffectNew                                
        8005beb8 d4 ff bd 27     addiu      sp,sp,-0x2c
        8005bebc 20 00 bf af     sw         ra,0x20(sp)
        8005bec0 1c 00 b3 af     sw         s3,0x1c(sp)
        8005bec4 18 00 b2 af     sw         s2,0x18(sp)
        8005bec8 14 00 b1 af     sw         s1,0x14(sp)
        8005becc 10 00 b0 af     sw         s0,0x10(sp)
        8005bed0 24 00 b4 af     sw         s4,0x24(sp)
        8005bed4 21 80 a0 00     move       s0,a1
        8005bed8 34 00 02 96     lhu        v0,0x34(s0)
        8005bedc 21 a0 07 00     move       s4,a3
        8005bee0 00 01 42 30     andi       v0,v0,0x100
        8005bee4 99 00 40 14     bne        v0,zero,0x8005c14c
        8005bee8 21 88 80 00     _move      s1,a0
        8005beec 13 80 02 3c     lui        v0,0x8013
        8005bef0 48 f3 42 8c     lw         v0,-0xcb8(v0)  //DigimonID                    
        8005bef4 21 18 00 00     clear      v1
        8005bef8 10 00 82 16     bne        s4,v0,0x8005bf3c
        8005befc 21 20 00 00     _clear     a0
        8005bf00 00 00 82 8e     lw         v0,0x0(s4)
        8005bf04 00 00 00 00     nop
        8005bf08 c0 08 02 00     sll        at,v0,0x3
        8005bf0c 22 08 22 00     sub        at,at,v0
        8005bf10 80 10 01 00     sll        v0,at,0x2
        8005bf14 12 80 01 3c     lui        at,0x8012
        8005bf18 c7 25 21 24     addiu      at,at,0x25c7
        8005bf1c 21 08 22 00     addu       at,at,v0
        8005bf20 00 00 22 80     lb         v0,0x0(at)  //RaiseData[DigimonID].moveBoost
        8005bf24 00 00 00 00     nop
        8005bf28 00 11 02 00     sll        v0,v0,0x4
        8005bf2c 03 00 c2 14     bne        a2,v0,0x8005bf3c
        8005bf30 21 a0 02 00     _move      s4,v0
        8005bf34 03 00 03 24     li         v1,0x3
        8005bf38 00 00 04 24     li         a0,0x0
                             LAB_8005bf3c                                    
        8005bf3c 12 80 02 3c     lui        v0,0x8012
        8005bf40 48 62 42 24     addiu      v0,v0,0x6248
        8005bf44 21 10 46 00     addu       v0,v0,a2
        8005bf48 21 10 43 00     addu       v0,v0,v1
        8005bf4c 00 00 42 90     lbu        v0,0x0(v0)  //AllMovementData[iVar3 + Tech4]                    
        8005bf50 21 98 06 00     move       s3,a2
        8005bf54 7d 00 40 10     beq        v0,zero,0x8005c14c
        8005bf58 21 90 40 00     _move      s2,v0
        8005bf5c 21 90 44 02     addu       s2,s2,a0
        8005bf60 b5 8d 02 0c     jal        0x800a36d4  //ReturnRandom                                     
        8005bf64 64 00 04 24     _li        a0,0x64
        8005bf68 2a 08 52 00     slt        at,v0,s2
        8005bf6c 77 00 20 10     beq        at,zero,0x8005c14c
        8005bf70 00 00 00 00     _nop
        8005bf74 21 18 00 00     clear      v1
        8005bf78 02 00 93 16     bne        s4,s3,0x8005bf84
        8005bf7c 00 00 00 00     _nop
        8005bf80 04 00 03 24     li         v1,0x4

                             LAB_8005bf84                                  
        8005bf84 12 80 02 3c     lui        v0,0x8012
        8005bf88 46 62 42 24     addiu      v0,v0,0x6246
        8005bf8c 21 10 53 00     addu       v0,v0,s3
        8005bf90 21 10 43 00     addu       v0,v0,v1
        8005bf94 00 00 42 90     lbu        v0,0x0(v0)  //AllMovementData[iVar3 + Tech4]                     
        8005bf98 04 00 01 24     li         at,0x4
        8005bf9c 57 00 41 10     beq        v0,at,0x8005c0fc
        8005bfa0 00 00 00 00     _nop
        8005bfa4 03 00 01 24     li         at,0x3
        8005bfa8 42 00 41 10     beq        v0,at,0x8005c0b4
        8005bfac 00 00 00 00     _nop
        8005bfb0 02 00 01 24     li         at,0x2
        8005bfb4 2d 00 41 10     beq        v0,at,0x8005c06c
        8005bfb8 00 00 00 00     _nop
        8005bfbc 08 00 41 2c     sltiu      at,v0,0x8
        8005bfc0 5a 00 01 10     beq        zero,at,0x8005c12c
        8005bfc4 00 00 00 00     _nop
        8005bfc8 34 00 03 96     lhu        v1,0x34(s0)
        8005bfcc 00 00 00 00     nop
        8005bfd0 01 00 61 30     andi       at,v1,0x1
        8005bfd4 55 00 20 14     bne        at,zero,0x8005c12c
        8005bfd8 00 00 00 00     _nop
        8005bfdc 01 00 01 24     li         at,0x1
        8005bfe0 04 00 41 14     bne        v0,at,0x8005bff4
        8005bfe4 00 00 00 00     _nop
        8005bfe8 01 00 02 24     li         v0,0x1
        8005bfec 06 00 00 10     b          0x8005c008
        8005bff0 57 00 22 a2     _sb        v0,0x57(s1)
                             LAB_8005bff4                                    
        8005bff4 05 00 01 24     li         at,0x5
        8005bff8 08 00 41 14     bne        v0,at,0x8005c01c
        8005bffc 00 00 00 00     _nop
        8005c000 02 00 02 24     li         v0,0x2
        8005c004 57 00 22 a2     sb         v0,0x57(s1)
                             LAB_8005c008                                   
        8005c008 01 00 62 34     ori        v0,v1,0x1
        8005c00c 34 00 02 a6     sh         v0,0x34(s0)
        8005c010 64 00 02 24     li         v0,0x64
        8005c014 10 00 00 10     b          0x8005c058
        8005c018 1c 00 02 a6     _sh        v0,0x1c(s0)
                             LAB_8005c01c                                    
        8005c01c 06 00 01 24     li         at,0x6
        8005c020 06 00 41 14     bne        v0,at,0x8005c03c
        8005c024 00 00 00 00     _nop
        8005c028 04 00 02 24     li         v0,0x4
        8005c02c 57 00 22 a2     sb         v0,0x57(s1)
        8005c030 01 00 62 34     ori        v0,v1,0x1
        8005c034 08 00 00 10     b          0x8005c058
        8005c038 34 00 02 a6     _sh        v0,0x34(s0)
                             LAB_8005c03c                                    
        8005c03c 07 00 01 24     li         at,0x7
        8005c040 3a 00 22 14     bne        at,v0,0x8005c12c
        8005c044 00 00 00 00     _nop
        8005c048 08 00 02 24     li         v0,0x8
        8005c04c 57 00 22 a2     sb         v0,0x57(s1)
        8005c050 01 00 62 34     ori        v0,v1,0x1
        8005c054 34 00 02 a6     sh         v0,0x34(s0)
                             LAB_8005c058                                    
        8005c058 21 20 11 00     move       a0,s1
        8005c05c ca 7a 01 0c     jal        0x8005eb28  //SetPoisonFX                                      
        8005c060 21 28 00 02     _move      a1,s0
        8005c064 31 00 00 10     b          0x8005c12c
        8005c068 00 00 00 00     _nop
                             LAB_8005c06c                                   
        8005c06c 34 00 03 96     lhu        v1,0x34(s0)
        8005c070 00 00 00 00     nop
        8005c074 02 00 62 30     andi       v0,v1,0x2
        8005c078 2c 00 40 14     bne        v0,zero,0x8005c12c
        8005c07c 00 00 00 00     _nop
        8005c080 02 00 62 34     ori        v0,v1,0x2
        8005c084 34 00 02 a6     sh         v0,0x34(s0)
        8005c088 b5 8d 02 0c     jal        0x800a36d4  //ReturnRandom                                     
        8005c08c 65 00 04 24     _li        a0,0x65
        8005c090 c8 00 42 20     addi       v0,v0,0xc8
        8005c094 1e 00 02 a6     sh         v0,0x1e(s0)
        8005c098 21 20 20 02     move       a0,s1
        8005c09c dc 7a 01 0c     jal        0x8005eb70  //SetConfusionFX                                  
        8005c0a0 21 28 00 02     _move      a1,s0
        8005c0a4 c3 7a 01 0c     jal        0x8005eb0c //ResetDumbCooldownTimers                         
        8005c0a8 21 20 00 02     _move      a0,s0
        8005c0ac 1f 00 00 10     b          0x8005c12c
        8005c0b0 00 00 00 00     _nop
                             LAB_8005c0b4                                   
        8005c0b4 34 00 03 96     lhu        v1,0x34(s0)
        8005c0b8 00 00 00 00     nop
        8005c0bc 04 00 62 30     andi       v0,v1,0x4
        8005c0c0 1a 00 40 14     bne        v0,zero,0x8005c12c
        8005c0c4 00 00 00 00     _nop
        8005c0c8 04 00 62 34     ori        v0,v1,0x4
        8005c0cc 34 00 02 a6     sh         v0,0x34(s0)
        8005c0d0 b5 8d 02 0c     jal        0x800a36d4  //ReturnRandom                                   
        8005c0d4 29 00 04 24     _li        a0,0x29
        8005c0d8 c8 00 42 20     addi       v0,v0,0xc8
        8005c0dc 20 00 02 a6     sh         v0,0x20(s0)
        8005c0e0 21 20 20 02     move       a0,s1
        8005c0e4 f9 7a 01 0c     jal        0x8005ebe4  //SetStunFX                                     
        8005c0e8 21 28 00 02     _move      a1,s0
        8005c0ec c3 7a 01 0c     jal        0x8005eb0c //ResetDumbCooldownTimers                         
        8005c0f0 21 20 00 02     _move      a0,s0
        8005c0f4 0d 00 00 10     b          0x8005c12c
        8005c0f8 00 00 00 00     _nop
                             LAB_8005c0fc                                  
        8005c0fc 34 00 02 96     lhu        v0,0x34(s0)
        8005c100 00 00 00 00     nop
        8005c104 08 00 42 30     andi       v0,v0,0x8
        8005c108 08 00 40 14     bne        v0,zero,0x8005c12c
        8005c10c 00 00 00 00     _nop
        8005c110 ff ff 02 24     li         v0,-0x1
        8005c114 22 00 02 a6     sh         v0,0x22(s0)
        8005c118 21 20 20 02     move       a0,s1
        8005c11c 1f 7b 01 0c     jal        0x8005ec7c  //RemoveEffectSprites                              
        8005c120 21 28 00 02     _move      a1,s0
        8005c124 c3 7a 01 0c     jal        0x8005eb0c //ResetDumbCooldownTimers                          
        8005c128 21 20 00 02     _move      a0,s0
                             LAB_8005c12c                                   
        8005c12c 20 92 82 8f     lw         v0=>CombatHeadAddr,-0x6de0(gp)  //CombatHead
        8005c130 00 00 00 00     nop
        8005c134 05 00 02 16     bne        s0,v0,0x8005c14c
        8005c138 21 18 40 00     _move      v1,v0
        8005c13c 44 06 62 94     lhu        v0,0x644(v1)  //CombatHead.statusCount
        8005c140 00 00 00 00     nop
        8005c144 01 00 42 24     addiu      v0,v0,0x1
        8005c148 44 06 62 a4     sh         v0,0x644(v1)
                             LAB_8005c14c                                                                                                                              
        8005c14c 20 00 bf 8f     lw         ra,0x20(sp)
        8005c150 1c 00 b3 8f     lw         s3,0x1c(sp)
        8005c154 18 00 b2 8f     lw         s2,0x18(sp)
        8005c158 14 00 b1 8f     lw         s1,0x14(sp)
        8005c15c 10 00 b0 8f     lw         s0,0x10(sp)
        8005c160 24 00 b4 8f     lw         s4,0x24(sp)
        8005c164 08 00 e0 03     jr         ra
        8005c168 2c 00 bd 27     _addiu     sp,sp,0x2c



//Remove status effect boost

void StatusEffectNew(DigimonEntity *AffectedPtr,FighterData *CombatPtr,int Tech4,int attPtr)
{
  if ((CombatPtr->BattleFlags & 0x100) != 0) 
    return;
  
  value = AllMovementData[Tech4].effectChance;
  if (value == 0) {
    return;
  }
  iVar3 = ReturnRandom(100);
  if (value < iVar3) {
    return;
  }
  value = AllMovementData[Tech4].effect;

//code ignored
}
                             
                             StatusEffectNew                                  
        8005beb8 d4 ff bd 27     addiu      sp,sp,-0x2c
        8005bebc 20 00 bf af     sw         ra,0x20(sp)
        8005bec0 1c 00 b3 af     sw         s3,0x1c(sp)
        8005bec4 18 00 b2 af     sw         s2,0x18(sp)
        8005bec8 14 00 b1 af     sw         s1,0x14(sp)
        8005becc 10 00 b0 af     sw         s0,0x10(sp)
        8005bed0 24 00 b4 af     sw         s4,0x24(sp)
        8005bed4 21 80 a0 00     move       s0,a1
        8005bed8 34 00 02 96     lhu        v0,0x34(s0)
        8005bedc 21 a0 07 00     move       s4,a3
        8005bee0 00 01 42 30     andi       v0,v0,0x100
        8005bee4 99 00 40 14     bne        v0,zero,0x8005c14c
        8005bee8 21 88 80 00     _move      s1,a0
        8005beec 21 20 00 00     clear      a0
        8005bef0 12 00 00 10     b          0x8005bf3c     //jumps to ignored code
        8005bef4 21 18 00 00     _clear     v1
        8005bef8 10 00 82 16     bne        s4,v0,0x8005bf3c
        8005befc 21 20 00 00     _clear     a0
        8005bf00 00 00 82 8e     lw         v0,0x0(s4)
        8005bf04 00 00 00 00     nop
        8005bf08 c0 08 02 00     sll        at,v0,0x3
        8005bf0c 22 08 22 00     sub        at,at,v0
        8005bf10 80 10 01 00     sll        v0,at,0x2
        8005bf14 12 80 01 3c     lui        at,0x8012
        8005bf18 c7 25 21 24     addiu      at,at,0x25c7
        8005bf1c 21 08 22 00     addu       at,at,v0
        8005bf20 00 00 22 80     lb         v0,0x0(at)     
        8005bf24 00 00 00 00     nop
        8005bf28 00 11 02 00     sll        v0,v0,0x4
        8005bf2c 03 00 c2 14     bne        a2,v0,0x8005bf3c
        8005bf30 21 a0 02 00     _move      s4,v0
        8005bf34 03 00 03 24     li         v1,0x3
        8005bf38 00 00 04 24     li         a0,0x0
                             LAB_8005bf3c                                   
        8005bf3c 12 80 02 3c     lui        v0,0x8012
        8005bf40 48 62 42 24     addiu      v0,v0,0x6248
        8005bf44 21 10 46 00     addu       v0,v0,a2
        8005bf48 21 10 43 00     addu       v0,v0,v1
        8005bf4c 00 00 42 90     lbu        v0,0x0(v0)  //AllMovementData[Tech4]                          
        8005bf50 21 98 06 00     move       s3,a2
        8005bf54 7d 00 40 10     beq        v0,zero,0x8005c14c
        8005bf58 21 90 40 00     _move      s2,v0
        8005bf5c 21 90 44 02     addu       s2,s2,a0
        8005bf60 b5 8d 02 0c     jal        0x800a36d4  //ReturnRandom                                    
        8005bf64 64 00 04 24     _li        a0,0x64
        8005bf68 2a 08 42 02     slt        at,s2,v0
        8005bf6c 77 00 20 14     bne        at,zero,0x8005c14c
        8005bf70 00 00 00 00     _nop
        8005bf74 21 18 00 00     clear      v1
        8005bf78 02 00 93 16     bne        s4,s3,0x8005bf84
        8005bf7c 00 00 00 00     _nop
        8005bf80 21 18 00 00     clear      v1


//Hardmode/Hardcore

 void NewStatusEffect(DigimonEntity *AffectedPtr,FighterData *CombatPtr,int Tech4,int attPtr)
{
  if (((ushort)combatPtr->BattleFlags & 0x100) != 0) 
    return;
  
  iVar4 = 0;
  iVar3 = (attPtr->Entity).DigimonType;
  
  //hardmode
  iVar5 = 10;
  
  //hardcore
  iVar5 = 20;
  
  //true hardcore
  iVar5 = 30;
  
  if (iVar3 < 66)
  {
    iVar5 = 0;
    attPtr = RaiseData[iVar3].moveBoost << 4);
	
    if (Tech4 == attPtr) 
      iVar4 = 3;    
  }
  
  value = AllMovementData[iVar4 + Tech4];
  if (value == 0) 
    return;
  
  iVar3 = ReturnRandom(100);
  if ((value + iVar5) <= iVar3) 
    return;
  
  iVar3 = 0;
  if (attPtr == Tech4) 
    iVar3 = 4;
} 

//tournament offset
                             NewStatusEffect                                
        80063ea0 d4 ff bd 27     addiu      sp,sp,-0x2c
        80063ea4 20 00 bf af     sw         ra,0x20(sp)
        80063ea8 1c 00 b3 af     sw         s3,0x1c(sp)
        80063eac 18 00 b2 af     sw         s2,0x18(sp)
        80063eb0 14 00 b1 af     sw         s1,0x14(sp)
        80063eb4 10 00 b0 af     sw         s0,0x10(sp)
        80063eb8 24 00 b4 af     sw         s4,0x24(sp)
        80063ebc 21 80 a0 00     move       s0,a1
        80063ec0 34 00 02 96     lhu        v0,0x34(s0)
        80063ec4 21 a0 07 00     move       s4,a3
        80063ec8 00 01 42 30     andi       v0,v0,0x100
        80063ecc 99 00 40 14     bne        v0,zero,0x80064134
        80063ed0 21 88 80 00     _move      s1,a0
        80063ed4 00 00 00 00     nop
        80063ed8 00 00 e2 8c     lw         v0,0x0(a3) attPtr->Entity.DigimonType
        80063edc 21 18 00 00     clear      v1
        80063ee0 00 00 00 00     nop
		//hardmode
		80063ee4 0a 00 04 24     li         a0,0xa
  
        //hardcore
		80063ee4 14 00 04 24     li         a0,0x14
  
        //true hardcore
		80063ee4 1e 00 04 24     li         a0,0x1e

        80063ee8 42 00 41 28     slti       at,v0,0x42
        80063eec 0d 00 20 10     beq        at,zero,0x80063f24
        80063ef0 00 00 00 00     _nop
        80063ef4 c0 08 02 00     sll        at,v0,0x3
        80063ef8 22 08 22 00     sub        at,at,v0
        80063efc 80 10 01 00     sll        v0,at,0x2
        80063f00 12 80 01 3c     lui        at,0x8012
        80063f04 c7 25 21 24     addiu      at,at,0x25c7
        80063f08 21 08 22 00     addu       at,at,v0
        80063f0c 00 00 22 80     lb         v0,0x0(at)  //RaiseData[(AttackerPtr->DigimonEntity).DigimonType]
        80063f10 21 20 00 00     clear      a0
        80063f14 00 11 02 00     sll        v0,v0,0x4
        80063f18 02 00 c2 14     bne        a2,v0,0x80063f24
        80063f1c 21 a0 02 00     _move      s4,v0
        80063f20 03 00 03 24     li         v1,0x3
                             LAB_80063f24                                   
        80063f24 12 80 02 3c     lui        v0,0x8012
        80063f28 48 62 42 24     addiu      v0,v0,0x6248
        80063f2c 21 10 46 00     addu       v0,v0,a2
        80063f30 21 10 43 00     addu       v0,v0,v1
        80063f34 00 00 42 90     lbu        v0,0x0(v0)  //AllMovementData[iVar4 + Tech4]                       
        80063f38 21 98 06 00     move       s3,a2
        80063f3c 7d 00 40 10     beq        v0,zero,0x80064134
        80063f40 21 90 40 00     _move      s2,v0
        80063f44 21 90 44 02     addu       s2,s2,a0
        80063f48 b5 8d 02 0c     jal        0x800a36d4  //ReturnRandom                                   
        80063f4c 64 00 04 24     _li        a0,0x64
        80063f50 2a 08 52 00     slt        at,v0,s2
        80063f54 77 00 20 10     beq        at,zero,0x80064134
        80063f58 00 00 00 00     _nop
        80063f5c 21 18 00 00     clear      v1
        80063f60 02 00 93 16     bne        s4,s3,0x80063f6c
        80063f64 00 00 00 00     _nop
        80063f68 04 00 03 24     li         v1,0x4




//Ultra effect

void NewStatusEffect(DigimonEntity *AffectedPtr,FighterData *CombatPtr,int Tech4,int attPtr)
{
  if (((ushort)combatPtr->BattleFlags & 0x100) != 0) 
    return;
  
  iVar3 = 0;
  iVar4 = 0;
  if (attPtr != PartnerPtr)
  {
    iVar3 = 3;
	
    //hardmode
    iVar4 = 10;
  
    //hardcore
    iVar4 = 20;
  
    //true hardcore
    iVar4 = 30;
	
    attPtr = Tech4;
    if (GameState[595] == 1) //is a boss battle
      //hardmode
      iVar4 = 20;
  
      //hardcore
      iVar4 = 30;
  
      //true hardcore
      iVar4 = 40;
    
  }
  value = AllMovementData[iVar3 + Tech4];
  if (value == 0) 
    return;
  
  iVar3 = ReturnRandom(100);
  if ((value + iVar4) <= iVar3) 
    return;
}



                             NewStatusEffect                               
        80063ea0 d4 ff bd 27     addiu      sp,sp,-0x2c
        80063ea4 20 00 bf af     sw         ra,0x20(sp)
        80063ea8 1c 00 b3 af     sw         s3,0x1c(sp)
        80063eac 18 00 b2 af     sw         s2,0x18(sp)
        80063eb0 14 00 b1 af     sw         s1,0x14(sp)
        80063eb4 10 00 b0 af     sw         s0,0x10(sp)
        80063eb8 24 00 b4 af     sw         s4,0x24(sp)
        80063ebc 21 80 a0 00     move       s0,a1
        80063ec0 34 00 02 96     lhu        v0,0x34(s0)
        80063ec4 21 a0 07 00     move       s4,a3
        80063ec8 00 01 42 30     andi       v0,v0,0x100
        80063ecc 99 00 40 14     bne        v0,zero,0x80064134
        80063ed0 21 88 80 00     _move      s1,a0
        80063ed4 13 80 02 3c     lui        v0,0x8013
        80063ed8 48 f3 42 8c     lw         v0,-0xcb8(v0)  //PartnerID                
        80063edc 21 18 00 00     clear      v1
        80063ee0 10 00 82 12     beq        s4,v0,0x80063f24
        80063ee4 21 20 00 00     _clear     a0
        80063ee8 00 00 00 00     nop
        80063eec 8c 94 82 8f     lw         v0,-0x6b74(gp) //GameState
        80063ef0 00 00 00 00     nop
        80063ef4 fa 00 42 24     addiu      v0,v0,0xfa
        80063ef8 59 01 42 90     lbu        v0,0x159(v0)
        80063efc 01 00 01 24     li         at,0x1
        80063f00 03 00 03 24     li         v1,0x3
		//hardmode
		80063f04 0a 00 04 24     li         a0,0xa
  
        //hardcore
		80063f04 14 00 04 24     li         a0,0x14
  
        //true hardcore
		80063f04 1e 00 04 24     li         a0,0x1e

        80063f08 06 00 41 14     bne        v0,at,0x80063f24
        80063f0c 21 a0 06 00     _move      s4,a2
		//hardmode
        80063f10 14 00 04 24     li         a0,0x14
  
        //hardcore
        80063f10 1c 00 04 24     li         a0,0x1e
  
        //true hardcore
		80063f10 28 00 04 24     li         a0,0x28

        80063f14 03 00 00 10     b          0x80063f24
        80063f18 00 00 00 00     _nop
        80063f1c 00 00 00 00     nop
        80063f20 00 00 00 00     nop
		
		
//Render the new effects

//Sorry, not the full function, only the changes

RenderBattleBubble(param_1)
{
  if ((0x200 < depth) && (depth < 0x10000)) 
  {
    PoisonBubble.UvX = BubblePosX[(FramesAlive / 2) % 6] + 0x20;
                   
    sVar3 = 487;
    cVar1 = DigimonEntity->NewEffects;
    if (cVar1 != '\0') 
	{
      if (cVar1 == 2) 
        sVar3 = 482;
      
      if (cVar1 == 4) 
        sVar3 = 493;
      
      if (cVar1 == 11) 
        sVar3 = 494;      
                    
      PoisonBubble.ClutY = sVar3;
      RenderSprite(&PoisonBubble, posX, posY, depth, 20132, 20132);	  
   }
}

                          RenderBattleBubble									
	    //battle
	    8006eed4 21 90 00 02     move       s2,s0		

        8006ef2c 10 00 b1 af     sw         s1,local_28(sp)
		
		8006efb8 43 18 11 00     sra        v1,s1,0x1

        8006eff8 5c b4 00 10     b          0x8005c16c
		
		                    LAB_8005c16c                                    
        8005c16c 08 00 42 8e     lw         v0,0x8(s2)
        8005c170 e7 01 03 24     li         v1,0x1e7
        8005c174 57 00 42 80     lb         v0,0x57(v0)
        8005c178 02 00 01 24     li         at,0x2
        8005c17c 0d 00 02 10     beq        zero,v0,0x8005c1b4
        8005c180 00 00 00 00     _nop
        8005c184 02 00 22 14     bne        at,v0,0x8005c190
        8005c188 04 00 01 24     _li        at,0x4
        8005c18c e2 01 03 24     li         v1,0x1e2
                             LAB_8005c190                                   
        8005c190 02 00 22 14     bne        at,v0,0x8005c19c
        8005c194 08 00 01 24     _li        at,0x8
        8005c198 ed 01 03 24     li         v1,0x1ed
                             LAB_8005c19c                                   
        8005c19c 02 00 22 14     bne        at,v0,0x8005c1a8
        8005c1a0 00 00 00 00     _nop
        8005c1a4 ee 01 03 24     li         v1,0x1ee
                             LAB_8005c1a8                                   
        8005c1a8 12 00 83 a4     sh         v1,0x12(a0)
        8005c1ac db 68 03 0c     jal        0x800da36c   //RenderSprite                                   
        8005c1b0 00 00 00 00     _nop
                             LAB_8005c1b4                                  
        8005c1b4 92 4b 00 10     b          0x8006f000
        8005c1b8 00 00 00 00     _nop



        //tournament

		80077474 21 90 00 02     move       s2,s0
		
		800774cc 10 00 b1 af     sw         s1,0x10(sp)

        80077558 43 18 11 00     sra        v1,s1,0x1

        80077598 ee b2 00 10     b          0x80064154
		
		                             LAB_80064154                                  
        80064154 08 00 42 8e     lw         v0,0x8(s2)
        80064158 e7 01 03 24     li         v1,0x1e7
        8006415c 57 00 42 80     lb         v0,0x57(v0)
        80064160 02 00 01 24     li         at,0x2
        80064164 0d 00 02 10     beq        zero,v0,0x8006419c
        80064168 00 00 00 00     _nop
        8006416c 02 00 22 14     bne        at,v0,0x80064178
        80064170 04 00 01 24     _li        at,0x4
        80064174 e2 01 03 24     li         v1,0x1e2
                             LAB_80064178                                  
        80064178 02 00 22 14     bne        at,v0,0x80064184
        8006417c 08 00 01 24     _li        at,0x8
        80064180 ed 01 03 24     li         v1,0x1ed
                             LAB_80064184                                    
        80064184 02 00 22 14     bne        at,v0,0x80064190
        80064188 00 00 00 00     _nop
        8006418c ee 01 03 24     li         v1,0x1ee
                             LAB_80064190                                   
        80064190 12 00 83 a4     sh         v1,0x12(a0)
        80064194 db 68 03 0c     jal        0x800da36c   //RenderSprite                                   
        80064198 00 00 00 00     _nop
                             LAB_8006419c                                   
        8006419c 00 4d 00 10     b          0x800775a0
        800641a0 00 00 00 00     _nop


//New function

int CalculateDamageRandom3(stats *stats)

{
  int iVar1;
  
  iVar1 = ReturnRandom(3);
  return ((int)stats->MaxDigimonHP * (iVar1 + 1)) / 100;
}

                            
//battle version
                             CalculateDamageRandom3                          
        8005c1bc e8 ff bd 27     addiu      sp,sp,-0x18
        8005c1c0 10 00 bf af     sw         ra,0x10(sp)
        8005c1c4 08 00 b0 af     sw         s0,0x8(sp)
        8005c1c8 00 00 00 00     nop
        8005c1cc 21 80 04 00     move       s0,stats
        8005c1d0 b5 8d 02 0c     jal        0x800a36d4 //ReturnRandom                                    
        8005c1d4 03 00 04 24     _li        stats,0x3
        8005c1d8 10 00 03 86     lh         v1,0x10(s0)
        8005c1dc 01 00 42 20     addi       v0,v0,0x1
        8005c1e0 18 00 62 00     mult       v1,v0
        8005c1e4 eb 51 02 3c     lui        v0,0x51eb
        8005c1e8 12 18 00 00     mflo       v1
        8005c1ec 1f 85 42 34     ori        v0,v0,0x851f
        8005c1f0 00 00 00 00     nop
        8005c1f4 18 00 43 00     mult       v0,v1
        8005c1f8 10 10 00 00     mfhi       v0
        8005c1fc c2 1f 03 00     srl        v1,v1,0x1f
        8005c200 43 11 02 00     sra        v0,v0,0x5
        8005c204 21 10 43 00     addu       v0,v0,v1
        8005c208 10 00 bf 8f     lw         ra,0x10(sp)
        8005c20c 08 00 b0 8f     lw         s0,0x8(sp)
        8005c210 08 00 e0 03     jr         ra
        8005c214 18 00 bd 27     _addiu     sp,sp,0x18

// the tournament version is the same, but located at 0x800641a4


//Another new function

void ResetspeedBuffer(short Damage, FighterData *fighter, DigimonEntity *digi)
{
  short sVar1;
  short sVar2;
  
  sVar1 = fighter->speedBuffer - Damage;
  
  if (digi->NewEffects == 8) //is shocked
  {
    sVar1 = sVar1 - Damage;
	//battle 
    sVar2 = -620;
	//tournament
	sVar2 = -400;
  }
  else 
    sVar2 = -155;
  
  fighter->speedBuffer = sVar1;
  
  if (fighter->speedBuffer < sVar2) 
    fighter->speedBuffer = sVar2;
  
  return;
}

//battle version
                             ResetspeedBuffer                               
        8005c46c e8 ff bd 27     addiu      sp,sp,-0x18
        8005c470 10 00 bf af     sw         ra,0x10(sp)
        8005c474 32 00 a2 84     lh         v0,0x32(a1)
        8005c478 00 00 00 00     nop
        8005c47c 22 10 44 00     sub        v0,v0,a0
        8005c480 57 00 c3 80     lb         v1,0x57(a2)
        8005c484 08 00 01 24     li         at,0x8
        8005c488 04 00 23 14     bne        at,v1,0x8005c49c
        8005c48c 00 00 00 00     _nop
        8005c490 22 10 44 00     sub        v0,v0,a0
        8005c494 02 00 00 10     b          0x8005c4a0
        8005c498 94 fd 04 24     _li        a0,-0x26c
                             LAB_8005c49c                                   
        8005c49c 65 ff 04 24     li         a0,-0x9b
                             LAB_8005c4a0                                  
        8005c4a0 32 00 a2 a4     sh         v0,0x32(a1)
        8005c4a4 32 00 a2 84     lh         v0,0x32(a1)
        8005c4a8 00 00 00 00     nop
        8005c4ac 2a 08 44 00     slt        at,v0,a0
        8005c4b0 02 00 01 10     beq        zero,at,0x8005c4bc
        8005c4b4 00 00 00 00     _nop
        8005c4b8 32 00 a4 a4     sh         a0,0x32(a1)
                             LAB_8005c4bc                                    
        8005c4bc 10 00 bf 8f     lw         ra,0x10(sp)
        8005c4c0 00 00 00 00     nop
        8005c4c4 08 00 e0 03     jr         ra
        8005c4c8 18 00 bd 27     _addiu     sp,sp,0x18
		

//tournament version
                             ResetSpeedBuffer                                
        80064454 e8 ff bd 27     addiu      sp,sp,-0x18
        80064458 10 00 bf af     sw         ra,0x10(sp)
        8006445c 32 00 a2 84     lh         v0,0x32(a1)
        80064460 00 00 00 00     nop
        80064464 22 10 44 00     sub        v0,v0,a0
        80064468 57 00 c3 80     lb         v1,0x57(a2)
        8006446c 08 00 01 24     li         at,0x8
        80064470 04 00 23 14     bne        at,v1,0x80064484
        80064474 00 00 00 00     _nop
        80064478 22 10 44 00     sub        v0,v0,a0
        8006447c 02 00 00 10     b          0x80064488
        80064480 70 fe 04 24     _li        a0,-0x190
                             LAB_80064484                                     
        80064484 65 ff 04 24     li         a0,-0x9b
                             LAB_80064488                                   
        80064488 32 00 a2 a4     sh         v0,0x32(a1)
        8006448c 32 00 a2 84     lh         v0,0x32(a1)
        80064490 00 00 00 00     nop
        80064494 2a 08 44 00     slt        at,v0,a0
        80064498 02 00 01 10     beq        zero,at,0x800644a4
        8006449c 00 00 00 00     _nop
        800644a0 32 00 a4 a4     sh         a0,0x32(a1)
                             LAB_800644a4                                    
        800644a4 10 00 bf 8f     lw         ra,0x10(sp)
        800644a8 00 00 00 00     nop
        800644ac 08 00 e0 03     jr         ra
        800644b0 18 00 bd 27     _addiu     sp,sp,0x18

	
	
//Change the chance to hit function to add the shock effect


ChanceToHit(DigimonEntity *AttackerPtr,DigimonEntity *DefenderPtr,FighterData *combatPtr, int MoveID)
{
//code ignored
    blockChance = (movementAccuracy / 2) * ((DefenderPtr->DigimonStats).DigimonSpeed - (AttackerPtr->DigimonStats).DigimonSpeed / 10)) / 999;
	
    if ((currentAnimation == IDLE) || (currentAnimation == IDLE_TIRED)) 
        blockChance = (blockChance * 6) / 5;
                
    if (DefenderPtr->NewEffects == 8) //is shocked
        blockChance = blockChance / 2;
                
    chanceToHit = movementAccuracy - blockChance;
//code ignored
}
//battle version 
                              ChanceToHit
        8005bd08 22 30 64 00     sub        a2,v1,a0
                                 
        8005bd38 18 00 26 03     mult       t9,a2		
                             
        8005bda0 ca 01 00 10     b          0x8005c4cc
        8005bda4 00 00 00 00     _nop
                                  
        8005c4cc 57 00 a6 80     lb         digi,0x57(a1)
        8005c4d0 08 00 01 24     li         at,0x8
        8005c4d4 02 00 26 14     bne        at,digi,0x8005c4e0
        8005c4d8 00 00 00 00     _nop
        8005c4dc 43 20 04 00     sra        a0,a0,0x1
                             LAB_8005c4e0                                    
        8005c4e0 22 10 64 00     sub        v0,v1,a0
        8005c4e4 00 14 02 00     sll        v0,v0,0x10
        8005c4e8 2f fe 00 10     b          0x8005bda8
        8005c4ec 00 00 00 00     _nop
		
//tournament version

        80063dd0 22 30 64 00     sub        a2,v1,a0

        80063e00 18 00 26 03     mult       t9,a2
                            
        80063e68 92 01 00 10     b          0x800644b4
        80063e6c 00 00 00 00     _nop
		                             
        800644b4 57 00 a6 80     lb         a2,0x57(a1)
        800644b8 08 00 01 24     li         at,0x8
        800644bc 02 00 26 14     bne        at,a2,0x800644c8
        800644c0 00 00 00 00     _nop
        800644c4 43 20 04 00     sra        a0,a0,0x1
                             LAB_800644c8                                 
        800644c8 22 10 64 00     sub        v0,v1,a0
        800644cc 00 14 02 00     sll        v0,v0,0x10
        800644d0 67 fe 00 10     b          0x80063e70
        800644d4 00 00 00 00     _nop



//tick battle effect function Change

TickStatusEffect(void)
{
//code ignored
      if (((currentFighter->BattleFlags & 1) != 0) && ((digimonEntity->newEffects & 3) != 0)) 
	  {
        if (NO_AI_FLAG == 0) {
          currentFighter->poisonTimer = currentFighter->poisonTimer + -1;
        }
        if (currentFighter->poisonTimer == 0) 
		{
          currentFighter->poisonTimer = 100;
          damage = CalculateDamageRandom3(&digimonEntity->DigimonStats);
		  
          currentFighter->HPDamage = currentFighter->HPDamage + damage;
		  
          if (9999 < currentFighter->HPDamage) 
            currentFighter->HPDamage = 9999;
          
          if (currentTarget != 0)  //is not your partner digimon
            CombatHead[iVar6 + 0x5c4] = 0;
          
          color = 12;
          if (digimonEntity->newEffects == 2) 
            color = 3;
          
          AddEntityText(digimonEntity, currentTarget, color, damage, 0);
        }
      }
//code ignored
}

//battle version
        8005e700 57 00 22 82     lb         v0,0x57(s1)
        8005e704 00 00 00 00     nop
        8005e708 03 00 42 30     andi       v0,v0,0x3
        8005e70c 2f 00 02 10     beq        zero,v0,0x8005e7cc
        8005e710 00 00 00 00     _nop
        8005e714 48 92 82 8f     lw         v0,-0x6db8(gp)  //NO_AI_FLAG
        8005e718 00 00 00 00     nop
        8005e71c 05 00 40 14     bne        v0,zero,0x8005e734
        8005e720 00 00 00 00     _nop
        8005e724 1c 00 02 86     lh         v0,0x1c(s0) //currentFighter->poisonTimer
        8005e728 00 00 00 00     nop
        8005e72c ff ff 42 20     addi       v0,v0,-0x1
        8005e730 1c 00 02 a6     sh         v0,0x1c(s0)
                             LAB_8005e734                                   
        8005e734 1c 00 02 86     lh         v0,0x1c(s0)
        8005e738 00 00 00 00     nop
        8005e73c 23 00 40 14     bne        v0,zero,0x8005e7cc
        8005e740 00 00 00 00     _nop
        8005e744 64 00 02 24     li         v0,0x64
        8005e748 1c 00 02 a6     sh         v0,0x1c(s0)
        8005e74c 6f 70 01 0c     jal        0x8005c1bc  //CalculateDamageRandom3                           
        8005e750 21 20 12 00     _move      a0,s2
        8005e754 21 38 02 00     move       a3,v0
        8005e758 00 1c 02 00     sll        v1,v0,0x10
        8005e75c 2e 00 02 86     lh         v0,0x2e(s0)  //currentFighter->HPDamage
        8005e760 03 1c 03 00     sra        v1,v1,0x10
        8005e764 20 10 43 00     add        v0,v0,v1
        8005e768 2e 00 02 a6     sh         v0,0x2e(s0)
        8005e76c 2e 00 02 86     lh         v0,0x2e(s0)
        8005e770 00 00 00 00     nop
        8005e774 10 27 41 28     slti       at,v0,0x2710
        8005e778 03 00 20 14     bne        at,zero,0x8005e788
        8005e77c 00 00 00 00     _nop
        8005e780 0f 27 02 24     li         v0,0x270f
        8005e784 2e 00 02 a6     sh         v0,0x2e(s0)
                             LAB_8005e788                                    
        8005e788 05 00 60 12     beq        s3,zero,0x8005e7a0
        8005e78c 00 00 00 00     _nop
        8005e790 20 92 82 8f     lw         v0,-0x6de0(gp)        
        8005e794 00 00 00 00     nop
        8005e798 21 10 82 02     addu       v0,s4,v0
        8005e79c c4 05 40 a0     sb         zero,0x5c4(v0) //CombatHead[iVar6 + 0x5c4]
                             LAB_8005e7a0                                     
        8005e7a0 10 00 a0 af     sw         zero,0x10(sp)
        8005e7a4 21 20 20 02     move       a0,s1
        8005e7a8 21 28 60 02     move       a1,s3
        8005e7ac 57 00 22 82     lb         v0,0x57(s1)
        8005e7b0 02 00 01 24     li         at,0x2
        8005e7b4 02 00 41 14     bne        v0,at,0x8005e7c0
        8005e7b8 0c 00 06 24     _li        a2,0xc
        8005e7bc 03 00 06 24     li         a2,0x3
                             LAB_8005e7c0                                   
        8005e7c0 21 28 60 02     move       a1,s3
        8005e7c4 1a 7e 03 0c     jal        0x800df868  //AddEntityText                             
        8005e7c8 00 00 00 00     _nop
		
//the tournament version is mostly the same, except for the address of the CalculateDamageRandom3
//the tournament changes are located at 0x80066478



//various changes to make the digimon walking adapt to the new effects

        800e7d7c 57 00 a6 80     _lb        a2,0x57(a1)
		
        800e8970 0d 00 c2 30     andi       v0,SlowEffect,0xd

		
		//battle
		8005a5bc 57 00 86 90     lbu        a2,0x57(a0)

        8005b154 0d 00 c2 30     andi       v0,a2,0xd

		
		//tournament
		80062270 57 00 86 80     _lb        a2,0x57(a0)
 
        80062cbc 0b 00 c2 30     andi       v0,a2,0xd


		
//Cure all the new effects when using various/sup restore or death

        8005e5d8 57 00 80 a0     _sb        zero,0x57(a0)
		
		//battle death
        8005b02c 57 00 20 a2     sb         zero,0x57(s1)	
		
        //no need to heal on tournament deaths since the battle ends

//Restart the new effect flag

//battle
        80057244 1f 00 80 a0     sb         zero,0x1f(a0)

        80057254 02 00 a4 a4     sh         a0,0x2(a1)                 

//tournament
        8005ee44 1f 00 80 a0     sb         zero,0x1f(a0)
        8005ee48 02 00 87 84     lh         a3,0x2(a0)
        8005ee4c 02 00 08 25     addiu      t0,t0,0x2
        8005ee50 02 00 c7 a4     sh         a3,0x2(a2)
        8005ee54 21 30 00 01     move       a2,t0
        8005ee58 04 00 87 84     lh         a3,0x4(a0)
        8005ee5c 02 00 c8 24     addiu      t0,a2,0x2
        8005ee60 00 00 c7 a4     sh         a3,0x0(a2)
        8005ee64 06 00 84 84     lh         a0,0x6(a0)
        8005ee68 02 00 63 20     addi       v1,v1,0x2
        8005ee6c 00 00 04 a5     sh         a0,0x0(t0)



